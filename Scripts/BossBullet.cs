using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        var transform1 = transform;
        transform1.position += new Vector3(-speed * transform1.localScale.x * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DealDamage();
        }
        Destroy(gameObject);
    }
}
