using UnityEngine;

public class DestroyColliderAfterDelay : MonoBehaviour
{
    public float delay = 5.0f; // Time delay before destroying the GameObject

    private void Start()
    {
        // Schedule the destruction of the GameObject after the specified delay
        Destroy(gameObject, delay);
    }
}

