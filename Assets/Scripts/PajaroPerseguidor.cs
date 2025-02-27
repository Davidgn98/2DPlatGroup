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

    private BoxCollider2D boxCollider2DGhostBody;
    private BoxCollider2D boxCollider2DGhostHead;


    private void Start()
    {
        boxCollider2DGhostBody = gameObject.GetComponent<BoxCollider2D>();
        boxCollider2DGhostHead = GameObject.FindGameObjectWithTag("CabezaEnemigo").GetComponent<BoxCollider2D>();

        StartCoroutine("ChangeVisualStatus");
    }

    private IEnumerator ChangeVisualStatus()
    {

        if (gameObject.CompareTag("Ghost"))
        {
            while (true)
            {
                boxCollider2DGhostBody.enabled = true;
                boxCollider2DGhostHead.enabled = true;
                GetComponent<Animator>().SetTrigger("Appear");
                yield return new WaitForSeconds(5f);
                boxCollider2DGhostBody.enabled = false;
                boxCollider2DGhostHead.enabled = false;
                GetComponent<Animator>().SetTrigger("Desappear");
                yield return new WaitForSeconds(5f);
            }
        }
    }
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
