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
        foreach (Transform child in parent)
        {
            MeshRenderer renderer = child.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                allRenderers.Add(renderer);
            }
            // Recursively check for further children
            if (child.childCount > 0)
            {
                FindAllRenderers(child);
            }
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
}

