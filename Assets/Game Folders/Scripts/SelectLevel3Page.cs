using GaweDeweStudio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel3Page : Page
{
    [SerializeField] private Button backButton;

    [SerializeField] private Level3Card[] allLevelCard;

    private void OnEnable()
    {
        var data = GameManager.Instance.levelData3;

        for (int i = 0; i < allLevelCard.Length; i++)
        {
            allLevelCard[i].Setup(i, null, data.saveData[i].star);
        }
    }

    protected override void Start()
    {
        base.Start();
        backButton.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync("Main Menu");
        });
    }
} 
