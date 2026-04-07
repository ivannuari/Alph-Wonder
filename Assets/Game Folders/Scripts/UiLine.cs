using UnityEngine;
using UnityEngine.UI;

public class UiLine : MonoBehaviour
{
    public string key;
    public LetterColor color;
    public LetterColor endColor;

    public RectTransform rect;
    public Image image;

    public void SetPosition(Vector2 start, Vector2 end)
    {
        Vector2 direction = (end - start);
        float distance = direction.magnitude;

        rect.sizeDelta = new Vector2(distance, 10f); // tebal garis

        rect.anchoredPosition = start + direction / 2f;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rect.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void SetColor(Color key, LetterColor lc)
    {
        image.color = key;
        color = lc;
    }
}