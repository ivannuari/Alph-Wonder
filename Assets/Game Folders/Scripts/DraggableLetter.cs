using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableLetter : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public LetterColor letterColor;
    public TMP_Text text;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Vector3 startPosition;
    private Animator _anim;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        _anim = GetComponent<Animator>();
    }

    public void Setup(string t,LetterColor color)
    {
        letterColor = color;
        text.text = t;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = rectTransform.position;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void ReturnToStart()
    {
        rectTransform.position = startPosition;
    }

    public void PlayCorrectAnimation()
    {
        _anim.PlayInFixedTime("Correct");
    }
}


public enum LetterColor
{
    Green,
    Blue,
    Red,
    Yellow,
    Gray,
    SkyBlue
}