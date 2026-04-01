using rail;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UWE;

namespace MoreCyclopsColors
{
    internal class DataBoxSpawner
    {
        // reused this stuff from the custom pda log mod :P
        private static string DataboxClassID = "d9ae8384-5bec-42c0-a51d-11bbb2662d95";

        public static bool Databoxspawned = false;

        internal static IEnumerator GetDatabox()
        {
            GameObject Databox = null;
            var task = PrefabDatabase.GetPrefabAsync(DataboxClassID);
            yield return task;
            bool loadedbox = task.TryGetPrefab(out Databox);
            if (!loadedbox)
            {
                ErrorMessage.AddError($"Failed to load databox with classID:{DataboxClassID} try a dif one");
                throw new Exception($"Failed to load databox with classID:{DataboxClassID} try a dif one");
            }

            GameObject FoundDataBox = Databox.transform.gameObject;
            Setupdataboxandspawn(FoundDataBox);
        }

        private static void Setupdataboxandspawn(GameObject DataBox)
        {
            GameObject ModuleDatabox = GameObject.Instantiate(DataBox, position: new Vector3(970.750854f, -83.2035141f ,125.735466f), rotation: new Quaternion(0.106179595f, 0.402825445f,-0.084617f, 0.905150533f));
            BlueprintHandTarget blueprintHandTarget = ModuleDatabox.GetComponent<BlueprintHandTarget>();
            blueprintHandTarget.unlockTechType = Module.ColorModule;
            blueprintHandTarget.secondaryTooltip = Language.main.Get("ModuleDataBox");
            blueprintHandTarget.onUseGoal.key = "ColorModuleunlocked";
            blueprintHandTarget.onUseGoal.goalType = Story.GoalType.Story;
            blueprintHandTarget.onUseGoal.delay = 2f;
            Databoxspawned = true;

        }
    }
}
