using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PajaroPerseguidor : MonoBehaviour
{

    //velocidad de movimiento
    public float speed;

    public Transform puntoA;
    public Transform puntoB;

    bool check = false;
    bool volver = false;


    // Update is called once per frame
    void Update()
    {
        
        //inicio
        if (!check && !volver)
        {
           
            Vector3 direction = puntoA.position - transform.position;
            direction.z = 0;
            transform.position += direction.normalized * speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, puntoA.position) < 0.1f)
            {
                check = true;
                

            }
        }
        //vuelta
        if (!check && volver)
        {
           
            Vector3 direction = puntoA.position - transform.position;
            direction.z = 0;
            transform.position += direction.normalized * speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, puntoA.position) < 0.1f)
            {
                check = true;
                volver = false;
                Flip();
            }
        }
        //volver a iniciar
        if (check)
        {
           
            Vector3 direction = puntoB.position - transform.position;
            direction.z = 0;
            transform.position += direction.normalized * speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, puntoB.position) < 0.1f)
            {
                check = false;
                volver = true;
                Flip();
            }
        }
    }

    private void Flip()
    {

           
            Vector3 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (puntoA != null && puntoB != null)
        {
            Gizmos.DrawLine(puntoA.position, puntoB.position);
        }
    }
}
