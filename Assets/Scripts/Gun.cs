using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 1f;

    public ParticleSystem muzzleFlash;
    public ScoreSystem score;

    private Transform shootingPoint;
    private float nextTimeToFire = 0f;

    void Start()
    {
        // Find the shooting point child object
        shootingPoint = transform.Find("ShootingPoint");
    }

    void Update()
    {
        Vector3 currentRotation = transform.eulerAngles;

        // Set the Z component of the rotation to 0
        currentRotation.z = 0;

        // Apply the modified rotation back to the transform
        transform.eulerAngles = currentRotation;

        if (Time.time >= nextTimeToFire) 
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }


    void Shoot()
    {
        muzzleFlash.Play();
        // Check if the shooting point is found
        if (shootingPoint != null)
        {
            RaycastHit hit;

            // Cast ray from shooting point's position and forward direction
            if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out hit, range))
            {
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {   
                    target.Flash(); // Call the Flash method on the target
                }
                if (hit.collider.CompareTag("Target"))
                {
                    // Call AddScore on the player script, passing damage as points
                    score.AddScore((int)damage);
                }
            }

        }
    }
}


