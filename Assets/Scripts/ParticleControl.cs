using UnityEngine;
using System.Collections;

public class ParticleControl : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public int maxLoops = 3;

    private int currentLoopCount = 0;
    private float duration;

    void Start()
    {
        if (particleSystem == null)
        {
            particleSystem = GetComponent<ParticleSystem>();
        }

        if (particleSystem != null)
        {
            duration = particleSystem.main.duration;
            StartCoroutine(CountLoops());
        }
    }

    private IEnumerator CountLoops()
    {
        while (currentLoopCount < maxLoops)
        {
            yield return new WaitForSeconds(duration);
            currentLoopCount++;
        }
        particleSystem.Stop();
    }
}
