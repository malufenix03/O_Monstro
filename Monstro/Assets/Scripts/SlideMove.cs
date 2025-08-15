using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideMove : MonoBehaviour
{
    public GameObject Target;
    public float speed = 10;
    public bool autoDestruction = false;


    void FixedUpdate()
    {
        if (transform.position.x != Target.transform.position.x)
            SendMessage("SlideX", (speed, Target.transform.position.x));
        else
        {
            if (autoDestruction)
                Destroy(this);
        }
            
    }

}
