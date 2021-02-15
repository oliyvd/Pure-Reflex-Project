using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that controls the players Combat
/// </summary>
public class HeroCombat : MonoBehaviour
{
    // Scripts
    PlayerController playerControllerScript;
    Statistics statisticsScript;
    public Animator animator;

    public enum HeroAttackType { Melee, Ranged };
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

            else
            {   
                // set destination
                playerControllerScript.setDesination(target, attackRange);
                    if (heroAttackType == HeroAttackType.Melee)
                        StartCoroutine("MeleeAttackInterval");


            }
        }
    }

    void Start()
    {
        playerControllerScript = GetComponent<PlayerController>();
        statisticsScript = GetComponent<Statistics>();
        //animator = GetComponent<Animator>();
    }
    void Update()
    {
        AutoCombat();
    }

    IEnumerator MeleeAttackInterval()
    {
        performMeleeAttack = false;
        animator.SetBool("Basic Attack", true);

        yield return new WaitForSeconds(statisticsScript.attackTime / ((100 + statisticsScript.attackTime) * 0.01f));

        if (targetEnemy == null)
        {
            animator.SetBool("Basic Attack", false);
            performMeleeAttack = true;
        }
    }

    public void MeleeAttack()
    {
        if (targetEnemy != null)
        {
            if (targetEnemy.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Player)
            {
                Debug.Log("Dealt " + statisticsScript.attackDmg + " to " + targetEnemy.name);
                targetEnemy.GetComponent<Statistics>().DealDamage(statisticsScript.attackDmg);
            }
        }

        performMeleeAttack = true;
    }
}
