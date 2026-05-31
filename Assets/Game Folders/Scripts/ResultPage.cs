using GaweDeweStudio;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultPage : Page
{
    [SerializeField] private Button selectLevelButton;
    [SerializeField] private Button menuButton;

    [SerializeField] private Image[] allStars;

    [SerializeField] private Image soalImage;
    [SerializeField] private TMP_Text poinText;

    [SerializeField] private TMP_Text resultText;

    public bool level1, level2, level3;

    protected override void Start()
    {
        base.Start();
        //menuButton.onClick.AddListener(() => SceneManager.LoadSceneAsync("Main Menu"));

        selectLevelButton.onClick.AddListener(() =>
        {
            if(level1)
            {
                GameManager.Instance.ChangeState(GameState.Level1);
            }
            if(level2)
            {
                GameManager.Instance.ChangeState(GameState.Level2);
            }
            if(level3)
            {
                GameManager.Instance.ChangeState(GameState.Level3);
            }
        });

        menuButton.onClick.AddListener(() =>
        {
            if (level1) 
            {
                int level = Level1Controller.Instance.GetLevel();
                level++;
                if (level > 7) { return; }
                Level1Controller.Instance.SetLevel(level);
                Level1Controller.Instance.ResetSoal();
                GameManager.Instance.ChangeState(GameState.Game);
            }
            if (level2) 
            {
                int level = Level2Controller.Instance.GetLevel();
                level++;
                if (level > 25) { return; }
                Level2Controller.Instance.SetLevel(level);
                Level2Controller.Instance.ResetSoal();
                GameManager.Instance.ChangeState(GameState.Game);
            }
            if (level3) 
            {
                int level = Level3Controller.Instance.GetLevel();
                level++;
                if(level > 7) { return; }
                Level3Controller.Instance.SetLevel(level);
                Level3Controller.Instance.ResetSoal();
                GameManager.Instance.ChangeState(GameState.Game);
            }
        });
    }

    private void OnEnable()
    {
        GameManager.Instance.GetSound().PlaySound("Game Over");

        if (level1) { SetLevel1(); }
        if (level2) { SetLevel2(); }
        if (level3) { SetLevel3(); }
    }

    private void SetLevel1()
    {
        int poin = Level1Controller.Instance.GetScore();
        int level = Level1Controller.Instance.GetLevel();

        int star = poin;
        StartCoroutine(SetStars(star));

        int sc = poin > 2 ? 100 : 0;
        resultText.text = $"{sc}%";

        GameManager.Instance.levelData1.Save(level, star);
    }

    private void SetLevel2()
    {
        int poin = Level2Controller.Instance.GetScore();
        int star = 0;
        int level = Level2Controller.Instance.GetLevel();

        soalImage.sprite = Level2Controller.Instance.GetTitleImage();
        resultText.text = $"{poin}/15";

        if (poin > 10) { star = 3; }
        if (poin > 5 && poin < 10) { star = 2; }
        if (poin > 0 && poin < 5) { star = 1; }

        StartCoroutine(SetStars(star));

        GameManager.Instance.levelData2.Save(level, star);
    }

    private void SetLevel3()
    {
        int poin = Level3Controller.Instance.GetScore();
        int level = Level3Controller.Instance.GetLevel();

        int star = poin;
        StartCoroutine(SetStars(star));

        int sc = poin > 2 ? 100 : 0;
        resultText.text = $"{sc}%";

        GameManager.Instance.levelData3.Save(level, star);
    }

    IEnumerator SetStars(int star)
    {
        foreach (var item in allStars)
        {
            item.gameObject.SetActive(false);
        }

        for (int i = 0; i < star; i++)
        {
            yield return new WaitForSeconds(1f);
            allStars[i].gameObject.SetActive(true);
            GameManager.Instance.GetSound().PlaySound("Star");
        }
    }
}
