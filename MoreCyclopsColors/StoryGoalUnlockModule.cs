using Nautilus.Handlers;
using Nautilus.Utility;
using Story;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreCyclopsColors
{
    internal class StoryGoalUnlockModule
    {
        public static void RegisterStoryGoals()
        {
            var Unlock = AudioUtils.GetFmodAsset("event:/loot/new_PDA_data");

            StoryGoalHandler.RegisterCustomEvent("ColorModuleunlocked", () => { Utils.PlayFMODAsset(Unlock, Player.main.transform.position);});

            StoryGoalHandler.RegisterItemGoal("ColorModulePickedUp", Story.GoalType.PDA, Module.ColorModule, 1f);

            PDAHandler.AddLogEntry("ColorModulePickedUp", "PDAPickupline",FmodAssetCreator.AudioAsset,SpriteManager.Get(TechType.PDA));
        }
    }
}
