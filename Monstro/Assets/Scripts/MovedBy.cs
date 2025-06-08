
using System;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;

public class MovedBy : Movement2D
{

    public float minDist = 30f;
    public Collider2D source;
    public Collider2D[] barrierX;
    public Collider2D[] barrierY;

    void HorizontalMove((int, int) pack)
    {
        (int speed, int dir) = pack;
        float dist =dir * Time.deltaTime * speed;
        print("Movendo pela velocidade e direção");
        dist = CheckMove(barrierX,dist);
        SimpleMoveX(dist);
    }
    void HorizontalMove(float dist)
    {
        print("Movendo pela distância");
        dist = CheckMove(barrierX,dist);
        SimpleMoveX(dist);
    }

    float MoveAllowed(Collider2D[] barrier)
    {
        float aux;
        if (barrier == null)
            return 0;
        foreach (Collider2D i in barrier)
        {
            aux = source.Distance(i).distance;
            if (aux < minDist)
                return aux;
        }
        return 0;
    }

    float CheckMove(Collider2D[] barrier,float dist)
    {
        float newDist, distBarrier;
        if ((distBarrier = MoveAllowed(barrier))==0f)
            return dist;
        int sign = (dist > 0) ? 1 : -1;
        newDist = sign*dist + distBarrier - minDist;
        newDist *= sign;
        //dist pode ser negativo
        //new dist pode ser sinal oposto
        return (newDist * dist > 0) ? newDist : 0;
    }
}
