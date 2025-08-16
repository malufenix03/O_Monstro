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


    //ANEXAR COLLIDERS ONDE FOR NECESSARIO-----------------------------------------------------------------------------------------
    static void GetColliders(Transform parent, Collider2D[] container, int i = 0)
    {
        foreach (Transform child in parent)
        {
            container[i++] = child.GameObject().GetComponent<Collider2D>();                           //coloca as paredes como obstaculos para a camera parar
        }
    }

    static void GetColliders(Collider2D[] parent, List<Collider2D> container)
    {
        foreach (Collider2D child in parent)
        {
            container.Add(child.GameObject().GetComponent<Collider2D>());                               //pega plataforma para contar como chao
        }
    }

    static Collider2D GetColliders(Transform parent)
    {
        return parent.GameObject().GetComponent<Collider2D>();                           //coloca as paredes como obstaculos para a camera parar
    }

    //PROCURAR OBJETO--------------------------------------------------------------------------------------------------------------

    //procura transform child em transform parent
    public static Transform Search(GameObject parent, string childName)
    {
        return Search(parent.transform, childName);                         //pega transform e manda para a outra funcao procurar
    }
    //procurando transform child em transform parent
    public static Transform Search(Transform parent, string childName)
    {
        return parent.Find(childName);
    }

    //SETAR PAREDES COMO LIMITE DA CAMERA------------------------------------------------------------------------------------------
    static void GetWalls(Transform x)
    {
        Transform barrierX = Search(x, "Side walls");                                               //acha as paredes
        GetColliders(barrierX, scriptCameraMove.barrierX);                                          //coloca collider no vetor de barreira x
    }

    //SETAR CHAO E PLATAFORMAS PARA PULO-----------------------------------------------------------------------------------------
    static void GetPlataforms(Transform parentPlataforms)
    {
        Collider2D[] colliders = parentPlataforms.GetComponentsInChildren<Collider2D>();                           //pega collider do objeto e dos filhos do objeto
        GetColliders(colliders, scriptPlayer.ground);

    }

    static Collider2D GetGround(Transform parentGround)
    {
        Collider2D ground = GetColliders(Search(parentGround, "Ground/Tilemap"));            //pega o chao do novo lugar
        scriptPlayer.ground = new List<Collider2D> { ground };            //seta chao como lugar que personagem tem que tocar para pular, limpando o que tinha antes

        //plataforma
        Transform plataforms = Search(currentPlace, "Plataforms");                                          //pegar plataformas para contar o pulo
        if (plataforms != null)                                                                             //se tem plataforma no lugar
        {
            GetPlataforms(plataforms);
        }
        return ground;                                                                                      //chao e usado por outra funcao
    }

    //SETAR INTERFACE NOVA SE NECESSARIO------------------------------------------------------------------------------------------
    static void GetBackInterface()
    {
        if (scriptPlayer.back != null)
            scriptPlayer.back.SetActive(false);                                                       //desligar interface back antiga
        GameObject backTela = Search(currentPlace, "Back interface").GameObject();                    //pega interface back para voltar
        if (backTela != null)
        {
            scriptPlayer.back = backTela;                                                             //conecta player à interface
            backTela.SetActive(true);                                                                 //ligar interface back para aparecer na tela se tiver
        }
        else
            scriptPlayer.back = null;

    }

    //CALCULAR SE PRECISA TRANCAR A CAMERA E CENTRO DO NOVO LUGAR------------------------------------------------------------------
    static void CheckCameraLock(Collider2D ground)
    {
        if (ground.bounds.size.x < screenWidth)                                                         //se lugar inteiro ja cabe na tela da camera
        {
            scriptPlayer.camLock = true;                                                                //tranca o movimento da camera
            charCamera.SendMessage("TeleportX", ground.bounds.center.x);                                //teleporta camera para o centro
        }
        else                                                                                            //lugar maior que tela
        {
            scriptPlayer.camLock = false;                                                               //se lugar for grande, destranca movimento da camera
            charCamera.SendMessage("TeleportX", GetX(player));                           //teleporta camera para mesmo valor x que player
        }
    }



    //MUDANCA DE COMODO/CENA------------------------------------------------------------------------------------------------------
    public static void ChangeScene()
    {
        //INICIAR VARIAVEIS
        MC mc = player.GetComponent<MC>();                                                                  //pega script anexado a player
        MovedBy camera = mc.charCamera.GetComponent<MovedBy>();                                             //pega script anexado a camera
        Transform structure = Search(currentPlace, "Structure");                                            //pegar estrutura

        //CONFIGURAR COMPONENTES
        GetWalls(structure);
        Collider2D ground = GetGround(structure);
        GetBackInterface();
        CheckCameraLock(ground);
    }

    //PEGAR POSICAO-----------------------------------------------------------------------------------------
    public static float GetX(GameObject obj)
    {
        return obj.transform.position.x;                                                                     
    }

    public static float GetX(Transform obj)
    {
        return obj.position.x;                                                                     
    }
    
    public static float GetX(Component obj)
    {
        return obj.transform.position.x;                                                                     
    }

    public static float GetY(GameObject obj)
    {
        return obj.transform.position.y;
    }

    public static float GetY(Transform obj)
    {
        return obj.position.y;                                                                     
    }

    public static float GetY(Component obj)
    {
        return obj.transform.position.y;                                                                     
    }

    
    //RESETAR SPRITE PLAYER-----------------------------------------------------------------------------------------
    public static void ResetPlayer()
    {
        player.SendMessage("ResetSprite");                                                                 //resetar animacao player
    }

    //FECHAR JOGO-----------------------------------------------------------------------------------------
    public static void LeaveGame()
    {
        Application.Quit();
    }

}




