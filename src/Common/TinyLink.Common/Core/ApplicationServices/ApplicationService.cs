using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace TinyLink.Common.Core.ApplicationServices
{
    public abstract class ApplicationService<T> : IApplicationService where T : class
    {
        public const string ExceptionMessage = "خطایی رخ داده است. از طریق شناسه {0} می‌توانید علت خطا را پیگیری کنید.";
        public ApplicationServiceContext Context { get; }        
        
        protected readonly ILogger<T> logger;
        protected IServiceProvider ServiceProvider => Context.ServiceProvider;

        public ApplicationService(ApplicationServiceContext context)
        {
            Context = context;

            var logFactory = ServiceProvider.GetRequiredService<ILoggerFactory>();
            logger = logFactory.CreateLogger<T>();
        }

        protected virtual async Task<IEnumerable<string>> Validate<TRequest>(TRequest request) 
        {
            var errorMessages = new List<string>();

            var validator = ServiceProvider.GetService<IValidator<TRequest>>();
            if (validator != null)
            {
                var validationResult = await validator.ValidateAsync(request);
                errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            return errorMessages;

        }
        protected virtual async Task<ApplicationServiceResult<TResponse>> Execute<TRequest, TResponse>(TRequest request, Func<TRequest, Task<ApplicationServiceResult<TResponse>>> handler)
        {
            try
            {
                var errorMessages = await Validate(request);
                if (errorMessages.Count() > 0)
                    return ValidationError<TResponse>(errorMessages.ToArray());

                return await handler(request);
            }
            catch (Exception ex)
            {
                var traceId = Guid.NewGuid().ToString();
                logger.LogError(ex, $"{ex.Message}. {traceId}", traceId);
                return Exception<TResponse>(string.Format(ExceptionMessage, traceId));
            }
        }

        protected virtual ApplicationServiceResult<TResponse> Ok<TResponse>(TResponse data = default(TResponse)) => ApplicationServiceResult<TResponse>.Ok(data);

        protected virtual ApplicationServiceResult<TResponse> ValidationError<TResponse>(params string[] messages) => ApplicationServiceResult<TResponse>.ValidationError(messages);

        protected virtual ApplicationServiceResult<TResponse> AccessDenied<TResponse>(params string[] messages) => ApplicationServiceResult<TResponse>.AccessDenied(messages);

        protected virtual ApplicationServiceResult<TResponse> NotFound<TResponse>(params string[] messages) => ApplicationServiceResult<TResponse>.NotFound(messages);

        protected virtual ApplicationServiceResult<TResponse> Exception<TResponse>(params string[] messages) => ApplicationServiceResult<TResponse>.Exception(messages);
    }
}
