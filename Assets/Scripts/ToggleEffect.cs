using UnityEngine;

public class ToggleEffect : MonoBehaviour
{
    public GameObject ShootPrefab;
    public GameObject AOEPrefab;
    public GameObject HealPrefab;

    void Update()
    {
        // Check if MouseButton1 is pressed down
        if (Input.GetMouseButtonDown(0))
        {
            InstantiateEffect(ShootPrefab);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            InstantiateEffect(AOEPrefab);
        }
        else if (Input.GetMouseButtonDown(2))
        {
            InstantiateEffect(HealPrefab);
        }
    }

    void InstantiateEffect(GameObject prefab)
    {
        // Check if the prefab is assigned in the inspector
        if (prefab != null)
        {
            // Instantiate the corresponding prefab
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Prefab not assigned in the inspector!");
        }
    }
}

