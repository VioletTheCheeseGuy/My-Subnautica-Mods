using Nautilus;
using Nautilus.FMod;
using Nautilus.FMod.Interfaces;
using Nautilus.Handlers;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace ProtectionShield
{
    internal class PdaStorygoal
    {
       internal static FMODAsset protectionsuitinstall;
       internal static bool donecreatingaudio = false;
       internal static bool donecreatingpdalog = false;
       internal static bool doneregistering = false;
        
        internal static IEnumerator Register()
        {
            var TimeSw = System.Diagnostics.Stopwatch.StartNew();
            StoryGoalHandler.RegisterCustomEvent("Protectionsuitinstalled", () => { });
            PDAHandler.AddLogEntry("Protectionsuitinstalled", "Protectionsuitinstalled",protectionsuitinstall);
            donecreatingpdalog = true;
            yield return new WaitUntil(() => donecreatingpdalog == true);
            Plugin.Log.LogInfo($"Registed Audio Log As PDA log in {TimeSw.ElapsedMilliseconds} Milliseconds!");
            TimeSw.Stop();
            doneregistering = true;
        }

        internal static IEnumerator RegisterAudio()
        {
            var TimeoutSW = System.Diagnostics.Stopwatch.StartNew();
            var bundlesoruce = new AssetBundleSoundSource(Plugin.pdaaudio);

            FModSoundBuilder builder = new FModSoundBuilder(bundlesoruce);

            IFModSoundBuilder fModSound = builder.CreateNewEvent("FirstInstallProtectionchip", Nautilus.Utility.AudioUtils.BusPaths.PDAVoice);
            fModSound.SetSound("assets/audio/protectionchip/firstinstall.wav").SetFadeDuration(0.5f).SetMode2D(false).Register();
            protectionsuitinstall = Nautilus.Utility.AudioUtils.GetFmodAsset("FirstInstallProtectionchip");
            yield return new WaitUntil(() => protectionsuitinstall != null || TimeoutSW.ElapsedMilliseconds >= 5000f);
            donecreatingaudio = true;
            Plugin.Log.LogInfo($"Registed Audio Log As Fmod Asset in {TimeoutSW.ElapsedMilliseconds} Milliseconds!");
            TimeoutSW.Stop();
             
        }
    }
}
