using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    //VARIÁVEIS

    [System.Serializable]
    public class ClipData{
        public AnimationClip Animantion;
        public AudioClip Audio;
        public float AudioDelay = 0f;
        public bool AutoGoToNext;
    }
    public ClipData[] Clips;
    private int m_ClipIdx = 0;

    //INICIALIZAÇÃO -----------------------------------------------------------------------------------------  
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnMouseDown(){
        StartCoroutine(PlayClip());
    }

    IEnumerator PlayClip(){
        GetComponent<Collider>().enabled = false;
        ClipData clip;
        do{
            clip = Clips[m_ClipIdx];
            GetComponentInParent<Animation>().Play(clip.Animantion.name);
            AudioSource audioSource = GetComponent<AudioSource>();
            if(audioSource != null){
                audioSource.clip = Clips[m_ClipIdx].Audio;
                audioSource.PlayDelayed(Clips[m_ClipIdx].AudioDelay);
            }
            m_ClipIdx = (m_ClipIdx +1) % Clips.Length;
            if(clip.AutoGoToNext){
                yield return new WaitForSeconds(clip.Animantion.length);
            }
        }while(clip.AutoGoToNext);
        GetComponent<Collider>().enabled=true;
    }
    
}
