using GaweDeweStudio;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game3Page : Page
{
    [SerializeField] private Button backButton;
    [SerializeField] private TMP_Text titleText;

    [SerializeField] private TopLetter[] allTopLetter;
    [SerializeField] private DropSlot[] allDropSlot;

    [SerializeField] private DraggableLetter[] allDraggableLetter;

    [SerializeField] private string soundId;
    [SerializeField] private float soundDelay;

    protected override void Start()
    {
        base.Start();
        backButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ChangeState(GameState.Level3);
        });
    }

    private void OnEnable()
    {
        Level3Controller.Instance.OnLevelChanged += Instance_OnLevelChanged;
        Instance_OnLevelChanged(Level3Controller.Instance.GetLevelData());
    }

    private void OnDisable()
    {
        Level3Controller.Instance.OnLevelChanged -= Instance_OnLevelChanged;
    }

    private void Instance_OnLevelChanged(DataSoalLevel3 data)
    {
        if(data.colors.Length > 4)
        {
            allDropSlot[4].gameObject.SetActive(true);
        }
        else
        {
            allDropSlot[4].gameObject.SetActive(false);
        }

        for (int i = 0; i < allTopLetter.Length; i++)
        {
            allTopLetter[i].Setup(data.opsiJawaban[i], data.opsiColor[i]);
        }

        for (int i = 0; i < data.colors.Length; i++)
        {
            allDropSlot[i].Setup(data.colors[i], data.word.ToCharArray()[i].ToString());
        }

        for (int i = 0; i < allDraggableLetter.Length; i++)
        {
            allDraggableLetter[i].Setup(data.opsiJawaban[i], data.opsiColor[i]);
        }

        StartCoroutine(StartAnimations());
    }

    IEnumerator StartAnimations()
    {
        GameManager.Instance.GetSound().PlaySound(soundId);
        yield return new WaitForSeconds(soundDelay);

        for (int i = 0; i < allTopLetter.Length; i++)
        {
            allTopLetter[i].StartAnimation();
            DropSlot dropPrefab = Array.Find(allDropSlot, x => x.key == allTopLetter[i].text.text);
            if(dropPrefab != null) { dropPrefab.StartAnimation(); }

            yield return new WaitForSeconds(1.5f);
        }
    }
}
