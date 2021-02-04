using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TEST
{
    public Image abilityIcon;
    public Image abilityIconCD;
    public float cooldownTimer = 0;
    public float cooldownTime;

    public KeyCode useButton;

    void CooldownManager()
    {
        abilityIconCD.fillAmount = cooldownTimer / 100 * cooldownTime;
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
        else if (cooldownTimer < 0)
            cooldownTimer = 0;
    }

    void Ability()
    {   
        CooldownManager();
        bool abUse = Input.GetKeyDown(useButton);

        if (cooldownTimer <= 0)
        {
            if (abUse)
                cooldownTimer = cooldownTime;
        }
    }
}
