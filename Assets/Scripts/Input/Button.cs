using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Image))]
public class Button : MonoBehaviour , IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    //Pressed is for when the button is first pressed, lasts for one frame
    private bool pressed = false;
    public bool IsPressed
    {
        get { return pressed; }
    }

    //HeldDown is true after pressed is set to false and the button is still pressed
    private bool heldDown = false;
    public bool IsHeldDown
    {
        get { return heldDown; }
    }

    private bool disabled = false;
    public bool Disabled
    {
        get { return disabled; }
        set
        {
            disabled = value;
            ResetButton();

            if(disabled)
            {
                SetImageSprite(disabledSprite);
            }
            else
            {
                SetImageSprite(baseSprite);
            }
        }
    }

    private Image targetGraphic; //Image component of the button
    public Sprite baseSprite, pressedSprite, disabledSprite; //Three sprites that can be set based on button state

    void Start()
    {
        targetGraphic = GetComponent<Image>();
        targetGraphic.type = Image.Type.Simple;
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (!disabled)
        {
            Debug.Log(name + " Pressed ...");
            StartCoroutine(PressButton());
            SetImageSprite(pressedSprite);
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (pressed || heldDown)
        {
            Debug.Log(name + " Released ...");
            ResetButton();
            SetImageSprite(baseSprite);
        }
    }
   
    public void OnPointerExit(PointerEventData data)
    {
        if (pressed || heldDown)
        {
            Debug.Log(name + " Released ...");
            ResetButton();
            SetImageSprite(baseSprite);
        }
    }

    private IEnumerator PressButton()
    {
        pressed = true;
        yield return new WaitForEndOfFrame();
        pressed = false;
        heldDown = true;
    }

    private void ResetButton()
    {
        pressed = false;
        heldDown = false;
    }

    private void SetImageSprite(Sprite sprite)
    {
        targetGraphic.sprite = sprite;
    }
}
