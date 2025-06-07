using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MC : Character
{
    //public Portrait hurt
    public Key interactButton = Key.W;
    public Key moveLeftButton = Key.A;
    public Key moveRightButton = Key.D;
    public Key jumpButton = Key.Space;

    public GameObject charCamera;
    private double prevX;

    public int maxLife = 10;
    protected int Life = 10;

    public int speed = 10;

    protected bool flagMove = false;

    protected bool flagJump = false;

    override public void Pause(bool paused)
    {
        flagInteract = !paused;
        flagInteractable = !paused;
        flagJump = !paused;
        flagMove = !paused;
    }

    void Move(int dir)
    {
        print("Movendo x em " + dir);
        transform.Translate(dir * Time.deltaTime * speed, 0, 0);
        double dist = transform.position.x - prevX;
        prevX = transform.position.x;
        if (charCamera != null)
        {
            charCamera.SendMessage("HorizontalMove", dist);
        }
        
    }

    void Gameplay()
    {
        print("Entrou gameplay");
        if (flagMove)
        {
            print("Movimento habilitado");
            
            if (Keyboard.current[moveLeftButton].isPressed)
                Move(-1);
            if (Keyboard.current[moveRightButton].isPressed)
                Move(1);
        }
    }

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

    // Update is called once per frame
    void Update()
    {
        Gameplay();
    }
    
}
