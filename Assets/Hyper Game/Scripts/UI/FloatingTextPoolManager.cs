using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class FloatingTextPoolManager : MonoBehaviour
{
    public static FloatingTextPoolManager Instance { get; private set; }

    [SerializeField] private GameObject FloatingTextPrefab;
    [SerializeField] private Transform canvasTransform; // UI Canvas
    private Queue<GameObject> damageTextPool = new Queue<GameObject>(); // Hàng đợi để quản lý Damage Text

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        GameEvents.OnShowFloatingText += ShowDamageText;
    }

    private void OnDisable()
    {
        GameEvents.OnShowFloatingText -= ShowDamageText;
    }

    private void ShowDamageText(Vector3 worldPosition, int damage,FloatingType type)
    {
        GameObject damageTextObj = GetDamageTextFromPool();
        damageTextObj.transform.position = worldPosition;
        damageTextObj.GetComponent<FloatingText>().Initialize(damage,type);
    }

    private GameObject GetDamageTextFromPool()
    {
        if (damageTextPool.Count > 0)
        {
            GameObject obj = damageTextPool.Dequeue(); // Lấy một Damage Text từ hàng đợi
            obj.SetActive(true);
            return obj;
        }
        return Instantiate(FloatingTextPrefab, canvasTransform); // Nếu hết, tạo mới
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        damageTextPool.Enqueue(obj); // Đưa lại vào hàng đợi để tái sử dụng
    }
}
