using ChipLibrary;
using ChipLibrary.Handler;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;

namespace ProtectionShield
{
    internal class ProtectionChipMK1Techtype
    {
        internal static TechType MK1Techtype = TechType.MapRoomHUDChip;
        internal static CustomPrefab MK1Chip;
        internal static void Register()
        {
            var Prefabinfo = PrefabInfo.WithTechType("ProtectionChipMK1", null, null);
            var MK1Prefab = new CustomPrefab(Prefabinfo);
            var chipclone = new CloneTemplate(Prefabinfo, TechType.ComputerChip);
            MK1Prefab.SetEquipment(EquipmentType.Chip);
            MK1Prefab.SetGameObject(chipclone);
            MK1Prefab.Register();
            MK1Techtype = MK1Prefab.Info.TechType;
            MK1Chip = MK1Prefab;

            ChipHandler.RegisterChip<ProtectionChipMK1>(MK1Techtype);
        }
    }

    internal class ProtectionChipMK1 : ChipBase
    {
        bool isequiped = false;
        bool installedlogsent = false;

        public override void OnEquip()
        {
            isequiped = true;
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

                if (installedlogsent == false)
                {
                    Plugin.Log.LogInfo("Protection Chip installed!");
                }


            }

        }
    }
}

