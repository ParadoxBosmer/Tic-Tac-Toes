using UnityEngine;
using UnityEngine.SceneManagement;


public class button_press : MonoBehaviour
{
    [Header("Press")]

    [SerializeField] private Sprite sprite;
    [SerializeField] private Sprite sprite2;

    [Header("Mouse Feedback")]

    private Vector3 defaultButtonScale;
    [SerializeField] private Vector3 buttonPressScale;
    private SpriteRenderer _spriteRenderer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        defaultButtonScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.sprite = sprite2;
        }
    }
    private void OnMouseExit()
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.sprite = sprite;
        }
    }

    private void OnMouseDown()
    {
        transform.localScale = buttonPressScale;
        SceneManager.LoadScene("SampleScene");
    }

    private void OnMouseUp()
    {
        transform.localScale = defaultButtonScale;
    }
}
