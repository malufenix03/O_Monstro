using UnityEngine;



//--------------------------------------------- ABSTRAÇÃO PERSONAGEM -------------------------------------------- 

public class SetPlace : MonoBehaviour              //mudar o lugar atual
{

    //VARIAVEIS
    public GameObject place;                                //lugar que esta

    void Start()
    {
        place = transform.parent.gameObject;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (VarGlobal.currentPlace != place)
        {
            VarGlobal.currentPlace = place;
            VarGlobal.OnChangeScene();
        }
    }

}

