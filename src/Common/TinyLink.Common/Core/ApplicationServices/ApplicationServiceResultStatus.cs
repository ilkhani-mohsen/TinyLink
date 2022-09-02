namespace TinyLink.Common.Core.ApplicationServices
{
    public enum ApplicationServiceResultStatus 
    {
        Ok = 200,
        ValidationError = 400,
        AccessDenied = 403,
        NotFound = 404,
        Exception = 500
    }
}
