using UnityEngine;

public class BouncerController : MonoBehaviour
{
    private Animator anim;
    public float bounceForce = 10f;

    private static readonly int Bounce = Animator.StringToHash("Bounce");
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {   
            AudioManager.instance.PlaySfx(1);
            PlayerController.instance.theRb.velocity = new Vector2(PlayerController.instance.theRb.velocity.x, bounceForce);
            anim.SetTrigger(Bounce); 
        }
    }
}
