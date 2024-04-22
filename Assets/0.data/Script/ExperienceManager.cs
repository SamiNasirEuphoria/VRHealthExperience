using UnityEngine;
using RenderHeads.Media.AVProVideo;
using BNG;
using System.Collections;

public class ExperienceManager : MonoBehaviour
{
    public ApplyToMesh meshMedia;
    public MediaPlayer[] videPlayer;
    public UnityEngine.UI.Button exit, play,pause;
    public float startScene;
    public GameObject outerSphere;
    private ApplyToMesh myPlayer;
    public CanvasGroup myGroup;
    public Animator myAnimator;
    private bool check;
    private void Start()
    {
        myGroup.alpha = 0;
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
            StartCoroutine(FadeInScreen());
            check = false;
        }
    }
    IEnumerator FadeInScreen()
    {
        yield return new WaitForSeconds(startScene);
        yield return FadeScreen(myGroup, 1f, 2);
        
    }
    IEnumerator FadeOutScreen()
    {
        myAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(0.05f);
        yield return FadeScreen(myGroup, 0f, 2);
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
        yield return new WaitForSeconds(0.75f);
        myPlayer.enabled = true;
        yield return new WaitForEndOfFrame();
        videPlayer[MainmenuManager.modeSelected].gameObject.SetActive(true);
    }
}
