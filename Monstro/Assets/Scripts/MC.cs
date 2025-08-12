using System;
using NUnit.Framework.Constraints;
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

/*
    //controles
    public Key interactButton = Key.W;
    public Key moveLeftButton = Key.A;
    public Key moveRightButton = Key.D;
    public Key jumpButton = Key.Space;
    */

    //camera
    public GameObject charCamera;

    //chao
    public Collider2D[] ground;

    //objeto alvo interacao
    public GameObject Target { private get; set; }
    public GameObject back;

    //sprite
    private SpriteRenderer sprite;
    private Collider2D spriteCollider;

    //status
    public int maxLife = 10;
    protected int Life = 10;
    public int speed = 10;
    public int jumpSpeed = 10;
    private float dir = 0;

    //flags
    protected bool flagMove = false;
    protected bool flagJump = false;
    public bool camLock = false;

    

    //auxiliares
    private double prevX;
    private Vector2 moveAmount;
    private bool iniJump;

    //INICIALIZAÇÃO -----------------------------------------------------------------------------------------  
    void Ini()
    {
        prevX = transform.position.x;                                                   //ultima posicao x
        sprite = GetComponent<SpriteRenderer>();                                        //pega sprite renderer
        spriteCollider = GetComponent<Collider2D>();                                    //pega collider

        VarGlobal.Pause.AddListener(OnPause);                                           //adiciona OnPause ao evento pause
        VarGlobal.Unpause.AddListener(OnUnpause);                                       //adiciona OnUnpause ao evento unpause
        Pause(false);                                                                   //jogo despausado
        iniJump = false;                                                                //nao esta iniciando um pulo
        Life = maxLife;                                                                 //vida inicial = vida maxima


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
    void OnPause()
    {
        Pause(true);
    }

    void OnUnpause()
    {
        Pause(false);
    }


    //REALIZAR TELEPORTE -----------------------------------------------------------------------------------------
    void MoveTo(Vector3 newPos)
    {
        prevX = newPos.x;
        SendMessage("Teleport", newPos); 
    }

    //REALIZAR MOVIMENTO -----------------------------------------------------------------------------------------
    void Move()
    {
        //print("Movendo x em " + dir);
        SendMessage("RigidBodyMoveX", dir * Time.fixedDeltaTime * speed);       //mover
        float dist = (float)(transform.position.x - prevX);                     //calcular distância que conseguiu mover
        prevX = transform.position.x;                                           //salvar posição atual
        if (charCamera != null && !camLock)                                     //se tem camera e esta destrancada
        {
            charCamera.SendMessage("HorizontalMove", dist);                     //mover câmera conectada ao personagem
            //charCamera.SendMessage("VerticalMove", dist);
        }

    }

    public void MoveAnimation()
    {
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
            if (!iniJump)
            {
                SendMessage("CustomBool", ("Airborne", false));    //parar animacao pulo 
            }

                                               
        }
        else
            iniJump = false;                                                    //terminou de iniciar pulo (detectou que saiu do chao) --> erro se pular e nao conseguir sair chao
        
    }

    //CHECAR SE ESTÁ NO CHAO -----------------------------------------------------------------------------------------
    bool OnGround()
    {
        foreach (Collider2D part in ground)
            if (spriteCollider.IsTouching(part))
                return true;                                //checa se esse collider está tocando o chão
        return false;                                       //se nao tocando nada que e chao nessa cena
    }




    //DETECTAR INPUTS -----------------------------------------------------------------------------------------

    public void OnMove(InputAction.CallbackContext context)                    //detecta input de movimento
    {
        moveAmount = context.ReadValue<Vector2>();                              //para nao ler depois com atraso se estava pressionado
        if (flagMove)
        {
            dir = moveAmount[0];
        }
    }

    public void OnJump(InputAction.CallbackContext context)                     //detecta input de pulo
    {
        print("Apertou espaco flag= "+ flagJump + " no chao=" + OnGround());
        if (flagJump && OnGround())
        {
            print("Entrou if");
            iniJump = true;
            SendMessage("RigidBodyMoveY", jumpSpeed);
            SendMessage("CustomBool", ("Airborne", true));
        }
    }

    public void OnInteract(InputAction.CallbackContext context)                 //detecta input interacao
    {
        if (flagInteract && Target!=null)
        {
            sprite.flipX =false;
            SendMessage("CustomTrigger", "Interact");
            Target.SendMessage("Interact");
        }
    }

    public void OnBack(InputAction.CallbackContext context)                     //detecta input voltar de onde veio
    {
        if (flagInteract && back != null)
        {
            SendMessage("CustomTrigger", "Reset"); 
            sprite.flipX =false;
            back?.SendMessage("MoveAnotherRoom");                                   //ativa evento voltar de onde veio
        }
    }
    

/*
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
                    */

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
