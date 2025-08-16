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

    //flag
    public bool touchTP = false;                                //se encostar no portal, teleporta para o destino

    //auxiliares
    static GameObject auxP;
    static GameObject auxC;
    static float altura;                                    //altura camera fica da cabeca do player

    //--------------------------------------------- INICIALIZACAO -------------------------------------------- 
    void Start()
    {

        auxP = VarGlobal.player;
        auxC = auxP.GetComponent<MC>().charCamera;
        altura = auxC.transform.position.y - auxP.transform.position.y;                       //altura camera acima cabeca player
    }


    //--------------------------------------------- IR PARA O PROXIMO COMODO --------------------------------------------
    public void MoveAnotherRoom()
    {


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
            if(GetComponent<Twin>()!=null)
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
    


    void OnTriggerEnter2D(Collider2D other)                                     //se tocar no collider
    {
        if (touchTP)                                                            //se e para teleportar, faz o teleporte
        {
            print("entramo");
            VarGlobal.ResetPlayer();                                            //resetar animacao player
            SendMessage("MoveAnotherRoom");                                     //ir para o comodo destino
        }

    }


}

