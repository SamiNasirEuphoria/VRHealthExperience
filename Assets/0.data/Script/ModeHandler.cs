using System.Collections;
using UnityEngine;

public class ModeHandler : MonoBehaviour
{
    public CanvasGroup modeScreen; // Array to hold CanvasGroups of all screens
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;
    public float startScene;
    public static int modeSelected;
    public Animator myAnimator;
    void Start()
    {
        modeScreen.alpha = 0;
        StartCoroutine(FadeInScreen());
    }
    public void ModeScreenButton()
    {
        StartCoroutine(FadeOutScreen());
        
    }
    IEnumerator FadeInScreen()
    {
        yield return new WaitForSeconds(startScene);
        yield return FadeScreen(modeScreen, 1f, fadeInDuration);
    }
    IEnumerator FadeOutScreen()
    {
        myAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(0.05f);
        yield return FadeScreen(modeScreen, 0f, fadeOutDuration);

        SceneHandler.Instance.Load("OpenHealthVR-Main");
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
