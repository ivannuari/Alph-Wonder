using GaweDeweStudio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public LetterColor slotColor;
    [SerializeField] private Image colorImage;

    public string key;
    public bool isFilled = false;

    private GameObject letterObj;
    private Animator _anim;

    private void OnEnable()
    {
        _anim = GetComponent<Animator>();
        isFilled = false;
    }

    public void Setup(LetterColor color,string k)
    {
        key = k;

        slotColor = color;
        Color c = Color.white;

        switch (color)
        {
            case LetterColor.Green:
                c = Color.green;
                break;
            case LetterColor.Blue:
                c = Color.blue;
                break;
            case LetterColor.Red:
                c = Color.red;
                break;
            case LetterColor.Yellow:
                c = Color.yellow;
                break;
            case LetterColor.Gray:
                c = Color.gray;
                break;
            case LetterColor.SkyBlue:
                c = Color.deepSkyBlue;
                break;
        }

        colorImage.color = c;
    }

    public void OnDrop(PointerEventData eventData)
    {
        DraggableLetter letter = eventData.pointerDrag.GetComponent<DraggableLetter>();

        if (letter == null) return;

        if (letter.letterColor == slotColor)
        {
            RectTransform letterRect = letter.GetComponent<RectTransform>();

            letterRect.SetParent(transform);
            letterRect.anchoredPosition = Vector2.zero;
            letterRect.localScale = Vector3.one;

            letterObj = letterRect.gameObject;

            // 🔥 play animasi
            letter.PlayCorrectAnimation();
            GameManager.Instance.GetSound().PlaySound(letter.text.text);

            // kirim jawaban ke controller
            Level3Controller.Instance.AddAnswer(letter.text.text);

            isFilled = true;
            // 🔥 cek apakah semua slot sudah terisi
            Level3Controller.Instance.CheckAllFilled();

        }
        else
        {
            letter.ReturnToStart();
        }
    }

    public void StartAnimation()
    {
        _anim.Play("Start");
    }

    public void ResetSoal()
    {
    }
}
