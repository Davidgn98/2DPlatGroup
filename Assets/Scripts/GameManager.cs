using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject[] corazonesImages;
    public GameObject[] frutas;
    public TMP_Text numFrutasTotal;
    public TMP_Text numFrutas;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ActualizarCorazones()
    {
        UnityEngine.Debug.Log("-1 vida");
        for (int i = 0; i < corazonesImages.Length; i++)
        {
            if (corazonesImages[i].activeSelf)
            {
                UnityEngine.Debug.Log("entra");
                corazonesImages[i].gameObject.SetActive(false); // Desactiva la imagen del corazón
                break;
            }

        }
    }

    //TODO Axel Audio
    private void Start()
    {
        //state = GameStates.Start;

        //// Set the initial visibility of UI elements
        //uiStart.SetActive(true);
        //uiHUD.SetActive(false);
        //uiEndMenu.SetActive(false);

        //// Set the initial scale of start menu elements
        //uiStart_ready.localScale = Vector3.zero;
        //uiStart_go.localScale = Vector3.zero;

        //// Set the starting value of the gameScore
        //gameScore = 0;

        //// Stop the music from Main Menu and play music of GameScene
        //AudioManager.instance.StopMusic("StageSelect");
        //AudioManager.instance.PlayMusic("Stage1");
        numFrutasTotal.text = frutas.Length.ToString();
    }

    public void UpdateNumFrutas()
    {
        int numFrutasInt = int.Parse(numFrutas.text);
        numFrutasInt += 1;
        numFrutas.text = numFrutasInt.ToString();
    }


}
