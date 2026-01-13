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
    internal class ProtectionChipMK3Techtype
    {
        internal static TechType MK3Techtype = TechType.MapRoomHUDChip;
        internal static CustomPrefab MK3Chip;

        internal static void Register()
        {
            var Prefabinfo = PrefabInfo.WithTechType("ProtectionChipMK3", null, null,unlockAtStart: true).WithIcon(SpriteManager.Get(TechType.ComputerChip));
            var MK3Prefab = new CustomPrefab(Prefabinfo);
            var chipclone = new CloneTemplate(Prefabinfo, TechType.ComputerChip);
            MK3Prefab.SetEquipment(EquipmentType.Chip); 
            MK3Prefab.SetGameObject(chipclone);
            MK3Prefab.SetRecipe(new Nautilus.Crafting.RecipeData(new Ingredient(ProtectionChipMK2Techtype.MK2Techtype,1),new Ingredient(TechType.AdvancedWiringKit, 1), new Ingredient(TechType.Lithium, 2), new Ingredient(TechType.PrecursorIonCrystal, 2))).WithCraftingTime(15f).WithFabricatorType(CraftTree.Type.Workbench);
            MK3Prefab.SetPdaGroupCategoryAfter(TechGroup.Personal, TechCategory.Equipment, TechType.Compass);
            MK3Prefab.Register();
            MK3Techtype = MK3Prefab.Info.TechType;
            MK3Chip = MK3Prefab;

            ChipHandler.RegisterChip<ProtectionChipMK3>(MK3Techtype);
        }
    }

    internal class ProtectionChipMK3 : ChipBase
    {
        internal static bool isequiped = false;
        bool installedlogsent = false;

        private bool Giviendetectioncollider;
        GameObject DetectionColldier = null;

        public override void OnEquip()
        {
            isequiped = true;

            if (ProtectionChipMK2.isequiped || ProtectionChipMK1.isequiped)
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
                    DetectionColldier.AddComponent<MK3TriggerDamageBlocker>();
                    Giviendetectioncollider = true;


                }
                if (installedlogsent == false)
                {
                    Plugin.Log.LogInfo("Protection Chip MK3 installed!");
                    installedlogsent = true;
                    StoryGoal.Execute("Protectionsuitinstalled", Story.GoalType.PDA);

                }


            }


        }




    }
}

