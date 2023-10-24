using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float followSpeed;

    [SerializeField] private Transform target;
    
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        Vector3 cameraPosition = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, cameraPosition, followSpeed * Time.deltaTime);
    }
}