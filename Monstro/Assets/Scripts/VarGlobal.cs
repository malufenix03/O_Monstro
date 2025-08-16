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
        InteractMenu.menu = GameObject.Find("Interact interface").transform.Find("Opcoes").GameObject();                        //pega menu interact

        InteractMenu.opcao = new GameObject[4];                                                                                     //pega cada opcao do menu interact
        for (int i = 0; i < 4; i++)
        {
            InteractMenu.opcao[i] = InteractMenu.menu.transform.GetChild(i).GameObject();
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

        i = 0;
        //SETAR CHAO COMO PLATAFORMA PARA PULO
        Collider2D ground = currentPlace.transform.Find("Structure/Ground/Tilemap").GameObject().GetComponent<Collider2D>();        //pega o chao do novo lugar

        

        mc.ground = new List<Collider2D> { ground };                                                          //seta chao como lugar que personagem tem que tocar para pular, limpando o que tinha antes



        Transform plataforms = currentPlace.transform.Find("Plataforms");                                   //pegar plataformas para contar o pulo
        if (plataforms != null)                                                                             //se tiver plataforma, pega cada uma delas
        {
            Collider2D[] aux = plataforms.GetComponentsInChildren<Collider2D>();                                //pega collider do objeto e dos filhos do objeto
            foreach (Collider2D child in aux)
            {
                mc.ground.Add(child.GameObject().GetComponent<Collider2D>());                               //pega plataforma para contar como chao
            }

                                      
        }

        //SETAR INTERFACE NOVA SE NECESSARIO
        if (mc.back != null)
            mc.back.SetActive(false);                                                                          //desligar interface back
        GameObject backTxt = currentPlace.transform.Find("Back interface").GameObject();                    //pega interface back para voltar
        mc.back = backTxt ? backTxt : null;                                                                 //conecta player à interface
        if(backTxt!=null)
            backTxt.SetActive(true);                                                                            //ligar interface back para aparecer na tela

        //CALCULAR SE PRECISA TRANCAR A CAMERA E CENTRO DO NOVO LUGAR
        if (ground.bounds.size.x < screenWidth)                                                             //se lugar inteiro ja cabe na tela da camera
        {
            //print(ground.bounds.size.x + " lockou");
            mc.camLock = true;                                                                              //tranca o movimento da camera
            mc.charCamera.SendMessage("TeleportX", ground.bounds.center.x);                                 //teleporta camera para o centro
        }
        else
        {
            mc.camLock = false;                                                                             //se lugar for grande, destranca movimento da camera
            /*    if (mc.charCamera.transform.position.x != player.transform.position.x)                 //se camera nao estiver na mesma posicao do outro fazer ela deslizar ate ele
                {
                    mc.charCamera.AddComponent<SlideMove>();                                          //conect script de deslizar na camera
                    SlideMove adjustCam;
                    adjustCam = mc.charCamera.GetComponent<SlideMove>();

                    //setar as variaveis
                    adjustCam.Target = player;
                    adjustCam.autoDestruction = true;
                }*/


            mc.charCamera.SendMessage("TeleportX", player.transform.position.x);
        }
                                                                                                            

    }


    public static void ResetPlayer() {
        player.SendMessage("ResetSprite");                                                   //resetar animacao player
    }

     

    //FECHAR JOGO-----------------------------------------------------------------------------------------
    public static void Leave()
    {
        Application.Quit();
    }
}




