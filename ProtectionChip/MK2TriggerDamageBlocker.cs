using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProtectionShield
{
    public class MK2TriggerDamageBlocker : MonoBehaviour
    {
        private bool divied = false;
        private bool undivied = false;
        void Update()
        {
            List<GameObject> creatures = GetAllActiveCreature();

            foreach (GameObject creature in creatures)
            {
                if (creature != null)
                {
                    
                    
                        if (ProtectionChipMK2.isequiped == true)
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
            var shockermeleeattack = creature.GetComponent<ShockerMeleeAttack>();
            var crabsnakemeleeattack = creature.GetComponent<CrabsnakeMeleeAttack>();
            var Warpermeleeattack = creature.GetComponent<WarperMeleeAttack>();
            var Reapermeleeattack = creature.GetComponent<ReaperMeleeAttack>();
            var Ghostmeleeattack = creature.GetComponent<GhostLeviathanMeleeAttack>();
            var Seadragonmeleeattack = creature.GetComponent<SeaDragonMeleeAttack>();
            if (meleeattack != null)
            {
                if (meleeattack.biteDamage <= 25f && !meleeattack.gameObject.name.Contains("Reaper") && !meleeattack.gameObject.name.Contains("GhostLeviathan") && !meleeattack.gameObject.name.Contains("SeaDragon"))
                {
                    meleeattack.canBitePlayer = false;
                }
                else
                {
                    if (divied == false && !meleeattack.gameObject.name.Contains("Reaper") && !meleeattack.gameObject.name.Contains("GhostLeviathan") && !meleeattack.gameObject.name.Contains("SeaDragon"))
                    {
                        meleeattack.biteDamage /= 2.5f;
                    }
                }

            }
            else if (shockermeleeattack != null)
            {

                shockermeleeattack.canBitePlayer = false;

            }
            else if (crabsnakemeleeattack != null)
            {
                crabsnakemeleeattack.canBitePlayer = false;
            }
            else if (Warpermeleeattack != null)
            {
                Warpermeleeattack.canBitePlayer = false;
            }
            else if (Reapermeleeattack != null)
            {
                if (divied == false)
                {
                    Reapermeleeattack.biteDamage /= 2.5f;
                }

            }
            else if (Ghostmeleeattack != null)
            {
                if (divied == false)
                {
                    Ghostmeleeattack.biteDamage /= 2.5f;
                }
            }
            else if (Seadragonmeleeattack != null)
            {
                if (divied == false)
                {
                    Seadragonmeleeattack.biteDamage /= 2.5f;
                }

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
                if (meleeattack.biteDamage <= 25f)
                {
                    meleeattack.canBitePlayer = true;
                }
                else
                {
                    if (undivied == false)
                    {
                        meleeattack.biteDamage *= 2.5f;
                    }
                }

            }
            else if (shockermeleeattack != null)
            {

                shockermeleeattack.canBitePlayer = true;

            }
            else if (crabsnakemeleeattack != null)
            {
                crabsnakemeleeattack.canBitePlayer = true;
            }
            else if(Warpermeleeattack != null)
            {
                Warpermeleeattack.canBitePlayer = true;
            }
            else if (Reapermeleeattack != null)
            {
                if (undivied == false)
                {
                    Reapermeleeattack.biteDamage *= 1.5f;
                }

            }
            else if (Ghostmeleeattack != null)
            {
                if (undivied == false)
                {
                    Ghostmeleeattack.biteDamage *= 1.5f;
                }
            }
            else if (Seadragonmeleeattack != null)
            {
                if (undivied == false)
                {
                    Seadragonmeleeattack.biteDamage *= 1.5f;
                }

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
