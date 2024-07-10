using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill : MonoBehaviour
{
    public GameObject firePrefab;
    public Transform launchPoint;
    public SpellCooldown spellCooldown; // Reference to the SpellCooldown script

    void Start()
    {
            spellCooldown = FindObjectOfType<SpellCooldown>();
    }

    public void FireCast()
    {
        if (spellCooldown != null && spellCooldown.UseSpell()) // Check if the spell can be used
        {
            GameObject fireInstance = Instantiate(firePrefab, launchPoint.position, firePrefab.transform.rotation);
            Destroy(fireInstance, 0.5f);
        }
    }
}
