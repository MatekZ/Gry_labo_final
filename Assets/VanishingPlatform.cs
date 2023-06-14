using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class VanishingPlatform : MonoBehaviour
{
    private float timer = 0f;
    public float interval = 5f;
    public Transform[] platforms;
    public int count = 3;

    public void VanishPlatform()
    {
        int rnd = Random.Range(0, (platforms.Length));

        if (platforms[rnd].gameObject.activeSelf && count > 1)
        {
            platforms[rnd].gameObject.SetActive(false);
            count--;
            
        }
        else
        {
            platforms[rnd].gameObject.SetActive(true);
            count++;
        }

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
            VanishPlatform();
            timer = 0f;
        }
    }
}
