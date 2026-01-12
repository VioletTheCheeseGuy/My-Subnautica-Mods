using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Nautilus.Handlers;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEngine;
using ProtectionShield;
using UWE;

namespace ProtectionShield
{
    [BepInPlugin(GUID, Name, Version)]
    [BepInDependency("com.alexius25.chiplibrary",BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.snmodding.nautilus",BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {
        private const string GUID = "Aether.ProtectionShield";
        private const string Name = "Pda Protection Shield";
        private const string Version = "1.0.0";

        internal static AssetBundle pdaaudio;
        internal static string Pluginfolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        internal static string Assetfolder = Path.Combine(Pluginfolder, "Assets");

        public static ManualLogSource Log;
        private static readonly Harmony harmony = new Harmony(GUID);

        private void Awake()
        {
            harmony.PatchAll();
            Log = Logger;

            Log.LogInfo($"Loading {Name} Version {Version}");

            LanguageHandler.RegisterLocalizationFolder("Localization");

            WaitScreenHandler.RegisterEarlyAsyncLoadTask("Protection Chip", LoadAudio, Language.main.Get("LoadingAudio"));
            WaitScreenHandler.RegisterAsyncLoadTask("Protection Chip", RegisterPDALog, Language.main.Get("RegisteringPDAlogs"));
            WaitScreenHandler.RegisterAsyncLoadTask("Protection Chip", RegisterChips, Language.main.Get("RegisteringChips"));

        }

        internal IEnumerator LoadAudio(WaitScreenHandler.WaitScreenTask task)
        {
            string Audiobundle = Path.Combine(Assetfolder, "audio");
            pdaaudio = AssetBundle.LoadFromFile(Audiobundle);
            yield return new WaitUntil(() => pdaaudio != null);

            foreach (var name in pdaaudio.GetAllAssetNames())
            {
                Log.LogInfo($"Audio file in the bundle is || {name}");
            }
        }

        private IEnumerator RegisterPDALog(WaitScreenHandler.WaitScreenTask task)
        {
            CoroutineHost.StartCoroutine(PdaStorygoal.RegisterAudio());
            CoroutineHost.StartCoroutine(PdaStorygoal.Register());
            yield return new WaitUntil(() => PdaStorygoal.doneregistering && PdaStorygoal.donecreatingaudio && PdaStorygoal.donecreatingpdalog == true);

        }

        private IEnumerator RegisterChips(WaitScreenHandler.WaitScreenTask task)
        {

            ProtectionChipMK1Techtype.Register();
            ProtectionChipMK2Techtype.Register();
            ProtectionChipMK3Techtype.Register();
            StoryUnlockGoals.RegisterCollderGoals();
            yield return new WaitUntil(() => ProtectionChipMK1Techtype.MK1Techtype == ProtectionChipMK1Techtype.MK1Chip.Info.TechType && ProtectionChipMK3Techtype.MK3Techtype == ProtectionChipMK3Techtype.MK3Chip.Info.TechType);

        }
    }
}
