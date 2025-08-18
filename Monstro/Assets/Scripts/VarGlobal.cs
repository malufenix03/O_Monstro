using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using static GameSettings;



//VARIAVEIS GLOBAIS -----------------------------------------------------------------------------------------

public class VarGlobal : MonoBehaviour
{
    //VARIAVEIS
    public GameObject[] DontDestroy;
    public bool reload = false;



    //PEGA OPCOES PARA MENU INTERACAO-----------------------------------------------------------------------------------------
    void SetInteractMenu()
    {
        InteractMenu.menu = Search(GameObject.Find("Interact interface"), "Opcoes").GameObject();                        //pega menu interact

        InteractMenu.opcao = new GameObject[4];                                                                         //pega cada opcao do menu interact
        for (int i = 0; i < 4; i++)
        {
            InteractMenu.opcao[i] = InteractMenu.menu.transform.GetChild(i).GameObject();
        }
    }

    //INICIALIZA AS VARIAVEIS GLOBAIS DO JOGO-----------------------------------------------------------------------------------------
    void SetGame()
    {
        currentPlace = Search(GameObject.Find("House Pablo"), "Bedroom").GameObject();          //pega lugar inicial

        player = GameObject.Find("Pablo");                                                      //pega player
        scriptPlayer = player.GetComponent<MC>();                                               //pega script do player

        charCamera = scriptPlayer.charCamera;                                                   //pegar camera
        scriptCameraMove = charCamera.GetComponent<MovedBy>();                                  //pega script da camera

        Portal.altura = GetY(charCamera) - GetY(player);                                        //altura camera acima cabeca player
    }

    //SETAR EVENTOS
    void SetEvents()
    {
        Pause = new UnityEvent();
        Unpause = new UnityEvent();
        SceneChange = new UnityEvent();
    }

    void SetDontDestroy()
    {
        foreach (GameObject child in DontDestroy)
        {
            if (!reload)
                DontDestroyOnLoad(child);
            else
                Destroy(child);
        }
        if (reload)
            Destroy(gameObject);
    }

    //INICIALIZACAO VARIAVEIS GLOBAIS E ESTATICAS APENAS UMA VEZ-----------------------------------------------------------------------------------------
    void Awake()
    {
        SetInteractMenu();
        SetGame();
        SetEvents();
        
    }
    void Start()
    {
        SetDontDestroy();
    }

    public void LeaveGame()
    {
        GameSettings.LeaveGame();
    }

}




