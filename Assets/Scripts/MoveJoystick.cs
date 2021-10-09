using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
     private Image threshold;
     private Image touch;


     public Vector3 InputDir;

     public bool shoot;

     public void OnDrag(PointerEventData eventData)
     {
         Vector2 position = Vector2.zero;

         if(RectTransformUtility.ScreenPointToLocalPointInRectangle(threshold.rectTransform,
             eventData.position,
             eventData.pressEventCamera,
             out position))
         {
             position.x = (position.x / threshold.rectTransform.sizeDelta.x);
             position.y = (position.y / threshold.rectTransform.sizeDelta.y);

             float x = (threshold.rectTransform.pivot.x == 1f) ? position.x * 2 + 1 : position.x * 2 - 1;
             float y = (threshold.rectTransform.pivot.y == 1f) ? position.y * 2 + 1 : position.y * 2 - 1;

             InputDir = new Vector3(x,y,0);
             InputDir = (InputDir.magnitude > 1) ? InputDir.normalized : InputDir;

             touch.rectTransform.anchoredPosition = new Vector3(InputDir.x * (threshold.rectTransform.sizeDelta.x / 2.5f),
                 InputDir.y * (threshold.rectTransform.sizeDelta.y / 2.5f));
         }
     }

     public void OnPointerDown(PointerEventData eventData)
     {
         OnDrag(eventData);
     }

     public void OnPointerUp(PointerEventData eventData)
     {
         InputDir = Vector3.zero;
         touch.rectTransform.anchoredPosition = Vector3.zero;
     }
 
     void Start()
     {
         threshold = GetComponent<Image>();
         touch = transform.GetChild(0).GetComponent<Image>();
         InputDir = Vector3.zero;
     }

}