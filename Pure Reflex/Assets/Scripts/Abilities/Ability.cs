using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public abstract class Ability
{   
    [Header("Icons")]
    public Image abilityIcon;
    public Image abilityIconCD;

    [Header("Ability Cooldown")]
    public float cooldownTime;
    [SerializeField]
    private float cooldownCurrentTimer = 0;
    [Header("Key")]
    public KeyCode useButton;

    public abstract void AbilityCode();

    void Use()
    {   
        CooldownManager();
        bool abUse = Input.GetKeyDown(useButton);

        if (cooldownCurrentTimer <= 0)
        {
            if (abUse)
                cooldownCurrentTimer = cooldownTime;
        }
    }

    // Process ability cooldown
    private void CooldownManager()
    {
        abilityIconCD.fillAmount = cooldownCurrentTimer / 100 * cooldownTime;
        if (cooldownCurrentTimer > 0)
            cooldownCurrentTimer -= Time.deltaTime;
        else if (cooldownCurrentTimer < 0)
            cooldownCurrentTimer = 0;
    }

}
