using UnityEngine;

public class LSPlayer : MonoBehaviour
{
    public MapPoint currentPoint;

    public float moveSpeed = 10f;

    public bool levelLoading;

    public LSManager theManager;
    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentPoint.transform.position) < .1f)
        {
            if (Input.GetAxisRaw("Horizontal") > .5f)
            {
                if (currentPoint.right != null)
                {
                    SetNextPoint(currentPoint.right);
                }
            }
            if (Input.GetAxisRaw("Horizontal") < -.5f)
            {
                if (currentPoint.left != null)
                {
                    SetNextPoint(currentPoint.left);
                }
            }
            if (Input.GetAxisRaw("Vertical") > .5f)
            {
                if (currentPoint.up != null)
                {
                    SetNextPoint(currentPoint.up);
                }
            }
            if (Input.GetAxisRaw("Vertical") < -.5f)
            {
                if (currentPoint.down != null)
                {
                    SetNextPoint(currentPoint.down);
                }
            }
            if (currentPoint.isLevel && currentPoint.levelToLoad != "" && !currentPoint.isLocked)
            {
                LSUIManager.instance.ShowInfo(currentPoint);
                if (Input.GetButtonDown("Jump"))
                {
                    levelLoading = true;
                    theManager.LoadLevel();
                }
            }
        }
    }

    public void SetNextPoint(MapPoint nextPoint){
        currentPoint = nextPoint;
        LSUIManager.instance.HideInfo();
    }
}
