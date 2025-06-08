
using System;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    protected Rigidbody2D rig;

    protected void Ini()
    {
        rig = GetComponent<Rigidbody2D>();
        if (rig != null)
        {
            rig.freezeRotation = true;
        }
    }
    protected void RigidBodyMoveX(float dist)
    {
   
        rig.linearVelocityX = dist;
    }
    protected void RigidBodyMoveX((int, int) pack)
    {
        (int speed, int dir) = pack;
        rig.linearVelocityX = speed * dir *Time.fixedDeltaTime;
    }
    protected void SimpleMoveX((int, int) pack)
    {
        (int speed, int dir) = pack;
        transform.Translate(dir * Time.deltaTime * speed, 0, 0);
    }
    protected void SimpleMoveX(float dist)
    {
        transform.Translate(dist, 0, 0);
    }

    void Start()
    {
        Ini();
    }

}
