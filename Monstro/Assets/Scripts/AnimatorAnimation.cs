using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Events;

public class AnimatorAnimation : MonoBehaviour
{
    //VARIAVEIS

    //animator
    private Animator animator;

    //vetor animacoes em sequencia
    public UnityEngine.Object[] proximo;
    private Animator[] proximoAnimator;
    private int id=0;
    
    //distancia trigger animacao
    public float TriggerDistance = 7f;

    //evento se animacao acabar
    public UnityEvent[] End;     //vetor eventos apos fim animacao
    public UnityEvent[] Begin;


    public bool flagNext = true;
    public string failTrigger;
    private bool firstInteraction=true;


    //INICIALIZACAO -----------------------------------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        //pegar animator desse objeto
        animator = GetComponent<Animator>();

        //pegar animator de todos objetos
        proximoAnimator = new Animator[proximo.Length];
        if(proximo.Length>0){
            for(int i=0;i<proximo.Length; i++){
                proximoAnimator[i] = proximo[i].GetComponent<Animator>();
            }
        }
    }


    //ATIVAR ANIMACAO -----------------------------------------------------------------------------------------

    //clique do mouse
    void OnMouseDown()
    {
        if(DistanceFromCamera() <= TriggerDistance){
            animator.SetTrigger("selectedTrigger");         //animacao ativada por clique
        }
    }

    //encostar no collider
    void OnTriggerEnter(){
        animator.SetTrigger("collisionTrigger");            //animacao ativada por colisao
    }

    void TriggerNext(){                                                     //serie de animacoes?
        if(flagNext){
            proximoAnimator[id++].SetTrigger("nextTrigger");
            id%=proximo.Length;
        }
        else{
            if(firstInteraction){
                SendMessage(failTrigger,-1);
                firstInteraction=false;
            }
                
        }

    }

    //mudar variaveis
    void CustomTrigger(string custom)
    {
        
        animator.SetTrigger(custom);            //animacao ativada por trigger recebido
    }

    void CustomBool((string,bool)pack)
    {
        (string custom, bool value) = pack;
        animator.SetBool(custom, value);        //mudar valor da variavel recebida
    }

    //EVENTOS ANIMACAO -----------------------------------------------------------------------------------------
    void OnAnimationEnd(int id)                                                                   //chama evento do id apos animacao nao invertida terminar 
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        float speed = state.speed * state.speedMultiplier;
        if (speed > 0)
        {
            End[id]?.Invoke();                  //se tem funcao para ser invocada, invoca
        }
    }

    void OnInverseAnimationEnd(int id)                                                            //chama evento apos animacao invertida terminar 
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        float speed = state.speed * state.speedMultiplier;
        if (speed < 0)
        {
            End[id]?.Invoke();                  //se tem funcao para ser invocada, invoca
        }
    }

    //RESETAR TRIGGER -----------------------------------------------------------------------------------------
    void ResetTrigger(string trigger)
    {
        animator.ResetTrigger(trigger);
    }

    void ResetAllTriggers()
    {
        foreach (var param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
                ResetTrigger(param.name);
        }
    }


    //CONTROLAR SOM ANIMACAO -----------------------------------------------------------------------------------------
    /*
        void PlaySoundBegin(UnityEngine.Object clip){                                                   //toca som no comeco animacao ---> window animator chama essa funcao?
            AudioSource audioSource = GetComponent<AudioSource>();                                      //pega componente de som desse objetor    //  <--- pode otimizar no start
            audioSource.clip=(AudioClip)clip;                                                           //seta som para tocar
            AnimatorStateInfo state = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);          //aponta para o estado do animator
            float speed = state.speed * state.speedMultiplier;                                          //velocidade do clipe de som
            if(speed >0){                                                                               
                GetComponent<Sound>().Play();                                                           //tocar som se não estiver animacao invertida
            }
        }

        void PlaySoundEnd(UnityEngine.Object clip){                                                     //toca som no comeco animacao ---> window animator chama essa funcao?
            AudioSource audioSource = GetComponent<AudioSource>();                                      //pega componente de som desse objetor    //  <--- pode otimizar no start
            audioSource.clip=(AudioClip)clip;                                                           //seta som para tocar
            AnimatorStateInfo state = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);          //aponta para o estado do animator
            float speed = state.speed * state.speedMultiplier;                                          //velocidade do clipe de som
            if(speed <0){
                GetComponent<Sound>().Play();                                                           //tocar som se estiver animacao invertida
            }
        }

        void PlaySoundReverse(UnityEngine.Object clip){
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip=(AudioClip)clip;
            AnimatorStateInfo state = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            audioSource.pitch = state.speed * state.speedMultiplier;
            if(audioSource.pitch <0){
                audioSource.time = audioSource.clip.length - 0.01f;
            }
            GetComponent<Sound>().Play();
        }

        void StopSound(UnityEngine.Object clip){
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip=(AudioClip)clip;
            audioSource.Stop();
        }
    */

    void Die(){
        GameObject.Destroy(gameObject);
    }

    float DistanceFromCamera(){
        Vector3 heading = transform.position - Camera.main.transform.position;
        float distance = Vector3.Dot(heading,Camera.main.transform.forward);
        return distance;
    }
}
