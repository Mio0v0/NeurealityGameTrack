using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health, maxHeath = 50f;
    [SerializeField] EnemyHealthBar healthBar;
    Transform target;
    Animator animator; // Reference to the Animator component
    // Start is called before the first frame update
    private void Awake()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
           // Debug.Log("hit");
            health -= 10f;
            healthBar.UpdateHealthBar(health, maxHeath);
            if (health <= 0)
            {
                Die();
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AOE"))
        {
            health -= 20f;
            healthBar.UpdateHealthBar(health, maxHeath);
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        if (animator != null)
        {
            // Trigger the "Die" animation
            animator.SetTrigger("Die");
        }

        Destroy(gameObject);
    }
}

