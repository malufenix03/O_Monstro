using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



//OPCOES DE INTERACAO -----------------------------------------------------------------------------------------


[Serializable]
public class InteractMenu
{
    //VARIAVEIS

    //funcao a ser chamada em
    [SerializeField]
    public UnityEvent open;             //funcao abrir porta
    [SerializeField]
    public UnityEvent close;            //funcao fechar porta
    [SerializeField]
    public UnityEvent grab;             //funcao pegar objeto
    [SerializeField]
    public UnityEvent look;             //funcao olhar de perto             //opcional
    [SerializeField]
    public UnityEvent use;              //funcao usar objeto                //opcional
    [SerializeField]
    public UnityEvent talk;             //funcao falar


    //texto opcoes
    public Language txtOpen = new("Abrir/Fechar");                           //texto botao
    public Language txtGrab = new("Pegar");
    public Language txtLook = new("Olhar de perto");
    public Language txtUse = new("Usar item");
    public Language txtTalk = new("Falar");



    //menu de interacao
    public static GameObject opcoes;
    public static GameObject[] opcao;

    //auxiliar
    private int op;



    //ATIVANDO E CONFIGURANDO MENU OPCAOS PARA INTERAGIR -----------------------------------------------------------------------------------------
    public void Interact(bool closed = false)                                   //player interagiu com objeto
    {

        op = 0;                                                                 //id opcao
        opcoes.SetActive(true);                                                 //ativar menu opcoes
        if (open.GetPersistentEventCount() != 0 || close.GetPersistentEventCount() != 0)           //se não foi adicionado nenhuma action ao evento, nao tem essa opcao
            Opcao(txtOpen.GetTxt(), (closed == true ? open : close));                              //se estiver fechado pode tentar abrir, se não estiver fechado pode tentar fechar

        if (grab.GetPersistentEventCount() != 0)
            Opcao(txtGrab.GetTxt(), grab);

        if (look.GetPersistentEventCount() != 0)
            Opcao(txtLook.GetTxt(), look);

        if (use.GetPersistentEventCount() != 0)
            Opcao(txtUse.GetTxt(), use);

        if (talk.GetPersistentEventCount() != 0)
            Opcao(txtTalk.GetTxt(), talk);

        for (; op < 4; op++)
            opcao[op].SetActive(false);                                                            //deixar desativada opcoes nao ativadas
    }


    ////CONFIGURANDO OPCAO ESPECIFICA -----------------------------------------------------------------------------------------
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


    ////SAIR MENU -----------------------------------------------------------------------------------------
    public void Leave()
    {
        opcoes.SetActive(false);
        VarGlobal.Unpause.Invoke();
    }


}


