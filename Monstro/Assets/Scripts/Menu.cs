using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;



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
        this.menu.SetActive(false);
        if(prevMenu==null){}
            VarGlobal.Unpause.Invoke();
        MC mc = VarGlobal.player.GetComponent<MC>();
        mc.activeMenu = prevMenu;

    }

    ////SAIR MENU -----------------------------------------------------------------------------------------
    public void Enter()
    {
        menu.SetActive(true);
        VarGlobal.Pause.Invoke();
        MC mc = VarGlobal.player.GetComponent<MC>();
        prevMenu = mc.activeMenu;
        mc.activeMenu = this;
    }

}

