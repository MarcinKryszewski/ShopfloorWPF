namespace Shopfloor.Models.UserModel
{
    internal sealed class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string User_Name { get; set; } = string.Empty;
        public string User_Surname { get; set; } = string.Empty;
        public string Image_Path { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}