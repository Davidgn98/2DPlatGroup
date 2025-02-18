using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaVoladora : MonoBehaviour
{
    //velocidad de movimiento
    public float speed;
    public Transform puntoA;
    public Transform puntoB;

    bool check = false;
    bool volver = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
        if (!check && volver)
        {
            Vector3 direction = puntoA.position - transform.position;
            direction.z = 0;
            transform.position += direction.normalized * speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, puntoA.position) < 0.1f)
            {
                check = true;
                volver = false;
            }
        }
        if (check)
        {
            Vector3 direction = puntoB.position - transform.position;
            direction.z = 0;
            transform.position += direction.normalized * speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, puntoB.position) < 0.1f)
            {
                check = false;
                volver = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        col.transform.SetParent(transform);  // Hace que el jugador siga la plataforma
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        col.transform.SetParent(null);  // El jugador deja de ser hijo de la plataforma

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
