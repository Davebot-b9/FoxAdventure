using UnityEngine;

public class BossTankHitBox : MonoBehaviour
{
    public BossTankController bossCont;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && PlayerController.instance.transform.position.y > transform.position.y)
        {
            bossCont.TakeHit();

            PlayerController.instance.Bounce();

            gameObject.SetActive(false);
        }
    }
}
