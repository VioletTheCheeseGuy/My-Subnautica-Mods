using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProtectionShield
{
    public class MK3TriggerDamageBlocker : MonoBehaviour
    {
        
        void Update()
        {
            List<GameObject> creatures = GetAllActiveCreature();

            foreach (GameObject creature in creatures)
            {
                if (creature != null)
                {


                    if (ProtectionChipMK3.isequiped == true)
                    {
                        SetMeleeDisabled(creature);
                    }
                    else
                    {
                        SetMeleeEnabled(creature);
                    }


                }
            }
        }

        private void SetMeleeDisabled(GameObject creature)
        {
            var meleeattack = creature.GetComponent<MeleeAttack>();
            
            if (meleeattack != null)
            {
                    meleeattack.canBitePlayer = false;

            }
            
        }

        private void SetMeleeEnabled(GameObject creature)
        {
            var meleeattack = creature.GetComponent<MeleeAttack>();
            var shockermeleeattack = creature.GetComponent<ShockerMeleeAttack>();
            var crabsnakemeleeattack = creature.GetComponent<CrabsnakeMeleeAttack>();
            var Warpermeleeattack = creature.GetComponent<WarperMeleeAttack>();
            var Reapermeleeattack = creature.GetComponent<ReaperMeleeAttack>();
            var Ghostmeleeattack = creature.GetComponent<GhostLeviathanMeleeAttack>();
            var Seadragonmeleeattack = creature.GetComponent<SeaDragonMeleeAttack>();
            if (meleeattack != null)
            {
                    meleeattack.canBitePlayer = true;
            }
            
        }

        public static List<GameObject> GetAllActiveCreature()
        {
            List<GameObject> activecreatures = new List<GameObject>();

            GameObject[] allcreatures = UnityEngine.Object.FindObjectsOfType<GameObject>();

            foreach (GameObject go in allcreatures)
            {
                if (go.activeInHierarchy)
                {
                    activecreatures.Add(go);
                }
            }

            return activecreatures;
        }

    }
}
