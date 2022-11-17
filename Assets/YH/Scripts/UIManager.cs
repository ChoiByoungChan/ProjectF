using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : Singleton<UIManager>, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Player player;
    public Camera cam;
    Vector3 finishPosition;
    private new void Awake()
    {
        base.Awake();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            float speed = Time.deltaTime * 10f;
            Vector3 tpos = Input.GetTouch(0).position;
            if (tpos.x <= Screen.width / 2)
            {
                cam.transform.Rotate(-Vector3.right * speed);
                finishPosition = cam.transform.localEulerAngles;
            }
            else if (tpos.x >= Screen.width / 2)
            {
                cam.transform.Rotate(Vector3.right * speed);
                finishPosition = cam.transform.localEulerAngles;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        player.transform.forward = finishPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

}
