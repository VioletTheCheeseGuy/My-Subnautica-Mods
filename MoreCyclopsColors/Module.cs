using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Handlers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoreCyclopsColors
{
    internal class Module
    {
        internal static bool registered = false;
        internal static TechType ColorModule;
        internal static Color EmissionColor = new Color(1,1,1,1);
        public static void Register()
        {
            if (!registered)
            {
                Sprite Icon = Plugin.Textures.LoadAsset<Sprite>("CyclopsColorModuleIcon");
                Sprite FabMiscIcon = Plugin.Textures.LoadAsset<Sprite>("CyclopsIcon");

                var prefabinfo = PrefabInfo.WithTechType("CyclopsColorModule", false).WithIcon(Icon);
                CustomPrefab prefab = new CustomPrefab(prefabinfo);

                var cloneobj = new CloneTemplate(prefabinfo, TechType.CyclopsFireSuppressionModule);
                prefab.SetGameObject(cloneobj);
                prefab.SetRecipe(new RecipeData()
                {
                    craftAmount = 1,
                    Ingredients = new List<Ingredient>
                {
                new Ingredient(TechType.Titanium,3),
                new Ingredient(TechType.Polyaniline,1),
                new Ingredient(TechType.ComputerChip,1),
                new Ingredient(TechType.PrecursorIonCrystal,1),
                new Ingredient(TechType.WiringKit,1)
                }
                });

                prefab.SetEquipment(EquipmentType.CyclopsModule);
                prefab.SetPdaGroupCategory(TechGroup.Cyclops, TechCategory.CyclopsUpgrades);
                CraftTreeHandler.AddTabNode(CraftTree.Type.CyclopsFabricator, "CyclopsMiscUp", Language.main.Get("CyclopsFabUpgradesMiscTabDisplay"), FabMiscIcon);
                CraftTreeHandler.AddCraftingNode(CraftTree.Type.CyclopsFabricator, prefabinfo.TechType, "CyclopsMiscUp");
                prefab.Register();
                ColorModule = prefabinfo.TechType;
            }
            registered = true;
        }


        public static void ConvertSettings(GameObject Cyclops)
        {
            var newcolor = Plugin.ColorModOptions.colorValue;
            var boostcolor = Plugin.ColorModOptions.Brightness;
            var Mesh = Cyclops.transform.Find("CyclopsMeshStatic/undamaged/cyclops_LOD0");

            if (Mesh != null)
            {
                var Rends = Mesh.GetAllComponentsInChildren<MeshRenderer>();
                foreach (var r in Rends)
                {
                    var Mats = r.materials;
                    foreach (var m in Mats)
                    {
                        m.SetColor(ShaderPropertyID._GlowColor, newcolor);
                        if (Plugin.ColorModOptions.EnableSpecColoring == true)
                        {
                            var color = m.color;
                            m.color = new Color(color.r + boostcolor, color.g + boostcolor, color.b + boostcolor, color.a);
                            m.SetColor(ShaderPropertyID._SpecColor, m.color / boostcolor);
                        }
                    }

                }

            }
        }

    }
}
