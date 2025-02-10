
namespace HandMadeEcommece.Services
{
    public interface IAuth
    {
        Task<AuthModel> RegisterUserAsync(RegisterUserModel Model);
        Task<AuthModel> LogInUserAsync(LogInUserModel Model);

        Task<AuthModel>RegisterAdminAsync(RegisterAdminDto Model);
        Task<AuthModel> LogInAdminAsync(LogInAdmin Model);

        Task<AuthModel> RegisterVendorAsync(RegisterVendor Model);
        Task<AuthModel> LogInVendorAsync(LogInVendor Model);

        Task<ChangePasswordDto> ChangePassword(ChangePasswordDto ChangePassword);

    }
}
