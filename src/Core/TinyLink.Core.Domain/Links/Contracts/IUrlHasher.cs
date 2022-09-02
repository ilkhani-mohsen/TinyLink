namespace TinyLink.Core.Domain.Links.Contracts
{
    public interface IUrlHasher
    {
        public Task<string> Hash(string url);
    }
}
