using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{

    [SerializeField] private UnityEvent buttonDown;
    [SerializeField] private UnityEvent buttonUp;
    [SerializeField] private UnityEvent buttonClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(buttonClick != null)
        {
            buttonClick.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (buttonDown != null)
        {
            buttonDown.Invoke();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (buttonUp != null)
        {
            buttonUp.Invoke();
        }
    }
}
