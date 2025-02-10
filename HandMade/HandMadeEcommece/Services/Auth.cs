
global using Microsoft.AspNetCore.Identity;
global using HandMadeEcommece.helper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using HandMadeEcommece.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using HandMadeEcommece.Models.Data;



namespace HandMadeEcommece.Services
{
    public class Auth : IAuth
    {
        private readonly UserManager<AppUser> _Manager;
        private readonly JWT _jwt;
        private readonly AppDbContext _Context;
        public Auth(UserManager<AppUser> Manager, IOptions<JWT> jwt, AppDbContext Context)
        {
            _Manager = Manager;
            _jwt = jwt.Value;
            _Context = Context;
        }


        public async Task<AuthModel> RegisterUserAsync(RegisterUserModel Model)
        {

            var checkEmail = await _Context.checkUserNameAndEmails.FirstOrDefaultAsync(e => e.Email == Model.Email);
            var checkUserName = await _Context.checkUserNameAndEmails.FirstOrDefaultAsync(e => e.UserName == Model.UserName);

            if (checkEmail != null) { return new AuthModel { Message = "This Email is already found" }; }
            if (checkUserName != null) { return new AuthModel { Message = "This UserName is already found" }; }
            if(Model.RoleId <= 0 ||await _Context.Roles.FirstOrDefaultAsync(e=>e.Id == Model.RoleId) == null) { return new AuthModel { Message = "This Role Is Not Found" }; }
            if(await Methods.IsValidEmail(Model.Email)) { return new AuthModel { Message = "This Email is not valid," }; }
            if(await Methods.IsValidPhone(Model.Phone)) { return new AuthModel { Message = "This Phone is not valid in egypt." }; }

            var user = new User// change from appuser to user
            {
                Email = Model.Email,
                Phone = Model.Phone,
                UserName = Model.UserName,
                FName = Model.FName,
                LName = Model.LName,
                Password = Model.Password,
                Image = await Methods.TransferImage(Model.Image),
                RoleId = Model.RoleId
            };


            //var result =  await _Manager.CreateAsync(user, Model.Password);
            // if (!result.Succeeded)
            // {
            //     var Error = "";
            //     foreach (var error in result.Errors)
            //     {
            //         Error += $"{ error.Description},";
            //     }
            //      return  new AuthModel { Message = Error };  
            // }





            await _Context.Users.AddAsync(user);
            await _Context.checkUserNameAndEmails.AddAsync(new CheckUserNameAndEmail { UserName = Model.UserName, Email = Model.Email });
            await _Context.SaveChangesAsync();

            var token = await CreateJwtToken(user);
            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = token.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "Customer" },
                UserName = user.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public async Task<AuthModel> LogInUserAsync(LogInUserModel Model)
        {
            var authModel = new AuthModel();
            var user = await _Context.Users.FirstOrDefaultAsync(e=>e.UserName == Model.UserName);
            if (user == null || Model.Password != user.Password)
            {
                authModel.Message = "UserName Or Password Is InCorrect";
                return authModel;
            }

            var token = await CreateJwtToken(user);
            
            authModel.ExpiresOn = token.ValidTo;
            authModel.UserName = user.UserName;
            authModel.IsAuthenticated = true;
            authModel.Email = user.Email;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(token);
            authModel.Roles = new List<string> { "Customer"};


            return authModel;
        }

        public async Task<AuthModel> RegisterAdminAsync(RegisterAdminDto Model)
        {

            var checkEmail = await _Context.checkUserNameAndEmails.FirstOrDefaultAsync(e => e.Email == Model.Email);
            var checkUserName = await _Context.checkUserNameAndEmails.FirstOrDefaultAsync(e => e.UserName == Model.UserName);

            if (checkEmail != null) { return new AuthModel { Message = "This Email is already found" }; }
            if (checkUserName != null) { return new AuthModel { Message = "This UserName is already found" }; }
            if (Model.RoleId <= 0 || await _Context.Roles.FirstOrDefaultAsync(e => e.Id == Model.RoleId) == null) { return new AuthModel { Message = "This Role Is Not Found" }; }
            if (await Methods.IsValidEmail(Model.Email)) { return new AuthModel { Message = "This Email is not valid," }; }
            if (await Methods.IsValidPhone(Model.Phone)) { return new AuthModel { Message = "This Phone is not valid in egypt." }; }
            var admin = new Admin
            {
                UserName = Model.UserName,
                Password = Model.Password,
                Email = Model.Email,
                LName = Model.LName,
                FName = Model.FName,
                Phone = Model.Phone,
                Salary = Model.Salary,
                Image = await Methods.TransferImage(Model.image),
                RoleId = Model.RoleId
            };

            //var result = await _Manager.CreateAsync(admin, Model.Password);
            //if (!result.Succeeded)
            //{
            //    var Error = "";
            //    foreach (var error in result.Errors)
            //    {
            //        Error += $"{error.Description},";
            //    }
            //    return new AuthModel { Message = Error };
            //}



            await _Context.Admins.AddAsync(admin);
            await _Context.checkUserNameAndEmails.AddAsync(new CheckUserNameAndEmail { UserName = Model.UserName, Email = Model.Email });
            await _Context.SaveChangesAsync();


            var token = await CreateJwtToken(admin);
            return new AuthModel
            {
                Email = admin.Email,
                ExpiresOn = token.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "Admin" },
                UserName = admin.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

        }


