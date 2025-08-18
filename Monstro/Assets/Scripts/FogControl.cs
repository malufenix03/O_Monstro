using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameSettings;


//--------------------------------------------- PORTA/PASSAGEM -------------------------------------------- 

public class FogControl : MonoBehaviour
{

    //VARIAVEIS
    public GameObject room;

    //--------------------------------------------- ABRIR E LIBERAR PROXIMO COMODO--------------------------------------------
    public void ShowAnotherRoom()
    {
        TurnFog(room, false);                                                    //desliga a fog
    }

    //--------------------------------------------- FECHAR E ESCONDER COMODO ANTERIOR --------------------------------------------
    public void HideRoom()
    {
        if (currentPlace != room)                                          //se player nao estiver no destino
        {
            TurnFog(room);                                                           //liga fog do destino, ja que a porta fechou
        }
        else                                                                                //se player chegou onde porta levava (destino)
        {
            if(GetComponent<Twin>()!=null)                                                      //se tem twin
                SendMessage("PassToTwin");                                                      //liga twin
        }


    }

    //--------------------------------------------- LIGAR/DESLIGAR FOG --------------------------------------------

    public void TurnFog(GameObject place)                                                     //ligar ou desligar a fog de algum lugar
    {
        TurnFog(place, true);
    }

    public void TurnFog(GameObject place, bool turnOn)                                                     //ligar ou desligar a fog de algum lugar
    {
        Transform fog = place.transform.Find("Structure/Fog/Temporary");                         //achar objeto fog
        if (fog != null)
            fog.GameObject().SetActive(turnOn);                                             //ligar ou desligar
        else
            print("Nao tem fog");
    }
    


}

