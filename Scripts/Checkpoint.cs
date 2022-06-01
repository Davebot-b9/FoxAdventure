using UnityEngine;
using UnityEngine.Serialization;

public class Checkpoint : MonoBehaviour
{
    [FormerlySerializedAs("theSR")] public SpriteRenderer theSr;

    public Sprite cpOn, cpOff;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            CheckpointController.instance.DeactivateCheckpoints();
            theSr.sprite = cpOn;
            CheckpointController.instance.SetSpawnPoint(transform.position);
        }
    }

    public void ResetCheckpoint(){
        theSr.sprite = cpOff;
    }
}
