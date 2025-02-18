
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
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;
using HandMadeEcommece.Models.Dto;
using System.Numerics;



namespace HandMadeEcommece.Services
{
    public class Auth : IAuth
    {
        private readonly UserManager<AppUser> _Manager;
        private readonly JWT _jwt;
        private readonly AppDbContext _Context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWebHostEnvironment webHostEnvironment;
        public Auth(UserManager<AppUser> Manager, IOptions<JWT> jwt, AppDbContext Context, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _Manager = Manager;
            _jwt = jwt.Value;
            _Context = Context;
            this.httpContextAccessor = httpContextAccessor;
            this.webHostEnvironment = webHostEnvironment;
        }


        public async Task<AuthModel> RegisterUserAsync(RegisterUserModel Model, HttpContext httpContext)
        {

            var checkEmail = await _Context.checkUserNameAndEmails.FirstOrDefaultAsync(e => e.Email == Model.Email);
            var checkUserName = await _Context.checkUserNameAndEmails.FirstOrDefaultAsync(e => e.UserName == Model.UserName);

            if (checkEmail != null) { return new AuthModel { Message = "This Email is already found" }; }
            if (checkUserName != null) { return new AuthModel { Message = "This UserName is already found" }; }
            if(Model.RoleId <= 0 ||await _Context.Roles.FirstOrDefaultAsync(e=>e.Id == Model.RoleId) == null) { return new AuthModel { Message = "This Role Is Not Found" }; }
            if(!await Methods.IsValidEmail(Model.Email)) { return new AuthModel { Message = "This Email is not valid," }; }
            if(!await Methods.IsValidPhone(Model.Phone)) { return new AuthModel { Message = "This Phone is not valid in egypt." }; }
           // var httpContext = httpContextAccessor.HttpContext;
            var user = new User// change from appuser to user
            {
                Email = Model.Email,
                Phone = Model.Phone,
                UserName = Model.UserName,
                FName = Model.FName,
                LName = Model.LName,
                Password = Model.Password,
                Image = await Methods.GetImagesFromPath(Model.Image,"Users",httpContext,webHostEnvironment),
                RoleId = Model.RoleId
            };


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

        public async Task<AuthModel> RegisterAdminAsync(RegisterAdminDto Model,HttpContext httpContext)
        {

            var checkEmail = await _Context.checkUserNameAndEmails.FirstOrDefaultAsync(e => e.Email == Model.Email);
            var checkUserName = await _Context.checkUserNameAndEmails.FirstOrDefaultAsync(e => e.UserName == Model.UserName);

            if (checkEmail != null) { return new AuthModel { Message = "This Email is already found" }; }
            if (checkUserName != null) { return new AuthModel { Message = "This UserName is already found" }; }
            if (Model.RoleId <= 0 || await _Context.Roles.FirstOrDefaultAsync(e => e.Id == Model.RoleId) == null) { return new AuthModel { Message = "This Role Is Not Found" }; }
            if (!await Methods.IsValidEmail(Model.Email)) { return new AuthModel { Message = "This Email is not valid," }; }
            if (!await Methods.IsValidPhone(Model.Phone)) { return new AuthModel { Message = "This Phone is not valid in egypt." }; }
           // var httpContext = httpContextAccessor.HttpContext;
            var admin = new Admin
            {
                UserName = Model.UserName,
                Password = Model.Password,
                Email = Model.Email,
                LName = Model.LName,
                FName = Model.FName,
                Phone = Model.Phone,
                Salary = Model.Salary,
                Image  = await Methods.GetImagesFromPath(Model.image, "Admins", httpContext,webHostEnvironment),
                RoleId = Model.RoleId
            };

      


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

        public async Task<AuthModel> RegisterVendorAsync(RegisterVendor Model, HttpContext httpContext)
        {
            var checkEmail = await _Context.checkUserNameAndEmails.FirstOrDefaultAsync(e=>e.Email == Model.Email);
            var checkUserName = await _Context.checkUserNameAndEmails.FirstOrDefaultAsync(e=>e.UserName == Model.UserName);

            if (checkEmail != null) { return new AuthModel { Message = "This Email is already found" }; }
            if (checkUserName != null) { return new AuthModel { Message = "This UserName is already found" }; }
            if (Model.RoleId <= 0 || await _Context.Roles.FirstOrDefaultAsync(e => e.Id == Model.RoleId) == null) { return new AuthModel { Message = "This Role Is Not Found" }; }
            if (!await Methods.IsValidEmail(Model.Email)) { return new AuthModel { Message = "This Email is not valid," }; }
            if (!await Methods.IsValidPhone(Model.Phone)) { return new AuthModel { Message = "This Phone is not valid in egypt." }; }
            // var httpContext = httpContextAccessor.HttpContext;
            var vendor = new Vendor
            {
                Email = Model.Email,
                Phone = Model.Phone,
                UserName = Model.UserName,
                FName = Model.FName,
                LName = Model.LName,
                Image = await Methods.GetImagesFromPath(Model.Image, "Vendors", httpContext, webHostEnvironment),
                RoleId = Model.RoleId,
                FbLink = Model.FbLink,
                InstaLink = Model.InstaLink,
                TwLink = Model.TwLink,
                Status = 1,
                ShopName = Model.ShopName,
                Banner = await Methods.GetImagesFromPath(Model.Image, "VendorBanners", httpContext, webHostEnvironment),
                Description = Model.Description,
                Password = Model.Password
            };



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

           

            return new ChangePasswordDto
            {
                UserName = ChangePassword.UserName,
                CurrentPassword = ChangePassword.CurrentPassword,
                NewPassword = ChangePassword.NewPassword,
                Message = "PasswordChangedSuccess",
                IsChange = true
            };
        }
    

        public async Task<Order> ProcessOfOrder(Order order, OrderDto orderDto)
        {
            var carts = await _Context.Carts.Where(e => e.Order.CartId == orderDto.CartId).ToListAsync();
            var cartItem = await _Context.CartItems.Where(e => e.CartId == orderDto.CartId).ToListAsync();

            var Qty = cartItem.Sum(e => e.Quantity);




            var products_id = await _Context.CartItems
                .Include(e => e.productVariantItem)
                .ThenInclude(pvi => pvi.ProductVariant)
                .Where(e => e.CartId == orderDto.CartId)
                .Select(e => new
                {
                    ProductId = e.productVariantItem.ProductVariant.ProductId
                })
                .ToListAsync();




            var productIds = products_id.Select(e => e.ProductId).ToList();
            var products = await _Context.Products.Where(e => productIds.Contains(e.Id)).ToListAsync();
            var cartItems = await _Context.CartItems.Include(e => e.productVariantItem)
                .ThenInclude(e => e.ProductVariant)
                .Where(e => productIds.Contains(e.productVariantItem.ProductVariant.ProductId)).ToListAsync();

            var vendorsIds = await _Context.Products.Select(e => e.VendorId).Distinct().ToListAsync();
            var vendors = await _Context.Vendors.Where(e => vendorsIds.Contains(e.Id)).ToListAsync();
            var updateProducts = new List<Product>();


            foreach (var product in products)
            {
                var cartItemProduct = cartItems.FirstOrDefault(c => c.productVariantItem.ProductVariant.ProductId == product.Id);
                if (cartItemProduct != null)
                {
                    product.Qty -= cartItemProduct.Quantity;
                }

                var add_product = new Product
                {
                    Id = product.Id,
                    BrandId = product.BrandId,
                    Price = product.Price,
                    Qty = product.Qty,
                    OfferEndDate = product.OfferEndDate,
                    OfferStartDate = product.OfferStartDate,
                    Name = product.Name,
                    Slug = product.Slug,
                    IsApproved = product.IsApproved,
                };

                updateProducts.Add(add_product);

            }

                _Context.Products.UpdateRange(updateProducts);
                await _Context.SaveChangesAsync();


                var amount = carts.Sum(e => e.TotalPrice);

                order.CompanyDeliveryId = orderDto.CompanyDeliveryId;
                order.Amount = amount;//calucalate coupon and discount
                order.ProductQty = Qty;
                order.OrderStatus = orderDto.OrderStatus.ToString();
                order.CartId = orderDto.CartId;
                order.UserId = orderDto.UserId;
                order.CreatedAt = DateTime.UtcNow;
                order.UpdatedAt = DateTime.UtcNow;
                order.CurrencyName = orderDto.CurrencyName.ToString();
                order.OrderAddress = orderDto.OrderAddress;
                order.PaymentMethod = orderDto.PaymentMethod.ToString();
                order.product = products;
                order.vendor = vendors;

                return order;
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
