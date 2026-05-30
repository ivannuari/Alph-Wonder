using GaweDeweStudio;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game1Page : Page
{
    [SerializeField] private Button backButton;
    [SerializeField] private TopLetter[] allTopLetters;
    [SerializeField] private TMP_Text[] allSoals;

    [SerializeField] private GameObject[] allGameContents;

    [SerializeField] private GameObject strip5;

    [SerializeField] private RectTransform contentArea;
    [SerializeField] private ColorDot colorDotPrefab;

    [SerializeField] private float minDistance = 80f; // jarak minimal antar dot
    [SerializeField] private int maxTries = 50; // biar gak infinite loop

    [SerializeField] private string soundId;
    [SerializeField] private float soundDelay;

    [SerializeField] private TMP_Text timerText;
    [SerializeField] private bool timerStart = false;

    private int timer = 50;
    private Coroutine countDownRoutine;

    private void OnEnable()
    {
        SetLevel(Level1Controller.Instance.dataSoal[Level1Controller.Instance.GetLevel()]);
        timer = 50;
        timerStart = true;
        countDownRoutine = StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        while (timerStart)
        {
            timerText.text = timer.ToString();
            yield return new WaitForSeconds(1f);

            timer--;
            if (timer == 0)
            {
                timerStart = false;
                Level1Controller.Instance.GameOver();
            }
        }
    }

    protected override void Start()
    {
        base.Start();
        backButton.onClick.AddListener(() =>
        {
            Level1Controller.Instance.ClearLines();
            GameManager.Instance.ChangeState(GameState.Level1);
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            Time.timeScale = 0f;
        }
    }

    private void SetLevel(DataSoalLevel1 data)
    {
        if(data.letters.Length > 4) 
        {
            strip5.SetActive(true);
            allSoals[4].gameObject.SetActive(true);
        }
        else
        {
            strip5.SetActive(false);
            allSoals[4].gameObject.SetActive(false);
        }

        for (int i = 0; i < allTopLetters.Length; i++)
        {
            allTopLetters[i].Setup(data.opsiJawaban[i], data.opsiColor[i]);
        }

        for (int i = 0;i < data.letters.Length; i++)
        {
            allSoals[i].text = data.letters[i];
        }

        SetGameContentLevel(data);
        StartCoroutine(StartAnimations(data.letters));
    }

    IEnumerator StartAnimations(string[] letters)
    {
        int level = Level1Controller.Instance.GetLevel();
        ColorDot[] levelDots = allGameContents[level].GetComponentsInChildren<ColorDot>();

        GameManager.Instance.GetSound().PlaySound(soundId);
        yield return new WaitForSeconds(soundDelay);

        for (int i = 0; i < letters.Length; i++)
        {
            allSoals[i].GetComponent<Animator>().Play("Start");
            GameManager.Instance.GetSound().PlaySound(allSoals[i].text);
            ColorDot dotPrefab = Array.Find(levelDots, x => x.key == letters[i]);
            if (dotPrefab != null)
            { dotPrefab.StartAnimation(); }

            yield return new WaitForSeconds(1.5f);
        }
    }

    private void SetGameContentLevel(DataSoalLevel1 data)
    {
        int level = Level1Controller.Instance.GetLevel();
        foreach(var item in allGameContents)
        {
            item.SetActive(false);
        }

        allGameContents[level].SetActive(true);

        for (int i = 0; i < data.colors.Length; i++)
        {
            allGameContents[level].transform.GetChild(i).GetComponent<ColorDot>().SetColor(data.colors[i], data.letters[i]); 
        }
    }
}
