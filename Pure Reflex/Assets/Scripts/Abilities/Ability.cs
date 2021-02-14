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

    /// <summary>
    /// Abstract method for customized ability code
    /// /// </summary>
    public abstract void AbilityCode();

    /// <summary>
    /// Ability Method for activating the ability
    /// </summary>
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

    /// <summary>
    /// Manage ability cooldown
    /// </summary>
    private void CooldownManager()
    {
        abilityIconCD.fillAmount = cooldownCurrentTimer / 100 * cooldownTime;
        if (cooldownCurrentTimer > 0)
            cooldownCurrentTimer -= Time.deltaTime;
        else if (cooldownCurrentTimer < 0)
            cooldownCurrentTimer = 0;
    }

}
