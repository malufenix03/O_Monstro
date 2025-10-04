
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameSettings;





namespace Assets.Scripts.SaveSystem
{

    //--------------------------------------------- GENERALIZACAO--------------------------------------------

    //--------------------------------------------- STRUCT DADOS GENERALIZADA--------------------------------------------
    public interface IData                                   //dados abstrato para generalizar
    {

    }

    //--------------------------------------------- ORIGINADOR GENERALIZADO--------------------------------------------
    public abstract class Originator : MonoBehaviour
    {
    //--------------------------------------------- SALVAR DADOS--------------------------------------------
        public abstract Memento Save();                                                                             //SALVA SNAPSHOT
    //--------------------------------------------- RECUPERAR DADOS--------------------------------------------
        public abstract void Recover(Memento memento);                                                              //RECUPERA SNAPSHOT

    //--------------------------------------------- PEGAR DADOS GENERALIZADOS--------------------------------------------
        protected static void GetData<T>(Dictionary<string, T> dataContainer, GameObject[] objects)                 //COLOCA SNAPSHOT NO DICIONARIO 
        {

            foreach (GameObject child in objects)                                                                   //PERCORRE VETOR OBJETOS CUJO ESTADO DEVE SER SALVO
            {
                Originator container = child.GetComponent<Originator>();                                            //PEGA O GERADOR DE SNAPSHOT

                if (container != null)                                                                              //SE TIVER
                {
                    Memento memento = container.Save();                                                             //PEGA SNAPSHOT DO ESTADO ATUAL
                    print(child.name);
                    if (memento is T tipoCerto && memento != null)                                                                     //VERIFICA QUE O TIPO É O MESMO E COLOCA NA VARIAVEL GENERICA
                        dataContainer[child.name] = tipoCerto;                                                      //COLOCA SNAPSHOT NO DICIONARIO INDICANDO NOME DO OBJETO CUJO DADOS FORAM SALVOS
                }

            }
        }

        protected static void SetData<T>(Dictionary<string, T> dataContainer, GameObject[] objects) where T:Memento //COLOCA SNAPSHOT NO DICIONARIO, GENERICO SO PODE SER MEMENTO
        {

            foreach (GameObject child in objects)                                                                   //PERCORRE VETOR OBJETOS CUJO ESTADO DE SER RESTAURADO
            {
                Originator container = child.GetComponent<Originator>();                                            //PEGA O GERADOR DE SNAPSHOT

                if (container != null)                                                                              //SE TIVER
                {
                    if (dataContainer.TryGetValue(child.name, out T memento)){                                       //VERIFICA SE E POSSIVEL PEGAR VALOR NO MAPA                                                           //VERIFICA QUE O TIPO E O MESMO E COLOCA EM VARIAVEL MEMENTO
                            container.Recover(memento);                                                               //PASSA SNAPSHOT PARA SER RESTAURADA
                    }


                }
            }
        }

        //--------------------------------------------- MEMENTO GENERALIZADO--------------------------------------------
        public abstract class Memento
        {
            internal abstract IData GetState();                //PEGAR DADOS DE STATE
                                                               //NAO PODE DEIXAR CONSTRUTOR ABSTRATO

        }



    }



}