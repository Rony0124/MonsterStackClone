using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public Vector2 viewOffset;
    public Vector2 viewSize;

    public LayerMask  targetMask;
    
    public Transform target { get; set; }
    
    public Vector2 targetDir => target != null ? (target.position - transform.position).normalized : Vector2.zero;
    
    void Start()
    {
        StartCoroutine(FindTargetsWithDelay(0.05f));
    }
    
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            var closestTarget =  FindVisibleTargets();
            if (closestTarget != null)
            {
                target = closestTarget;
            }

            if (GameManager.Instance.IsGameOver)
            {
                target = null;
                break;
            }
               
        }
    }
    
    private Transform FindVisibleTargets()
    {
        Vector2 viewPos = Vector2.right * (transform.position.x + viewOffset.x) + Vector2.up * (transform.position.y + viewOffset.y);
        Collider2D[] targetsInView = Physics2D.OverlapBoxAll(viewPos, viewSize,0, targetMask);
        float minDistance = float.MaxValue;
        Transform closestTarget = null;
        
        foreach (Collider2D targetCollider in targetsInView)
        {
            Transform target = targetCollider.transform;
            float dstToTarget = Vector2.Distance(transform.position, target.position);
                
            if (minDistance > dstToTarget)
            {
                closestTarget = target;
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
        Vector2 viewPos = Vector2.right * (transform.position.x + viewOffset.x) + Vector2.up * (transform.position.y + viewOffset.y);
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(viewPos, viewSize);

        Gizmos.color = Color.red;
        
        if(target != null)
            Gizmos.DrawLine(transform.position, target.position);
    }
}
