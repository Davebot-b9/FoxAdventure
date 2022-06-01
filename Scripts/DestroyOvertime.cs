using UnityEngine;

public class DestroyOvertime : MonoBehaviour
{
    public float lifeTime;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeTime);
    }
}
