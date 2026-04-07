using GaweDeweStudio;
using UnityEngine;
using UnityEngine.UI;

public class SettingPage : Page
{
    [SerializeField] private Button backButton;

    protected override void Start()
    {
        base.Start();
        backButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Menu));
    }
}
