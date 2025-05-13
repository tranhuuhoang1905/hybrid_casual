using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    
    [SerializeField] private Slider slider;
    [SerializeField] private Image fillImage;
    
    public Color highColor = Color.green;  // Màu trên 70%
    public Color mediumColor = Color.yellow; // Màu từ 30% - 70%
    public Color lowColor = Color.red;    // Màu dưới 30%
    public void UpdateSliderBar(float currentValue, float maxValue)
    {
        
        float percentage = currentValue / maxValue;
        slider.value = percentage;
        // Kiểm tra phần trăm và thay đổi màu
        if (percentage >= 0.7f)
        {
            fillImage.color = highColor; // Xanh lá cây
        }
        else if (percentage >= 0.3f)
        {
            fillImage.color = mediumColor; // Vàng
        }
        else
        {
            fillImage.color = lowColor; // Đỏ
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
