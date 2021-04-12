using System.Collections.Generic;
using BepInEx.Logging;
using Mono.Cecil;

namespace ModClientPreloader
{
    public static class Patcher
    {
        public static void Initialize() => new Downloader(Logger.CreateLogSource("Polus.gg Preloader"))
            .Run().GetAwaiter().GetResult();

        public static IEnumerable<string> TargetDLLs { get; } = new List<string>();
        public static void Patch(AssemblyDefinition assembly) {}
    }
}