        public async Task<AuthModel> LogInAdminAsync(LogInAdmin Model)
        {
            var authModel = new AuthModel();
            var user = await _Context.Users.FirstOrDefaultAsync(e=>e.UserName == Model.UserName);
            if (user == null || Model.Password != user.Password)
            {
                authModel.Message = "UserName Or Password Is InCorrect";
                return authModel;
            }

            var token = await CreateJwtToken(user);
            return new AuthModel
            {
                IsAuthenticated = true,
                Email = user.Email,
                UserName = user.UserName,
                Roles = new List<string> { "Admin"},
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresOn = token.ValidTo,
            };

        }

        public async Task<AuthModel> RegisterVendorAsync(RegisterVendor Model)
        {
            var checkEmail = await _Context.checkUserNameAndEmails.FirstOrDefaultAsync(e=>e.Email == Model.Email);
            var checkUserName = await _Context.checkUserNameAndEmails.FirstOrDefaultAsync(e=>e.UserName == Model.UserName);

            if (checkEmail != null) { return new AuthModel { Message = "This Email is already found" }; }
            if (checkUserName != null) { return new AuthModel { Message = "This UserName is already found" }; }
            if (Model.RoleId <= 0 || await _Context.Roles.FirstOrDefaultAsync(e => e.Id == Model.RoleId) == null) { return new AuthModel { Message = "This Role Is Not Found" }; }
            if (await Methods.IsValidEmail(Model.Email)) { return new AuthModel { Message = "This Email is not valid," }; }
            if (await Methods.IsValidPhone(Model.Phone)) { return new AuthModel { Message = "This Phone is not valid in egypt." }; }
            var vendor = new Vendor
            {
                Email = Model.Email,
                Phone = Model.Phone,
                UserName = Model.UserName,
                FName = Model.FName,
                LName = Model.LName,
                Image = await Methods.TransferImage(Model.Image),
                RoleId = Model.RoleId,
                FbLink = Model.FbLink,
                InstaLink = Model.InstaLink,
                TwLink = Model.TwLink,
                Status = 1,
                ShopName = Model.ShopName
            };

            //var result = await _Manager.CreateAsync(vendor, Model.Password);
            //if (!result.Succeeded)
            //{
            //    var Error = "";
            //    foreach (var error in result.Errors)
            //    {
            //        Error += $"{error.Description},";
            //    }
            //    return new AuthModel { Message = Error };
            //}



            await _Context.Vendors.AddAsync(vendor);
            await _Context.checkUserNameAndEmails.AddAsync(new CheckUserNameAndEmail { UserName = Model.UserName, Email = Model.Email });
            await _Context.SaveChangesAsync();



            var token = await CreateJwtToken(vendor);
            return new AuthModel
            {
                Email = vendor.Email,
                ExpiresOn = token.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "Vendor" },
                UserName = vendor.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
        public async Task<AuthModel> LogInVendorAsync(LogInVendor Model)
        {
            var authModel = new AuthModel();
            var user = await _Context.Users.FirstOrDefaultAsync(e=>e.UserName == Model.UserName);
            if (user == null || Model.Password != user.Password)
            {
                authModel.Message = "UserName Or Password Is InCorrect";
                return authModel;
            }
            var Vendor = await _Context.Vendors.FirstOrDefaultAsync(e=>e.UserName == Model.UserName);
            if (Vendor == null || Vendor.Status == 0) { return new AuthModel { Message = "This Vendor Is Broken" }; }

            var token = await CreateJwtToken(user);
            return new AuthModel
            {
                IsAuthenticated = true,
                Email = user.Email,
                UserName = user.UserName,
                Roles = new List<string> { "Vendor"},
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresOn = token.ValidTo,
            };
        }


        public async Task<ChangePasswordDto> ChangePassword(ChangePasswordDto ChangePassword)
        {
            var user = await _Context.checkUserNameAndEmails.FirstOrDefaultAsync(e=>e.UserName == ChangePassword.UserName);
            if (user == null) { return new ChangePasswordDto { Message = "This User Not Found" }; }
            string password = "";
            if(await _Context.Users.FirstOrDefaultAsync(e=>e.UserName == ChangePassword.UserName) != null)
            {
                var ur = await _Context.Users.FirstOrDefaultAsync(e => e.UserName == ChangePassword.UserName);
                if (ur != null)password = ur.Password;
            }
            else if(await _Context.Admins.FirstOrDefaultAsync(e => e.UserName == ChangePassword.UserName) != null)
            {
                var ur = await _Context.Admins.FirstOrDefaultAsync(e => e.UserName == ChangePassword.UserName);
                if (ur != null) password = ur.Password;
            }
            else
            {
                var ur = await _Context.Vendors.FirstOrDefaultAsync(e => e.UserName == ChangePassword.UserName);
                if (ur != null) password = ur.Password;
            }
            if (ChangePassword.CurrentPassword != password) { return new ChangePasswordDto { Message = "The Password Is Wrong!!!" }; }

           // var result = await _Manager.ChangePasswordAsync(user, ChangePassword.CurrentPassword, ChangePassword.NewPassword);

            //var Error = "";
            //if (!result.Succeeded)
            //{
            //    foreach(var error in result.Errors)
            //    {
            //        Error += $"{error},";
            //    }
            //    return new ChangePasswordDto { Message = Error };
            //}

            return new ChangePasswordDto
            {
                UserName = ChangePassword.UserName,
                CurrentPassword = ChangePassword.CurrentPassword,
                NewPassword = ChangePassword.NewPassword,
                Message = "PasswordChangedSuccess",
                IsChange = true
            };
        }
    
        private async Task<JwtSecurityToken> CreateJwtToken<TEntity>(TEntity entity) where TEntity : class
        {
            var ClaimsEntity = new List<Claim>();
            var RolesEntity = new List<Role>();
            var user = new CheckUserNameAndEmail();
            if(typeof(TEntity).Name == "Admin")
            {
                var ProperityId = typeof(TEntity).GetProperty("Id");
                var adminId = ProperityId.GetValue(entity);
                var Id = Convert.ToInt32(adminId);
                ClaimsEntity = await _Context.ClaimAdmins.Where(e => e.AdminId == Id).Select(c=>new Claim ( c.ClaimType, c.ClaimValue)).ToListAsync();
                RolesEntity = await _Context.Roles.Where(e=>e.Id == Id).ToListAsync();
                var admin = await _Context.Admins.FindAsync(Id);
                user.UserName = admin.UserName;
                user.Email = admin.Email;
            }
            if(typeof(TEntity).Name == "Vendor")
            {
                var ProperityId = typeof(TEntity).GetProperty("Id");
                var vendorId = ProperityId.GetValue(entity);
                var Id = Convert.ToInt32(vendorId);
                ClaimsEntity = await _Context.ClaimVendors.Where(e => e.VendorId == Id).Select(c=>new Claim(c.ClaimType,c.ClaimType)).ToListAsync();
                RolesEntity = await _Context.Roles.Where(e => e.Id == Id).ToListAsync();
                var vendor = await _Context.Vendors.FindAsync(Id);
                user.UserName = vendor.UserName;
                user.Email = vendor.Email;
            }
            if(typeof(TEntity).Name == "User")
            {
                var ProperityId = typeof(TEntity).GetProperty("Id");
                var userId = ProperityId.GetValue(entity);
                var Id = Convert.ToInt32(userId);
                ClaimsEntity = await _Context.ClaimUsers.Where(e => e.UserId == Id).Select(c=>new Claim(c.ClaimType,c.ClaimValue)).ToListAsync();
                RolesEntity = await _Context.Roles.Where(e => e.Id == Id).ToListAsync();
                var User = await _Context.Users.FindAsync(Id);
                user.UserName = User.UserName;
                user.Email = User.Email;
            }
            //var userClaims = await _Manager.GetClaimsAsync(user);
            //var roles = await _Manager.GetRolesAsync(user);
            var roleClaims = RolesEntity
            .Select(role => new Claim("roles", role.Name)) 
                .ToList();


            var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email)
                    };

            claims.AddRange(roleClaims);
            claims.AddRange(ClaimsEntity);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_jwt.Expire)

            );
            return token;
        }
    }
}
