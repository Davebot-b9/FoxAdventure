using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform target;

    public Transform farBackground, middleBackground;

    public float minHeight, maxHeight;
    
    private Vector2 lastPos;

    public bool stopFollow;

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        //se define la ultima posición en x del background
        lastPos =  transform.position;
    }

    void Update()
    {

        if (!stopFollow)
        {
            transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);

            Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

            farBackground.position = farBackground.position + new Vector3(amountToMove.x, amountToMove.y, 0f);
            middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * .5f;
            lastPos = transform.position;
        }
    }
}
