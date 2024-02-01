using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveTowardsPoint : MonoBehaviour
{
    public float speed = 1f;       // Speed of movement
    private Animator animator;     // Animator component
    private Vector3 targetPoint = new Vector3(0, 0, 0);  // Target point

    void Start()
    {
        // Get the Animator component from the GameObject
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the current state of the Animator has the tag 'walk'
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Walk"))
        {
            // Calculate the direction towards the target point
            Vector3 direction = (targetPoint - transform.position).normalized;

            // Move the monster towards the target point
            transform.position += direction * speed * Time.deltaTime;

            // Optionally, make the monster face the direction of movement
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            // Stop the movement if the monster is close enough to the target point
            if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
            {
                enabled = false;  // Disable this script
            }
        }
    }
}
