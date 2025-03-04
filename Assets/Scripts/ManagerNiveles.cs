using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerNiveles : MonoBehaviour
{
    //El nivel 1 es el nivel 0, el nivel 2 es el nivel 1 y asi...
    public int maxLevel;


    void Start()
    {
        PlayerPrefs.SetInt("MaxLevel", maxLevel);
        PlayerPrefs.Save();

        var botonesHijos=gameObject.GetComponentsInChildren<Button>();


        for(int i = 0; i < botonesHijos.Length; i++) 
        {
            botonesHijos[i].interactable = false;
        
        }

        maxLevel = PlayerPrefs.GetInt("MaxLevel", 1);
        for (int i = 0; i < botonesHijos.Length; i++)
        {
            if (i<=maxLevel) 
            {
                botonesHijos[i].interactable = true;
            }
        }
    }

    public void DesbloqeuarUnNivelMas()
    {
        PlayerPrefs.SetInt("MaxLevel", maxLevel + 1);
        PlayerPrefs.Save();
    }


}
