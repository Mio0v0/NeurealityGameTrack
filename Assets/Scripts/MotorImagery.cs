using UnityEngine;
using System.Collections;

public class MotorImagery : MonoBehaviour
{
    // Method to activate the GameObject
    public void ActivateChild(int childIndex, float duration)
    {
        if (childIndex >= 0 && childIndex < transform.childCount)
        {
            StartCoroutine(ActivateDeactivateAfterDelay(transform.GetChild(childIndex).gameObject, duration));
        }
    }

    private IEnumerator ActivateDeactivateAfterDelay(GameObject child, float delay)
    {
        child.SetActive(true);
        yield return new WaitForSeconds(delay);
        child.SetActive(false);
    }
}
