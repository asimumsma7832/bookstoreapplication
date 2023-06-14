namespace BookStoreApplication.Service
{
    public interface IUserService
    {
        string GetUserId();

        bool IsAuthenticated();
    }
}