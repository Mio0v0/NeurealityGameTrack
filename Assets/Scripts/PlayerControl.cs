using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public WeaponSwitching weaponSwitchingController;
    public WeaponSwitching weaponSwitchingHand;
    public MotorImagery childActivator;
    public Camera mainCamera;
    public float maxViewDistance = 100f;
    public float fieldOfViewAngle = 110f;
    public ScoreSystem score;

    private WeaponSwitching activeWeaponSwitching;

    void Update()
    {
        DetermineActiveWeaponSwitching();

        // Example input to switch weapons on the active weapon holder
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (activeWeaponSwitching != null)
            {
                activeWeaponSwitching.SetSelectedWeapon(0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (activeWeaponSwitching != null)
            {
                activeWeaponSwitching.SetSelectedWeapon(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (activeWeaponSwitching != null)
            {
                activeWeaponSwitching.SetSelectedWeapon(2);
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            // Assuming the first child is at index 0
            childActivator.ActivateChild(0, 16);
            StartCoroutine(StartCameraShake(3f, 3f, 12f));
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Assuming the second child is at index 1
            childActivator.ActivateChild(1, 20);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyClosestTargetInView();
        }
    }

    void DestroyClosestTargetInView()
    {
        GameObject[] robots = GameObject.FindGameObjectsWithTag("Robot");
        GameObject[] drones = GameObject.FindGameObjectsWithTag("Drone");

        // Combine the robots and drones arrays
        GameObject[] targets = new GameObject[robots.Length + drones.Length];
        robots.CopyTo(targets, 0);
        drones.CopyTo(targets, robots.Length);

        GameObject closestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject target in targets)
        {
            Vector3 directionToTarget = target.transform.position - currentPosition;
            float angle = Vector3.Angle(directionToTarget, transform.forward);
            float sqrDistanceToTarget = directionToTarget.sqrMagnitude;

            if (angle < fieldOfViewAngle * 0.5f && sqrDistanceToTarget < closestDistanceSqr && sqrDistanceToTarget <= maxViewDistance * maxViewDistance)
            {
               
                
                    closestDistanceSqr = sqrDistanceToTarget;
                    closestTarget = target;
          
            }
        }

        if (closestTarget != null)
        {
            int scoreToAdd = 0; // Default score to add
            if (closestTarget.CompareTag("Robot"))
            {
                scoreToAdd = 120; // Score value for Robot
            }
            else if (closestTarget.CompareTag("Drone"))
            {
                scoreToAdd = 240; // Score value for Drone
            }

            closestTarget.GetComponent<Target>().DestroySelf(); // Destroy the target
            score.AddScore(scoreToAdd);
        }
    }



    IEnumerator StartCameraShake(float delay, float intensity, float duration)
    {
        yield return new WaitForSeconds(delay);

        StartCoroutine(CameraShake(intensity, duration)); // Start camera shake after the delay
    }

    IEnumerator CameraShake(float intensity, float duration)
    {
        float elapsed = 0.0f;

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not assigned!");
            yield break;
        }

        Vector3 originalPos = mainCamera.transform.localPosition;

        while (elapsed < duration)
        {
            float x = Random.Range(-intensity, intensity) * 0.02f; // Reduce intensity
            float y = Random.Range(-intensity, intensity) * 0.02f; // Reduce intensity

            mainCamera.transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        mainCamera.transform.localPosition = originalPos; // Reset camera position
    }

    void DetermineActiveWeaponSwitching()
    {
        // Check if the controller weapon holder is active, and assign it as the active weapon switching component
        if (weaponSwitchingController != null && weaponSwitchingController.gameObject.activeInHierarchy)
        {
            activeWeaponSwitching = weaponSwitchingController;
        }
        // Otherwise, check if the hand weapon holder is active
        else if (weaponSwitchingHand != null && weaponSwitchingHand.gameObject.activeInHierarchy)
        {
            activeWeaponSwitching = weaponSwitchingHand;
        }
        else
        {
            activeWeaponSwitching = null; // No active weapon holder found
        }
    }
}

