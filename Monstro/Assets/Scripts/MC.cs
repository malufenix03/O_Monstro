using System;
using NUnit.Framework.Constraints;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using UnityEngine.UIElements;

//--------------------------------------------- CONTROLLER PERSONAGEM PRINCIPAL -------------------------------------------- 

public class MC : Character
{

    //VARIAVEIS

    //public Portrait hurt

    //controles
    public Key interactButton = Key.W;
    public Key moveLeftButton = Key.A;
    public Key moveRightButton = Key.D;
    public Key jumpButton = Key.Space;

    //camera
    public GameObject charCamera;

    //chao
    public Collider2D ground;

    //sprite
    private SpriteRenderer sprite;
    private Collider2D spriteCollider;
    //public Sprite SideView;
    //public Sprite BackView;

    //status
    public int maxLife = 10;
    protected int Life = 10;
    public int speed = 10;
    public int jumpSpeed = 10;
    private float dir = 0;

    //flags
    protected bool flagMove = false;
    protected bool flagJump = false;

    //auxiliares
    private double prevX;
    private Vector2 moveAmount;
    private bool iniJump;

    //INICIALIZAÇÃO -----------------------------------------------------------------------------------------  
    void Ini()
    {
        prevX = transform.position.x;                //ultima posicao x
        sprite = GetComponent<SpriteRenderer>();    //pega sprite renderer

        spriteCollider = GetComponent<Collider2D>();
        Pause(false);                               //jogo despausado
        iniJump = false;                            //nao esta iniciando um pulo
    //    sprite.sprite = SideView;

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
        print("Direcao: "+dir);
        if (dir > 0)
        {
        //    sprite.sprite = SideView;
            SendMessage("CustomTrigger", "Right");
            sprite.flipX = false;
            
        }
        else if (dir < 0)
        {
        //    sprite.sprite = SideView;
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
        if (OnGround())                                             //se estiver no chao e nao estiver iniciando pulo
        {
            Debug.LogWarning("chao");
            if (!iniJump)
            {
                SendMessage("CustomBool", ("Airborne", false));    //parar animacao pulo 
            }

                                               
        }
        else
        {
            iniJump = false;                                                    //terminou de iniciar pulo
            Debug.LogWarning("ceu");
        }
    }

    //CHECAR SE ESTÁ NO CHAO -----------------------------------------------------------------------------------------
    bool OnGround()
    {
        return spriteCollider.IsTouching(ground);                               //checa se esse collider está tocando o chão
    }


    //DETECTAR INPUTS -----------------------------------------------------------------------------------------

    public void OnMove(InputAction.CallbackContext context)                    //detecta input de movimento
    {
        if (flagMove)
        {
            moveAmount = context.ReadValue<Vector2>();
            dir = moveAmount[0];
        }
    }

    public void OnJump(InputAction.CallbackContext context)                     //detecta input de pulo
    {
        if (flagJump && OnGround())
        {
            iniJump = true;
            SendMessage("RigidBodyMoveY", jumpSpeed);
            SendMessage("CustomBool",("Airborne", true));
            print("PULOOOOOOOOOOOOOOOOOOOO");
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (flagInteract)
        {
            SendMessage("CustomTrigger", "Interact");
        }
    }

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
        //Gameplay();
    }
    void FixedUpdate()
    {
        PhysicalGameplay();
    }
}
