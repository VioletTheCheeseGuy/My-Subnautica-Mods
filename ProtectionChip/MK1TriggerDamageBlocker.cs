using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProtectionShield
{
    public class MK1TriggerDamageBlocker : MonoBehaviour
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
                    if (creature.CompareTag("Creature"))
                    {
                        if (ProtectionChipMK1.isequiped == true)
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
        }

        private void SetMeleeDisabled(GameObject creature)
        {
            var meleeattack = creature.GetComponent<MeleeAttack>();
            if (meleeattack != null)
            {
                if (meleeattack.biteDamage <= 10f)
                {
                    meleeattack.canBitePlayer = false;
                }
                else
                {
                    if (divied == false)
                    {
                        meleeattack.biteDamage /= 1.5f;
                    }
                }

            }
        }

        private void SetMeleeEnabled(GameObject creature)
        {
            var meleeattack = creature.GetComponent<MeleeAttack>();
            if (meleeattack != null)
            {
                if (meleeattack.biteDamage <= 10f)
                {
                    meleeattack.canBitePlayer = true;
                }
                else
                {
                    if (undivied == false)
                    {
                        meleeattack.biteDamage *= 1.5f;
                    }
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
