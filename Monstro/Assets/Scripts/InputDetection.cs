/*
using UnityEngine;
using UnityEngine.InputSystem;

public struct ActionType                    //tecla a detectar e acao a ser chamada
{
    public string Function;                 //funcao de gameplay a ser chamada
    public 

}

//--------------------------------------------- CONTROLLER PERSONAGEM PRINCIPAL -------------------------------------------- 

public class InputDetection : MonoBehaviour
{

    //controles
    private Keyboard keyboard;

    private Key[] activeKeys;
    public Key interactButton = Key.W;
    public Key moveLeftButton = Key.A;
    public Key moveRightButton = Key.D;
    public Key jumpButton = Key.Space;



    //INICIALIZAÇÃO -----------------------------------------------------------------------------------------  
    void Ini()
    {
        prevX = transform.position.x;                //ultima posicao x
        sprite = GetComponent<SpriteRenderer>();    //pega sprite renderer
        Pause(false);                               //jogo despausado
        sprite.sprite = SideView;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Ini();
    }

    //PAUSAR -----------------------------------------------------------------------------------------    
    override public void Pause(bool paused)
    {
        flagInteract = !paused;
        flagInteractable = !paused;
        flagJump = !paused;
        flagMove = !paused;
    }

    //REALIZAR MOVIMENTO -----------------------------------------------------------------------------------------
    void Move()
    {
        //print("Movendo x em " + dir);
        SendMessage("RigidBodyMoveX", dir * Time.fixedDeltaTime * speed);       //mover
        float dist = (float)(transform.position.x - prevX);                     //calcular distância que conseguiu mover
        prevX = transform.position.x;                                           //salvar posição atual
        if (charCamera != null)
        {
            charCamera.SendMessage("HorizontalMove", dist);                     //mover câmera conectada ao personagem
        }

    }

    public void MoveAnimation()
    {
        print("Direcao: " + dir);
        if (dir > 0)
        {
            sprite.sprite = SideView;
            SendMessage("CustomTrigger", "Right");
            sprite.flipX = false;

        }
        else if (dir < 0)
        {
            sprite.sprite = SideView;
            SendMessage("CustomTrigger", "Left");
            sprite.flipX = true;

        }
        else
        {
            SendMessage("CustomTrigger", "Still");
            //sprite.flipX = false;
        }

    }

    public void PhysicalGameplay()
    {
        //print("Entrou cálculo física");
        if (flagMove)
        {
            //print("Movimento habilitado");
            Move();                                                             //mover rig body
            MoveAnimation();                                                    //iniciar animacao movimento
        }
    }


    //DETECTAR INPUTS -----------------------------------------------------------------------------------------
    public void Gameplay()
    {
        print("Entrou gameplay");
        if (flagInteract)
        {
            //            print("Interacao habilitado");
            if (Keyboard.current[interactButton].isPressed)
            {
                SendMessage("CustomTrigger", "Interact");
            }
        }


        if (flagMove)
        {
            //            print("Movimento habilitado");
            dir = 0;
            if (Keyboard.current[moveLeftButton].isPressed)
                dir += -1;
            if (Keyboard.current[moveRightButton].isPressed)
                dir += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Gameplay();
    }
    void FixedUpdate()
    {
        PhysicalGameplay();
    }
}
*/