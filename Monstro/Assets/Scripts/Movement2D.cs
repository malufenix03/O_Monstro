
using System;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;

public class Movement2D : MonoBehaviour
{

//VARIÁVEIS

    protected Rigidbody2D rig;


    
//MOVIMENTO HORIZONTAL COM FÍSICA -----------------------------------------------------------------------------------------

    protected void RigidBodyMoveX(float dist)
    {

        rig.linearVelocityX = dist;
    }
    protected void RigidBodyMoveX((int, int) pack)
    {
        (int speed, int dir) = pack;
        rig.linearVelocityX = speed * dir *Time.fixedDeltaTime;
    }

//MOVIMENTO HORIZONTAL SEM FÍSICA -----------------------------------------------------------------------------------------

    protected void SimpleMoveX((int, int) pack)
    {
        (int speed, int dir) = pack;
        transform.Translate(dir * Time.deltaTime * speed, 0, 0);
    }
    protected void SimpleMoveX(float dist)
    {
        transform.Translate(dist, 0, 0);
    }


//INICIALIZAÇÃO -----------------------------------------------------------------------------------------------------------

    protected void GetRig()
    {
        rig = GetComponent<Rigidbody2D>();
        if (rig != null)
        {
            rig.freezeRotation = true;
        }
    }


    void Start()
    {
        GetRig();
    }

}
