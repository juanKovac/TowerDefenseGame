using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;

    public float health = 100;
    public int enemyWorth = 100;

    void Start()
    {
        speed = startSpeed;
    }
    public void TakeDamege(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Slow(float slowPct)
    {
        speed = startSpeed * (1f - slowPct);
    }

    void Die()
    {
        PlayerStats.Money += enemyWorth;
        Destroy(gameObject);
    }


}
