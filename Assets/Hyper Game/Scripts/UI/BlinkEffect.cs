using System.Collections;
using UnityEngine;
using TMPro;

public class BlinkEffect : MonoBehaviour
{
    [SerializeField] private float scaleSpeed = 2f; 
    [SerializeField] private float minScale = 0.9f; 
    [SerializeField] private float maxScale = 2f;
    [SerializeField] private TextMeshProUGUI timeText;


    private bool isBlinking = false;

    void Start()
    {
        StopBlinking();
    }

    public void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            timeText.color = ColorUtility.TryParseHtmlString("#B71D41", out Color newColor) ? newColor : Color.white;
            StartCoroutine(ScaleBlinkCoroutine());
        }
    }

    public void StopBlinking()
    {
        timeText.color = Color.white;
        isBlinking = false;
        StopCoroutine(ScaleBlinkCoroutine());
        transform.localScale = Vector3.one; // Đặt lại kích thước mặc định
    }

    private IEnumerator ScaleBlinkCoroutine()
    {
        while (isBlinking)
        {
            // Phóng to
            for (float t = 0; t <= 1; t += Time.deltaTime * scaleSpeed)
            {
                transform.localScale = Vector3.Lerp(Vector3.one * minScale, Vector3.one * maxScale, t);
                yield return null;
            }
            // Thu nhỏ
            for (float t = 0; t <= 1; t += Time.deltaTime * scaleSpeed)
            {
                transform.localScale = Vector3.Lerp(Vector3.one * maxScale, Vector3.one * minScale, t);
                yield return null;
            }
        }
    }
}