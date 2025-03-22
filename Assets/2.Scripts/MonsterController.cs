using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private FOV fov;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    
    [SerializeField] private float yThreshold;
    
    [SerializeField] private GroundChecker groundChecker;

    private Rigidbody2D rb;
    
    public GroundChecker GroundChecker => groundChecker;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveSpeed * ( Vector2.left), ForceMode2D.Force);
        if (rb.velocity.x > moveSpeed)
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    public void JumpOnBack()
    {
        rb.AddForce(jumpForce * (Vector2.up), ForceMode2D.Impulse);
    }
}
