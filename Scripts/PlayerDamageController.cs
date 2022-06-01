using UnityEngine;


public class PlayerDamageController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DealDamage();
        }
    }
}

