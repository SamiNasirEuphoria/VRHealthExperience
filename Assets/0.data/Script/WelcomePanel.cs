using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WelcomePanel : MonoBehaviour
{
    private CanvasGroup myCanvasGroup;
    public float fadeInDuration = 1f;

    void Start()
    {
        myCanvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        myCanvasGroup.alpha = 0f;
        myCanvasGroup.blocksRaycasts = false;

        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            myCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            yield return null;
        }

        myCanvasGroup.alpha = 1f;
        myCanvasGroup.blocksRaycasts = true;
    }
}
