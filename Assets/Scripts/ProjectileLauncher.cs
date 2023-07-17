using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;

    public void FireProjectile()
    {
        Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);
    }
}
