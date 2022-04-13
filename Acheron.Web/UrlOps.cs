using System.Operations;

namespace Acheron.Web
{
    public static class UrlOps
    {
        public static byte[] GetBytes(this Uri uri, double timout = 150) => GetBytes(uri.AbsoluteUri, timout);
        public static byte[] GetBytes(this string uri, double timout = 150)
        {
            if (File.Exists(uri))
            {
                return File.ReadAllBytes(uri);
            }
            else
            {
                using (HttpClient client = new())
                {
                    client.DefaultRequestHeaders.Add("user-agent", $"mtk-{new Random().Next(1000, 9999)}");
                    client.Timeout = TimeSpan.FromSeconds(timout);
                    return client.GetByteArrayAsync(uri).GetAwaiter().GetResult();
                }
            }
        }

        public static async Task DownloadFile(this Uri uri, string file, bool overwrite = true) => await DownloadFile(uri.AbsoluteUri, file, overwrite);
        public static async Task DownloadFile(this string uri, string file, bool overwrite = true)
        {
            if (File.Exists(file))
            {
                if (overwrite)
                    File.Delete(file);
                else return;
            }

            Directory.CreateDirectory(new FileInfo(file).DirectoryName ?? file.EditPath());
            await File.WriteAllBytesAsync(file, GetBytes(uri));
        }
    }
}
