using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorCaller : MonoBehaviour
{
    HeroCombat heroCombatScript;
    // Start is called before the first frame update
    void Start()
    {
        heroCombatScript = transform.parent.GetComponent<HeroCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MeleeAttack()
    {
        heroCombatScript.MeleeAttack();
    }
}
