
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameSettings;


namespace Assets.Scripts.SaveSystem
{
    



//--------------------------------------------- SISTEMA DE SALVAMENTO -------------------------------------------- 
public static class SaveSystem
{

    //VARIAVEIS
    static Dictionary<string, SceneOriginator.SceneMemento> dados = new();



    //----------------------------------------------SALVAR DADOS-----------------------------------------------------

    public static void SaveData()                                       //progresso do jogo inteiro
    {
        
    }

    public static void SaveSceneData(GameObject scene)                  //estado da cena ----> importante apenas durante troca de cena
    {
        string nomeCena = SceneManager.GetActiveScene().name;                           //nome da cena
        dados[nomeCena]=(SceneOriginator.SceneMemento)scene.GetComponent<SceneOriginator>().Save();
 
    }

    //--------------------------------------------- CARREGA DADOS SALVOS --------------------------------------------
    public static void LoadData()                       //progresso do jogo inteiro
    {

    }

    public static void LoadSceneData(GameObject scene)                              //estado da cena anteriormente----> importante apenas durante troca de cena
    {

        string nomeCena = SceneManager.GetActiveScene().name;      //nome da cena
        if (dados.TryGetValue(nomeCena, out SceneOriginator.SceneMemento dadosAtual))
        {
                scene.GetComponent<SceneOriginator>().Recover(dadosAtual);
        }
    }

   


}

}