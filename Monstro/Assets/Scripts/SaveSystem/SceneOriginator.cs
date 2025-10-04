
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameSettings;


namespace Assets.Scripts.SaveSystem
{


    [Serializable]
    public struct SceneData : IData
    {
        [SerializeField]
        public Dictionary<string, RoomOriginator.RoomMemento> quarto;
   //     [SerializeField]
  //      public Dictionary<string, DoorOriginator.DoorMemento> porta;
    }


    public class SceneOriginator : Originator
    {
        //VARIAVEIS
        [SerializeField]
        SceneData state = new();
        [SerializeField]
        GameObject[] places;
        [SerializeField]
//        GameObject[] doors;
        public static string Tag = "Scene";

        void GetRoomData()
        {
            GetData(state.quarto, places);
//            GetData(state.porta, doors);
        }


        void Awake()
        {
            state.quarto = new Dictionary<string, RoomOriginator.RoomMemento>();                                          //inicializar dicionario
//            state.porta = new Dictionary<string, DoorOriginator.DoorMemento>();
            places = GameObject.FindGameObjectsWithTag(RoomOriginator.Tag);                                               //pegar lugares cena
//            doors = GameObject.FindGameObjectsWithTag(DoorOriginator.Tag);                                               //pegar lugares cena
        }

        void Start()
        {
            GetRoomData();
        }

        //--------------------------------------------- RETORNAR DADOS PARA SALVAR--------------------------------------------
        public override Memento Save()                       //salvar estado 
        {
            GetRoomData();                                   //pegar estado quarto antes de salvar
            return new SceneMemento(state);
        }

        //--------------------------------------------- RECEBE DADOS PARA RECUPERAR--------------------------------------------
        public override void Recover(Memento memento)        //recuperar estado salvo
        {
            state = (SceneData)memento.GetState();        //pega o estado salvo
            Restore();
        }

        //--------------------------------------------- RESTAURAR DADOS--------------------------------------------
        void Restore()
        {
            SetData(state.quarto, places);
//            SetData(state.porta, doors);
        }


        //--------------------------------------------- COPIA ESTADO QUARTO--------------------------------------------
        public class SceneMemento : Memento                            //memento que salva estado do objeto
        {
            SceneData state;

            internal SceneMemento(SceneData state)           //classe originadora pode acessar
            {
                this.state = state;
            }

            internal override IData GetState()              //retorna estado
            {
                return state;
            }
        }
    }

}

    


