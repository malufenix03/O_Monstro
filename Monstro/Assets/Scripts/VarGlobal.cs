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
        MovedBy camera = player.GetComponent<MC>().charCamera.GetComponent<MovedBy>();
        int i = 0;
        Transform barrierX = currentPlace.transform.Find("Structure/Side walls");
        foreach (Transform child in barrierX)
        {
            camera.barrierX[i++] =child.GameObject().GetComponent<Collider2D>();
        }
        
    }
}




