namespace DesafioATS.Domain.Interfaces
{
    public interface IAspNetUser
    {
        string ObterId();
        string ObterToken();
        string ObterRefreshToken();
        bool EstaAutenticado();
        string ObterNomeCompleto();
        string ObterPerfil();
    }
}
