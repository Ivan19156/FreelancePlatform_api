namespace FreelancePlatform.Core.DTOs.Users
{
    public class UpdateUserDto
    {
        public int Id { get; set; } // Потрібен для ідентифікації користувача
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        // Інші поля, які можна оновити, додавай сюди
    }
}

