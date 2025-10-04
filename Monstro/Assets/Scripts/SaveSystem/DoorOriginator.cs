
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
    public struct DoorData : IData
    {
        public bool active;                            //fog ligada ou desligada
    }
    
    //--------------------------------------------- ESTADO ATUAL--------------------------------------------

    public class DoorOriginator : Originator                                                                 //origina memento ---> anexar em Door para restaurar estado
    {
        //VARIAVEIS
        [SerializeField]
        DoorData state;                             //estado

        public static string Tag = "Door";

        void Awake()
        {
            state.active = gameObject.activeSelf;
        }



        //--------------------------------------------- RETORNAR DADOS PARA SALVAR--------------------------------------------
        public override Memento Save()                       //salvar estado 
        {
            state.active = gameObject.activeSelf;
            return new DoorMemento(state);
        }

        //--------------------------------------------- RECEBE DADOS PARA RECUPERAR--------------------------------------------
        public override void Recover(Memento memento)        //recuperar estado salvo
        {
            state = (DoorData)memento.GetState();        //pega o estado salvo
            Restore();
        }

        //--------------------------------------------- RESTAURAR DADOS--------------------------------------------
        void Restore()
        {
            gameObject.SetActive(state.active);
        }

        //--------------------------------------------- COPIA ESTADO QUARTO--------------------------------------------
        public class DoorMemento : Memento                            //memento que salva estado do objeto
        {
            DoorData state;                                          //estado do quarto

            internal DoorMemento(DoorData state)                    //seta estado do quarto a ser salvo
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