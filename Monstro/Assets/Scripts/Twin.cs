using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



//--------------------------------------------- PORTA -------------------------------------------- 

public class Twin : MonoBehaviour
{

    //VARIAVEIS


    //para onde leva
    public GameObject other;                              //objeto gemeo



    //--------------------------------------------- DESATIVA ESSE OBJETO E ATIVA OBJETO GEMEO SE ESTA NO OUTRO COMODO -------------------------------------------- 

    public void PassToTwin()
    {
        gameObject.SetActive(false);                                                  //desativa esse objeto
        other.SetActive(true);                                                        //ativa gemeo com destino = comodo esse objeto
        other.SendMessage("TurnFog",other.GetComponent<Portal>().destination);        //ligar fog desse comodo
    }


}

