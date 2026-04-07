using GaweDeweStudio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Level2Card : MonoBehaviour
{
    [SerializeField] protected GameObject[] allStars;

    protected int level;

    protected Image _bg;
    protected Button _button;

    public void Setup(int newLevel, Sprite _sprite, int star)
    {
        _bg = GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PlayLevel);

        level = newLevel;

        if (_sprite != null)
        {
            _bg.sprite = _sprite;
        }
        SetStar(star);
    }

    protected void SetStar(int total)
    {
        foreach (var item in allStars) 
        {
            item.SetActive(false);
        }
        for (int i = 0; i < total; i++) 
        {
            allStars[i].SetActive(true);
        }
    }

    protected virtual void PlayLevel()
    {
        Level2Controller.Instance.SetLevel(level);
        GameManager.Instance.ChangeState(GameState.Game);
    }
}
