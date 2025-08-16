using System.Collections.Generic;
using UnityEngine;
using static GameSettings;



//--------------------------------------------- CONTROLLER OBJETO -------------------------------------------- 

public class Object : MonoBehaviour
{

    //VARIAVEIS

    public Language nome;


    //flags
    [SerializeField]
    protected bool interactable = true;                     //se player pode interagir com esse objeto fora do pause
    protected bool flagInteractable = false;                //se player pode interagir com esse objeto agora
    protected bool inventory = false;                       //se objeto esta no inventario
    [SerializeField]
    protected bool open = false;                            //se objeto esta aberto


    //interacoes possiveis
    [SerializeField]
    public InteractMenu interactMenu;


    //INICIALIZAÇÃO -----------------------------------------------------------------------------------------  
    protected void Ini()
    {

        GameSettings.Pause.AddListener(OnPause);               //adiciona OnPause ao evento pause
        Unpause.AddListener(OnUnpause);           //adiciona OnUnpause ao evento unpause
        interactMenu.Ini();                                 //inicializa menu interacao
        Pause(false);                                       //jogo despausado
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Ini();
    }

    //PAUSAR -----------------------------------------------------------------------------------------    
    public void Pause(bool paused)
    {
        flagInteractable = !paused;
    }

    void OnPause()
    {
        Pause(true);
    }

    void OnUnpause()
    {
        Pause(false);
    }


    //INTERACAO -----------------------------------------------------------------------------------------
    public void Interact()
    {
        if (flagInteractable)
            interactMenu.Enter(!open);  
        
    }

}

