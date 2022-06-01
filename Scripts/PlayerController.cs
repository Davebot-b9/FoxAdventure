using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    //Movimiento
    public float moveSpeed;
    // Salto
    private bool canDoubleJump;
    public float jumpForce;
    public float bounceForce;
    //Componentes
    [FormerlySerializedAs("theRB")] public Rigidbody2D theRb;
    //Animator
    public Animator anim;
    private SpriteRenderer theSr;
    //Grounded
    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    public float knockBackLength, knockBackForce;
    private float knockBackCounter;

    public bool stopInput;
    private static readonly int MoveSpeed = Animator.StringToHash("moveSpeed");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //se agrega la variable "anim" en la funcion start para que se aplique las animaciones del player al iniciar el juego
        anim = GetComponent<Animator>();
        //Se agrega los componenetes del SpriteRenderer para cambiar la orientacion del jugador en la animaciones al retroceder (osea de media vuelta)
        theSr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.instance.isPaused && !stopInput){
            if (knockBackCounter <= 0)
        {
             //Se genera la velocidad de movimiento dada las variables declaradas (pueden inicializarse desde este script o desde Unity)
            theRb.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRb.velocity.y);
            // se genera la fisica para no dar un doble salto, despues se genera una condicion para dar saltos solo si pisa el suelo del escenario
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);
            //la logica aplicada para el doble salto (se reinicia el doble salto), para que al momento de estar en el aire no permita realizar dos saltos más de la cuenta
            if (isGrounded)
            {
                canDoubleJump = true;
            }
            //Se generan los saltos
            if(Input.GetButtonDown("Jump")){    
                //condicion para que solo salte una sola vez siempre y cuando solo detecte el suelo del escenario
                if (isGrounded)
                {
                    //fuerza de velocidad
                    theRb.velocity = new Vector2(theRb.velocity.x, jumpForce);
                    // Audio cuando salta el personaje
                    AudioManager.instance.PlaySfx(10);
                }else
                {
                    if (canDoubleJump)
                    {
                        theRb.velocity = new Vector2(theRb.velocity.x, jumpForce); //fuerza de velocidad aplicada en el doble salto (es la misma que la de un solo salto)
                        //Audio cuando salta el personaje
                        AudioManager.instance.PlaySfx(10);
                        canDoubleJump = false; 
                    }
                }
            }
            if (theRb.velocity.x < 0 )
            {
                theSr.flipX = true;
            } else if (theRb.velocity.x > 0)
            {
                theSr.flipX = false;
            }
        }else{
            knockBackCounter -= Time.deltaTime;
            if (!theSr.flipX)
            {
                theRb.velocity = new Vector2(-knockBackForce, theRb.velocity.y);
            }else
            {
                theRb.velocity = new Vector2(knockBackForce, theRb.velocity.y);
            }
        }
        }

        anim.SetFloat(MoveSpeed, Mathf.Abs(theRb.velocity.x)); // la fuerza de velocidad se aplica en la animacion de Run para que al tocar la tecla de movimiento se relice la animacion
        anim.SetBool(IsGrounded, isGrounded); // la animacion de salto se aplica con la logica de salto ya establecida
    }

    public void Knockback(){
        knockBackCounter = knockBackLength;
        theRb.velocity = new Vector2(0f, knockBackForce);
    }

    public void Bounce(){
        theRb.velocity = new Vector2(theRb.velocity.x, bounceForce);
    }
}
