using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityOLD : MonoBehaviour
{
    //public GameObject cube;
    public Image abilityIcon;
    public Image abilityIconCD;
    public float cooldownTimer = 0;
    public float cooldownTime;

    void Update()
    {
        abilityIconCD.fillAmount = cooldownTimer / 100 * cooldownTime;
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
        else if (cooldownTimer < 0)
            cooldownTimer = 0;

        AbilityUse();
    }

    void AbilityUse()
    {   
        bool abUse = Input.GetKeyDown(KeyCode.Q);

        if (cooldownTimer <= 0)
        {
            if (abUse)
                cooldownTimer = cooldownTime;
        }
    }
}
