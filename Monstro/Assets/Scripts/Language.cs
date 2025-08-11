using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


//TRADUCAO -----------------------------------------------------------------------------------------

[Serializable]
public struct Language
{
    public string portugues;
    public string ingles;

    public Language(string padrao)
    {
        portugues = padrao;
        ingles = "";
    }

    public string GetTxt()
    {
        if (VarGlobal.currentLanguage == VarGlobal.languages.Portugues)
        {
            return portugues;
        }
        else
        {
            return ingles;
        }
    }
}