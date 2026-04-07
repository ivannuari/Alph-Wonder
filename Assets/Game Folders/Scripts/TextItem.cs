using GaweDeweStudio;
using UnityEngine;

public class TextItem : MonoBehaviour
{
    public bool isBomb;
    public bool isWrong;

    private bool isCollected = false;

    public float fallSpeed;
    private SpriteRenderer _rend;

    public void Setup(Sprite gambar, bool bomb, bool wrong)
    {
        isBomb = bomb;
        isWrong = wrong;

        _rend = GetComponent<SpriteRenderer>();
        _rend.sprite = gambar;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCollected) return;
        

        if (collision.CompareTag("keranjang"))
        {
            isCollected = true;
            if (isBomb) 
            {
                Level2Controller.Instance.TakeDamage();
                GameManager.Instance.GetSound().PlaySound("Zonk");
            }

            if(isWrong)
            {
                GameManager.Instance.GetSound().PlaySound("Miss");
            }

            if (!isWrong && !isBomb)
            {
                Level2Controller.Instance.AddScore();
                GameManager.Instance.GetSound().PlaySound("Correct");
            }
            if(collision.TryGetComponent(out KeranjangController keranjang))
            {
                keranjang.Pop();
            }

            Destroy(gameObject);
        }

        if (collision.CompareTag("dead zone"))
        {
            isCollected = true;
            if (!isWrong && !isBomb) 
            {
                Level2Controller.Instance.TakeDamage();
                GameManager.Instance.GetSound().PlaySound("Zonk");
            }

            Destroy(gameObject);
        }
    }
}