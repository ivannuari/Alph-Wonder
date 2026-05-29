using GaweDeweStudio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Controller : MonoBehaviour
{
    public static Level3Controller Instance;

    public string jawaban;
    [SerializeField] private List<DropSlot> allSlots;

    public DataSoalLevel3[] dataSoal;

    [SerializeField] private int level;
    [SerializeField] private int poin;

    public event Action<DataSoalLevel3> OnLevelChanged;
    public event Action OnGameReseted;

    private void Awake()
    {
        if(Instance == null) { Instance = this; }
    }

    private void GenerateSoal()
    {
        OnLevelChanged?.Invoke(dataSoal[level]);
    }

    public void AddAnswer(string n)
    {
        jawaban += n;
    }

    public void SetLevel(int level)
    {
        this.level = level;
        GenerateSoal();
    }

    public void CheckAllFilled()
    {
        bool allFilled = true;

        foreach (var slot in allSlots)
        {
            if (slot.transform.childCount == 0)
            {
                allFilled = false;
                break;
            }
        }

        if (allFilled)
        {
            CheckAnswer();
        }
    }

    private void CheckAnswer()
    {
        string result = "";

        foreach (var slot in allSlots)
        {
            if (slot.transform.childCount > 1)
            {
                var letter = slot.transform.GetChild(1).GetComponent<DraggableLetter>();
                result += letter.text.text;
            }
        }

        string correctAnswer = dataSoal[level].word;

        if (result.Equals(correctAnswer))
        {
            //Debug.Log("✅ Jawaban Benar!");
            poin = 3;
            StartCoroutine(ShowResult());
        }
        else
        {
            GameOver();
            
            //Debug.Log("❌ Jawaban Salah!");
        }
    }

    IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.ShowWidget();
        yield return new WaitForSeconds(2f);
        GameManager.Instance.ChangeState(GameState.Result);
    }

    public DataSoalLevel3 GetLevelData()
    {
        return dataSoal[level];
    }

    public int GetScore()
    {
        return poin;
    }

    public int GetLevel()
    {
        return level;
    }

    public void ResetSoal()
    {
        poin = 0;
        foreach (var item in allSlots)
        {
            item.ResetSoal();
        }

        OnGameReseted?.Invoke();
    }

    public void GameOver()
    {
        poin = 0;
        StartCoroutine (ShowResult());
    }
}


[System.Serializable]
public class DataSoalLevel3
{
    public string word;
    public LetterColor[] colors;

    public string[] opsiJawaban;
    public LetterColor[] opsiColor;
}
