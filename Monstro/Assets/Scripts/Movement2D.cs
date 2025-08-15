
using UnityEngine;


//--------------------------------------------- MOVIMENTAÇÃO GERAL --------------------------------------------
public class Movement2D : MonoBehaviour
{

    //VARIÁVEIS

    protected Rigidbody2D rig;


    //INICIALIZAÇÃO -----------------------------------------------------------------------------------------------------------

    protected void GetRig()
    {
        rig = GetComponent<Rigidbody2D>();
        if (rig != null)
        {
            rig.freezeRotation = true;
        }
    }
    void Start()
    {
        GetRig();
    }

    //MOVIMENTO HORIZONTAL COM FÍSICA -----------------------------------------------------------------------------------------

    protected void RigidBodyMoveX(float dist)
    {
        rig.linearVelocityX = dist;
    }
    protected void RigidBodyMoveX((int, float) pack)
    {
        (int speed, float dir) = pack;
        rig.linearVelocityX = speed * dir * Time.fixedDeltaTime;
    }

    //MOVIMENTO VERTICAL COM FÍSICA -----------------------------------------------------------------------------------------

    protected void RigidBodyMoveY(float dist)
    {
        rig.linearVelocityY = dist;
    }
    protected void RigidBodyMoveY(int speed)
    {
        rig.linearVelocityY = speed * Time.fixedDeltaTime;
    }

    //MOVIMENTO HORIZONTAL SEM FÍSICA -----------------------------------------------------------------------------------------

    protected void SimpleMoveX((int, float) pack)
    {
        (int speed, float dir) = pack;
        transform.Translate(dir * Time.deltaTime * speed, 0, 0);
        //print("Mover para " + transform.position);
    }
    protected void SimpleMoveX(float dist)
    {
        transform.Translate(dist, 0, 0);
        //print("Mover para " + transform.position);
    }

    //TELEPORTE -----------------------------------------------------------------------------------------
    protected void Teleport(Vector3 pos)
    {
        transform.position = pos;
        //print("Teleporte para " + pos);
    }
    protected void TeleportX(float pos)
    {
        transform.position = new Vector3(pos, transform.position.y, transform.position.z);
        //print("Teleporte para " + pos);
    }

    protected void TeleportY(float pos)
    {
        transform.position = new Vector3(transform.position.x, pos, transform.position.z);
        //print("Teleporte para " + pos);
    }

    protected void Teleport(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

    //DESLIZAR -----------------------------------------------------------------------------------------
    protected void SlideX((float,float) pack)
    {
        (float speed, float pos) = pack;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(pos, transform.position.y, transform.position.z), speed * Time.deltaTime);
    }


}
