using UnityEngine;



//--------------------------------------------- ABSTRAÇÃO PERSONAGEM -------------------------------------------- 

public abstract class Character : MonoBehaviour
{

    //VARIAVEIS

    //public Portrait portrait
    public Sprite FrontView;

    //flags
    [SerializeField]
    protected bool interact = false;                         //se pode interagir fora do pause ou nao
    protected bool flagInteract = false;                     //se pode interagir nesse segundo
    [SerializeField]
    protected bool interactable = false;                    //se player pode interagir com esse personagem fora do pause
    protected bool flagInteractable = false;                //se player pode interagir com esse personagem agora
    
    public abstract void Pause(bool paused);

}

