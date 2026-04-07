using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LineDrawer : MonoBehaviour
{
    public static LineDrawer Instance;

    [SerializeField] private UiLine linePrefab;
    [SerializeField] private RectTransform canvasRect;

    public List<UiLine> allLines = new List<UiLine>();

    private UiLine currentLine;
    private ColorDot startDot;
    private Level1Controller levelController;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        levelController = GetComponent<Level1Controller>();
    }

    private void Update()
    {
        if (currentLine != null)
        {
            Vector2 mousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                Input.mousePosition,
                null,
                out mousePos
            );

            currentLine.SetPosition(startDot.GetUIPos(canvasRect), mousePos);
        }
    }

    public void StartLine(ColorDot dot)
    {
        startDot = dot;

        currentLine = Instantiate(linePrefab, canvasRect);
        currentLine.SetColor(GetColor(dot.color), dot.color);
    }

    public void EndLine()
    {
        if (currentLine == null) return;

        ColorDot endDot = GetDotUnderMouse();

        if (endDot == null || endDot == startDot)
        {
            Destroy(currentLine.gameObject);
            ResetLine();
            return;
        }

        currentLine.SetPosition(
            startDot.GetUIPos(canvasRect),
            endDot.GetUIPos(canvasRect)
        );

        startDot.Connected();

        allLines.Add(currentLine);
        Level1Controller.Instance.SetLetterColor(startDot.color, endDot.color);

        ResetLine();
    }

    private ColorDot GetDotUnderMouse()
    {
        PointerEventData pointerData = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        UnityEngine.EventSystems.EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            ColorDot dot = result.gameObject.GetComponent<ColorDot>();
            if (dot != null)
                return dot;
        }

        return null;
    }

    private void ResetLine()
    {
        currentLine = null;
        startDot = null;
    }

    private Color GetColor(LetterColor c)
    {
        switch (c)
        {
            case LetterColor.Red: return Color.red;
            case LetterColor.Blue: return Color.blue;
            case LetterColor.Green: return Color.green;
            case LetterColor.Yellow: return Color.yellow;
            case LetterColor.Gray: return Color.gray;
            case LetterColor.SkyBlue: return Color.cyan;
        }
        return Color.white;
    }

    public void ClearLines()
    {
        foreach (var item in allLines)
        {
            Destroy(item.gameObject);
        }
    }
}