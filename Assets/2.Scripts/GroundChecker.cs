using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public LayerMask layerMask;
    
    public bool IsGrounded { get; private set; }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(layerMask == (layerMask | (1 << other.gameObject.layer)))
        {
            Debug.Log(other.gameObject.name + " Triggered");
            IsGrounded = true;
        }
    }
    
    
}
