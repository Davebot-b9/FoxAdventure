using UnityEngine;

public class BossTankMine : MonoBehaviour
{
    public GameObject explosion;
    

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            Explode();
            PlayerHealthController.instance.DealDamage();
        }
    }

    public void Explode(){
        Destroy(gameObject);

        var transform1 = transform;
        Instantiate(explosion, transform1.position, transform1.rotation);
    }
}
