using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform lauchPoint;

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, lauchPoint.position, projectilePrefab.transform.rotation);

        Vector3 origScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(origScale.x * transform.localScale.x > 0 ? 1 : -1, origScale.y, origScale.z);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
