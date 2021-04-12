using System.IO;
using BepInEx;

namespace ModClientPreloader
{
    public static class Context
    {
        public const string BucketUrl = "https://polusgg-mod-client.nyc3.digitaloceanspaces.com";
        
        public const string PolusModPrefix = "polusgg";

        public static string GlobalGameManagerPath =>
            Path.Combine(Paths.GameRootPath, "Among Us_Data", "globalgamemanagers");
    }
}