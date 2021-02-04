using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public bool dead;
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        DeathHandler();
    }

    // Public method to inflict damage
    public void DealDamage(int amount)
    {
        if (!dead)
            health -= amount;
    }

    // Public method to heal
    public void heal(int amount)
    {   
        if (!dead)
        health += amount; 
    }

    void DeathHandler()
    {
        if (health <= 0)
            dead = true;
        else
            dead = false;
    }
}
