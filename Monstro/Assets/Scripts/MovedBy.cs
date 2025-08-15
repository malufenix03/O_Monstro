
using UnityEngine;


//--------------------------------------------- MOVIMENTAÇÃO LIGADA A OUTRO OBJETO -------------------------------------------- //

public class MovedBy : Movement2D
{
    //VARIAVEIS
    

    //distancia parede camera
    public float minDistX = 30f;
    public float minDistY = 30f;

    //fonte do movimento
    public Collider2D source;

    //barreira
    public Collider2D[] barrierX;
    public Collider2D[] barrierY;

    //auxiliares
    bool outBound = false;

    //MOVIMENTO HORIZONTAL ------------------------------------------

    void HorizontalMove((int, float) pack)
    {
        (int speed, float dir) = pack;
        float dist = dir * Time.deltaTime * speed;          //distância outro objeto foi movido
        //print("Movendo pela velocidade e direção");
        dist = CheckMove(barrierX, dist,minDistX);                   //calcular quanto vai movimentar objeto
        SimpleMoveX(dist);
    }
    void HorizontalMove(float dist)
    {
        //print("Movendo pela distância");
        dist = CheckMove(barrierX, dist,minDistX);                   //calcular quanto vai movimentar objeto
        SimpleMoveX(dist);
    }

//MOVIMENTO Vertical ------------------------------------------
/*
    void VerticalMove((int, float) pack)
    {
        (int speed, float dir) = pack;
        float dist = dir * Time.deltaTime * speed;          //distância outro objeto foi movido
        //print("Movendo pela velocidade e direção");
        dist = CheckMove(barrierX, dist,minDistY);                   //calcular quanto vai movimentar objeto
        SimpleMoveX(dist);
    }
    void VerticalMove(float dist)
    {
        //print("Movendo pela distância");
        dist = CheckMove(barrierX, dist,minDistY);
        SimpleMoveX(dist);
    }
*/


    //PROCURA A DISTÂNCIA DE UMA BARREIRA MENOR QUE DISTÃNCIA MÍNIMA ------------------------------------------
    float MoveAllowed(Collider2D[] barrier,float minDist)
    {
        float aux;
        if (barrier == null)                                            //se nao tem barreira
            return 0;
        Vector3 pos = transform.position;                               //posicao objeto antes
        foreach (Collider2D i in barrier)
        {
            ColliderDistance2D a = source.Distance(i);                  
            aux = source.Distance(i).distance;
            if (aux < minDist)                                          //se objeto distancia menor que minima da barreira
            {
                outBound = true;                                        //esta alem da distancia minima
//                print(pos);
                Vector2 thisDist =(Vector2)pos - i.ClosestPoint(pos);   //distancia esse objeto antes e barreira
                if (Mathf.Abs(thisDist.x) < minDist)               //se ja estava na menor distancia
//                    print("camera a " + thisDist.x);
                    return 0;                                           //mover mesma quantidade que fonte
                return aux;
            }
                
        }
        
        return 0;                                                      //se nenhuma menor que minimo
    }

    //CALCULA MOVIMENTO DO OBJETO ------------------------------------------
    float CheckMove(Collider2D[] barrier, float dist,float minDist)
    {
        float newDist, distBarrier;
        if ((distBarrier = MoveAllowed(barrier, minDist)) == 0f)         //0 = nao tem barreira ou fonte esta mais distante que distancia minima de todas as barreiras
        {
            if (outBound == true)                                         //se moveu saindo de alem da distancia minima
            {
                print("Saiu\nCamera: " + transform.position.x + " Pablo: " + source.transform.position.x);
                outBound = false;                                           //nao esta alem da distancia minima
                return source.transform.position.x - transform.position.x ;               //objeto move distancia que estava da borda
            } 
            outBound = false;
            return dist; 
        }
                                                           //move mesmo tanto que fonte
        int sign = (dist > 0) ? 1 : -1;                                 //se move para direita, sentido positivo, se move para esquerda negativo
        newDist = sign * dist + distBarrier - minDist;                  //( quanto fonte se moveu + distancia fonte da parede ) - distancia minima
        newDist *= sign;                                                //deixar no mesmo sentido
        //dist pode ser negativo
        //new dist pode ser sinal oposto
        return (newDist * dist > 0) ? newDist : 0;
    }
}
