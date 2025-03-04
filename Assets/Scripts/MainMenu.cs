using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public ManagerNiveles managerNiveles;
    public void Start()
    {
        AudioManager.instance.PlayMusic("MusicaMenu");
    }
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
}
