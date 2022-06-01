using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isGem, isHeal;
    private bool isCollected;
    public GameObject pickupEffect;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !isCollected)
        {
            if (isGem)
            {
                LevelManager.instance.gemsCollected++;

                UIController.instance.UpdateGemeCount();

                var transform1 = transform;
                Instantiate(pickupEffect ,transform1.position, transform1.rotation);

                AudioManager.instance.PlaySfx(0);

                isCollected = true;
                Destroy(gameObject);
            }

            if (isHeal)
            {
                if (PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
                {
                    PlayerHealthController.instance.HealPlayer();

                    AudioManager.instance.PlaySfx(7);

                    isCollected = true;
                    Destroy(gameObject);
                }
            }
        }
    }
}
