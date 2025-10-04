
using Assets.Scripts.SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameSettings;


//--------------------------------------------- PORTA -------------------------------------------- 

public class SceneTransitionManager : MonoBehaviour
{

    public static GameObject sceneManager;
    //VARIAVEIS

    void Awake()
    {
        sceneManager = gameObject;
        SetSaveSystem();
        
    }

    void Start()                            //depois de setar sistema salvamento
    {
        LoadData();
        PortalPositioning(gameObject);
    }

    //PREPARAR SISTEMA DE SALVAR DURANTE CENA
    void SetSaveSystem()
    {
        ConectScriptTag<RoomOriginator>("Place");
//        ConectScriptTag<DoorOriginator>("Door");
        ConectScriptTag<SceneOriginator>("Scene Manager");
        
    }

    

    //COLOCAR SCRIPT ORIGINATOR NOS OBJETOS CORRETOS
    void ConectScriptTag<T>(string tag) where T:Originator
    {
        GameObject[] objetos = GameObject.FindGameObjectsWithTag(tag);
        //    print("Ei" + objetos);
        foreach (GameObject child in objetos)
        {
            //        print("Entrou " + child);
            child.AddComponent<T>();
            
        }
    }
        

    //--------------------------------------------- MUDA DE CENA --------------------------------------------
    public static void ChangeScene(string scene)                                                                //mudar de cena
    {
        SaveData();
        SceneManager.LoadScene(scene);

    }

    //----------------------------------------------SALVAR DADOS-----------------------------------------------------

    public static void SaveData()
    {
        SaveSystem.SaveSceneData(sceneManager);
    }

    //--------------------------------------------- CARREGA DADOS SALVOS --------------------------------------------
    public void LoadData()
    {
        SaveSystem.LoadSceneData(gameObject);
    }
    
    //--------------------------------------------- POSICIONA PERSONAGEM --------------------------------------------
    public static void PortalPositioning(GameObject gameObject)                                                                      //colocar onde portal apontou
    {
        Portal twin;
        twin = gameObject.AddComponent<Portal>();                                                                                    //cria objeto para teleportar
        
        if (TransitionData.destinationName != "" && TransitionData.destinationName != null)                     //se tem algo salvo na transicao executa posicionamento
        {
            twin.destination = Search(GameObject.Find("/"+TransitionData.destinationParentName), TransitionData.destinationName).gameObject; //destino do portal igual a destino da transicao
                               //para achar objeto primeiro tem que achar pai,  e depois procura transform filho
            TransitionData.destinationName = null;                                                              //reseta para nao ter erro de dados ultrapassados
            if (twin.destination == null)                                                                       //se nao tinha destino algo errado
                Debug.LogError("nao achou comodo na nova cena");
            else
                print(TransitionData.destinationParentName);
            twin.newPosition = TransitionData.newPosition;                                                      //posicao nova que portal leva = posicao transicao
            twin.MoveAnotherRoom();
        }

    }

}

