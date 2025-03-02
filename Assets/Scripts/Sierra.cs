using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sierra : MonoBehaviour
{

    public Transform pointA;
    public Transform pointB;
    public float speed;
    bool check = false;
    bool volver = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //inicio
        if (!check && !volver)
        {

            Vector3 direction = pointA.position - transform.position;
            direction.z = 0;
            transform.position += direction.normalized * speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, pointA.position) < 0.1f)
            {
                check = true;


            }
        }
        //vuelta
        if (!check && volver)
        {

            Vector3 direction = pointA.position - transform.position;
            direction.z = 0;
            transform.position += direction.normalized * speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, pointA.position) < 0.1f)
            {
                check = true;
                volver = false;
                Flip();
            }
        }
        //volver a iniciar
        if (check)
        {

            Vector3 direction = pointB.position - transform.position;
            direction.z = 0;
            transform.position += direction.normalized * speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, pointB.position) < 0.1f)
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
        if (pointA != null && pointB != null)
        {
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }

}
