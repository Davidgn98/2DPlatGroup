using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public GameObject menuPausa;
    // Start is called before the first frame update
    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (menuPausa.activeSelf)
            {
                menuPausa.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                menuPausa.SetActive(true);
                Time.timeScale = 0;
            }

        }
    }
}
