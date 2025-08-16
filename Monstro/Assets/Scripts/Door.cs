using System.Collections.Generic;
using UnityEngine;
using static GameSettings;


//--------------------------------------------- PORTA -------------------------------------------- 

public class Door : Object
{

    //VARIAVEIS



    //--------------------------------------------- INICIALIZACAO -------------------------------------------- 
    void Start()
    {
        Ini();

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
        SendMessage("CustomTrigger", "Open");                               //animacao porta fechando
    }


    //--------------------------------------------- DEPOIS ANIMACAO -------------------------------------------- 
    public void AfterAnimationOpen()
    {
        //RESETAR
        ResetPlayer();                    //resetar animacao player
        SendMessage("MoveAnotherRoom");
    }

    public void AfterAnimationClose()
    {
        ResetPlayer();                    //resetar animacao player
    }




}

