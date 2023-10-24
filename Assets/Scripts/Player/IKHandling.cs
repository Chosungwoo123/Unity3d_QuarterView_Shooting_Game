using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class IKHandling : MonoBehaviour
{
    private Animator anim;

    public float LeftHandWeight = 1;
    public Transform LeftHandTarget;

    public float RightHandWeight = 1;
    public Transform RightHandTarget;

    public Transform waepon;
    public Vector3 lookPos;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, LeftHandWeight);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandTarget.position);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, LeftHandWeight);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandTarget.rotation);
        
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, RightHandWeight);
        anim.SetIKPosition(AvatarIKGoal.RightHand, RightHandTarget.position);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, RightHandWeight);
        anim.SetIKRotation(AvatarIKGoal.RightHand, RightHandTarget.rotation);
    }
}