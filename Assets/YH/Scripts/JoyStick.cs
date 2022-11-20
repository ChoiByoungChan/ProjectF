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
    private Vector2 value;

    private void Start()
    {
        moveSpeed = 30f;
        radius = rectBackGround.rect.width * 0.5f;
    }
    private void Update()
    {
        if (isTouch)
        {
            //player.transform.position += movePosition * moveSpeed;
            player.Move(movePosition);
            this.player.transform.rotation = Quaternion.Euler(0f, Mathf.Atan2(-value.x, -value.y) * Mathf.Rad2Deg, 0f);
            player.SetState(player.playerWalkState);
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        value = eventData.position - (Vector2)rectBackGround.position;
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
        player.isMoveOver = true;
        isTouch = false;
        rectJoyStcik.localPosition = Vector3.zero;
        movePosition = Vector3.zero;
    }
}
