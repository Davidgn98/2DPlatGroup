using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    //public GameObject[] corazonesImages;
    private GameObject[] frutas;
    //public TMP_Text numFrutasTotal;
    //public TMP_Text numFrutas;
    private GameObject numFrutasTotal;
    private GameObject numFrutas;
    private TMP_Text numFrutasText;
    private TMP_Text numFrutasTextTotal;
    private int numFrutasInt;
    private void Awake()
    {
        PlayerPrefs.SetInt("MaxLevel", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        numFrutas = GameObject.FindWithTag("numFrutas");
        numFrutasText = numFrutas.GetComponent<TMP_Text>();
        numFrutasText.text = "0";
        numFrutasTotal = GameObject.FindWithTag("numFrutasTotal");
        numFrutasTextTotal = numFrutasTotal.GetComponent<TMP_Text>();
        frutas = GameObject.FindGameObjectsWithTag("Fruit");
        numFrutasTextTotal.text = frutas.Length.ToString();
    }

    //public void ActualizarCorazones()
    //{
    //    UnityEngine.Debug.Log("-1 vida");
    //    for (int i = 0; i < corazonesImages.Length; i++)
    //    {
    //        if (corazonesImages[i].activeSelf)
    //        {
    //            UnityEngine.Debug.Log("entra");
    //            corazonesImages[i].gameObject.SetActive(false); // Desactiva la imagen del corazón
    //            break;
    //        }

    //    }
    //}

    public void UpdateNumFrutas()
    {
        ResetCanvas();
        numFrutasInt = int.Parse(numFrutasText.text);
        numFrutasInt += 1;
        numFrutasText.text = numFrutasInt.ToString();
    }

    public void Restart()
    {
        numFrutasText.text = "0";
        numFrutasInt = 0;
    }

    public void ResetCanvas()
    {
        numFrutas = GameObject.FindWithTag("numFrutas");
        numFrutasText = numFrutas.GetComponent<TMP_Text>();
    }


}
