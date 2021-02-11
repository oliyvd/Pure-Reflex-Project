using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTargeting : MonoBehaviour
{
    public GameObject selectedHero;
    public bool heroPlayer;
    RaycastHit hit;

    void Start()
    {  
        // Find player gameobject on Parent
        selectedHero = transform.parent.Find("Player").gameObject;
    }

    void Update()
    {
        // Player Targeting
        if (Input.GetMouseButtonDown(1))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {   
                // IF the player is targetable
                if (hit.collider.GetComponent<Targetable>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Player)
                    {
                        selectedHero.GetComponent<HeroCombat>().targetEnemy = hit.collider.gameObject;
                        Debug.Log("Enemy Located at: " + hit.collider.transform.position);
                    }

                    else if(hit.collider.GetComponent<Targetable>() == null)
                    {
                        selectedHero.GetComponent<HeroCombat>().targetEnemy = null;
                    
                    }
                }
            }
        }
    }
}
