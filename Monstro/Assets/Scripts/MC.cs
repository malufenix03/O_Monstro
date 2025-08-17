using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static GameSettings;

//--------------------------------------------- CONTROLLER PERSONAGEM PRINCIPAL -------------------------------------------- 

public class MC : Character
{

    //VARIAVEIS

    //public Portrait hurt

    //camera
    public GameObject charCamera;

    //chao
    public List <Collider2D> ground;

    //barra de vida
    [SerializeField]
    private GameObject LifeBar;

    //objeto alvo interacao
    public GameObject Target { private get; set; }          //objeto que esta na frente para interagir
    public GameObject back;                                 //objeto que aparece para voltar na cena

    //menu
    public Menu activeMenu;                           //menu que está ativo na tela
    public Menu pauseMenu;                            //menu de pause

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
        prevX = GetX(transform);                                                   //posicao x
        sprite = GetComponent<SpriteRenderer>();                                        //pega sprite renderer
        spriteCollider = GetComponent<Collider2D>();                                    //pega collider

        Pause.AddListener(OnPause);                                                     //adiciona OnPause ao evento pause
        Unpause.AddListener(OnUnpause);                                                 //adiciona OnUnpause ao evento unpause
        PausePlayer(false);                                                             //jogo despausado
        iniJump = false;                                                                //nao esta iniciando um pulo
        Life = maxLife;                                                                 //vida inicial = vida maxima


    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Ini();
    }

    //PAUSAR -----------------------------------------------------------------------------------------    
    override public void PausePlayer(bool paused)
    {
        flagInteract = !paused;
        flagInteractable = !paused;
        flagJump = !paused;
        flagMove = !paused;
    }
    void OnPause()
    {
        PausePlayer(true);
    }
    void OnUnpause()
    {
        PausePlayer(false);
    }

    //RESETAR SPRITE -----------------------------------------------------------------------------------------    
    public void ResetSprite()                     //RESETAR SPRITE PARA FRENTE NAO INVERTIDO
    {
        SendMessage("ResetAllTriggers");
        SendMessage("CustomBool", ("Airborne", false));
        SendMessage("CustomTrigger", "Reset");
        sprite.flipX = false;
    }

    //RETORNAR AO JOGO
    public void ReturnToGame()                     //RETOMAR JOGO   
    {
        if (activeMenu != null && activeMenu?.menu != null)                              //se tem um menu aberto
        {
            ResetSprite();
            activeMenu.Leave();                                                     //fecha menu
        }
        else
        {
            //print("Pausa");
            pauseMenu.Enter();
        }
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
        float dist = (float)(GetX(transform) - prevX);                     //calcular distância que conseguiu mover
        prevX = GetX(transform);                                           //salvar posição atual
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

    public void OnMove(InputAction.CallbackContext context)                     //detecta input de movimento
    {
                                     
            if (flagMove && context.performed)                                 //se estiver pressionado, pode mover
            {
                moveAmount = context.ReadValue<Vector2>();
                dir = moveAmount[0];
            }
            else
                dir = 0;                                                      //para de andar se nao pode mover      
    }

    public void OnJump(InputAction.CallbackContext context)                     //detecta input de pulo
    {
        //print("Apertou espaco flag= "+ flagJump + " no chao=" + OnGround());
        if (flagJump && OnGround())
        {
            //print("Entrou if");
            iniJump = true;
            SendMessage("RigidBodyMoveY", jumpSpeed);
            SendMessage("CustomBool", ("Airborne", true));
        }
    }

    public void OnInteract(InputAction.CallbackContext context)                 //detecta input interacao
    {
        if (flagInteract && Target!=null && context.started)                    //se estiver na frente de um objeto, puder interagir e acabou de pressionar botao
        {
            sprite.flipX =false;
            SendMessage("CustomTrigger", "Interact");
            Target.SendMessage("Interact");
        }
    }

    public void OnBack(InputAction.CallbackContext context)                     //detecta input voltar de onde veio
    {
        if (flagInteract && back != null && context.started)                    //se estiver na sala da interface back, puder interagir e acabou de pressionar botao
        {
            ResetSprite(); 
            back?.SendMessage("MoveAnotherRoom");                                   //ativa evento voltar de onde veio
        }
    }

    public void OnEscape(InputAction.CallbackContext context)                     //detecta input pause/sair menu de onde veio
    {
        if (context.canceled)                                                                                        //se deixou de pressionar o botao
            ReturnToGame();
    }

    //CONTROLE DE VIDA -----------------------------------------------------------------------------------------
    public void TakeDamage(int damage)                                                    //levar dano
    {
        Life -= damage;
        if (Life <= 0)                                                                    //se zerou vida, game over
            GameOver();
        LifeBar.SendMessage("ChangeBar", (Life, maxLife));
    }

    public void Heal(int lifeHeal)                                                       //curar
    {
        Life += lifeHeal;
        Life = (Life > maxLife) ? maxLife : Life;                                        //se ficar com  vida maior que vida máxima, deixa vida igual a vida máxima
        LifeBar.SendMessage("ChangeBar", (Life, maxLife));
    }

    void GameOver() {
        
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
