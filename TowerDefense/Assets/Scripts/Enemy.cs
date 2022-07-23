using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;

    public float startHealth = 100;
    public float health;
    public int enemyWorth = 100;

    [Header("Unity Stuff")]
    public Image healthBar;

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }
    public void TakeDamege(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

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
