using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



//--------------------------------------------- SALVA INFORMACAO DE PORTAL PARA OUTRA CENA -------------------------------------------- 

public static class TransitionData
{

    //VARIAVEIS


    //para onde tem que ir na outra cena
    public static string destinationName;                     //nome do lugar que player tem que ir
    public static string destinationParentName;               //find acha so objeto pai
    public static Vector3 newPosition;                       //posicao que player tem que ir no novo lugar

}

