
using System;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework.Internal;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

//--------------------------------------------- PIXELS NO SPRITE -------------------------------------------- 
public class Teste : MonoBehaviour
{
    void Start()
    {
        var parent = GetComponents<Collider2D>();
        foreach (Collider2D child in parent)
        {
            print(child.bounds.size);
        }
    }


}
