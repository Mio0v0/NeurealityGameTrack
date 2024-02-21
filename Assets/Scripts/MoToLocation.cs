using UnityEngine;
using System.Collections;

public class MoveToLocation : MonoBehaviour
{
    public Transform[] targetPositions; // The target positions the player will move towards
    public float[] moveDelays; // The delays before each movement
    public float moveDuration = 3f; // The duration in seconds for each movement

    private int currentMoveIndex = 0; // Index of the current movement
    private Vector3 initialPosition; // Initial position of the player

    void Start()
    {
        initialPosition = transform.position; // Store the initial position of the player
        // Start the movement sequence
        StartCoroutine(MoveSequence());
    }

    IEnumerator MoveSequence()
    {
        for (int i = 0; i < targetPositions.Length; i++)
        {
            yield return new WaitForSeconds(moveDelays[i]); // Wait for the specified delay

            float elapsedTime = 0f;
            Vector3 startPosition = transform.position;

            while (elapsedTime < moveDuration)
            {
                float t = elapsedTime / moveDuration;
                transform.position = Vector3.Lerp(startPosition, targetPositions[i].position, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPositions[i].position; // Ensure reaching the exact position
        }
    }
}

