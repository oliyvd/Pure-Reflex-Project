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
        Selected();
        Hover();
    }
    void Selected()
    {
        // Player Targeting
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                // IF the player is targetable
                if (hit.collider.GetComponent<Targetable>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Player)
                    {
                        selectedHero.GetComponent<HeroCombat>().targetEnemy = hit.collider.gameObject;
                        Debug.Log("Enemy Located at: " + hit.collider.transform.position);

                    }

                    else if (hit.collider.GetComponent<Targetable>() == null)
                    {
                        selectedHero.GetComponent<HeroCombat>().targetEnemy = null;

                    }
                }
            }
        }
    }
    private bool hitting = false;
    private GameObject hitObject;
    void Hover()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {

            GameObject go = hit.transform.gameObject;

            // IF the player is targetable
            if (hit.collider.GetComponent<Targetable>() != null)
            {
                // If no registred hitobject => Entering
                if (hitObject == null)
                {
                    go.SendMessage("OnHitEnter", selectedHero);
                }
                // If hit object is the same as the registered one => Stay
                else if (hitObject.GetInstanceID() == go.GetInstanceID())
                {
                    //hitObject.SendMessage( "OnHitStay" );
                }
                // If new object hit => Exit last + Enter new
                else
                {
                    hitObject.SendMessage("OnHitExit");
                    //go.SendMessage("OnHitEnter");
                }

                hitting = true;
                hitObject = go;
            }

            // No object hit => Exit last one
            else if (hitting)
            {
                hitObject.SendMessage("OnHitExit");
                hitting = false;
                hitObject = null;
            }
            //hit.transform.SendMessage("Hovering");

        }

    }
}