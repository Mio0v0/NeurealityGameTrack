using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healing : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera camera;
    [SerializeField] private Vector3 screenPosition;
    [SerializeField] PlayerHealthBar healthBar;
    [SerializeField] float health, maxHeath = 50f;

    [SerializeField] private GameObject prefabToInstantiate;

    private void Awake()
    {
        healthBar = GetComponentInChildren<PlayerHealthBar>();
        healthBar.UpdateHealthBar(health, maxHeath);
    }
    private void Update()
    {
        // Check for Fire2 button press (you can customize the input button)
        if (Input.GetButtonDown("Fire3"))
        {
            InstantiatePrefabAtCameraPosition();
            if(health < maxHeath)
            {
                health += 10f;
                healthBar.UpdateHealthBar(health, maxHeath);
                Debug.Log("Add");
            } 
        }
    }

    private void InstantiatePrefabAtCameraPosition()
    {
        if (prefabToInstantiate != null)
        {
            // Get the camera's position and set Y to 0
            Vector3 cameraPosition = camera.transform.position;
            cameraPosition.y = 0;

            // Instantiate the prefab at the modified camera position
            Instantiate(prefabToInstantiate, cameraPosition, Quaternion.identity);
        }
    }
}
