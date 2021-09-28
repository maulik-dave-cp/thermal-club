namespace ThermalClub.Modules.AdminUsers.Models.DTOs
{
    public class AdminLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsRememberMe { get; set; }
    }
}