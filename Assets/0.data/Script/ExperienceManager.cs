using UnityEngine;
using RenderHeads.Media.AVProVideo;
using BNG;
using System.Collections;
using UnityEngine.Events;

public class ExperienceManager : MonoBehaviour
{
    public ApplyToMesh meshMedia;
    public MediaPlayer[] videoPlayer;
    public UnityEngine.UI.Button exit, play,pause, rewind;
   
    public GameObject outerSphere, canvesPanel;
    private ApplyToMesh myPlayer;
    public CanvasGroup myGroup;
    public Animator myAnimator;
    public float startScene, fadeInScreen, fadeOutScreen;
    private bool check;
    public UnityEvent buttonResetState;
    private void Start()
    {
        myGroup.alpha = 0;
        canvesPanel.SetActive(false);
        myPlayer = outerSphere.GetComponent<ApplyToMesh>();
        myPlayer.enabled = false;
        foreach (MediaPlayer player in videoPlayer)
        {
            player.gameObject.SetActive(false);
        }
        meshMedia.Player = videoPlayer[MainmenuManager.modeSelected];
        exit.onClick.AddListener(Exit);
        play.onClick.AddListener(Play);
        pause.onClick.AddListener(Pause);
        rewind.onClick.AddListener(Rewind);
        StartCoroutine(Wait());
        check = true;
    }
    private void Update()
    {
        if (InputBridge.Instance.RightTriggerDown && check)
        {
            canvesPanel.SetActive(true);
            StartCoroutine(FadeInScreen());
            check = false;
        }
    }
    IEnumerator FadeInScreen()
    {
        Debug.Log("Calling screen fade in");
        yield return new WaitForSeconds(0.05f);
        yield return FadeScreen(myGroup, 1f, fadeInScreen);
        StartCoroutine(FadeOutScreen());
    }
    IEnumerator FadeOutScreen()
    {
            Debug.Log("calling screen fade out");
            yield return new WaitForSeconds(8.5f);
            yield return FadeScreen(myGroup, 0f, fadeOutScreen);
            canvesPanel.SetActive(false);
            check = true;
    }
    IEnumerator ExitScreen()
    {
        myAnimator.SetTrigger("FadeIn");
        buttonResetState.Invoke();
        yield return new WaitForSeconds(0.05f);
        yield return FadeScreen(myGroup, 0f, fadeOutScreen);
        SceneHandler.Instance.Load("OpenHealthVR-BackToMenu");
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
    public void Exit()
    {
        StartCoroutine(ExitScreen());
    }
    public void Play()
    {
        StopCoroutine(FadeOutScreen());
        videoPlayer[MainmenuManager.modeSelected].gameObject.SetActive(true);
        StartCoroutine(FadeOutScreen());
    }
    public void Pause()
    {
        StopCoroutine(FadeOutScreen());
        videoPlayer[MainmenuManager.modeSelected].gameObject.SetActive(false);
    }
    public void Rewind()
    {
        StopCoroutine(FadeOutScreen());
        videoPlayer[MainmenuManager.modeSelected].GetComponent<MediaPlayer>().Rewind(true);
        videoPlayer[MainmenuManager.modeSelected].GetComponent<MediaPlayer>().Play();
        StartCoroutine(FadeOutScreen());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(startScene);
        yield return new WaitForSeconds(0.25f);
        myPlayer.enabled = true;
        yield return new WaitForEndOfFrame();
        videoPlayer[MainmenuManager.modeSelected].gameObject.SetActive(true);
    }
}
