using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Statistics))]
public class PlayerAnimation : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public Statistics statisticsScript;

    // transition time between animations
    public float motionSmoothTime = .1f;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        statisticsScript = gameObject.GetComponent<Statistics>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Dead", statisticsScript.dead);
        movement();
    }

    void movement()
    {
        // Calulate speed to a scale of 0-1
        float speed = agent.velocity.magnitude / agent.speed;
        // Set animator variable to speed variable
        animator.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);
    }
}
