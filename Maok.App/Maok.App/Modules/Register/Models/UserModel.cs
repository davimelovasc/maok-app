namespace Maok.App.Modules.Register.Models
{
    public class UserModel
    {
        public int ExternalId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
    }
}
