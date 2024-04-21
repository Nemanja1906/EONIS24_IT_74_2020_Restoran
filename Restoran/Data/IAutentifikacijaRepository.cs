namespace Restoran.Data
{
    public interface IAutentifikacijaRepository
    {
        Task<object> Authenticate(string username, string password);
    }
}
