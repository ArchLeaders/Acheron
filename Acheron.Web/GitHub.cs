using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Acheron.Web
{
    public class GitHub
    {
        public static async Task<GitHub> FetchLatest(string https, int asset = 1, bool isPreRelease = false)
        {
            if (https.Contains(";"))
            {
                var info = https.Split(';');
                https = isPreRelease ? $"https://api.github.com/repos/{info[0]}/{info[1]}/releases" :
                    $"https://api.github.com/repos/{info[0]}/{info[1]}/releases/latest";
            }

            // Create HttpsClient
            using (HttpClient client = new())
            {
                // Setup request headers
                client.DefaultRequestHeaders.Add($"User-Agent", $"archleaders-{new Random().Next(1000, 9999)}");

                // Get API information
                string json = await client.GetStringAsync(https);

                // Parse API information
                dynamic? gitinfo = JsonSerializer.Deserialize<dynamic>(json);

                // Return the desired asset download link
                if (gitinfo != null)
                {
                    if (isPreRelease)
                        return gitinfo[0]["assets"][asset - 1]["browser_download_url"];
                    else
                        return gitinfo["assets"][asset - 1]["browser_download_url"];
                }
            }

            // Return found release link
            return new(https);
        }

        public static async Task<GitHub> FetchLatest(string repo, string user, int asset, bool isPreRelease = false) => await FetchLatest($"{user};{repo}", asset, isPreRelease);

        public string HttpsLink { get; set; } = "";
        public byte[] Content { get; set; } = new byte[0];

        public GitHub(string https)
        {
            HttpsLink = https;

            using (HttpClient client = new())
            {
                Content = client.GetByteArrayAsync(https).GetAwaiter().GetResult();
            }
        }
    }
}
