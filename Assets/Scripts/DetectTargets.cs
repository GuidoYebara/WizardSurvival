using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTargets : MonoBehaviour
{
    [SerializeField] Spell spell;

    [SerializeField] List<GameObject> noTargets;

    private void Start()
    {
        if (gameObject.GetComponentInParent<Spell>()) spell = gameObject.GetComponentInParent<Spell>();

        noTargets = new List<GameObject>();
        spell.OnDeath += Restart;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy") return;
        if (noTargets.Contains(other.gameObject)) return;
        if (spell != null)
        {
            noTargets.Add(other.gameObject);
            spell.AddEnemy(other.gameObject);
        }
    }

    private void Restart(GameObject go)
    {
        noTargets.Clear();
    }
}
