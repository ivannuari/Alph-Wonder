using GaweDeweStudio;
using System.Collections;
using UnityEngine;

public class StickerBenarWidget : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(CloseWidget());
    }

    IEnumerator CloseWidget()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.GetSound().PlaySound("Pintar");
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}