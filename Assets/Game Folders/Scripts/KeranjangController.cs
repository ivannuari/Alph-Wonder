using UnityEngine;

public class KeranjangController : MonoBehaviour
{
    [SerializeField] private float edge = 4.35f;

    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var mousePos = Input.mousePosition;
            var _worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            float xPos = Mathf.Clamp(_worldPos.x, -edge, edge);
            transform.position = new Vector2(xPos, transform.position.y);
        }
    }

    public void Pop()
    {
        _anim.PlayInFixedTime("keranjang pop");
    }
}
