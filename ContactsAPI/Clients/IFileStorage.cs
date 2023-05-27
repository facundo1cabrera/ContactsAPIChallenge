namespace ContactsAPI.Clients
{
    public interface IFileStorage
    {
        Task<string> SaveFile(byte[] data, string extension, string container, string contentType);
        Task<string> EditFile(byte[] data, string extension, string container, string path, string contentType);
        Task DeleteFile(string path, string container);

        string GetContainerName();
    }
}
