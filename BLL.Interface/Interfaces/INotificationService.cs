namespace BLL.Interface.Interfaces
{
    public interface INotificationService
    {
        void Send(string destination, string body);
    }
}
