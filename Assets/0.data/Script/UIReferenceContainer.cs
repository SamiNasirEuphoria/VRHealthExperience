using UnityEngine;
using UnityEngine.UI;

public class UIReferenceContainer : MonoBehaviour
{
    private static UIReferenceContainer instance;
    public static UIReferenceContainer Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    public GameObject welcomeScreen, introScreen, controllerScreen,modesScreen;
    public Button welcomeScreenButton, introScreenButton, controllerScreenButton, modeScreenButton;
}
