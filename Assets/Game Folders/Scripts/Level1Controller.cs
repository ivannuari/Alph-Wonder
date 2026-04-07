using GaweDeweStudio;
using System;
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

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    internal void AddConnection(LetterColor a, LetterColor b)
    {
        playerConnections.Add((a, b));

        CheckAnswer();
    }

    private void CheckAnswer()
    {
        var correct = dataSoal[level].colors;

        if (playerConnections.Count < correct.Length - 1)
            return;

        bool isCorrect = true;

        for (int i = 0; i < correct.Length - 1; i++)
        {
            var expected = (correct[i], correct[i + 1]);
            var player = playerConnections[i];

            if (player != expected)
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            Debug.Log("✅ BENAR!");
        }
        else
        {
            Debug.Log("❌ SALAH!");
        }
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

    public void SetLetterColor(LetterColor startColor,LetterColor endColor)
    {
        allJawabans.Add(startColor);

        if (allJawabans.Count < 3) { return; }

        allJawabans.Add(endColor);

        if (allJawabans.Count < dataSoal[level].colors.Length)
            return;

        jawaban = allJawabans.ToArray();

        if (jawaban.SequenceEqual(dataSoal[level].colors))
        {
            isWin = true;
        }
        else
        {
            isWin = false;
        }

        GameManager.Instance.ChangeState(GameState.Result);
        ClearLines();
    }

    public void ClearLines()
    {
        GetComponent<LineDrawer>().ClearLines();
    }

    public int GetLevel() { return level; }
    public int GetScore() { return poin; }
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