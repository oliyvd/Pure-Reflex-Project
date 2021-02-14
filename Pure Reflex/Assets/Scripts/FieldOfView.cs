using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <Author> Sebastian Lague </Author>
/// <URL> https://www.youtube.com/watch?v=rQG9aUWarwE&t </URL>
/// </summary>
public class FieldOfView : MonoBehaviour
{
    // Radius Around player
    public float viewRadius;
    [Range(0,360)]
    // Angle in radius the player can see
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    // A list of targets visible to the player
    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();
    
    void Start()
    {   
        StartCoroutine("FindTargetsWithDelay", .2f);
    }
    
    /// <summary>
    /// Finds targets within view angle 
    /// </summary>
    /// <param name="delay">The delay between finds</param>
    /// <returns></returns>
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds (delay);
            FindVisibleTargets();
            }
    }

    /// <summary>
    /// Finds visible players 
    /// </summary>
    void FindVisibleTargets() 
    {   
        // Clear current finds
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        // Loop for each target collided in view radius
        for (int i = 0; i < targetsInViewRadius.Length; i++) 
        {
            // Add gameObject to array
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            
            // If gameObject is in viewAngle
            if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2) 
            {
                float dstToTarget = Vector3.Distance (transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    // REWATCH VIDEO FOR PROPER EXPLANATION 
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) 
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
