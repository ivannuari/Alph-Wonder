using GaweDeweStudio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private Page[] allPages;

    [SerializeField] private GameObject stickerWidget;

    [SerializeField] private string soundKey;

    private void Start()
    {
        allPages = GetComponentsInChildren<Page>(true);

        GameManager.Instance.OnStateChanged += Instance_OnStateChanged;
        GameManager.Instance.OnWidgetOpened += ShowWidget;

        if (!GameManager.Instance.GetSound().IsBgmPlay())
        {
            GameManager.Instance.GetSound().PlaySound("BGM");
        }

        UnityEngine.SceneManagement.Scene s = SceneManager.GetActiveScene();
        if(s.name == "Main Menu" && GameManager.Instance.isFromGame)
        {
            ChangePage(PageName.SelectLevel);
        }

        if (!string.IsNullOrEmpty(soundKey)) 
        {
            GameManager.Instance.GetSound().PlaySound(soundKey);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStateChanged -= Instance_OnStateChanged;
        GameManager.Instance.OnWidgetOpened -= ShowWidget;
    }

    private void Instance_OnStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Menu:
                ChangePage(PageName.Menu);
                break;
            case GameState.SelectLevel:
                ChangePage(PageName.SelectLevel);
                break;
            case GameState.Setting:
                ChangePage(PageName.Setting);
                break;
            case GameState.Information:
                ChangePage(PageName.Information);
                break;
            case GameState.Exit:
                ChangePage(PageName.Exit);
                break;
            case GameState.Game:
                ChangePage(PageName.Game);
                break;
            case GameState.Level1:
                ChangePage(PageName.SelectLevel1);
                break;
            case GameState.Level2:
                ChangePage(PageName.SelectLevel2);
                break;
            case GameState.Level3:
                ChangePage(PageName.SelectLevel3);
                break;
            case GameState.Result:
                ChangePage(PageName.Result);
                break;
        }
    }

    private void ChangePage(PageName key)
    {
        foreach (Page page in allPages) 
        {
            page.DisablePage();
        }

        Page _findPage = Array.Find(allPages, x => x.pageName == key);
        if (_findPage != null)
        {
            _findPage.gameObject.SetActive(true);
        }
    }

    public void ShowWidget()
    {
        stickerWidget.SetActive(true);
    }
}
