﻿namespace ThermalClub.Modules.AdminUsers.Models.DTOs
{
    public class AdminChangePasswordDto
    {
        public int Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}