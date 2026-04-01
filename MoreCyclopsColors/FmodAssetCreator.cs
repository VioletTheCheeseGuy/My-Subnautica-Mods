using Nautilus.FMod;
using Nautilus.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoreCyclopsColors
{
    internal class FmodAssetCreator
    {
        public static AssetBundle Audio = Plugin.Audio;
        public static FMODAsset AudioAsset = null;
        public static IEnumerator CreateModuleFirstCraft()
        {
            if (Audio == null){ Audio = Plugin.Audio; }
            CustomSoundSourceBase soundSourceBase = new AssetBundleSoundSource(Audio);
            FModSoundBuilder builder = new FModSoundBuilder(soundSourceBase);
            builder.CreateNewEvent("ColorModuleFirstCraftPDA", AudioUtils.BusPaths.PDAVoice).SetMode2D().SetSound("cyclopsmoduleUnlock").Register();
            AudioAsset = AudioUtils.GetFmodAsset("ColorModuleFirstCraftPDA");
            yield return new WaitUntil(() => AudioAsset != null);


        }
    }
}
