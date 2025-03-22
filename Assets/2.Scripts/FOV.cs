using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask  targetMask;
    
    public List<Transform> visibleTargets = new List<Transform>();
    public Transform target { get; set; }
    
    void Start()
    {
        StartCoroutine(FindTargetsWithDelay(0.05f));
    }
    
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            var target =  FindVisibleTargets();
            if (target != null)
            {
                Vector2 newPos = target.transform.position - transform.position;
                float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotZ);
                
                this.target = target;
            }
        }
    }
    
    private Transform FindVisibleTargets()
    {
        visibleTargets.Clear();
        
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);
        float minDistance = float.MaxValue;
        Transform closestTarget = null;
        
        foreach (Collider2D targetCollider in targetsInViewRadius)
        {
            Transform target = targetCollider.transform;
            Vector2 dirToTarget = (target.position - transform.position).normalized;
            
            if (Vector2.Angle(transform.right, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector2.Distance(transform.position, target.position);
                
                if (minDistance > dstToTarget)
                {
                    closestTarget = target;
                }
            }
        }

        return closestTarget;
    }
    
    public Vector2 DirFromAngle(float angleDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleDegrees += transform.eulerAngles.z;
        }

        float radian = angleDegrees * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector2 viewAngleA = DirFromAngle(-viewAngle / 2, false);
        Vector2 viewAngleB = DirFromAngle(viewAngle / 2, false);

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + viewAngleB * viewRadius);

        Gizmos.color = Color.red;
        foreach (Transform visible in visibleTargets)
        {
            Gizmos.DrawLine(transform.position, visible.position);
        }
    }
}
