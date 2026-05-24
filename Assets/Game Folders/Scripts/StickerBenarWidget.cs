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
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}