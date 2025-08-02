using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.VisualScripting;

public class AnimationTrigger : MonoBehaviour
{
    public UnityEngine.Object[] animations;
    private Animator[] animationAnimator;
    private int id=0;
    // Start is called before the first frame update
    void Start()
    {
        animationAnimator = new Animator[animations.Length];
        if(animations.Length>0){
            for(int i=0;i<animations.Length; i++){
                animationAnimator[i] = animations[i].GetComponent<Animator>();
            }
                
            
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(){
        animationAnimator[id++].SetTrigger("nextTrigger");
        if(id==animations.Length)
            Die();
    }

    void Die(){
        GameObject.Destroy(gameObject);
    }
}
