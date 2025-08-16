using System.Collections.Generic;
using UnityEngine;
using static GameSettings;


//--------------------------------------------- PORTA -------------------------------------------- 

public class SideDoor : Door
{

    //VARIAVEIS


    //--------------------------------------------- FECHAR PORTA -------------------------------------------- 
    public new void OnClose()
    {
        open = false;
        interactMenu.Leave();                                               //tirar menu
        SendMessage("CustomTrigger", "Close");                              //animacao porta fechando
    }


    //--------------------------------------------- SE CHEGA NA AREA -------------------------------------------- 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            OnOpen();
            print("Mensagem logo apos");
            SendMessage("ShowAnotherRoom");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            OnClose();
        }
    }
    
    public new void AfterAnimationClose()           //depois animacao fechar
    {
        SendMessage("HideRoom");                    //esconder onde estava apos porta fechar
    }



}

