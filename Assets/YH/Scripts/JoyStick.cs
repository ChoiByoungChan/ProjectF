using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    [SerializeField] private RectTransform rectBackGround;
    [SerializeField] private RectTransform rectJoyStcik;
    private float radius;
    [SerializeField] private Player player;
    [SerializeField] private float moveSpeed;

    private bool isTouch = false;
    public Vector3 movePosition;

    private void Start()
    {
        moveSpeed = 70f;
        radius = rectBackGround.rect.width * 0.5f;
    }
    private void Update()
    {
        if (isTouch)
        {
            //player.transform.position += movePosition;
           player.Move(movePosition);
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 value = eventData.position - (Vector2)rectBackGround.position;
        value = Vector2.ClampMagnitude(value,radius);
        rectJoyStcik.localPosition = value;
        float distance = Vector2.Distance(rectBackGround.position, rectJoyStcik.position) / radius;
        value = value.normalized;
        movePosition = new Vector3(value.x * moveSpeed * distance * Time.deltaTime, 0f, value.y * moveSpeed * distance * Time.deltaTime);
        //movePosition = transform.InverseTransformDirection(movePosition);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 value = eventData.position - (Vector2)rectBackGround.position;
        value = Vector2.ClampMagnitude(value, radius);
        rectJoyStcik.localPosition = value;
        float distance = Vector2.Distance(rectBackGround.position, rectJoyStcik.position) / radius;
        value = value.normalized;
        movePosition = new Vector3(value.x * moveSpeed * distance * Time.deltaTime, 0f, value.y * moveSpeed * distance * Time.deltaTime);
        isTouch = true;
        //movePosition = transform.InverseTransformDirection(movePosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false;
        rectJoyStcik.localPosition = Vector3.zero;
        movePosition = Vector3.zero;
    }
}
