using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCombat : MonoBehaviour
{   
    private PlayerController playerControllerScript;

    public enum HeroAttackType {Melee, Ranged};
    public HeroAttackType heroAttackType;
    public GameObject targetEnemy;
    public float attackRange;
    
    public bool basicAtkIdle = false;
    public bool isHeroAlive;
    public bool performMeleeAttack = true;
    // Start is called before the first frame update
    
    void AutoCombat()
    {
        GameObject target = GetComponent<HeroCombat>().targetEnemy;
        float attackRange = GetComponent<HeroCombat>().attackRange;

        if (target != null)
        {
            if (Vector3.Distance(gameObject.transform.position, target.transform.position) > attackRange)
            {
                // set destination
                playerControllerScript.setDesination(target, attackRange);
            }
        }
    }

    void Start()
    {
        playerControllerScript = GetComponent<PlayerController>();
    }
    void Update()
    {
        AutoCombat();
    }
}
