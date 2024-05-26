namespace Firebase.Api;

public interface INotificationClient
{
    Task RecibeNotification(string message);
}