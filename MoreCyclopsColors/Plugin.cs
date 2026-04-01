using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Nautilus.Handlers;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEngine;
using UWE;

namespace MoreCyclopsColors 
{
    [BepInPlugin(PluginGUID,PluginName,Version)]
    [BepInDependency("com.snmodding.nautilus",BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {
        private const string PluginGUID = "com.Aether.MoreCyclopsColors";
        private const string PluginName = "More Cyclops Colors";
        private const string Version = "1.0.0";
        
        // normally harmony is not used but i add it just incase lol
        private static readonly Harmony harmony = new Harmony(PluginGUID);

        public static ManualLogSource Log;

        // path related stuff idk what do you want me to write ¯\_(ツ)_/¯
        private static string PluginPath = Assembly.GetExecutingAssembly().Location;
        private string AssetsPath = Path.Combine(Path.GetDirectoryName(PluginPath), "Assets");

        public static ModOptionssaving ColorModOptions;
        public static AssetBundle Textures = null;
        public static AssetBundle Audio = null;
        public static bool LoadedBundles = false;

        private void Start()
        {
            Log = Logger;
            harmony.PatchAll();

            Log.LogInfo($"loading {PluginName} Version:{Version}");

            LanguageHandler.RegisterLocalizationFolder("Localization");
            ColorModOptions = OptionsPanelHandler.RegisterModOptions<ModOptionssaving>();

            WaitScreenHandler.RegisterEarlyAsyncLoadTask(Language.main.Get("LoadingScreenName") ,LoadAssets,Language.main.Get("LoadingAssets"));
            WaitScreenHandler.RegisterEarlyAsyncLoadTask(Language.main.Get("LoadingScreenName") , Register,Language.main.Get("RegisterModule"));
            WaitScreenHandler.RegisterAsyncLoadTask(Language.main.Get("LoadingScreenName"), LoadDataBox, Language.main.Get("Loaddatadoxinworld"));
        }

        private IEnumerator Register(WaitScreenHandler.WaitScreenTask task)
        {
            Module.Register();
            StoryGoalUnlockModule.RegisterStoryGoals();
            yield return new WaitUntil(() => Module.registered);
        }

        private IEnumerator LoadAssets(WaitScreenHandler.WaitScreenTask task)
        {
            if (!LoadedBundles)
            {
                string AssetBundletex = Path.Combine(AssetsPath, "textures");
                string AssetBundleaudio = Path.Combine(AssetsPath, "audio");
                Textures = AssetBundle.LoadFromFile(AssetBundletex);
                Audio = AssetBundle.LoadFromFile(AssetBundleaudio);
                LoadedBundles = true;
            }

            CoroutineHost.StartCoroutine(FmodAssetCreator.CreateModuleFirstCraft());
            yield return new WaitUntil(() => Textures && Audio != null);

        }

        private IEnumerator LoadDataBox(WaitScreenHandler.WaitScreenTask task)
        {
            CoroutineHost.StartCoroutine(DataBoxSpawner.GetDatabox());
            yield return new WaitUntil(() => DataBoxSpawner.Databoxspawned);
        }
    }
}
