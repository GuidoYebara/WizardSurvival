using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpellsPool : MonoBehaviour
{
    [SerializeField] GameObject[] spells;

    [SerializeField] List<GameObject> spellsActive;
    [SerializeField] List<GameObject> spellsInactive;

    private void Start()
    {
        spellsActive = new List<GameObject>();
        spellsInactive = new List<GameObject>();
    }

    private void SetInactive(GameObject go)
    {
        if (spellsActive.Contains(go)) spellsActive.Remove(go);
        if (!spellsInactive.Contains(go)) spellsInactive.Add(go);
    }

    public void GetSpell(Vector3 position, Quaternion rotation, TypeOfSpells type)
    {
        switch (type)
        {
            case TypeOfSpells.Fireball:
                for (int i = 0; i < spellsInactive.Count; i++)
                {
                    if (spellsInactive[i].GetComponent<Spell>().SpellType == type)
                    {
                        spellsInactive[i].GetComponent<Spell>().Reset(position, rotation);

                        spellsActive.Add(spellsInactive[i]);
                        spellsInactive.Remove(spellsInactive[i]);
                        return;
                    }
                }
                GameObject spell = Instantiate(spells[0], position, rotation, transform);
                spell.GetComponent<Spell>().OnDeath += SetInactive;
                spellsActive.Add(spell);
                break;

            case TypeOfSpells.Ligthingball:
                for (int i = 0; i < spellsInactive.Count; i++)
                {
                    if (spellsInactive[i].GetComponent<Spell>().SpellType == type)
                    {
                        spellsInactive[i].GetComponent<Spell>().Reset(position, rotation);

                        spellsActive.Add(spellsInactive[i]);
                        spellsInactive.Remove(spellsInactive[i]);
                        return;
                    }
                }
                GameObject spell2 = Instantiate(spells[1], position, rotation, transform);
                spell2.GetComponent<Spell>().OnDeath += SetInactive;
                spellsActive.Add(spell2);
                break;

            default:
                break;
        }
    }
}
