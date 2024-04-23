using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModeSelectionHandler : MonoBehaviour
{
    public Button[] buttons; // Array of buttons

    void Start()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true; // Disable the button
            button.GetComponent<ButtonPopupAnimation>().enabled = true;
        }
        // Add listeners to all buttons in the array
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(DisableAllButtons);
        }
    }
    // Method to disable all buttons
    void DisableAllButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false; // Disable the button
            button.GetComponent<ButtonPopupAnimation>().enabled = false;
        }
    }

}
