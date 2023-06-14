using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLaunch : MonoBehaviour
{
    public GameObject projectilePrefab;
    private float timer = 0f;
    public float interval = 5f;
    public Transform[] arrowlLaunchPoint;

    public void FireProjectile()
    {

        int rnd = Random.Range(0, (arrowlLaunchPoint.Length));
        GameObject projectile = Instantiate(projectilePrefab, arrowlLaunchPoint[rnd].position, projectilePrefab.transform.rotation);
        Vector3 origScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(origScale.x * transform.localScale.x > 0 ? 1 : -1, origScale.y, origScale.z);

    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            FireProjectile();
            timer = 0f;
        }
    }
}
