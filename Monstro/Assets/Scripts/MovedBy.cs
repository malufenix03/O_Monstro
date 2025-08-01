
using UnityEngine;


//--------------------------------------------- MOVIMENTAÇÃO LIGADA A OUTRO OBJETO -------------------------------------------- //

public class MovedBy : Movement2D
{
    //VARIAVEIS
    
    public float minDist = 30f;
    public Collider2D source;
    public Collider2D[] barrierX;
    public Collider2D[] barrierY;

    //MOVIMENTO HORIZONTAL ------------------------------------------

    void HorizontalMove((int, int) pack)
    {
        (int speed, int dir) = pack;
        float dist = dir * Time.deltaTime * speed;          //distância outro objeto foi movido
        //print("Movendo pela velocidade e direção");
        dist = CheckMove(barrierX, dist);                   //calcular quanto vai movimentar objeto
        SimpleMoveX(dist);
    }
    void HorizontalMove(float dist)
    {
        //print("Movendo pela distância");
        dist = CheckMove(barrierX, dist);
        SimpleMoveX(dist);
    }


    //PROCURA A DISTÂNCIA DE UMA BARREIRA MENOR QUE DISTÃNCIA MÍNIMA ------------------------------------------
    float MoveAllowed(Collider2D[] barrier)
    {
        float aux;
        if (barrier == null)
            return 0;
        foreach (Collider2D i in barrier)
        {
            ColliderDistance2D a = source.Distance(i);
            aux = source.Distance(i).distance;
            print("Distância entre objeto e barreira: " + aux);
            if (aux < minDist)
                return aux;
        }
        return 0;
    }

    //CALCULA MOVIMENTO DO OBJETO ------------------------------------------
    float CheckMove(Collider2D[] barrier, float dist)
    {
        float newDist, distBarrier;
        if ((distBarrier = MoveAllowed(barrier)) == 0f)
            return dist;
        int sign = (dist > 0) ? 1 : -1;
        newDist = sign * dist + distBarrier - minDist;
        newDist *= sign;
        //dist pode ser negativo
        //new dist pode ser sinal oposto
        return (newDist * dist > 0) ? newDist : 0;
    }
}
