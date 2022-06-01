using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth, maxHealth;

    public float invincibleLength;
    private float invincibleCounter;

    private SpriteRenderer theSr;

    public GameObject deathEffect;
    private static readonly int Hurt = Animator.StringToHash("Hurt");

    private void Awake() {
        instance = this;
    }
    
    void Start()
    {
        //la vida esta al maximo o completa
        currentHealth = maxHealth;

        theSr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleCounter > 0){
            invincibleCounter -= Time.deltaTime;

            if (invincibleCounter <= 0)
            {
                var color = theSr.color;
                color = new Color(color.r, color.g, color.b, 1f);
                theSr.color = color;
            }
        }
    }

    public void DealDamage(){
        if(invincibleCounter <=0){
            currentHealth--;
            PlayerController.instance.anim.SetTrigger(Hurt);

            AudioManager.instance.PlaySfx(9);

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                var transform1 = PlayerController.instance.transform;
                Instantiate(deathEffect, transform1.position, transform1.rotation);   

                AudioManager.instance.PlaySfx(8);

                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                invincibleCounter = invincibleLength;
                var color = theSr.color;
                color = new Color(color.r, color.g, color.b, .5f);
                theSr.color = color;

                PlayerController.instance.Knockback();
            }
            UIController.instance.UpdateHealthDisplay();
        }
    }

    public void HealPlayer(){
        currentHealth++;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.instance.UpdateHealthDisplay();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Platforme") ){
            transform.parent = other.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Platforme") ){
            transform.parent = null;
        }
    }
}
