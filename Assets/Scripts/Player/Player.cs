using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [SerializeField] private Camera camera;

    [SerializeField] private LayerMask whatIsGround;

     private float horizontal;
     private float vertical;

    private bool isRun;
    
    private Rigidbody rigid;
    private Vector3 lookPos;
    private Animator anim;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        SpeedControl();
        DirectionUpdate();
        AnimationUpdate();
    }

    private void FixedUpdate()
    {
        MoveUpdate();
    }

    private void MoveUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;

        if (moveDirection != Vector3.zero)
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }

        rigid.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);
    }
    
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rigid.velocity.x, 0f, rigid.velocity.z);
        
        // Limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rigid.velocity = new Vector3(limitedVel.x, rigid.velocity.y, limitedVel.z);
        }
    }

    private void DirectionUpdate()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, whatIsGround))
        {
            lookPos = hit.point;
        }

        Vector3 lookDir = lookPos - transform.position;

        lookDir.y = 0;
        
        transform.LookAt(transform.position + lookDir, Vector3.up);
    }
    
    private void AnimationUpdate()
    {
        anim.SetBool("isRun", isRun);
        anim.SetFloat("X", horizontal);
        anim.SetFloat("Y", vertical);
    }
}