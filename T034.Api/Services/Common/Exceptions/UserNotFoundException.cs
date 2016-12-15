namespace T034.Api.Services.Common.Exceptions
{
    public class UserNotFoundException : BusinessException
    {
        public UserNotFoundException(string email) 
            : base($"Не найден пользователь {email}")
        {
        }
    }
}
