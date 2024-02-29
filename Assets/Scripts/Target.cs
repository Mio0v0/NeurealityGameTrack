using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Material originalMaterial; // Assign in the inspector
    public Material flashMaterial; // Assign a bright or white material in the inspector
    private List<MeshRenderer> allRenderers = new List<MeshRenderer>(); // List to hold all MeshRenderers

    void Start()
    {
        // Recursively find all MeshRenderers in this GameObject's children and sub-children
        FindAllRenderers(transform);
    }

   
    void FindAllRenderers(Transform parent)
    {
        // First, check the parent itself (this ensures it works even if the GameObject has no children)
        MeshRenderer parentRenderer = parent.GetComponent<MeshRenderer>();
        if (parentRenderer != null)
        {
            allRenderers.Add(parentRenderer);
        }

        // Then, recursively check all children
        foreach (Transform child in parent)
        {
            FindAllRenderers(child);
        }
    }


    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        // Change to the flash material for all found MeshRenderers
        foreach (var renderer in allRenderers)
        {
            renderer.material = flashMaterial;
        }

        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds

        // Revert to the original material for all found MeshRenderers
        foreach (var renderer in allRenderers)
        {
            renderer.material = originalMaterial;
        }
    }

    public void MakeInvisibleAndBack()
    {
        // Add any additional logic here (e.g., play animations, sound effects)
        StartCoroutine(InvisibleAfterDelay(0.5f));
    }

    IEnumerator InvisibleAfterDelay(float delay)
    {
       /* // Optionally, start your flash routine here if you want it to run during the delay
        StartCoroutine(FlashRoutine());

        // Wait for the specified delay
        yield return new WaitForSeconds(delay);*/

        // Make the game object invisible
        SetVisibility(false);

        // Wait for 8 seconds while the object is invisible
        yield return new WaitForSeconds(8f);

        // Then make the game object visible again
        SetVisibility(true);
    }

    void SetVisibility(bool isVisible)
    {
    // Assuming allRenderers has been populated by FindAllRenderers method beforehand
    foreach (MeshRenderer renderer in allRenderers)
        {
        renderer.enabled = isVisible;
        }
    }
}

