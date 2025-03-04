using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour, IIteam
{
    public Frutas fruta;
    //private int puntuacion;
    
    public void Collect()
    {
        Destroy(gameObject);  
    }

    public enum Frutas
    {
        platano,
        manzana,
        melon,
        cereza
    }
    private void Start()
    {
        //switch (fruta) 
        //{ 
        //    case Frutas.platano:puntuacion = 1; break;
        //    case Frutas.manzana: puntuacion = 2; break;
        //    case Frutas.cereza: puntuacion = 3; break;
        //    case Frutas.melon: puntuacion = 4; break;
        //}
    }


}
