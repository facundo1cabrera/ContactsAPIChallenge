namespace ContactsAPI.Clients
{
    public class LocalFileStorage : IFileStorage
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _containerName = "contacts";

        public LocalFileStorage(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }
        public Task DeleteFile(string path, string container)
        {
            if (path != null)
            {
                var fileName = Path.GetFileName(path);
                string fileDirectory = Path.Combine(_env.WebRootPath, container, fileName);

                if (File.Exists(fileDirectory))
                {
                    File.Delete(fileDirectory);
                }

            }
            return Task.FromResult(0);
        }

        public async Task<string> EditFile(byte[] data, string extension, string container, string path, string contentType)
        {
            await DeleteFile(path, container);
            return await SaveFile(data, extension, container, contentType);
        }

        public async Task<string> SaveFile(byte[] data, string extension, string container, string contentType)
        {
            var fileName = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(_env.WebRootPath, container);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string path = Path.Combine(folder, fileName);
            await File.WriteAllBytesAsync(path, data);

            var url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var fileUrl = Path.Combine(url, container, fileName).Replace("\\", "/");
            return fileUrl;
        }

        public string GetContainerName()
        {
            return _containerName;
        }
    }
}
