using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerNiveles : MonoBehaviour
{
    //El nivel 1 es el nivel 0, el nivel 2 es el nivel 1 y asi...
    private  int maxLevel;


    void Start()
    {

        //PlayerPrefs.SetInt("MaxLevel", 0);
        //PlayerPrefs.Save();

        var botonesHijos = gameObject.GetComponentsInChildren<Button>();


        for(int i = 0; i < botonesHijos.Length; i++) 
        {
            botonesHijos[i].interactable = false;
        
        }

        maxLevel = PlayerPrefs.GetInt("MaxLevel", 1);
        //print(maxLevel);
        for (int i = 0; i < botonesHijos.Length; i++)
        {
            if (i<=maxLevel) 
            {
                botonesHijos[i].interactable = true;
            }
        }
    }

    public void StartBouton() 
    {
        AudioManager.instance.StopMusic("MusicaMenu");
        AudioManager.instance.PlayMusic("Level" + (maxLevel+1));
        SceneManager.LoadScene(maxLevel);
    }




}
