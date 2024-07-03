using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill : MonoBehaviour
{
    public GameObject firePrefab;
    public Transform launchPoint;
    public void FireCast()
    {
        GameObject fireInstance = Instantiate(firePrefab, launchPoint.position, firePrefab.transform.rotation);
        Destroy(fireInstance, 0.5f);
    }
}
