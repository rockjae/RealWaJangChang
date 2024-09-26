using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Image joyStickBackGround;
    private Image joyStick;
    private Vector2 touchPosition;

    private void Awake()
    {
        joyStickBackGround = GetComponent<Image>();
        joyStick = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        /*throw new System.NotImplementedException();*/
    }

    public void OnDrag(PointerEventData eventData)
    {
        touchPosition = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joyStickBackGround.rectTransform, eventData.position, eventData.pressEventCamera, out touchPosition) )
        {
            touchPosition.x = (touchPosition.x / joyStickBackGround.rectTransform.sizeDelta.x);
            touchPosition.y = (touchPosition.y / joyStickBackGround.rectTransform.sizeDelta.y);

            touchPosition = new Vector2(touchPosition.x * 2 - 1, touchPosition.y * 2 - 1);

            touchPosition = (touchPosition.magnitude > 1) ? touchPosition.normalized : touchPosition;

            joyStick.rectTransform.anchoredPosition = new Vector2(
                touchPosition.x * joyStickBackGround.rectTransform.sizeDelta.x / 2,
                touchPosition.y * joyStickBackGround.rectTransform.sizeDelta.y / 2);

            
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joyStick.rectTransform.anchoredPosition = Vector2.zero;

        touchPosition = Vector2.zero;
    }

    public float Horizontal()
    {
        return touchPosition.x;
    }
    public float Vertical()
    {        
        return touchPosition.y;
    }

}
