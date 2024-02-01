using UnityEngine;

public class AOE : MonoBehaviour
{
    public GameObject sphereColliderPrefab; // Prefab of the sphere collider
    public Camera mainCamera; // Reference to the main camera
    public float maxDistance = 50f; // Maximum distance from the camera
    public float zRotationMultiplier = 0.1f; // Multiplier for X-rotation effect

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // If the main camera reference is not assigned, get it from the scene
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            SpawnSphereCollider();
        }
    }

    private void SpawnSphereCollider()
    {
        // Calculate the ray from the camera
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        // Calculate the intersection point with the Y-plane (Y-coordinate is 0)
        float t = -ray.origin.y / ray.direction.y;
        Vector3 intersectionPoint = ray.origin + t * ray.direction;

        // Adjust the position in the X-direction based on the camera's X-rotation
        intersectionPoint.z += mainCamera.transform.rotation.eulerAngles.x * zRotationMultiplier;

        // Ensure the position does not exceed the maximum distance
        intersectionPoint.z = Mathf.Clamp(intersectionPoint.z, -maxDistance, maxDistance);

        // Instantiate the sphere collider prefab at the calculated intersection point
        Instantiate(sphereColliderPrefab, intersectionPoint, Quaternion.identity);
    }
}
