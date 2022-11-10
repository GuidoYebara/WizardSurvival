using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NameOfSpell", menuName = "New Spell")]

public class SpellSO : ScriptableObject
{
    public float cost;
    public float speed;
    public float damage;
    public float duration;
    public float deathTime;

    public bool isAutoAim;
    public bool isExplosive;
    public bool traversesUnits;

    public string description;
}
