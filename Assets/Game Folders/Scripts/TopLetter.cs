using GaweDeweStudio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopLetter : MonoBehaviour
{
    public TMP_Text text;
    [SerializeField] private Image image;

    private Animator _anim;

    private void OnEnable()
    {
        _anim = GetComponent<Animator>();
    }

    public void Setup(string t, LetterColor color)
    {
        text.text = t;
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

        image.color = c;
    }

    public void StartAnimation()
    {
        _anim.Play("Start");
        GameManager.Instance.GetSound().PlaySound(text.text);
    }
}