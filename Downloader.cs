using System;
using System.IO;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Logging;
using ModClientPreloader.Extensions;
using ModClientPreloader.Services;

namespace ModClientPreloader
{
    public class Downloader
    {
        private readonly ManualLogSource _log;
        
        private readonly ApiClient _client;
        private readonly string _currentGameVersion;
        
        public Downloader(ManualLogSource log)
        {
            _log = log;
            
            _client = new ApiClient();
            _currentGameVersion = GameVersionParser.Parse(Context.GlobalGameManagerPath);
            
            _log.LogMessage($"Initialized downloader on Among Us version ({_currentGameVersion})");
        }

        public async Task Run()
        {
            try
            {
                var downloadable = await _client.GetGamePluginDownloadable(_currentGameVersion);

                bool hashFound = false;
                foreach (var filePath in InstalledModLocator.FindPolusModFiles(Paths.PluginPath))
                {
                    await using var file = File.OpenRead(filePath);
                    // If hash has already been found and current file's hash is equal to latest hash,
                    // current file is a duplicate of the already-installed latest plugin version
                    if (downloadable.MD5Hash != file.MD5Hash() || hashFound)
                        File.Delete(filePath);
                    else
                        hashFound = true;
                }

                if (!hashFound)
                {
                    await using var stream = await _client.DownloadPlugin(downloadable);
                    await stream.CopyToAsync(File.OpenWrite(Path.Combine(Paths.PluginPath, downloadable.DllName)));
                }
            }
            catch (Exception e)
            {
                _log.LogError($"Error in Polus.gg preloader process: {e.Message}.\nStack at time of exception: {e.StackTrace}");
            }
        }
    }
}