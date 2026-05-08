using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorDot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public LetterColor color;
    public bool isConnect = false;
    private Image colorImage;

    public string key;
    private Animator _anim;

    private void OnEnable()
    {
        _anim = GetComponent<Animator>();
    }

    public void SetColor(LetterColor letterColor,string k)
    {
        colorImage = GetComponent<Image>();

        key = k;

        color = letterColor;
        switch (letterColor)
        {
            case LetterColor.Green:
                colorImage.color = Color.green;
                break;
            case LetterColor.Blue:
                colorImage.color = Color.blue;
                break;
            case LetterColor.Red:
                colorImage.color = Color.red;
                break;
            case LetterColor.Yellow:
                colorImage.color = Color.yellow;
                break;
            case LetterColor.Gray:
                colorImage.color = Color.gray;
                break;
            case LetterColor.SkyBlue:
                colorImage.color = Color.skyBlue;
                break;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(isConnect) {return; }
        LineDrawer.Instance.StartLine(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        LineDrawer.Instance.EndLine();
    }

    public Vector2 GetUIPos(RectTransform canvasRect)
    {
        RectTransform rect = GetComponent<RectTransform>();

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            RectTransformUtility.WorldToScreenPoint(null, rect.position),
            null,
            out localPoint
        );

        return localPoint;
    }

    public void Connected()
    {
        isConnect = true;
    }

    public void StartAnimation()
    {
        _anim.Play("Start");
    }
}