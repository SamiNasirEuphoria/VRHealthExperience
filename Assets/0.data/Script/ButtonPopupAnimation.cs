using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonPopupAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private RectTransform buttonTransform;

    void Start()
    {
        button = GetComponent<Button>();
        buttonTransform = button.GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Scale up animation on pointer enter
        buttonTransform.DOScale(Vector3.one * 1.2f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Scale down animation on pointer exit
        buttonTransform.DOScale(Vector3.one, 0.2f);
    }
}
