using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MC : Character
{

    //VARIÁVEIS

    //public Portrait hurt
    
    //controles
    public Key interactButton = Key.W;
    public Key moveLeftButton = Key.A;
    public Key moveRightButton = Key.D;
    public Key jumpButton = Key.Space;

    //camera
    public GameObject charCamera;

    //status
    public int maxLife = 10;
    protected int Life = 10;
    public int speed = 10;
    private int dir = 0;

    //flags
    protected bool flagMove = false;
    protected bool flagJump = false;

    //auxiliares
    private double prevX;

    //INICIALIZAÇÃO -----------------------------------------------------------------------------------------  
    void Ini()
    {
        prevX = transform.position.x;
        Pause(false);

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
        SendMessage("RigidBodyMoveX",dir * Time.fixedDeltaTime* speed);         //mover
        float dist = (float) (transform.position.x - prevX);                    //calcular distância que conseguiu mover
        prevX = transform.position.x;                                           //salvar posição atual
        if (charCamera != null)
        {
            charCamera.SendMessage("HorizontalMove", dist);                     //mover câmera conectada ao personagem
        }
        
    }
    public void PhysicalGameplay()
    {
        //print("Entrou cálculo física");
        if (flagMove)
        {
            //print("Movimento habilitado");
            Move();                                                             //mover rig body
        }
    }


    //DETECTAR INPUTS -----------------------------------------------------------------------------------------
    public void Gameplay()
    {
        print("Entrou gameplay");
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
