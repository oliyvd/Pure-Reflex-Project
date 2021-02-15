using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Statistics))]
[RequireComponent(typeof(HeroCombat))]

/// <summary>
/// A class used for managing and controlling player movement
/// </summary>
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
    
    [SerializeField]
    private float viewAngle;

    [Header("Statistics")]
    public bool hasRequest;

    // Scripts
    HeroCombat heroCombatScript;
    Statistics statisicsScript;
    FieldOfView fieldScript;

    void Start()
    {
        fieldScript = GetComponent<FieldOfView>();
        heroCombatScript = GetComponent<HeroCombat>();

        hasRequest = false;
        destination = this.transform.position;
        agent.updateRotation = false;
        statisicsScript = GetComponent<Statistics>();
    }

    //----------------------------------- Basic Movement -----------------------------------\\

    void Update()
    {   
        viewAngle = fieldScript.viewAngle;
        // If player has health
        if (!statisicsScript.dead)
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

    /// <summary>
    /// Controls player movement (including rotation and View Angle)
    /// </summary>
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

    Quaternion targetRotation;
    /// <summary>
    /// Rotates the play towards the target Position
    /// </summary>
    private void CheckRotation()
    {
        // Obtain the rotation towards the target
        if (hasRequest)
            targetRotation = Quaternion.LookRotation(agent.steeringTarget - this.transform.position, Vector3.up);

        // Rotate towards the target
        if (this.transform.rotation != targetRotation && targetRotation != Quaternion.Euler(Vector3.zero))
            transform.rotation = Quaternion.Slerp(targetRotation, this.transform.rotation, RotationSpeed);
    }

    public void CheckRotation(GameObject target)
    {
        // Obtain the rotation towards the target
            targetRotation = Quaternion.LookRotation(target.transform.position - this.transform.position, Vector3.up);

        // Rotate towards the target
            transform.rotation = Quaternion.Slerp(targetRotation, this.transform.rotation, RotationSpeed);
    }

    /// <summary>
    /// Calculates the an angle the player can see its new Target Position
    /// </summary>
    /// <returns>TRUE if in angle<br/>FALSE is not in angle</returns>
    bool MovementView()
    {
        Vector3 dirToTarget = (targetPosition - transform.position).normalized;
        if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
        { return true; }
        else
        { return false; }
    }

    public bool MovementView(GameObject target)
    {
        Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
        { return true; }
        else
        { return false; }
    }

    /// <summary>
    /// Checks if the navMesh as a request
    /// </summary>
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

    /// <summary>
    /// A player input method 
    /// </summary>
    void KeyInput()
    {
        // Stop Action
        if (Input.GetKeyDown(KeyCode.S))
        {
            heroCombatScript.targetEnemy = null;
            agent.ResetPath();
        }
    }

    //----------------------------------- Public Methods -----------------------------------\\  

    /// <summary>
    /// [Overloaded Method] Sets destination to target WITHOUT a stopping distance(attackRange)
    /// </summary>
    /// <param name="target">The navagent target</param>
    public void setDesination(GameObject target)
    {
        // set destination
        hasRequest = true;
        destination = target.transform.position;
        agent.SetDestination(destination);
    }
    
    /// <summary>
    /// [Overloaded Method] Sets destination to target WITH a stopping distance(attackRange)
    /// </summary>
    /// <param name="target">The navAgent target</param>
    /// <param name="attackRange">The navAgent stop distance</param>
    public void setDesination(GameObject target, float attackRange)
    {
        // set destination
        hasRequest = true;
        destination = target.transform.position;
        agent.SetDestination(destination);
        agent.stoppingDistance = attackRange;
    }
}
