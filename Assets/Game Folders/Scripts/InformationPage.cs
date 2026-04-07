using GaweDeweStudio;
using UnityEngine;
using UnityEngine.UI;

public class InformationPage : Page
{
    [SerializeField] private Button backButton;

    protected override void Start()
    {
        base.Start();
        backButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Menu));
    }
}
