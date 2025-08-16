using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameSettings;



//--------------------------------------------- CONTROLLER MENU -------------------------------------------- 
[Serializable]
public class Menu
{
    //VARIAVEIS
    public GameObject menu;
    protected Menu prevMenu;

    //ao abrir menu
    public void OnOpenMenu()
    {

    }


    //ao abrir submenu
    public void OnOpenSubmenu()
    {

    }

    ////SAIR MENU -----------------------------------------------------------------------------------------
    public void Leave()
    {
        menu.SetActive(false);
        if(prevMenu==null){}
            Unpause.Invoke();
        scriptPlayer.activeMenu = prevMenu;

    }

    ////ENTRAR MENU -----------------------------------------------------------------------------------------
    public void Enter()
    {
        menu.SetActive(true);
        Pause.Invoke();
        prevMenu = scriptPlayer.activeMenu;
        scriptPlayer.activeMenu = this;
    }

}

