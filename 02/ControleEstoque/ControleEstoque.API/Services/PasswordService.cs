namespace ControleEstoque.API.Services
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            
            // Implementação simples usando BCrypt
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool VerifyPassword(string password, string hash)
        {
            // Verifica se a senha corresponde ao hash armazenado
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }

}
