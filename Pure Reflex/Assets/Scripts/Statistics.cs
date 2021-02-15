using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class managing the players health
/// </summary>
public class Statistics : MonoBehaviour
{   
    [Header("Statistics")]
    public float maxHealth;
    public int health;
    public int attackDmg;
    public float attackSpeed;
    public float attackTime;
    public bool dead;

    // Scripts

    HeroCombat heroCombatScript;
    
    void Start()
    {
        heroCombatScript = GetComponent<HeroCombat>();
        dead = false;
    }

    void Update()
    {
        DeathHandler();
    }

    /// <summary>
    /// Public method to inflict damage
    /// </summary>
    /// <param name="amount">Amount to damage</param>
    public void DealDamage(int amount)
    {
        if (!dead)
            health -= amount;
    }

    /// <summary>
    /// Public method to heal 
    /// </summary>
    /// <param name="amount">Amount to heal</param>
    public void heal(int amount)
    {   
        if (!dead)
        health += amount; 
    }

    /// <summary>
    /// Handles if the player is dead
    /// </summary>
    void DeathHandler()
    {
        if (health <= 0)
        {
            dead = true;

            try 
            {
                heroCombatScript.targetEnemy = null;
                heroCombatScript.performMeleeAttack = false;
            }
            catch
            {}
        }
        else
            dead = false;
    }
}
