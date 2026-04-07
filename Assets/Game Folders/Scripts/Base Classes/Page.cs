using GaweDeweStudio;
using UnityEngine;
using UnityEngine.UI;

public class Page : MonoBehaviour
{
    public PageName pageName;

    protected Button[] allButttons;

    protected virtual void Start()
    {
        allButttons = GetComponentsInChildren<Button>(true);
        foreach (var item in allButttons)
        {
            item.onClick.AddListener(() => GameManager.Instance.GetSound().PlaySound("button"));
        }
    }

    protected virtual void OnDestroy()
    {
        foreach (var item in allButttons)
        {
            item.onClick.RemoveAllListeners();
        }
    }

    public void DisablePage()
    {
        gameObject.SetActive(false);
    }
}
