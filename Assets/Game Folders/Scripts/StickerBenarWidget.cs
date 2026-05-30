using GaweDeweStudio;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StickerBenarWidget : MonoBehaviour
{
    [SerializeField] private Sprite[] allSprites;
    [SerializeField] private Image iconImage;

    [SerializeField] private int level;

    private bool isLose = false;

    private void OnEnable()
    {
        int score = 0;

        if (level == 1)
        {
            score = Level1Controller.Instance.GetScore();
        }

        if (level == 2)
        {
            score = Level2Controller.Instance.GetScore();
        }

        if (level == 3)
        {
            score = Level3Controller.Instance.GetScore();
        }

        isLose = score < 1;
        iconImage.sprite = allSprites[isLose ? 1 : 0];

        StartCoroutine(CloseWidget());
    }

    IEnumerator CloseWidget()
    {
        yield return new WaitForSeconds(0.5f);
        if (!isLose) { GameManager.Instance.GetSound().PlaySound("Pintar"); }
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}