using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 5f; // Tốc độ bay lên
    public float destroyTime = 0.5f; // Thời gian tồn tại
    [SerializeField] private TextMeshProUGUI textMesh; // Text hiển thị
    private Color textColor;
    private float currentTime;

    void Start()
    {
        if (textMesh == null)
        {
            textMesh = GetComponent<TextMeshProUGUI>();
        }
        textColor = textMesh.color;
        
    }
    public void Initialize(int damage, FloatingType type)
    {
        switch (type)
        {
            case FloatingType.ExceptBlood:
                textMesh.text = "-" + damage.ToString();
                textColor = Color.red;
                textMesh.color = textColor;
                break;
            case FloatingType.AddBlood:
                textMesh.text = "+" + damage.ToString();
                textColor = Color.green;
                textMesh.color = textColor;
                break;
            case FloatingType.AddExp:
                textMesh.text = "+" + damage.ToString();
                textColor = Color.yellow;
                textMesh.color = textColor;
                break;
            default:
                textMesh.text = damage.ToString();
                textColor = Color.white; // Mặc định là trắng nếu không xác định
                textMesh.color = textColor;
                break;
        }
        gameObject.SetActive(true);
        currentTime = 0f;
    }

    void Update()
    {
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0); // Bay lên
        textColor.a -= Time.deltaTime / destroyTime; // Mờ dần
        textMesh.color = textColor;
        currentTime += Time.deltaTime;
        if (currentTime >= destroyTime)
        {
            FloatingTextPoolManager.Instance.ReturnToPool(gameObject);
        }
    }
}