using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainmenuManager : MonoBehaviour
{
    public CanvasGroup[] screens; // Array to hold CanvasGroups of all screens
    public float fadeInDuration = 1f; // Duration for fade-in effect
    public float fadeOutDuration = 1f; // Duration for fade-out effect
    public float timeBetweenScreens = 2f; // Time delay between fading in screens

    void Start()
    {

        StartCoroutine(FadeInScreensSequentially());
    }

    IEnumerator FadeInScreensSequentially()
    {
        // Initial setup: set all screens to fully transparent and disable raycast targeting
        foreach (CanvasGroup screen in screens)
        {
            screen.alpha = 0f;
            screen.blocksRaycasts = false;
        }

        // Fade in each screen sequentially
        foreach (CanvasGroup screen in screens)
        {
            yield return FadeScreen(screen, 1f, fadeInDuration);
            yield return new WaitForSeconds(timeBetweenScreens);
            yield return FadeScreen(screen, 0f, fadeOutDuration);
        }
    }

    IEnumerator FadeScreen(CanvasGroup screen, float targetAlpha, float duration)
    {
        float elapsedTime = 0f;
        float startAlpha = screen.alpha;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            screen.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            yield return null;
        }
        screen.alpha = targetAlpha;
        screen.blocksRaycasts = (targetAlpha == 1f); // Enable raycast targeting if fading in, disable if fading out
    }
}
