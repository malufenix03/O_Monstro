using System.Collections.Generic;
using UnityEngine;



//--------------------------------------------- PORTA -------------------------------------------- 

public class Door : Object
{

    //VARIAVEIS

    //collider
    Collider2D doorCollider;                                     //collider da porta


    //--------------------------------------------- INICIALIZACAO -------------------------------------------- 
    void Start()
    {
        Ini();
        doorCollider = GetComponent<Collider2D>();

    }

    //--------------------------------------------- ABRIR PORTA -------------------------------------------- 
    public void OnOpen()
    {
        open = true;
        interactMenu.Leave();                                               //tirar menu
        SendMessage("CustomTrigger", "Open");                               //animacao porta abrindo
    }

    public void OnClose()
    {
        open = false;
        interactMenu.Leave();                                               //tirar menu
        SendMessage("CustomTrigger", "Open");                               //animacao porta abrindo
    }


    //--------------------------------------------- DEPOIS ANIMACAO -------------------------------------------- 
    public void afterAnimationOpen()
    {
        //RESETAR
        VarGlobal.player.SendMessage("CustomTrigger", "Reset");                    //resetar animacao player
        if (doorCollider.isTrigger != true)                                           //se porta tem collider 
        {
            ChangeDoorCollider();
        }
        else
        {
            SendMessage("MoveAnotherRoom");
        }
    }

    public void afterAnimationClose()
    {
        VarGlobal.player.SendMessage("CustomTrigger", "Reset");                    //resetar animacao player
    }

    void ChangeDoorCollider()
    {
        doorCollider.enabled = !doorCollider.enabled;
    }



}

