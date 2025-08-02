
using System;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework.Internal;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

//--------------------------------------------- PIXELS NO SPRITE -------------------------------------------- 
public class PixelInfo : MonoBehaviour
{
    public int width;
    public int height;
    public int[] meio;
    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        width = (int)(sprite.rect.width / 9);
        height = (int)(sprite.rect.height / 9);
        meio = new int[2];
        (meio[0], meio[1]) = (width / 2, height / 2);
    }


}
