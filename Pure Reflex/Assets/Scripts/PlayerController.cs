using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(HeroCombat))]

public class PlayerController : MonoBehaviour
{
    [Header("Camera")]
    public Camera cam;
    public NavMeshAgent agent;

    [Header("Movement")]
    public Vector3 targetPosition;
    public Vector3 destination;
    public bool isMoving;
    public float RotationSpeed;
    public float walkRange;

    public GameObject graphics;

    [Header("MovementView")]
    public float viewAngle;

    [Header("Statistics")]
    public bool hasRequest;

    // Scripts
    HeroCombat heroCombatScript;

    Health health;

    //----------------------------------- Basic Movement -----------------------------------\\
    void Start()
    {
        heroCombatScript = GetComponent<HeroCombat>();

        hasRequest = false;
        destination = this.transform.position;
        agent.updateRotation = false;
        health = GetComponent<Health>();
    }
    void Update()
    {
        if (!health.dead)
        {
            RequestTracker();
            Movement();
            KeyInput();

            // Raycast mousedown to terrain
            RaycastHit hit;
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Floor")
                    {   
                        agent.stoppingDistance = 0;
                        heroCombatScript.targetEnemy = null;

                        // set destination
                        hasRequest = true;
                        destination = hit.point;
                        agent.SetDestination(destination);
                    }
                }
            }
        }

        else
            agent.ResetPath();
    }

    void Movement()
    {
        // Obtain current destination in full path
        targetPosition = agent.steeringTarget;

        // Identify if agent is moving
        if (agent.velocity != Vector3.zero)
            isMoving = true;
        else
            isMoving = false;

        // Check rotation information
        CheckRotation();

        // Check if targetPosition is in view
        if (!MovementView())
        {
            gameObject.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
            agent.isStopped = true;
        }
        else
            agent.isStopped = false;
    }

    //----------------------------------- Target View -----------------------------------\\  
    Quaternion transRot;
    private void CheckRotation()
    {
        // Obtain the rotation towards the target
        if (hasRequest)
            transRot = Quaternion.LookRotation(agent.steeringTarget - this.transform.position, Vector3.up);

        // Rotate towards the target
        if (this.transform.rotation != transRot && transRot != Quaternion.Euler(Vector3.zero))
            transform.rotation = Quaternion.Slerp(transRot, this.transform.rotation, RotationSpeed);
    }

    // Calculates the an angle the player can see its new Target Position
    bool MovementView()
    {
        Vector3 dirToTarget = (targetPosition - transform.position).normalized;
        if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
        { return true; }
        else
        { return false; }
    }

    void RequestTracker()
    {
        if (hasRequest)
        {
            if (!agent.hasPath)
            {
                hasRequest = false;
            }
        }
    }

    //----------------------------------- Hotkeys -----------------------------------\\  
    void KeyInput()
    {
        // Stop Action
        if (Input.GetKeyDown(KeyCode.S))
        {
            heroCombatScript.targetEnemy = null;
            agent.ResetPath();
        }
    }

    //----------------------------------- Combat -----------------------------------\\  

    public void setDesination(GameObject target)
    {
        // set destination
        hasRequest = true;
        destination = target.transform.position;
        agent.SetDestination(destination);
    }

    public void setDesination(GameObject target, float attackRange)
    {
        // set destination
        hasRequest = true;
        destination = target.transform.position;
        agent.SetDestination(destination);
        agent.stoppingDistance = attackRange;
    }
}
