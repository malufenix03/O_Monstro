using UnityEngine;

public abstract class Character: MonoBehaviour
{
    //public Portrait portrait
    public GameObject sprite;
    protected bool flagInteract=false;
    protected bool flagInteractable=false;
    public abstract void Pause(bool paused);
    
}

