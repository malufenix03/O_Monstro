using UnityEngine;



//--------------------------------------------- ABSTRAÇÃO PERSONAGEM -------------------------------------------- 

public abstract class Character : MonoBehaviour
{

    //VARIAVEIS

    //public Portrait portrait
//    public GameObject sprite;
    protected bool flagInteract = false;
    protected bool flagInteractable = false;
    public abstract void Pause(bool paused);

}

