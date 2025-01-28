
global using Microsoft.AspNetCore.Identity;
global using HandMadeEcommece.helper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;


namespace HandMadeEcommece.Services
{
    public class Auth : IAuth
    {
        private readonly UserManager<AppUser> _Manager;
        private readonly JWT _jwt;
        public Auth(UserManager<AppUser>Manager, IOptions<JWT>jwt)
        {
            _Manager = Manager;
            _jwt = jwt.Value;
        }


        public async Task<AuthModel> RegisterUserAsync(RegisterUserModel Model)
        {
            if(await _Manager.FindByEmailAsync(Model.Email) != null) { return new AuthModel { Message = "This Email is already found" }; }
            if(await _Manager.FindByNameAsync(Model.UserName) != null) { return new AuthModel { Message = "This UserName is already found" }; }



            var user = new AppUser
            {
                Email=Model.Email,
                ConfirmEmail=Model.ConfirmEmail,
                PhoneNumber = Model.Phone,
                UserName=Model.UserName,
                FName=Model.FName,
                LName=Model.LName,
                Image = await TransferImage(Model.Image)
            };

           var result =  await _Manager.CreateAsync(user, Model.Password);
            if (!result.Succeeded)
            {
                var Error = "";
                foreach (var error in result.Errors)
                {
                    Error += $"{ error.Description},";
                }
                 return  new AuthModel { Message = Error };  
            }

            await _Manager.AddToRoleAsync(user,"Customer");

            var token = await CreateJwtToken(user);
            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = token.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "Costomer" },
                UserName=user.UserName,
                Token=new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public async Task<AuthModel> LogInUserAsync(LogInUserModel Model)
        {
            var authModel = new AuthModel();
            var user = await _Manager.FindByNameAsync(Model.UserName);
            if (user == null || !await _Manager.CheckPasswordAsync(user,Model.Password))
            {
                authModel.Message = "UserName Or Password Is Incorrect";
                return authModel;
            }

            var token = await CreateJwtToken(user);
            var roles = await _Manager.GetRolesAsync(user);
            authModel.ExpiresOn = token.ValidTo;
            authModel.UserName= user.UserName;
            authModel.IsAuthenticated = true;
            authModel.Email = user.Email;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(token);
            authModel.Roles = roles.ToList();
                

            return authModel;
        }

        private async Task<JwtSecurityToken>CreateJwtToken(AppUser user)
        {

            var userClaims = await _Manager.GetClaimsAsync(user);
            var roles = await _Manager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach(var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email)
            }
            .Union(roleClaims)
            .Union(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims:claims ,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_jwt.Expire)
                
            );
            return token;
        }

        private async Task<byte[]> TransferImage(IFormFile Image)
        {
            if (Image == null || Image.Length == 0)
                return null;
            using (var dataStream = new MemoryStream())
            {
                await Image.CopyToAsync(dataStream);
                return dataStream.ToArray();
            }
        }
    }
}
