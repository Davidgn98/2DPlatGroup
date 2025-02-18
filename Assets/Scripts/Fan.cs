using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fan : MonoBehaviour
{
    public enum Direction { Arriba, Abajo, Derecha, Izquierda };
    public PlayerMovementNew playerMov;
    public Direction currDirection;
    public Rigidbody2D rb;
    private float velocityY = 50;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currDirection == Direction.Arriba && rb.velocity.x == 0f )
        {
            rb.velocity = new Vector2(rb.velocity.x, velocityY);
        }
        if (currDirection == Direction.Abajo)
        {
            rb.velocity = new Vector2(rb.velocity.x, -velocityY/2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerMov.getFanned() == false)
        {
            if (currDirection == Direction.Izquierda)
            {
                playerMov.setFanned(true);
                rb.velocity = new Vector2(10f, rb.velocity.y);
            }
            if (currDirection == Direction.Derecha)
            {
                playerMov.setFanned(true);
                rb.velocity = new Vector2(-10f, rb.velocity.y);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (playerMov.getFanned() == true)
        {
            playerMov.setFanned(false);
        }
    }


}
