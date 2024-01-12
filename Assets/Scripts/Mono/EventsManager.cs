using System.Collections;
using UBlockly;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventsManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent wasPressedEvent, wasReleasedEvent, longPressEvent, wasDoublePressedEvent;
    private bool isPressedFirstTime = false, isPressed = false;
    public bool eventSucces = false;
    private float pressTime = 0f, longPressDuration = 1f, doublePressDelay = 0.3f;
    public static EventsManager instance;

    private void Awake() => instance = this;

	public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        if (isPressedFirstTime)
        {
            if (Time.time - pressTime < doublePressDelay)
            {
                isPressedFirstTime = false;
                wasDoublePressedEvent.Invoke();
            }
        }
        else
        {
            isPressedFirstTime = true;
        }
        pressTime = Time.time;
        wasPressedEvent.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        wasReleasedEvent.Invoke();

        isPressed = false;
        if (Time.time - pressTime > longPressDuration )
        {
            longPressEvent.Invoke();
        }
    }
 
    public void CheckEventSucces()
    {
        eventSucces = true;
    }
    public void UnCheckEventSucces()
    {
        eventSucces = false;
    }
    public bool GetIsPressed() => isPressed;
    public bool GetIsReleased() => !isPressed;
}