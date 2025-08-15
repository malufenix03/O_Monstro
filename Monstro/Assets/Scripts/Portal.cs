using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



//--------------------------------------------- PORTA/PASSAGEM -------------------------------------------- 

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

    //--------------------------------------------- MUDAR NAS CONFIGURACOES DO JOGO --------------------------------------------
    void ChangePlace()
    {
        VarGlobal.currentPlace = destination;
        VarGlobal.OnChangeScene();
    }

    //--------------------------------------------- ABRIR E LIBERAR PROXIMO COMODO--------------------------------------------
    public void ShowAnotherRoom()
    {
        print("teste");
        TurnFog(destination, false);                                                    //desliga a fog
    }

    //--------------------------------------------- FECHAR E ESCONDER COMODO ANTERIOR --------------------------------------------
    public void HideRoom()
    {
        if (VarGlobal.currentPlace != destination)                                          //se player nao estiver no destino
        {
            print("teste");
            TurnFog(destination);                                                           //liga fog do destino, ja que a porta fechou
        }
        else                                                                                //se player chegou onde porta levava (destino)
        {
            SendMessage("PassToTwin");                                                      //liga twin
        }


    }

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

