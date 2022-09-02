namespace TinyLink.Common.Core.ApplicationServices
{
    public class ApplicationServiceResult<TData> 
    {
        protected List<string> messages = new List<string>();

        public ApplicationServiceResultStatus Status { get; set; }
        public bool Succeeded => Status == ApplicationServiceResultStatus.Ok;
        public IEnumerable<string> Messages => messages;
        public TData Data { get; set; }

        public void AddMessages(params string[] messages)
        {
            this.messages.AddRange(messages);
        }

        public void ClearMessages()
        {
            messages.Clear();
        }

        public static ApplicationServiceResult<TData> Ok(TData data)
        {
            var result = new ApplicationServiceResult<TData>()
            {
                Status = ApplicationServiceResultStatus.Ok,
                Data = data
            };

            return result;
        }
        private static ApplicationServiceResult<TData> Failed(ApplicationServiceResultStatus status, params string[] messages)
        {
            var result = new ApplicationServiceResult<TData>()
            {
                Status = status,
                messages = messages.ToList(),
                Data = default(TData)
            };

            return result;
        }

        public static ApplicationServiceResult<TData> ValidationError(params string[] messages) => Failed(ApplicationServiceResultStatus.ValidationError, messages);

        public static ApplicationServiceResult<TData> AccessDenied(params string[] messages) => Failed(ApplicationServiceResultStatus.AccessDenied, messages);

        public static ApplicationServiceResult<TData> NotFound(params string[] messages) => Failed(ApplicationServiceResultStatus.NotFound, messages);

        public static ApplicationServiceResult<TData> Exception(params string[] messages) => Failed(ApplicationServiceResultStatus.Exception, messages);
    }
}
