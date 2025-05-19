namespace GestVeicular.Services.SenhaService
{
    public interface ISenhaInterface
    {
        void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
        bool VerificarSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt);
    }
}
