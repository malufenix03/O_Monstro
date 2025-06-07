
using System;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;

public class MovedBy : MonoBehaviour
{

    bool[] moveAllowed = { true, true, true };
    public bool MoveAllowed { get { return moveAllowed[1]; } set { moveAllowed[1] = value; } }

    bool rightAllowed = true;
    public bool RightAllowed { get { return rightAllowed; } set { rightAllowed = value; moveAllowed[2] = value; } }

    bool leftAllowed = true;
    public bool LeftAllowed { get { return leftAllowed; } set { leftAllowed = value; moveAllowed[0] = value; } }

    bool upAllowed = true;
    bool downAllowed = true;

    void HorizontalMove((int,int)pack)
    {
        (int dir, int speed) = pack;
        if (MoveAllowed && moveAllowed[dir + 1])
            transform.Translate(dir * Time.deltaTime * speed, 0, 0);
    }
    void HorizontalMove(int dist)
    {
        if (MoveAllowed)
            transform.Translate(dist, 0, 0);
    }
    
    
}
