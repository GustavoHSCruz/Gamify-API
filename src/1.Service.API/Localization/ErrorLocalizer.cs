using Domain.Shared.Enums;
using Microsoft.Extensions.Localization;
using Service.API.Interfaces;

namespace Service.API.Localization
{
    public class ErrorLocalizer : IErrorMessageProvider
    {
        private readonly IStringLocalizer _loc;

        public ErrorLocalizer(IStringLocalizerFactory factory)
        {
            var asm = typeof(ErrorLocalizer).Assembly.GetName().Name!;
            _loc = factory.Create("Errors", asm);
        }

        public string Get(EErrorCode code) => _loc[code.ToString()];
    }
}
