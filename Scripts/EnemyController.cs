using UnityEngine;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    private bool movingRight;
    private Rigidbody2D theRb;
    [FormerlySerializedAs("theSR")] public SpriteRenderer theSr;
    private Animator anim;

    public float moveTime, waitTime;
    private float moveCount, waitCount;

    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    // Start is called before the first frame update
    void Start()
    {
        theRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        movingRight = true;
        moveCount = moveTime;
    }

    // Update is called once per frame
    void Update()
    { 
            if (moveCount > 0)
            {

                moveCount -= Time.deltaTime;

                if (movingRight)
                {
                    theRb.velocity = new Vector2(moveSpeed, theRb.velocity.y);
                    theSr.flipX = true;

                    if (transform.position.x > rightPoint.position.x)
                    {
                        movingRight = false;
                    }
                }else
                {
                    theRb.velocity = new Vector2(-moveSpeed, theRb.velocity.y);

                    theSr.flipX = false;

                    if (transform.position.x < leftPoint.position.x)
                    {
                        movingRight = true;
                    }
                }

                if (moveCount <= 0)
                {
                    waitCount = Random.Range(waitTime * .75f, waitTime * 1.25f);
                }

                anim.SetBool(IsMoving, true);
                
            } else if (waitCount > 0)
            {
                waitCount -= Time.deltaTime;
                theRb.velocity = new Vector2(0f, theRb.velocity.y); 

                if (waitCount <= 0)
                {
                    moveCount = Random.Range(moveTime * .75f, waitTime * 1.25f);
                }
                anim.SetBool(IsMoving, false);
            }
    }
}
