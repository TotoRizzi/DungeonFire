using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickController : Controller, IDragHandler, IEndDragHandler
{
    Vector3 inicialPosition;
    Vector3 dir;

    float maxJoyStickRange;
    [SerializeField] float joyStickScreenPercentage;

    private void Start()
    {
        maxJoyStickRange = Screen.height / joyStickScreenPercentage;
        inicialPosition = transform.position;
    }
    public override Vector3 GetDir()
    {
        return new Vector3(dir.x, 0 , dir.y).normalized;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dir = Vector3.ClampMagnitude((Vector3)eventData.position - inicialPosition, maxJoyStickRange);

        transform.position = dir + inicialPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = inicialPosition;
        dir = Vector3.zero;
    }
}
