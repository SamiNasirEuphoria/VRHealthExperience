using UnityEngine;
using UnityEngine.UI;

public class SelectMode : MonoBehaviour
{
    private Button myButton;
    public int modeNumber;
    // Start is called before the first frame update
    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(ClickButton);
    }
    private void ClickButton()
    {
        MainmenuManager.modeSelected = modeNumber;
    } 
}
