using GaweDeweStudio;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
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
        menuButton.onClick.AddListener(() => SceneManager.LoadSceneAsync("Main Menu"));

        if(level1)
        {
            selectLevelButton.onClick.AddListener(()=> GameManager.Instance.ChangeState(GameState.Level1));
        }
        
        if (level2)
        {
            selectLevelButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Level2));
        }

        if(level3)
        {
            selectLevelButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Level3));
        }
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

        int star = 3;
        StartCoroutine(SetStars(star));

        GameManager.Instance.levelData1.Save(level, 3);
    }

    private void SetLevel2()
    {
        int poin = Level2Controller.Instance.GetScore();
        int star = 0;
        int level = Level2Controller.Instance.GetLevel();

        soalImage.sprite = Level2Controller.Instance.GetTitleImage();
        resultText.text = $"{poin}/30";

        if (poin > 20) { star = 3; }
        if (poin > 10 && poin < 20) { star = 2; }
        if (poin > 0 && poin < 10) { star = 1; }

        StartCoroutine(SetStars(star));

        GameManager.Instance.levelData2.Save(level, star);
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

    private void SetLevel3()
    {
        int poin = Level3Controller.Instance.GetScore();
        int level = Level3Controller.Instance.GetLevel();

        int star = 3;
        StartCoroutine(SetStars(star));

        GameManager.Instance.levelData3.Save(level, 3);
    }
}
