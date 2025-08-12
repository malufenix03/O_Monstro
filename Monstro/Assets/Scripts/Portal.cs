using System.Collections.Generic;
using UnityEngine;



//--------------------------------------------- PORTA -------------------------------------------- 

public class Portal : MonoBehaviour
{

    //VARIAVEIS


    //para onde leva
    public GameObject destination;                              //para onde a portal leva
    public Vector3 newPosition;                               //posicao personagem deve estar


    //--------------------------------------------- INICIALIZACAO -------------------------------------------- 
    void Start()
    {


    }


    //--------------------------------------------- IR PARA O PROXIMO COMODO --------------------------------------------
    public void MoveAnotherRoom()
    {
        GameObject auxP = VarGlobal.player;
        GameObject auxC = auxP.GetComponent<MC>().charCamera;
        float altura = auxC.transform.position.y - auxP.transform.position.y;                       //altura camera acima cabeca player
        auxP.SendMessage("MoveTo", newPosition);                                                    //teleporta player para lugar novo
        auxC.SendMessage("Teleport", new Vector3(newPosition.x, newPosition.y + altura, -10));      //teleporta camera player para lugar novo, tem que estar profundidade -10 para pegar tudo
        ChangePlace();
        
    }

     
    void ChangePlace()
    {
        VarGlobal.currentPlace = destination;
        VarGlobal.OnChangeScene();
    }


}

