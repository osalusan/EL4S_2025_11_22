using System.Collections;
using UnityEngine;

public class SampleTransitionTrial : MonoBehaviour
{
    public TransitionProgressControllerTrial transitionProgressControllerTrial;
    public float duration = 1.0f;

    public void StartTransition()
    {
        StartCoroutine(AnimateProgressCoroutine());
    }

    private IEnumerator AnimateProgressCoroutine()
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transitionProgressControllerTrial.progress = elapsedTime / duration;
            yield return null;
        }
        transitionProgressControllerTrial.progress = 1.0f;

        yield return new WaitForSeconds(1);

        elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transitionProgressControllerTrial.progress = 1.0f - (elapsedTime / duration);
            yield return null;
        }
        transitionProgressControllerTrial.progress = 0.0f;
    }
}
