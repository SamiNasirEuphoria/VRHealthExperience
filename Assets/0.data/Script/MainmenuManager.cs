using System.Collections;
using UnityEngine;

public class MainmenuManager : MonoBehaviour
{
    private CanvasGroup welcomesScreen, introScreen, controllerScreen, modeScreen; // Array to hold CanvasGroups of all screens
    public float fadeInDuration = 1f; 
    public float fadeOutDuration = 1f; 
    public float timeBetweenScreens = 2f; 
    public float startScene;
    public static int modeSelected;
    public Animator myAnimator;
    void Start()
    {
        welcomesScreen = UIReferenceContainer.Instance.welcomeScreen.GetComponent<CanvasGroup>();
        introScreen = UIReferenceContainer.Instance.introScreen.GetComponent<CanvasGroup>();
        controllerScreen = UIReferenceContainer.Instance.controllerScreen.GetComponent<CanvasGroup>();
        modeScreen = UIReferenceContainer.Instance.modesScreen.GetComponent<CanvasGroup>();

        welcomesScreen.alpha = 0f;
        introScreen.alpha = 0f;
        controllerScreen.alpha = 0f;
        modeScreen.alpha = 0f;

        welcomesScreen.blocksRaycasts = false;
        introScreen.blocksRaycasts = false;
        controllerScreen.blocksRaycasts = false;
        modeScreen.blocksRaycasts = false;


        StartCoroutine(FadeInScreen());

        UIReferenceContainer.Instance.welcomeScreenButton.onClick.AddListener(WelcomeScreenButton);
        UIReferenceContainer.Instance.introScreenButton.onClick.AddListener(IntroScreenButton);
        UIReferenceContainer.Instance.controllerScreenButton.onClick.AddListener(ControllerScreenButton);
        //UIReferenceContainer.Instance.modeScreenButton.onClick.AddListener(ModeScreenButton);

    }
   
    public void WelcomeScreenButton()
    {
       StartCoroutine(Fade(welcomesScreen, introScreen))  ;
    }
    public void IntroScreenButton()
    {
        StartCoroutine(Fade(introScreen,controllerScreen));
    }
    public void ControllerScreenButton()
    {
        StartCoroutine(Fade(controllerScreen,modeScreen));
    }
    public void ModeScreenButton()
    {
        StartCoroutine(FadeOutScreen());
       
    }
    IEnumerator FadeInScreen()
    {
        yield return new WaitForSeconds(startScene);
        yield return FadeScreen(welcomesScreen, 1f, fadeInDuration);
    }
    IEnumerator FadeOutScreen()
    {
        myAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(0.05f);
        yield return FadeScreen(modeScreen, 0f, fadeOutDuration);
        SceneHandler.Instance.Load("OpenHealthVR-Main");
    }
    IEnumerator Fade(CanvasGroup currentScreen, CanvasGroup nextScreen)
    {
        Debug.Log("fade method");
        yield return StartCoroutine(FadeScreen(currentScreen, 0f, fadeOutDuration));
        yield return StartCoroutine(FadeScreen(nextScreen, 1f, fadeInDuration));
        Debug.Log("fade method");

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
