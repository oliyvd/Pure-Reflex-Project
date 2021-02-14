using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class managing the players health
/// </summary>
public class Health : MonoBehaviour
{   
    public int health;
    public bool dead;
    
    void Start()
    {
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
            dead = true;
        else
            dead = false;
    }
}
