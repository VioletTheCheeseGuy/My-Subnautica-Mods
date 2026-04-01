using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoreCyclopsColors.TestingScripts
{
    // this is a test made for unity explorer and is not run able with out it.
    internal class CyclopsColorTest
    {
        public float boostcolor = 2f;
        

        private void StartTest(GameObject Cyclops, Color newcolor)
        {
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
                        var color = m.color;
                        m.color = new Color(color.r * boostcolor, color.g * boostcolor, color.b * boostcolor,color.a);
                            m.SetColor(ShaderPropertyID._SpecColor, newcolor / boostcolor);
                    }

                }

            }
        }

        
    }
}
