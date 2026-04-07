using GaweDeweStudio;
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
        for (int i = 0; i < allTopLetter.Length; i++)
        {
            allTopLetter[i].Setup(data.opsiJawaban[i], data.opsiColor[i]);
        }

        for (int i = 0; i < allDropSlot.Length; i++)
        {
            allDropSlot[i].Setup(data.colors[i]);
        }

        for (int i = 0; i < allDraggableLetter.Length; i++)
        {
            allDraggableLetter[i].Setup(data.opsiJawaban[i], data.opsiColor[i]);
        }
    }
}
