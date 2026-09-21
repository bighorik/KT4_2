namespace Application.Interfaces.Services
{
    public interface IEmailService
    {
        public Task SendConfirmationMessage(string email, string confirmationCode);
    }
}
