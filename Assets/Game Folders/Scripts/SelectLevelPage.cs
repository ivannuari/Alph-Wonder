using GaweDeweStudio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevelPage : Page
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button level1Button;
    [SerializeField] private Button level2Button;
    [SerializeField] private Button level3Button;

    protected override void Start()
    {
        base.Start();

        backButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Menu));
        level1Button.onClick.AddListener(() => SelectLevel(1));
        level2Button.onClick.AddListener(() => SelectLevel(2));
        level3Button.onClick.AddListener(() => SelectLevel(3));
    }

    private void SelectLevel(int level)
    {
        if (level == 1)
        {
            GameManager.Instance.isFromGame = true;
            SceneManager.LoadSceneAsync("Game Koding Garis Huruf");
        }

        if (level == 2)
        {
            GameManager.Instance.isFromGame = true;
            SceneManager.LoadSceneAsync("Game Koleksi Huruf");
        }

        if (level == 3)
        {
            GameManager.Instance.isFromGame = true;
            SceneManager.LoadSceneAsync("Game Koding Warna Huruf");
        }
    }
}
