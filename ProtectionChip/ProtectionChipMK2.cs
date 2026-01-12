using ChipLibrary;
using ChipLibrary.Handler;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Utility;
using Story;
using UnityEngine;

namespace ProtectionShield
{
    internal class ProtectionChipMK2Techtype
    {
        internal static TechType MK2Techtype = TechType.MapRoomHUDChip;
        internal static CustomPrefab MK2Chip;

        internal static void Register()
        {
            var Prefabinfo = PrefabInfo.WithTechType("ProtectionChipMK2", null, null,unlockAtStart:false).WithIcon(SpriteManager.Get(TechType.ComputerChip));
            var MK2Prefab = new CustomPrefab(Prefabinfo);
            var chipclone = new CloneTemplate(Prefabinfo, TechType.ComputerChip);
            MK2Prefab.SetEquipment(EquipmentType.Chip);
            MK2Prefab.SetGameObject(chipclone);
            MK2Prefab.SetRecipe(new Nautilus.Crafting.RecipeData(new Ingredient(ProtectionChipMK1Techtype.MK1Techtype,1),new Ingredient(TechType.AdvancedWiringKit, 1), new Ingredient(TechType.CopperWire, 1), new Ingredient(TechType.Battery,1))).WithCraftingTime(6f).WithFabricatorType(CraftTree.Type.Workbench);
            MK2Prefab.SetPdaGroupCategoryAfter(TechGroup.Personal, TechCategory.Equipment, TechType.Compass);
            MK2Prefab.Register();
            MK2Techtype = MK2Prefab.Info.TechType;
            MK2Chip = MK2Prefab;

            ChipHandler.RegisterChip<ProtectionChipMK2>(MK2Techtype);
        }
    }

    internal class ProtectionChipMK2 : ChipBase
    {
        internal static bool isequiped = false;
        bool installedlogsent = false;

        private bool Giviendetectioncollider;
        GameObject DetectionColldier = null;

        public override void OnEquip()
        {
            isequiped = true;

            if (ProtectionChipMK1.isequiped || ProtectionChipMK3.isequiped)
            {
                BasicText warningmessage = new BasicText();

                warningmessage.SetSize(15);
                warningmessage.SetFont(Nautilus.Utility.FontUtils.Aller_Rg);
                warningmessage.ShowMessage("It is quite possible that using two Protection Chip's at the same time has bugs so if you can, just use the MK3.", 10);
            }
        }
        public override void OnUnequip()
        {
            isequiped = false;
            installedlogsent = false;
        }

        private void Update()
        {
            if (isequiped)
            {
                if (!Giviendetectioncollider || DetectionColldier == null)
                {
                    var Player = GameObject.Find("Player").gameObject;
                    DetectionColldier = new GameObject();
                    DetectionColldier.name = "ProtectionChipDetectionColliderHolder";
                    DetectionColldier.transform.parent = Player.transform;
                    DetectionColldier.AddComponent<MK2TriggerDamageBlocker>();
                    Giviendetectioncollider = true;


                }
                if (installedlogsent == false)
                {
                    Plugin.Log.LogInfo("Protection Chip MK2 installed!");
                    installedlogsent = true;
                    StoryGoal.Execute("Protectionsuitinstalled", Story.GoalType.PDA);

                }


            }


        }




    }
}

