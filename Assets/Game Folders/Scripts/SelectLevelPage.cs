using GaweDeweStudio;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevelPage : Page
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button level1Button;
    [SerializeField] private Button level2Button;
    [SerializeField] private Button level3Button;

    private Button[] allButtons;

    protected override void Start()
    {
        base.Start();

        allButtons = GetComponentsInChildren<Button>();

        backButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Menu));
        level1Button.onClick.AddListener(() => SelectLevel(1));
        level2Button.onClick.AddListener(() => SelectLevel(2));
        level3Button.onClick.AddListener(() => SelectLevel(3));
    }

    private void SelectLevel(int level)
    {
        foreach (var button in allButtons) 
        {
            button.interactable = false;
        }

        if (level == 1)
        {
            GameManager.Instance.isFromGame = true;
            GameManager.Instance.GetSound().PlaySound("Coding Garis");
            StartCoroutine(LoadScene("Game Koding Garis Huruf"));
        }

        if (level == 2)
        {
            GameManager.Instance.isFromGame = true;
            GameManager.Instance.GetSound().PlaySound("Koleksi Huruf");
            StartCoroutine(LoadScene("Game Koleksi Huruf"));
        }

        if (level == 3)
        {
            GameManager.Instance.isFromGame = true;
            GameManager.Instance.GetSound().PlaySound("Coding Warna");
            StartCoroutine(LoadScene("Game Koding Warna Huruf"));
        }
    }

    IEnumerator LoadScene(string key)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync(key);
    }
}
