using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ModClientPreloader.Extensions;
using ModClientPreloader.Models;

namespace ModClientPreloader.Services
{
    // Removed HttpClient and use WebRequest.CreateHttp now.
    public class ApiClient
    {
        public async Task<GamePluginDownloadable> GetGamePluginDownloadable(string version)
        { 
            var req = WebRequest.CreateHttp($"{Context.BucketUrl}/{version}/manifest.txt");
            var manifest = (await req.GetResponseAsync()).GetResponseStream();

            var array = Encoding.ASCII.GetString(manifest.ReadByteArray()).Split(";");
            return new GamePluginDownloadable
            {
                Version = version,
                DllName = array[0],
                MD5Hash = array[1]
            };
        }

        public async Task<Stream> DownloadPlugin(GamePluginDownloadable downloadable)
        {
            var req = WebRequest.CreateHttp($"{Context.BucketUrl}/{downloadable.Version}/{downloadable.DllName}");
            return (await req.GetResponseAsync()).GetResponseStream();
        }
    }
}