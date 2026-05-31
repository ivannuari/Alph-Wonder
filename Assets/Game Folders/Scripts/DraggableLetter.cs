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

    [SerializeField] private RectTransform dragArea;

    private Vector3 startPosition;
    private Animator _anim;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        _anim = GetComponent<Animator>();

        startPosition = transform.position;
    }

    private void OnEnable()
    {
        Level3Controller.Instance.OnLevelChanged += Instance_OnLevelChanged;
    }

    private void OnDisable()
    {
        Level3Controller.Instance.OnLevelChanged -= Instance_OnLevelChanged;
    }

    private void Instance_OnLevelChanged(DataSoalLevel3 data)
    {
        transform.parent = null;
        transform.position = startPosition;
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
        //rectTransform.position = Input.mousePosition;
        Vector2 localPoint;

        // Convert mouse position ke local position area
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            dragArea,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint))
        {
            // Ambil batas area
            float clampedX = Mathf.Clamp(
                localPoint.x,
                dragArea.rect.xMin,
                dragArea.rect.xMax);

            float clampedY = Mathf.Clamp(
                localPoint.y,
                dragArea.rect.yMin,
                dragArea.rect.yMax);

            // Set posisi object
            rectTransform.localPosition = new Vector3(clampedX, clampedY, 0);
            var pos = Camera.main.WorldToScreenPoint(Input.mousePosition);
            Debug.LogWarning($"local point: {localPoint}, mouse: {pos.x},{pos.y}");
        }
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