using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.Video;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.UI;
using TMPro;


public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;

    public Animator animator;
    public SpriteRenderer spriteRenderer;


    public int vida;
    public bool isKnockBack=false;

    [Header("Movement")]
    public float moventSpeed = 5.0f;
    float horizontalMovement;

    [Header("Jumping")]
    public float jumpPower = 10f;
    public int maxJumps = 2;

    [Header("Drop")]
    // Velocidad de la caída
    public float fallSpeed = 10f;

    [Header("KnockBack")]
    public float knockbackForce = 30f;  // Fuerza del retroceso
    public float knockbackDuration = 0.5f;  // Duración del retroceso


    [SerializeField]
    int jumpsRemaining;
    bool jumpLongPress=false;
    bool jumpShortPress=false;

    [Header("Platform")]
    public LayerMask platformLayer;
    [SerializeField]
    bool isPlatform;
    public TilemapCollider2D tilemapCollider2DPlatform;


    [Header("GroundCheck")]
    public UnityEngine.Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);
    public LayerMask groundLayer;

    [SerializeField]
    bool isGrounded;

    [Header("WallCheck")]
    public UnityEngine.Transform wallCheckPos;
    public Vector2 wallCheckSize = new Vector2(0.5f, 0.5f);
    public LayerMask wallLayer;
    [SerializeField]
    bool iswalled;

    [Header("Sliding")]
    public float wallSlideSpeed = 2f;
    [SerializeField]
    bool isWallSling;

    [Header("WallJumping")]
    public Vector2 wallJumpPower = new Vector2(5f, 10f);
    public float wallJumpTime = 0.25f;
    [SerializeField]
    public float wallumpTime;
    [SerializeField]
    public float wallJumpTimer;
    [SerializeField]
    bool isWallJumping;
    float wallJumpDirection;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float fallSpeedMultipler = 2f;

    [Header("Mirada")]
    public bool isFacingRigth = true;

    public UnityEngine.UI.Image basketUI;
    private PlayerCollector playerCollector;
    public Animator checkpointAnimator;
    public TMP_Text numFrutas;
    public TMP_Text numFrutasTotal;
    public Camera cam;

    private Animator pigAnimator;
    private void Start()
    {
        playerCollector = GetComponent<PlayerCollector>();
    }
    // Update is called once per frame
    void Update()
    {
        //comprobaciones
        GroundCheck();
        WallCheck();
        PlatformCheck();

        //Gravedad
        Gravity();
        WallSlide();

        //MOVIMIENTO
        WallJump();
        ProcessJump();

        //para evitar que el jugador durante el salto desde la paredse peuda mover y joder todo durante los 0.25 segundos que duran unas cosas
        if (!isWallJumping && !isKnockBack)
        {
            //movimiento del jugador
            rb.velocity = new Vector2(horizontalMovement * moventSpeed, rb.velocity.y);
            Flip();

            isKnockBack = false;
        }
        if (iswalled) 
        { 
            jumpsRemaining=maxJumps;
        
        }
        //Animaciones
        animator.SetFloat("magnitude", rb.velocity.magnitude);
        animator.SetFloat("Yvelocity", rb.velocity.y);
        animator.SetBool("isWallSliding", isWallSling);
    }


    //Voltear al personaje
    private void Flip() 
    {
        if(isFacingRigth==true && horizontalMovement<0f || !isFacingRigth && horizontalMovement > 0) 
        { 
            isFacingRigth=!isFacingRigth;
            Vector3 ls=transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
    }

    //Metodo que se le pasa al PlayerInput para registrar el movimiento en horizontal
    public void GetMoveInputAction(InputAction.CallbackContext context) 
    {
        horizontalMovement= context.ReadValue<Vector2>().x;
    }

    private void GroundCheck() 
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            if (Mathf.Abs(rb.velocity.y) < 0.01f)
            {
                isGrounded = true;
                jumpsRemaining = maxJumps;
            }
        }
        else
        {
            isGrounded = false;
        }
    }

    //Metodo que se le pasa al PlayerInput para registrar el salto
    public void GetJumpInputAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (jumpsRemaining != 0)
            {
                jumpLongPress = true;
            }
        }
        if (context.canceled)
        {
            if (context.duration < 0.25f)
            {
                if (jumpsRemaining != 0)
                {
                    jumpShortPress = true;
                }
            }
        }
    }

    public void GetDropInputAction(InputAction.CallbackContext context)
    {
        if (context.started && isPlatform)
        {
            StartCoroutine(EsperarSegundos());
        }

    }
    IEnumerator EsperarSegundos()
    {
        tilemapCollider2DPlatform.enabled = false;

        yield return new WaitForSeconds(0.4f);  // Espera 0.5 segundos

        tilemapCollider2DPlatform.enabled = true;



    }

    public void PlatformCheck() 
    {
       isPlatform = Physics2D.OverlapBox(groundCheckPos.position, wallCheckSize, 0, platformLayer);

        if (isPlatform) 
        {
            jumpsRemaining = maxJumps;
        }

    }


    private void WallCheck()
    {
        //me pone en true o false si mi cuadrado, en una posicion concreta, con un tamaño concreto y un angulo concreto(0), en una layer concreto, esta seindo colisionado
        iswalled = Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer);
    }

    private void ProcessJump() 
    {
        if (jumpsRemaining > 0 && !isWallSling)
        {
            if (jumpLongPress)
            {
                // Si quedan saltos pendientes y se ha producido una pulsación larga -> saltamos
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpsRemaining--;       // Actualizamos los saltos pendientes
                jumpLongPress = false;  // Reseteamos la variable de pulsación larga
                animator.SetTrigger("jump");
            }
            else if (jumpShortPress)
            {
                // Si se ha producido una pulsación corta, reducimos el salto
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                jumpShortPress = false;
            }
        }
        //salto desde el muro
        if(jumpLongPress && jumpsRemaining > 0 && isWallSling) 
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            jumpLongPress=false;
            jumpShortPress=false;
            animator.SetTrigger("jump");

            if (transform.localScale.x != wallJumpDirection) 
            { 
                //Hacemos un flip (se puede sustituir por el metodo flip en teoria)
                isFacingRigth = !isFacingRigth;
                Vector3 ls = transform.localScale;
                ls.x *= -1;

                transform.localScale = ls;
            
            }
        }

    }

    private void Gravity() 
    {

        if (rb.velocity.y < 0f)
        {
            rb.gravityScale = baseGravity * fallSpeedMultipler;
        }
        else 
        {
            rb.gravityScale =baseGravity;
        }
    
    }

    private void WallSlide() 
    {
        if (iswalled && !isGrounded && horizontalMovement != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -wallSlideSpeed));
            isWallSling = true;
            jumpsRemaining = 1;

        }
        else 
        { 
            isWallSling=false;
        }
    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlataformaVoladora") 
        {
            jumpsRemaining = maxJumps;
        }
        if(collision.gameObject.tag == "Trampa") 
        {
            RecibirDaño(collision);
        }
        if(collision.gameObject.tag == "PajaroPerseguidor") 
        {
            RecibirDaño(collision);
        }

        if (collision.gameObject.tag == "CabezaEnemigo") 
        {
            UnityEngine.Debug.Log("Retroceso Cabeza");
            Retroceso(collision);
            Destroy(collision.transform.parent.gameObject);
        }


    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;  // Cambia el color del jugador a rojo
        yield return new WaitForSeconds(0.5f);  // Espera durante un tiempo breve
        spriteRenderer.color = Color.white;  // Vuelve al color original
        
    }

    //private IEnumerator Death()
    //{
        
    //    animator.SetTrigger("Death");

    //}

    public void RecibirDaño(Collision2D collision) 
    {
        //StartCoroutine("Death");
        isKnockBack = true;
        //restamos vida
        vida--;

        Retroceso(collision);

        //Actulizamos corazones en el UI
        //GameManager.instance.ActualizarCorazones();
        StartCoroutine(FlashRed());
    }
    public void Retroceso(Collision2D collision) 
    {
        //Retroceso
        Vector2 knockbackDirection = transform.position - collision.transform.position;
        UnityEngine.Debug.Log("Aplicando retroceso");
        rb.velocity = Vector2.zero;  // Detiene el movimiento actual del personaje
        rb.AddForce(knockbackDirection.normalized * knockbackForce, ForceMode2D.Impulse);  // Aplica la fuerza de retroceso
        StartCoroutine(FinRetroceso());
    }

    private IEnumerator FinRetroceso() 
    {
        yield return new WaitForSeconds(0.5f);
        isKnockBack = false;

    }

    private void WallJump() 
    {
        if (isWallSling ) 
        { 
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x;
            wallJumpTimer = wallJumpTime;
        
        
        }else if (wallJumpTimer > 0f) 
        { 
        
            wallJumpTimer -= Time.deltaTime;  
        
        }else if (wallJumpTimer < 0f) 
        { 
            isWallJumping=false;
        }
    
    }


    /// <summary>
    /// Dibuja gizmos personalizados
    /// </summary>
    public void OnDrawGizmos() 
    { 
        //suelo
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckPos.position, groundCheckSize);

        //pared
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(wallCheckPos.position, wallCheckSize);

        //plataforma
        Gizmos.color = Color.red;
    }



}
