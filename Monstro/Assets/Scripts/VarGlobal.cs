using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


//TRADUCAO -----------------------------------------------------------------------------------------

[Serializable]
public struct Language
{
    public string Portugues;
    public string Ingles;
}

//OPCOES DE INTERACAO -----------------------------------------------------------------------------------------


[Serializable]
public struct InteractMenu
{

    //funcao a ser chamada nas respectivas opcoes

    [SerializeField]
    public UnityEvent open;
    [SerializeField]
    public UnityEvent close;
    [SerializeField]
    public UnityEvent grab;
    [SerializeField]
    public UnityEvent look;
    [SerializeField]
    public UnityEvent use;
    [SerializeField]
    public UnityEvent talk;

    //menu de interacao
    public static GameObject opcoes;
    public static GameObject[] opcao;

    //auxiliar
    private int op;


    //COLOCANDO OPCAOS PARA INTERAGIR -----------------------------------------------------------------------------------------

    public void Interact(bool closed = false)                                   //player interagiu
    {
        op = 0;                                                                 //id opcao
        opcoes.SetActive(true);
        if (open.GetPersistentEventCount() != 0 || close.GetPersistentEventCount() != 0)           //se não foi adicionado nenhuma action
            Opcao("Abrir/Fechar", (closed == true ? open : close));                                //se estiver fechado pode tentar abrir, se não estiver fechado pode tentar fechar

        if (grab.GetPersistentEventCount() != 0)
            Opcao("Pegar", grab);

        if (look.GetPersistentEventCount() != 0)
            Opcao("Olhar de perto", look);

        if (use.GetPersistentEventCount() != 0)
            Opcao("Usar item", use);

        if (talk.GetPersistentEventCount() != 0)
            Opcao("Falar", talk);

        for (; op < 4; op++)
            opcao[op].SetActive(false);                                         //deixar desativada opcoes nao ativadas
    }


    private void Opcao(string texto, UnityAction function)                      //setar opcao especifica
    {
        //setar botao
        Button btn = opcao[op].GetComponent<Button>();                              //pegar botao
        btn.onClick.RemoveAllListeners();                                           //limpar listeners objeto antigo
        btn.onClick.AddListener(function);                                          //adicionar funcao a ser chamada se clicar

        //setar texto
        TextMeshPro txt = opcao[op].GetComponentInChildren<TextMeshPro>();
        txt.text = texto;
        opcao[op].SetActive(true);
        op++;
    }
     
     private void Opcao(string texto, UnityEvent function)                      //setar opcao especifica
    {
        //setar botao
        Button btn = opcao[op].GetComponent<Button>();                              //pegar botao
        btn.onClick.RemoveAllListeners();                                           //limpar listeners objeto antigo
        btn.onClick.AddListener(() => function?.Invoke());                                          //adicionar funcao anonima a ser chamada se clicar

        //setar texto
        TMP_Text txt = opcao[op].GetComponentInChildren<TMP_Text>();
        Debug.LogWarning(opcao[op]);
        txt.text = texto;
        opcao[op].SetActive(true);
        op++;
    }
}


//VARIAVEIS GLOBAIS -----------------------------------------------------------------------------------------

public class VarGlobal : MonoBehaviour
{
    //VARIAVEIS

    //eventos
    public static UnityEvent Pause;
    public static UnityEvent Unpause;

    //gameObject
    public static GameObject Player;



    void Awake()
    {
        InteractMenu.opcoes = GameObject.Find("Interact interface").transform.Find("Opcoes").GameObject();                        //pega menu interact

        InteractMenu.opcao = new GameObject[4];                                 //pega cada opcao do menu interact
        for (int i = 0; i < 4; i++)
        {
            InteractMenu.opcao[i] = InteractMenu.opcoes.transform.GetChild(i).GameObject();
        }

        Player = GameObject.Find("Pablo");

        Pause = new UnityEvent();
        Unpause = new UnityEvent();

    }
}