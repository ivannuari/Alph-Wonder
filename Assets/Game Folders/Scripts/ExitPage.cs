using GaweDeweStudio;
using UnityEngine;
using UnityEngine.UI;

public class ExitPage : Page
{
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    protected override void Start()
    {
        base.Start();

        yesButton.onClick.AddListener(() => { Application.Quit(); });
        noButton.onClick.AddListener(() => { GameManager.Instance.ChangeState(GameState.Menu); });
    }
}
