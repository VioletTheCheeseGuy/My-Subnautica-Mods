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
    internal class ProtectionChipMK1Techtype
    {
        internal static TechType MK1Techtype = TechType.MapRoomHUDChip;
        internal static CustomPrefab MK1Chip;
        
        
        internal static void Register()
        {
            var Prefabinfo = PrefabInfo.WithTechType("ProtectionChipMK1", null, null, unlockAtStart:false).WithIcon(SpriteManager.Get(TechType.ComputerChip));
            var MK1Prefab = new CustomPrefab(Prefabinfo);
            var chipclone = new CloneTemplate(Prefabinfo, TechType.ComputerChip);
            MK1Prefab.SetEquipment(EquipmentType.Chip);
            MK1Prefab.SetGameObject(chipclone);
            MK1Prefab.SetRecipe(new Nautilus.Crafting.RecipeData(new Ingredient(TechType.WiringKit, 1), new Ingredient(TechType.CopperWire, 2), new Ingredient(TechType.ComputerChip, 1))).WithCraftingTime(5f).WithFabricatorType(CraftTree.Type.Fabricator);
            MK1Prefab.SetPdaGroupCategoryAfter(TechGroup.Personal, TechCategory.Equipment, TechType.Compass);
            MK1Prefab.Register();
            MK1Techtype = MK1Prefab.Info.TechType;
            MK1Chip = MK1Prefab;

            ChipHandler.RegisterChip<ProtectionChipMK1>(MK1Techtype);
        }
    }

    internal class ProtectionChipMK1 : ChipBase
    {
        internal static bool isequiped = false;
        bool installedlogsent = false;
        
        private bool Giviendetectioncollider;
        GameObject DetectionColldier = null;

        public override void OnEquip()
        {
            isequiped = true;

            if (ProtectionChipMK2.isequiped || ProtectionChipMK3.isequiped)
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
                    DetectionColldier.AddComponent<MK1TriggerDamageBlocker>();
                    Giviendetectioncollider = true;


                }
                if (installedlogsent == false)
                {
                    Plugin.Log.LogInfo("Protection Chip installed!");
                    installedlogsent = true;
                    StoryGoal.Execute("Protectionsuitinstalled", Story.GoalType.PDA);
                    
                }


            }
            

        }

        

        
    }
}


