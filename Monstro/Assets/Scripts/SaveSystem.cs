
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameSettings;



public struct RoomData
{
    public bool fog;                            //fog ligada ou desligada
}

public class RoomOriginator:MonoBehaviour                    //origina memento ---> anexar em room para restaurar estado
{
    RoomData state;                             //estado

    public Memento Save()                       //salvar estado 
    {
        return new Memento(state);
    }

    public void Restore(Memento memento)
    {
        
    }

    public class Memento
    {
        RoomData state;

        internal Memento(RoomData state)           //classe originadora pode acessar
        {
            this.state = state;
        }

        internal RoomData GetState()
        {
            return state;
        }
    }
}

public struct SavedData
{

}


//--------------------------------------------- PORTA -------------------------------------------- 
public static class SaveSystem
{

    //VARIAVEIS
    static Dictionary<string, SavedData> dados;



    //----------------------------------------------SALVAR DADOS-----------------------------------------------------

    public static void SaveDate()
    {

    }

    //--------------------------------------------- CARREGA DADOS SALVOS --------------------------------------------
    public static void LoadData()
    {

    }



}

