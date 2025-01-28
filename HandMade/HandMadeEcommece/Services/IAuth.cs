
namespace HandMadeEcommece.Services
{
    public interface IAuth
    {
        Task<AuthModel> RegisterUserAsync(RegisterUserModel Model);
        Task<AuthModel> LogInUserAsync(LogInUserModel Model);
    }
}
