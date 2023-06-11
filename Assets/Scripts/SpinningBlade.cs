using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class SpinningBlade : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float waypointReachedDistance = 0.1f;
    public float rotateSpeed = 1f;

    public List<Transform> waypoints;

    Rigidbody2D rb;
    Transform nextWaypoint;

    int waypointNum = 0;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    }
    private void Update()
    {
        transform.Rotate(0, 0, -rotateSpeed);
    }



    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.velocity = directionToWaypoint * moveSpeed;
        UpdateDirection();
        if (distance <= waypointReachedDistance)
        {
            waypointNum++;

            if (waypointNum >= waypoints.Count)
            {
                waypointNum = 0;
            }

            nextWaypoint = waypoints[waypointNum];
        }
    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;

        if (transform.localScale.x > 0)
        {
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
    }

}
