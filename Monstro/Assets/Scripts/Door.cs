using System.Collections.Generic;
using UnityEngine;



//--------------------------------------------- PORTA -------------------------------------------- 

public class Door : Object
{

    //VARIAVEIS

    //collider
    Collider2D doorCollider;                                     //collider da porta


    //para onde leva
    public GameObject destination;                              //para onde a porta leva
    public Vector3 newPosition;                               //posicao personagem deve estar


    //--------------------------------------------- INICIALIZACAO -------------------------------------------- 
    void Start()
    {
        Ini();
        doorCollider = GetComponent<Collider2D>();

    }

    //--------------------------------------------- ABRIR PORTA -------------------------------------------- 
    public void OnOpen()
    {
        SendMessage("CustomTrigger", "Open");                               //animacao porta abrindo
    }

    public void afterAnimation()
    {
        if (doorCollider.isTrigger != true)                                           //se porta tem
        {
            ChangeDoorCollider();
        }
        else
        {
            moveAnotherRoom();
        }
    }

    void ChangeDoorCollider()
    {
        doorCollider.enabled = !doorCollider.enabled;
    }

    void moveAnotherRoom()
    {
        GameObject auxP = VarGlobal.player;
        GameObject auxC = auxP.GetComponent<MC>().charCamera;
        float altura = auxC.transform.position.y - auxP.transform.position.y;
        auxP.SendMessage("MoveTo", newPosition);                                                    //teleporta player para lugar novo
        auxC.SendMessage("Teleport", new Vector3(newPosition.x, newPosition.y + altura,-10));      //teleporta camera player para lugar novo, tem que estar profundidade -10 para pegar tudo
        changePlace();
        auxP.SendMessage("CustomTrigger", "Reset");
    }

    //--------------------------------------------- IR PARA O PROXIMO COMODO -------------------------------------------- 
    void changePlace()
    {
        VarGlobal.currentPlace = destination;
        VarGlobal.OnChangeScene();
        interactMenu.Leave();
    }


}

