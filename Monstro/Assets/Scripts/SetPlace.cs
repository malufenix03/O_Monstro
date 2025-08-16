using UnityEngine;



//--------------------------------------------- ABSTRAÇÃO PERSONAGEM -------------------------------------------- 

public class SetPlace : MonoBehaviour              //mudar o lugar atual
{

    //VARIAVEIS
    public GameObject place;                                //lugar que esta
    public bool tp = false;

    void Start()
    {
        place = transform.parent.gameObject;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
                                       //se nao e para teleportar, so muda as configuracoes de qual lugar esta
            if (VarGlobal.currentPlace != place)
            {
                VarGlobal.currentPlace = place;
                VarGlobal.OnChangeScene();
            }
    }
}

