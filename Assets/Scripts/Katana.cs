using UnityEngine;
using System.Collections;

public class Katana : MonoBehaviour
{
    public float damage = 5f;

    public ScoreSystem score;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object we collided with has the tag "Target"
        if (other.CompareTag("Target"))
        { 
            // Attempt to call the Flash method on the target. This requires the target to have a script component that contains the Flash method.
            Target target = other.GetComponent<Target>();
            if (target != null)
            {
                target.Flash(); // Make sure the Target script has a method named Flash
            }

            // Call AddScore on the score system, passing damage as points
            score.AddScore((int)damage);
        }
       
    }
}
