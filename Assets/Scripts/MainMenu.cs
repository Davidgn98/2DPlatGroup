using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public ManagerNiveles managerNiveles;
    private AudioManager audioManager;
    public void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        if (audioManager != null)
        {
            Debug.Log("AudioManager encontrado correctamente.");
            audioManager.PlayMenu();
        }
        else
        {
            Debug.Log("AudioManager no encontrado.");
        }

        
    }
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
