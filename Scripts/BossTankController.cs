using UnityEngine;
public class BossTankController : MonoBehaviour
{
    public enum BossStates { Shooting, Hurt, Moving, Ended };
    public BossStates currentStates;

    public Transform theBoss;
    public Animator anim;

    [Header("Movement")]
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    private bool moveRight;
    public GameObject mine;
    public Transform minePoint;
    public float timeBetweenMine;
    private float mineCounter;

    [Header("Shoothing")]
    public GameObject bullet;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;

    [Header ("Hurt")]
    public float hurtTime;
    private float hurtCounter;

    public GameObject hitBox;

    [Header("Health")]
    public int health = 5;
    public GameObject explosion, winPlatform;
    private bool isDefeated;
    public float shotSpeedUp, mineSpeedUp;
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int StopMoving = Animator.StringToHash("stopMoving");

    void Start()
    {
        currentStates = BossStates.Shooting;
    }

    
    void Update()
    {
        switch (currentStates)
        {
            case BossStates.Shooting:
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots;

                    var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    newBullet.transform.localScale = theBoss.localScale;
                }

                break;
            
            case BossStates.Hurt:
                if (hurtCounter > 0)
                {
                    hurtCounter -= Time.deltaTime;

                    if (hurtCounter <= 0)
                    {
                        currentStates = BossStates.Moving;

                        mineCounter = 0;

                        if (isDefeated)
                        {
                            theBoss.gameObject.SetActive(false);
                            Instantiate(explosion, theBoss.position, theBoss.rotation);
                            winPlatform.SetActive(true);

                            AudioManager.instance.StopBossMusic();

                            currentStates = BossStates.Ended;
                        }
                    }
                }

                break;

            case BossStates.Moving:
                if (moveRight)
                {
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if (theBoss.position.x > rightPoint.position.x)
                    {
                        theBoss.localScale = Vector3.one;
                        moveRight = false;
                        EndMovement();
                    }
                }else
                {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(-1f,1f,1f);
                        moveRight = true;
                        EndMovement();
                    }
                }

                mineCounter -= Time.deltaTime;


                if (mineCounter <= 0)
                {
                    mineCounter = timeBetweenMine;

                    Instantiate(mine, minePoint.position, minePoint.rotation);
                }

                break;
        }
    }
    public void TakeHit(){
        currentStates = BossStates.Hurt;
        hurtCounter = hurtTime;

        anim.SetTrigger(Hit);

        BossTankMine[] mines = FindObjectsOfType<BossTankMine>();
        if (mines.Length > 0)
        {
            foreach (BossTankMine foundMine in mines)
            {
                foundMine.Explode();
            }
        }

        health--;
        if (health <=  0)
        {
            isDefeated = true;
        }else
        {
            timeBetweenShots /= shotSpeedUp;
            timeBetweenMine /= mineSpeedUp;
        }
    }

    private void EndMovement(){
        currentStates = BossStates.Shooting;
        shotCounter = 0f;
        anim.SetTrigger(StopMoving);
        hitBox.SetActive(true);
    }
}
