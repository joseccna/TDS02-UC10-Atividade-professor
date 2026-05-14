namespace ControleEstoque.API.Services
{
    public interface IPasswordService
    {
        // criptografa a senha usando um algoritmo de hash seguro

        string HashPassword(string password);

        // verifica se a senha fornecida corresponde à senha armazenada (hash)
        bool VerifyPassword(string password, string hash);

    }

}
