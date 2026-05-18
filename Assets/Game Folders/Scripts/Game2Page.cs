using GaweDeweStudio;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game2Page : Page
{
    [SerializeField] private Button backButton;

    [SerializeField] private Image titleImage;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject[] hpImages;
    [SerializeField] private int hp;

    [SerializeField] private string soundId;

    protected override void Start()
    {
        base.Start();
        Level2Controller.Instance.OnScoreUpdated += Instance_OnScoreUpdated;
        Level2Controller.Instance.OnHealthUpdated += Instance_OnHealthUpdated;
        backButton.onClick.AddListener(() =>
        {
            Level2Controller.Instance.StopGame();
            GameManager.Instance.ChangeState(GameState.Level2);
        });
    }

    private void OnEnable()
    {
        titleImage.sprite = Level2Controller.Instance.GetTitleImage();
        scoreText.text = $"{0}/{15}";

        GameManager.Instance.GetSound().PlaySound(soundId);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    
        Level2Controller.Instance.OnScoreUpdated -= Instance_OnScoreUpdated;
        Level2Controller.Instance.OnHealthUpdated -= Instance_OnHealthUpdated;
    }

    private void Instance_OnHealthUpdated(int hp)
    {
        foreach (var hpImage in hpImages) 
        {
            hpImage.SetActive(false);
        }

        for (int i = 0; i < hp; i++)
        {
            hpImages[i].SetActive(true);
        }
    }

    private void Instance_OnScoreUpdated(int score)
    {
        scoreText.text = $"{score}/{15}";
    }
}
