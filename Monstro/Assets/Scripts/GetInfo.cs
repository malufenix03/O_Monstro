using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Text;



//--------------------------------------------- INFO DO PATH NA NOVA CENA -------------------------------------------- 

public class GetInfo : MonoBehaviour
{

    //VARIAVEIS
    public Transform limit;

    //--------------------------------------------- PEGA PATH DENTRO DO DE UM PAI -------------------------------------------- 
    

    public string GetPath()
    {
        StringBuilder pathBuilder = new StringBuilder();                        //StringBuilder para facilitar cocatenacao
        Transform pointer = this.transform;
        while (pointer != limit)                                               //se nao for o limite
        {
            print("b " + pointer);
            pathBuilder.Insert(0, pointer.name);                                //adicionar nome no comeco da string
            if (pointer.parent != limit)                                        //se o pai nao for limite
            {
                pathBuilder.Insert(0, "/");                                     //adicionar barra no comeco da string
            }
            pointer = pointer.parent;                                           //aponta para pai
        }
        return pathBuilder.ToString();                                          //transforma em string e retorn
    }

    public string GetScene()
    {
        Transform pointer = transform;
        string scene="";
        while (pointer != limit)
        {
            scene = pointer.name;
            pointer = pointer.parent;
        }                                        
        return scene;
    }
}

