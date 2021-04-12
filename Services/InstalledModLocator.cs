using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModClientPreloader.Services
{
    public static class InstalledModLocator
    {
        public static IEnumerable<string> FindPolusModFiles(string folderPath)
        {
            return Directory.EnumerateFiles(folderPath)
                .Where(fileName => fileName.ToLower().Contains(Context.PolusModPrefix))
                .Select(file => Path.Combine(folderPath, file));
        }
    }
}