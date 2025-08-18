using UnityEngine;
using static GameSettings;



//--------------------------------------------- DESTROI OBJETOS DUPLICADOS POR CAUSA DO DONT DESTROY ON LOAD -------------------------------------------- 

public class DestroyDuplicate : MonoBehaviour              //mudar o lugar atual
{

    //VARIAVEIS
    public static GameObject instance;

    void Awake()
    {
        if (instance != null && instance != this)           //se nunca carregou instancia ou instancia carregada e diferente da atual
        {
            print("Recarregou cena ja carregada");
            GetComponent<VarGlobal>().reload = true;                                                //significa que recarregou objeto que ja existe
        }
        else
            instance = gameObject;
    }

    
}

