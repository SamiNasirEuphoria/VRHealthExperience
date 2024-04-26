using UnityEngine;
using RenderHeads.Media.AVProVideo;
using BNG;
using System.Collections;
using UnityEngine.Events;

public class ExperienceManager : MonoBehaviour
{
    public ApplyToMesh meshMedia;
    public MediaPlayer videoPlayer;
    public UnityEngine.UI.Button exit, play,pause, rewind;
   
    public GameObject outerSphere, canvesPanel, camreferenceObject, prefab;
    private ApplyToMesh myPlayer;
    public CanvasGroup myGroup;
    public Animator myAnimator;
    public float startScene, fadeInScreen, fadeOutScreen;
    private bool check, _check;
    public UnityEvent buttonResetState;
    public UIPointer raycastLine;
    private GameObject raycast;
    //private string basicUrl= "/Users/mac/Downloads/";
    private string basicUrl = "/storage/emulated/0/Movies/";
    private void Start()
    {
        myGroup.alpha = 0;
        canvesPanel.SetActive(false);
        myPlayer = outerSphere.GetComponent<ApplyToMesh>();
        //myPlayer.enabled = false;
        //foreach (MediaPlayer player in videoPlayer)
        //{
        //    player.gameObject.SetActive(false);
        //}
        //meshMedia.Player = videoPlayer[MainmenuManager.modeSelected];

        


        exit.onClick.AddListener(Exit);
        play.onClick.AddListener(Play);
        pause.onClick.AddListener(Pause);
        rewind.onClick.AddListener(Rewind);
        StartCoroutine(Wait());
        check = true;
        _check = true;

        if (raycastLine._cursor)
        {
            raycast = raycastLine._cursor;
        }

    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    DropObjects();
        //}
        if (InputBridge.Instance.RightTriggerDown && check)
        {
            //CanvesPosition();
            //DropObjects();
            canvesPanel.SetActive(true);
            StartCoroutine(FadeInScreen());
            check = false;
        }
    }
    //public void CanvesPosition()
    //{
    //    if (_check)
    //    {
    //        canvesPanel.transform.SetParent(camreferenceObject.transform);
    //        canvesPanel.SetActive(true);
    //        canvesPanel.transform.localPosition = new Vector3(0, 0, 0);
    //        canvesPanel.transform.localEulerAngles = new Vector3(0, 0, 0);
    //        //canvesPanel.transform.SetParent(null);
    //        //canvesPanel.transform.position = camreferenceObject.transform.position;
    //        _check = false;
    //    }

    //}
    public void DropObjects()
    {
        GameObject newPlane = Instantiate(prefab, camreferenceObject.transform);
        // Set the parent of the new GameObject to the parentObject
        newPlane.transform.parent = null;

    }
    public void ButtonsState(bool check)
    {
        exit.interactable = check;
        play.interactable = check;
        rewind.interactable = check;
        pause.interactable = check;
    }
    IEnumerator FadeInScreen()
    {
        raycast.SetActive(true);
        Debug.Log("Calling screen fade in");
        yield return new WaitForSeconds(0.05f);
        yield return FadeScreen(myGroup, 1f, fadeInScreen);
        ButtonsState(true);
        StartCoroutine(FadeOutScreen());
    }
    IEnumerator FadeOutScreen()
    {
        Debug.Log("calling screen fade out");
        yield return new WaitForSeconds(7.5f);
        ButtonsState(false);
        yield return FadeScreen(myGroup, 0f, fadeOutScreen);
        canvesPanel.SetActive(false);
        check = true;
        raycast.SetActive(false);
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
        StopAllCoroutines();
        //videoPlayer[MainmenuManager.modeSelected].gameObject.SetActive(true);
        videoPlayer.gameObject.SetActive(true);
        StartCoroutine(FadeOutScreen());
    }
    public void Pause()
    {
        StopAllCoroutines();
        //videoPlayer[MainmenuManager.modeSelected].gameObject.SetActive(false);
        videoPlayer.gameObject.SetActive(false);
    }
    public void Rewind()
    {
        StopAllCoroutines();
        //videoPlayer[MainmenuManager.modeSelected].GetComponent<MediaPlayer>().Rewind(true);
        //videoPlayer[MainmenuManager.modeSelected].GetComponent<MediaPlayer>().Play();
        videoPlayer.Rewind(true);
        videoPlayer.Play();
        StartCoroutine(FadeOutScreen());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(startScene);
        yield return new WaitForSeconds(0.25f);
        myPlayer.enabled = true;
        yield return new WaitForEndOfFrame();
        videoPlayer.OpenMedia(new MediaPath(basicUrl + "OH_Video_" + MainmenuManager.modeSelected + ".mp4", MediaPathType.AbsolutePathOrURL), autoPlay: true);

        //videoPlayer[MainmenuManager.modeSelected].gameObject.SetActive(true);
    }
}
