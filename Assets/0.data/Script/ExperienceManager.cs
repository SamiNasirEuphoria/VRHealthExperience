using UnityEngine;
using RenderHeads.Media.AVProVideo;
using BNG;
using System.Collections;
using UnityEngine.Events;

public class ExperienceManager : MonoBehaviour
{
    public ApplyToMesh meshMedia;
    public MediaPlayer[] videPlayer;
    public UnityEngine.UI.Button exit, play,pause;
   
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
        foreach (MediaPlayer player in videPlayer)
        {
            player.gameObject.SetActive(false);
        }
        meshMedia.Player = videPlayer[MainmenuManager.modeSelected];
        exit.onClick.AddListener(Exit);
        play.onClick.AddListener(Play);
        pause.onClick.AddListener(Pause);
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
        yield return new WaitForSeconds(0.05f);
        yield return FadeScreen(myGroup, 1f, fadeInScreen);
        
    }
    IEnumerator FadeOutScreen()
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
        StartCoroutine(FadeOutScreen());
    }
    public void Play()
    {
        videPlayer[MainmenuManager.modeSelected].gameObject.SetActive(true);
    }
    public void Pause()
    {
        videPlayer[MainmenuManager.modeSelected].gameObject.SetActive(false);
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(startScene);
        yield return new WaitForSeconds(0.25f);
        myPlayer.enabled = true;
        yield return new WaitForEndOfFrame();
        videPlayer[MainmenuManager.modeSelected].gameObject.SetActive(true);
    }
}
