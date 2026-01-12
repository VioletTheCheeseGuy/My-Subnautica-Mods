using Nautilus.Handlers;
using Story;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtectionShield
{
    internal class StoryUnlockGoals
    {

        internal static void RegisterCollderGoals()
        {
            StoryGoalHandler.RegisterItemGoal("UnlockProtectionChip2", Story.GoalType.Story, ProtectionChipMK1Techtype.MK1Techtype, 0f);
            StoryGoalHandler.RegisterItemGoal("UnlockProtectionChip3", Story.GoalType.Story, ProtectionChipMK2Techtype.MK2Techtype, 0f);

            // Goal completion
            StoryGoalHandler.RegisterOnGoalUnlockData("UnlockProtectionChip2", new UnlockBlueprintData[] { new UnlockBlueprintData() { techType = ProtectionChipMK2Techtype.MK2Techtype, unlockType = UnlockBlueprintData.UnlockType.Available } });
            StoryGoalHandler.RegisterOnGoalUnlockData("UnlockProtectionChip3", new UnlockBlueprintData[] { new UnlockBlueprintData() { techType = ProtectionChipMK3Techtype.MK3Techtype, unlockType = UnlockBlueprintData.UnlockType.Available } });


        }



    }
}
