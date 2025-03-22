using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
    
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask  targetMask, obstacleMask;
    
    public List<Transform> visibleTargets = new List<Transform>();
    
    void Start()
    {
        // 0.2초 간격으로 코루틴 호출
        StartCoroutine(FindTargetsWithDelay(0.2f));
    }
    
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    
    void FindVisibleTargets()
    {
        visibleTargets.Clear();

        // 원 반경 내의 타겟을 탐색
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        foreach (Collider2D targetCollider in targetsInViewRadius)
        {
            Transform target = targetCollider.transform;
            Vector2 dirToTarget = (target.position - transform.position).normalized;

            // 시야각 내에 있는지 확인
            if (Vector2.Angle(-transform.right, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector2.Distance(transform.position, target.position);

                // 장애물 체크 (Raycast)
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask);
                if (!hit.collider)
                {
                    visibleTargets.Add(target);
                    Debug.Log(hit.collider.gameObject.name);
                }
            }
        }
    }
    
    public Vector2 DirFromAngle(float angleDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleDegrees += transform.eulerAngles.z;
        }

        float radian = angleDegrees * Mathf.Deg2Rad;
        return new Vector2(-Mathf.Cos(radian), Mathf.Sin(radian));
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

[CustomEditor (typeof (FOV))]
public class FovEditor : Editor
{
    /*void OnSceneGUI()
    {
        FOV fow = (FOV)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        Handles.color = Color.red;
        foreach (Transform visible in fow.visibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visible.transform.position);
        }
    }*/
}
