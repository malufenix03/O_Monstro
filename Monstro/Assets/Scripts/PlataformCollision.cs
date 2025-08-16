using UnityEngine;
using static GameSettings;


//--------------------------------------------- SO COLIDIR SE PLAYER CAIR EM CIMA DO OBJETO--------------------------------------------
public class PlataformCollision : MonoBehaviour
{
    Collider2D[] colliders;
    float altura;

    void Start()
    {
        colliders = GetComponentsInChildren<Collider2D>();                                          //pega todos os colliders da plataforma
        altura = colliders[0].bounds.center.y + colliders[0].bounds.extents.y;                      //pega a altura topo da plataforma
        
    }

    void Update()
    {
        Collider2D pCollider = player.GetComponent<Collider2D>();                       //pega collider player
        float alturaPe = pCollider.bounds.center.y - pCollider.bounds.extents.y;        //calcula a altura do pe em relacao ao collider
        if (alturaPe < altura)                                                          //se player esta para baixo da plataforma
        {
            foreach (Collider2D child in colliders)
                child.enabled = false;                                                  //desativa todos os colliders
        }
        else
        {
            foreach (Collider2D child in colliders)
                child.enabled = true;                                                   //ativa todos os colliders se player estiver em cima
        }
    }

}
