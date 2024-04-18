using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Testing : MonoBehaviour
{
    public CanvasGroup myCanvasGroup;
    public float fadeInDuration = 1f;
    public Text screenText;
    public InputField myInput;
    public Button okClick;

    void Start()
    {
        StartCoroutine(FadeIn());
        okClick.onClick.AddListener(ShowMessage);
    }
    public void ShowMessage()
    {
        screenText.text = myInput.text;
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
