using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    public int health = 100;
    public int value = 100;


    private Transform target;
    private int wavepointIndex = 0;

    void Start()
    { 
        target = Waypoints.points[0];
    }
    public void TakeDamege(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        PlayerStats.Money += value;
        Destroy(gameObject);
    }
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized*speed*Time.deltaTime,Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.1f) 
        {
            GetNextWavePoint();
            
        }
    }

    void GetNextWavePoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives --;
        Destroy(gameObject);
    }
}