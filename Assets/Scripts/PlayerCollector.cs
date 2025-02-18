using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    public TextMeshProUGUI puntuacionPantalla;
    private bool hasBasket = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasBasket)
        {
            IIteam iteam = collision.gameObject.GetComponent<IIteam>();

            if (iteam != null)
            {
                iteam.Collect();
                GameManager.instance.UpdateNumFrutas();
            }
        }
        
    }

    public void UpdateBasket(bool basket)
    {
        hasBasket = basket;
    }
}
