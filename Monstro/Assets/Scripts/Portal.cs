using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameSettings;


//--------------------------------------------- PORTA/PASSAGEM -------------------------------------------- 

public class Portal : MonoBehaviour
{

    //VARIAVEIS


    //para onde leva
    public GameObject destination;                              //para onde a portal leva
    public Vector3 newPosition;                               //posicao personagem deve estar

    //flag
    public bool touchTP = false;                                //se encostar no portal, teleporta para o destino
    public bool anotherScene = false;

    //auxiliares
    public static float altura;                                    //altura camera fica da cabeca do player

    //--------------------------------------------- INICIALIZACAO -------------------------------------------- 
    void Start()
    {

        
    }


    //--------------------------------------------- IR PARA O PROXIMO COMODO --------------------------------------------
    public void MoveAnotherRoom()
    {

        if (!anotherScene)
        {
            MoveSameScene();
        }
        else
        {
            PrepareSceneChange();
            
        }
        

    }

    void MoveSameScene()
    {
        player.SendMessage("MoveTo", newPosition);                                                    //teleporta player para lugar novo
        charCamera.SendMessage("Teleport", new Vector3(newPosition.x, newPosition.y + altura, -10));      //teleporta camera player para lugar novo, tem que estar profundidade -10 para pegar tudo
        print("altura: " + altura);
        if (destination != null)
            ChangePlace();
        else
        {
            ResetPlayer();
        }
    }

    //--------------------------------------------- COMODO EM OUTRA CENA --------------------------------------------
    void PrepareSceneChange()
    {
        GetInfo script = destination.GetComponent<GetInfo>();
        print("prestes a ir ao getPath");
        TransitionData.destinationName = destination.name;                                          //guarda dados na transicao
        TransitionData.newPosition = newPosition;
        TransitionData.destinationParentName = script.GetScene();
        SceneTransitionManager.ChangeScene(TransitionData.destinationParentName);                   //muda para a cena que objeto pertence

    }

    //--------------------------------------------- MUDAR NAS CONFIGURACOES DO JOGO --------------------------------------------
    void ChangePlace()
    {
        currentPlace = destination;
        ChangeRoom();
    }

    //--------------------------------------------- DETECTAR TELEPORTE POR TOQUE --------------------------------------------

    void OnTriggerEnter2D(Collider2D other)                                     //se tocar no collider
    {
        if (touchTP && other.gameObject == player)                                                            //se e para teleportar, faz o teleporte
        {
            print("entramos " + gameObject + " " + other.gameObject);
            ResetPlayer();                                            //resetar animacao player
            SendMessage("MoveAnotherRoom");                                     //ir para o comodo destino
        }

    }


}

