using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



//NAMESPACE JOGO -----------------------------------------------------------------------------------------

public static class GameSettings
{
    //VARIAVEIS

    //eventos
    public static UnityEvent Pause;
    public static UnityEvent Unpause;
    public static UnityEvent SceneChange;

    //objetos jogo
    public static GameObject player;
    public static GameObject currentPlace;
    public static GameObject charCamera;
    public static MC scriptPlayer;
    public static MovedBy scriptCameraMove;

    //tamanho da tela
    public static float screenWidth = 32;

    //linguagem
    public enum Languages { Portugues, Ingles }
    public static Languages currentLanguage = Languages.Portugues;


    //
    public static void GetColliders(Transform parent, Collider2D[] container)
    {
        int i = 0;
        foreach (Transform child in parent)
        {
            container[i++] = child.GameObject().GetComponent<Collider2D>();                           //coloca as paredes como obstaculos para a camera parar
        }
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
        if (backTxt != null)
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



            mc.charCamera.SendMessage("TeleportX", player.transform.position.x);
        }


    }



}

/*
public VarGlobal: MonoBehaviour
{



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

*/


