namespace Pustokk.BLL.ViewModels.AppUserViewModels
{
    public class RegisterViewModel : IViewModel
    {
        public required string Email { get; set; }
        public required string Username { get; set; }
        public  required string Password { get; set; }
        public required string FullName { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}
