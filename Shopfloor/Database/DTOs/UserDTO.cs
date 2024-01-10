using System;

namespace Shopfloor.Database.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = String.Empty;
        public string User_Name { get; set; } = String.Empty;
        public string User_Surname { get; set; } = String.Empty;
        public string Image_Path { get; set; } = String.Empty;
        public bool Active { get; set; }
    }
}