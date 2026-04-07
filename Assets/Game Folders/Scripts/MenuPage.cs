using GaweDeweStudio;
using UnityEngine;
using UnityEngine.UI;

public class MenuPage : Page
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button informationButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Toggle muteToggle;

    protected override void Start()
    {
        base.Start();

        playButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.SelectLevel));
        settingButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Setting));
        informationButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Information));
        exitButton.onClick.AddListener(()=> GameManager.Instance.ChangeState(GameState.Exit));
        muteToggle.onValueChanged.AddListener((isMute) =>
        {
            GameManager.Instance.GetSound().MuteBGM(isMute);
        } );
    }
}
