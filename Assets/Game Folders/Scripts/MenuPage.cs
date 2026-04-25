using GaweDeweStudio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuPage : Page
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button informationButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Toggle muteToggle;

    private bool isHide = true;

    protected override void Start()
    {
        base.Start();

        playButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.SelectLevel));
        settingButton.onClick.AddListener(() => ShowAudio());
        informationButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Information));
        exitButton.onClick.AddListener(()=> GameManager.Instance.ChangeState(GameState.Exit));
        muteToggle.onValueChanged.AddListener((isMute) =>
        {
            GameManager.Instance.GetSound().MuteBGM(isMute);
        } );
    }

    private void ShowAudio()
    {
        isHide = !isHide;
        muteToggle.gameObject.SetActive(isHide);
    }
}
