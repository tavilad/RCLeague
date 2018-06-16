using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image _backgroundImage;
    private Image _joystickImage;

    private Vector3 _inputVector;

    public Vector3 InputVector
    {
        get { return _inputVector; }
    }

    // Use this for initialization
    void Start()
    {
        this._backgroundImage = GetComponent<Image>();
        this._joystickImage = transform.GetChild(0).GetComponent<Image>();
    }


    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this._backgroundImage.rectTransform,
            eventData.position,
            eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / this._backgroundImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / this._backgroundImage.rectTransform.sizeDelta.y);

            float x = (this._backgroundImage.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
            float y = (this._backgroundImage.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

            this._inputVector = new Vector3(x, 0, y);
            this._inputVector = (this._inputVector.magnitude > 1) ? this._inputVector.normalized : this._inputVector;
            this._joystickImage.rectTransform.anchoredPosition = new Vector3(
                this._inputVector.x * (this._backgroundImage.rectTransform.sizeDelta.x / 3)
                , this._inputVector.z * (this._backgroundImage.rectTransform.sizeDelta.y / 3));
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this._inputVector = Vector3.zero;
        this._joystickImage.rectTransform.anchoredPosition = Vector3.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
}