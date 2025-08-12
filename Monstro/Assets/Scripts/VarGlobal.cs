using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



//VARIAVEIS GLOBAIS -----------------------------------------------------------------------------------------

public class VarGlobal : MonoBehaviour
{
    //VARIAVEIS

    //eventos
    public static UnityEvent Pause;
    public static UnityEvent Unpause;
    public static UnityEvent SceneChange;

    //gameObject
    public static GameObject player;
    public static GameObject currentPlace;

    //linguagem
    public enum languages { Portugues, Ingles };
    public static languages currentLanguage = languages.Portugues;

    public static float screenWidth = 32;



    //INICIALIZACAO VARIAVEIS GLOBAIS E ESTATICAS APENAS UMA VEZ-----------------------------------------------------------------------------------------
    void Awake()
    {
        InteractMenu.opcoes = GameObject.Find("Interact interface").transform.Find("Opcoes").GameObject();                        //pega menu interact

        InteractMenu.opcao = new GameObject[4];                                                                                     //pega cada opcao do menu interact
        for (int i = 0; i < 4; i++)
        {
            InteractMenu.opcao[i] = InteractMenu.opcoes.transform.GetChild(i).GameObject();
        }


        player = GameObject.Find("Pablo");                                                                                         //pega player
        currentPlace = GameObject.Find("House Pablo").transform.Find("Bedroom").GameObject();

        //evento pause e despause
        Pause = new UnityEvent();
        Unpause = new UnityEvent();
        SceneChange = new UnityEvent();

    }


    //MUDANCA DE COMODO/CENA-----------------------------------------------------------------------------------------
    public static void OnChangeScene()
    {


        //INICIAR VARIAVEIS
        MC mc = player.GetComponent<MC>();                                                                  //pega script anexado a player
        MovedBy camera = mc.charCamera.GetComponent<MovedBy>();                                             //pega script anexado a camera
        int i = 0;                                                                                          //indice


        //SETAR PAREDES COMO LIMITE DA CAMERA
        Transform barrierX = currentPlace.transform.Find("Structure/Side walls");                           //acha as paredes / obstaculos verticais
        foreach (Transform child in barrierX)
        {
            camera.barrierX[i++] = child.GameObject().GetComponent<Collider2D>();                           //coloca as paredes como obstaculos para a camera parar
        }

        //SETAR CHAO COMO PLATAFORMA PARA PULO
        Collider2D ground = currentPlace.transform.Find("Structure/Ground/Tilemap").GameObject().GetComponent<Collider2D>();        //pega o chao do novo lugar
        mc.ground[0] = ground;                                                                              //seta chao como lugar que personagem tem que tocar para pular

        //SETAR INTERFACE NOVA SE NECESSARIO
        GameObject backTxt = currentPlace.transform.Find("Back interface").GameObject();                    //pega interface back para voltar
        mc.back = backTxt ? backTxt : null;                                                                 //conecta player à interface

        //CALCULAR SE PRECISA TRANCAR A CAMERA E CENTRO DO NOVO LUGAR
        if (ground.bounds.size.x < screenWidth)                                                             //se lugar inteiro ja cabe na tela da camera
        {
            print(ground.bounds.size.x+" lockou");
            mc.camLock = true;                                                                              //tranca o movimento da camera
            mc.charCamera.SendMessage("TeleportX", ground.bounds.center.x);                                 //teleporta camera para o centro
        }
        else
            mc.camLock = false;                                                                             //se lugar for grande, destranca movimento da camera
            
    }
}




