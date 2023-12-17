using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected float maxHealth = 100;
    protected float health = 0;
    protected float HealthPercent => Mathf.InverseLerp(0, maxHealth, health);
    protected bool isDead = false;

    protected virtual void Start(){
        health = maxHealth;
        UpdateHealth();
    }
    public virtual void AddDamage(float damage)
    {
        health -= damage;
        UpdateHealth();
        if (health <= 0) Die();
    }
    protected virtual void Die(){
        if(isDead) return;
        isDead = true;
    }

    protected virtual void UpdateHealth(){}

}
