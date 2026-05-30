using GaweDeweStudio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level1Controller : MonoBehaviour
{
    public static Level1Controller Instance;

    public List<(LetterColor, LetterColor)> playerConnections = new();

    public DataSoalLevel1[] dataSoal;
    public List<LetterColor> allJawabans = new();
    public LetterColor[] jawaban;

    public bool isWin;

    [SerializeField] private int level;
    [SerializeField] private int poin;

    private LineDrawer _line;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        _line = GetComponent<LineDrawer>();
    }

    internal void SetLevel(int level)
    {
        this.level = level;
    }

    public void CheckJawaban(List<UiLine> allLines)
    {
        List<LetterColor> jawaban = new List<LetterColor>();
        for (int i = 0; i < allLines.Count; i++) 
        {
            jawaban.Add(allLines[i].color);
        }
    }

    public void SetLetterColor(LetterColor startColor,LetterColor endColor, string a, string b)
    {
        allJawabans.Add(startColor);

        StartCoroutine(PlaySound(a, b, endColor));
    }

    IEnumerator PlaySound(string a, string b, LetterColor endColor)
    {
        GameManager.Instance.GetSound().PlaySound(a);
        yield return new WaitForSeconds(1f);

        GameManager.Instance.GetSound().PlaySound(b);
        yield return new WaitForSeconds(1f);

        CheckAnswer(endColor);
    }

    private void CheckAnswer(LetterColor endColor)
    {
        if (allJawabans.Count < 3) { return; }

        allJawabans.Add(endColor);

        if (allJawabans.Count < dataSoal[level].colors.Length)
            return;

        jawaban = allJawabans.ToArray();

        if (jawaban.SequenceEqual(dataSoal[level].colors))
        {
            poin = 3;
            isWin = true;

            ClearLines();
            StartCoroutine(ShowResult());

            return;
        }
        else
        {
            GameOver();
        }
    }

    IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.ShowWidget();
        yield return new WaitForSeconds(2f);
        GameManager.Instance.ChangeState(GameState.Result);
    }

    public void ResetSoal()
    {
        allJawabans.Clear();
    }

    public void ClearLines()
    {
        _line.ClearLines();
    }

    public int GetLevel() { return level; }
    public int GetScore() { return poin; }

    public void GameOver()
    {
        poin = 0;
        isWin = false;

        ClearLines();
        StartCoroutine(ShowResult());
    }
}





[System.Serializable]
public class DataSoalLevel1
{
    public string word;
    public LetterColor[] colors;
    public string[] letters;

    public string[] opsiJawaban;
    public LetterColor[] opsiColor;
}