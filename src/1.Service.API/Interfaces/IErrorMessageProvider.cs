using Domain.Shared.Enums;

namespace Service.API.Interfaces
{
    public interface IErrorMessageProvider
    {
        string Get(EErrorCode errorCode);
    }
}
