
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameSettings;


namespace Assets.Scripts.SaveSystem
{


    //--------------------------------------------- DADOS PARA SALVAR DO QUARTO--------------------------------------------
    [Serializable]
    public struct RoomData : IData
    {
        public bool fog;                            //fog ligada ou desligada
        public Dictionary<string, bool> door;
    }
    
    //--------------------------------------------- ESTADO ATUAL--------------------------------------------

    public class RoomOriginator : Originator                                                                 //origina memento ---> anexar em room para restaurar estado
    {
        //VARIAVEIS
        [SerializeField]
        RoomData state;                             //estado
        [SerializeField]
        GameObject fog;                             //objeto para mudar
        GameObject[] door;
        public static string Tag = "Place";

        void Awake()
        {
            fog = FindChildWithTag(transform, "Fog");                                                //pegar fog
            door = FindChildrenWithTag(transform, "Door");                                           //pegar porta
            state.door = new Dictionary<string, bool>();
            if (fog == null && door == null)
            {
                print("Destruir" + this);
                Destroy(this);                                                                                  //se nao tem neblina destroi o objeto pois so salva essa info
            }
            else
            {
                if(fog!=null)
                    state.fog = fog.activeSelf;
                GetDoor();

            }                                                                                       
            
        }

        void GetDoor() {                                                                                        //pegar dados portas
            foreach (GameObject child in door)
            {
                state.door[child.name] = child.activeSelf;                                                      //setar estado portas
            }
        }

        void SetDoor() {                                                                                        //pegar dados portas
            foreach (GameObject child in door)
            {
                child.SetActive(state.door[child.name]);                                                      //setar estado portas
            }
        }

        //--------------------------------------------- RETORNAR DADOS PARA SALVAR--------------------------------------------
        public override Memento Save()                       //salvar estado 
        {

            if (fog != null)
                state.fog = fog.activeSelf;
            GetDoor();
            return new RoomMemento(state);
        }

        //--------------------------------------------- RECEBE DADOS PARA RECUPERAR--------------------------------------------
        public override void Recover(Memento memento)        //recuperar estado salvo
        {
            state = (RoomData)memento.GetState();        //pega o estado salvo
            Restore();
        }

        //--------------------------------------------- RESTAURAR DADOS--------------------------------------------
        void Restore()
        {
            if(fog!=null)
                fog.SetActive(state.fog);
            SetDoor();
        }

        //--------------------------------------------- COPIA ESTADO QUARTO--------------------------------------------
        public class RoomMemento : Memento                            //memento que salva estado do objeto
        {
            RoomData state;                                          //estado do quarto

            internal RoomMemento(RoomData state)                    //seta estado do quarto a ser salvo
            {
                this.state = state;
            }

            internal override IData GetState()                      //retorna estado quarto
            {
                return state;
            }

        }
    }

}