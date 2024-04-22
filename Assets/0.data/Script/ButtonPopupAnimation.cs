using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonPopupAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private RectTransform buttonTransform;
    private bool check;
    public bool isPlayPause;
    void Start()
    {
        button = GetComponent<Button>();
        button.interactable = true;
        buttonTransform = button.GetComponent<RectTransform>();
        button.onClick.AddListener(Clicked);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //if (!isPlayPause)
        //{
            if (!check)
            {
                buttonTransform.DOScale(Vector3.one * 1.05f, 0.25f);
            }
        //}
       
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //if (!isPlayPause)
        //{
            //if (!check)
            //{
                buttonTransform.DOScale(Vector3.one, 0.25f);
            //}
        //}
       
    }
    public void Clicked()
    {
        if (!isPlayPause)
        {
            button.interactable = false;
            check = true;
        }
    }
}
