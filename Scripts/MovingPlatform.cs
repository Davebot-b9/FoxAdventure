using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    public int currentPoint;

    public Transform platform;

    // Update is called once per frame
    void Update()
    {
        platform.position = Vector3.MoveTowards(platform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(platform.position, points[currentPoint].position) < .05f)
        {
            currentPoint++;

            if (currentPoint >= points.Length)
            {
                currentPoint = 0;
            }
        }
    }
}
