using System.Collections.Generic;
using UnityEngine;
using static GameSettings;



//--------------------------------------------- CONTROLAR INTERACAO -------------------------------------------- 

public class Interaction : MonoBehaviour
{

    //--------------------------------------------- DETECTAR QUANDO PLAYER ESTA NA FRENTE DO OBJETO -------------------------------------------- 

    //COLOCA ESSE OBJETO COMO ALVO INTERACT SE PLAYER ESTIVER NA FRENTE
    void OnTriggerEnter2D(Collider2D other)                                                 //se collider com rigidbody entra nesse collider
    {
        if (other.gameObject != player)                                                     //se não for collider principal do player (tem que ser collider de ponteiro)
        {
            print("Entrou " + name);
            scriptPlayer.Target = gameObject;                                               //target player setado como esse objeto
        }
    }

    void OnTriggerExit2D(Collider2D other)                                                  //se collider com rigidbody sai desse collider
    {
        if (other.gameObject != player)                                           //se não for collider principal do player (tem que ser collider de ponteiro)
        {
            print("Saiu " + name);
            scriptPlayer.Target = null;                                                     //tira esse objeto de target player
        }
    }

}

