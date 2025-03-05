using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FanJump : MonoBehaviour
{
    public enum Direction { Arriba, Abajo, Derecha, Izquierda };
    public PlayerMovementNew playerMov;
    public Direction currDirection;
    public Rigidbody2D rb;
    private float velocityY = 35;
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
        if (currDirection == Direction.Arriba && rb.velocity.x == 0f)
        {
            AudioManager.instance.PlayFX("Air");
            rb.velocity = new Vector2(rb.velocity.x, velocityY);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (playerMov.getFanned() == false)
        {
            if (currDirection == Direction.Izquierda)
            {
                AudioManager.instance.PlayFX("Air");
                playerMov.setFanned(true);
                rb.velocity = new Vector2(45f, rb.velocity.y);
            }
            if (currDirection == Direction.Derecha)
            {
                AudioManager.instance.PlayFX("Air");
                playerMov.setFanned(true);
                rb.velocity = new Vector2(-10f, rb.velocity.y);
            }
            if (currDirection == Direction.Abajo)
            {
                AudioManager.instance.PlayFX("Air");
                rb.velocity = new Vector2(rb.velocity.x, -velocityY / 2);
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