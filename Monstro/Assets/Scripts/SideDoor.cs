using System.Collections.Generic;
using UnityEngine;



//--------------------------------------------- PORTA -------------------------------------------- 

public class SideDoor : Door
{

    //VARIAVEIS

    //collider
    Collider2D doorCollider;                                     //collider da porta


    //--------------------------------------------- FECHAR PORTA -------------------------------------------- 
    public new void OnClose()
    {
        open = false;
        interactMenu.Leave();                                               //tirar menu
        SendMessage("CustomTrigger", "Close");                              //animacao porta abrindo
    }


    //--------------------------------------------- SE CHEGA NA AREA -------------------------------------------- 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == VarGlobal.player)
        {
            OnOpen();
            print("Mensagem logo apos");
            SendMessage("ShowAnotherRoom");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == VarGlobal.player)
        {
            OnClose();
        }
    }
    
    public new void AfterAnimationClose()           //depois animacao fechar
    {
        SendMessage("HideRoom");                    //esconder onde estava apos porta fechar
    }



}

