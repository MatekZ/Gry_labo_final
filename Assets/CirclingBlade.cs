using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclingBlade : MonoBehaviour
{
    public float speed = 5f;
    public float radius = 5f;

    private float angle = 0f;
    public float rotateSpeed = 1f;
    public Transform centerObject;


    private void Update()
    {
        float x = centerObject.position.x + Mathf.Cos(angle) * radius;
        float y = centerObject.position.y + Mathf.Sin(angle) * radius;

        transform.position = new Vector3(x, y, transform.position.z);

        angle += speed * Time.deltaTime;

        if (angle >= Mathf.PI * 2)
        {
            angle = 0f;
        }
    }
}

