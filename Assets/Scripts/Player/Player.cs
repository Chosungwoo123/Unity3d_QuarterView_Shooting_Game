using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [SerializeField] private Camera camera;

    [SerializeField] private LayerMask whatIsGround;
    
    private Rigidbody rigid;
    private Vector3 lookPos;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        SpeedControl();
        DirectionUpdate();
    }

    private void FixedUpdate()
    {
        MoveUpdate();
    }

    private void MoveUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movePos = new Vector3(horizontal, 0, vertical);

        rigid.AddForce(movePos.normalized * moveSpeed * 10, ForceMode.Force);
        
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
}