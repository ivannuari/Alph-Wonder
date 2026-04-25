using GaweDeweStudio;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game1Page : Page
{
    [SerializeField] private Button backButton;
    [SerializeField] private TopLetter[] allTopLetters;
    [SerializeField] private TMP_Text[] allSoals;

    [SerializeField] private GameObject[] allGameContents;

    [SerializeField] private RectTransform contentArea;
    [SerializeField] private ColorDot colorDotPrefab;

    [SerializeField] private float minDistance = 80f; // jarak minimal antar dot
    [SerializeField] private int maxTries = 50; // biar gak infinite loop

    private void OnEnable()
    {
        SetLevel(Level1Controller.Instance.dataSoal[Level1Controller.Instance.GetLevel()]);
    }

    protected override void Start()
    {
        base.Start();
        backButton.onClick.AddListener(() =>
        {
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
        for (int i = 0; i < allTopLetters.Length; i++)
        {
            allTopLetters[i].Setup(data.opsiJawaban[i], data.opsiColor[i]);
        }

        for (int i = 0;i < data.letters.Length; i++)
        {
            allSoals[i].text = data.letters[i];
        }

        SetGameContentLevel(data);
        //SpawnColorDots(data.colors);
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
            allGameContents[level].transform.GetChild(i).GetComponent<ColorDot>().SetColor(data.colors[i]); 
        }
    }

    private void SpawnColorDots(LetterColor[] colors)
    {
        foreach (Transform child in contentArea)
        {
            Destroy(child.gameObject);
        }

        List<Vector2> usedPositions = new List<Vector2>();

        float width = contentArea.rect.width;
        float height = contentArea.rect.height;

        float padding = 40f;

        for (int i = 0; i < colors.Length; i++)
        {
            Vector2 spawnPos;
            bool validPosition = false;
            int attempts = 0;

            while (!validPosition && attempts < maxTries)
            {
                float randomX = UnityEngine.Random.Range(-width / 2f + padding, width / 2f - padding);
                float randomY = UnityEngine.Random.Range(-height / 2f + padding, height / 2f - padding);

                spawnPos = new Vector2(randomX, randomY);

                validPosition = true;

                // cek jarak ke semua dot sebelumnya
                foreach (var pos in usedPositions)
                {
                    if (Vector2.Distance(pos, spawnPos) < minDistance)
                    {
                        validPosition = false;
                        break;
                    }
                }

                attempts++;

                if (validPosition)
                {
                    usedPositions.Add(spawnPos);

                    ColorDot dot = Instantiate(colorDotPrefab, contentArea);
                    dot.SetColor(colors[i]);

                    RectTransform rect = dot.GetComponent<RectTransform>();
                    rect.anchoredPosition = spawnPos;
                }
            }

            // fallback kalau gagal cari posisi
            if (!validPosition)
            {
                Debug.LogWarning("Gagal menemukan posisi tanpa overlap, coba kecilkan minDistance atau perbesar area.");
            }
        }
    }
}
