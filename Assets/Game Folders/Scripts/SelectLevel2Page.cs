using GaweDeweStudio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel2Page : Page
{
    [SerializeField] private Button backButton;

    [SerializeField] private Sprite[] allImages;

    [SerializeField] Level2Card cardPrefabs;
    [SerializeField] private Transform content;

    protected override void Start()
    {
        base.Start();
        backButton.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync("Main Menu");
        });

        GameManager.Instance.ChangeState(GameState.Level2);
    }

    private void OnEnable()
    {
        foreach (Transform item in content) 
        {
            Destroy(item.gameObject);
        }

        SpawnCards();
    }

    private void SpawnCards()
    {
        LevelData2 dataLevel = GameManager.Instance.levelData2;

        for (int i = 0; i < allImages.Length; i++)
        {
            var _clone = Instantiate(cardPrefabs, content);
            _clone.Setup(i, allImages[i], dataLevel.saveData[i].star);
        }
    }
}
