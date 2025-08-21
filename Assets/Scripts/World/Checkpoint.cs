// Assets/Scripts/World/Checkpoint.cs
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Checkpoint : MonoBehaviour
{
    public static Vector3 lastCheckpointPosition;
    public static bool hasCheckpoint = false;

    public GameObject reachedVfx; // ¼±ÅÃ: ±ê¹ß ¹ÝÂ¦ÀÓ °°Àº °Å

    void Reset()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        lastCheckpointPosition = transform.position;
        hasCheckpoint = true;

        if (reachedVfx != null)
        {
            Instantiate(reachedVfx, transform.position, Quaternion.identity);
        }
    }
}
