using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoreCyclopsColors.Patches
{
    [HarmonyPatch(typeof(SubRoot))]
    public static class CyclopsUpgradePatches
    {
        // idk why but the fields by default is private and wont let me read it strange ¯\_(ツ)_/¯ lol found it i had allow unsafe code off fixed it now but im still using this cuz im lazy
        private static readonly FieldInfo subModulesDirtyField = AccessTools.Field(typeof(SubRoot), "subModulesDirty");
        private static readonly FieldInfo SubModuleSlots = AccessTools.Field(typeof(SubRoot), "slotNames");

        [HarmonyPatch(nameof(SubRoot.UpdateSubModules))]
        [HarmonyPrefix]
        public static void UpdateSubModulesPatch(SubRoot __instance)
        {
            if ((bool)subModulesDirtyField.GetValue(__instance))
            {
                UpdateColor(__instance);
            }
        }

        private static void UpdateColor(SubRoot subroot)
        {
            if (subroot.upgradeConsole == null || subroot.upgradeConsole.modules == null) return;
            else
            {
                var moduleslot = (string[])SubModuleSlots.GetValue(subroot);
                var modules = subroot.upgradeConsole.modules;
                for (var i = 0; i < 6; i++)
                {
                    
                    var module = moduleslot[i];
                    var ModuleInSlot = modules.GetTechTypeInSlot(module);
                    if (ModuleInSlot == Module.ColorModule)
                    {
                        Module.ConvertSettings(subroot.gameObject);
                        break;
                    }
                }
            }
        } 

    }
}
