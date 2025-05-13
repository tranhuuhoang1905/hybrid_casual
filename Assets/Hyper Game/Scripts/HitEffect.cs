using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour,IHitEffect
{
    [SerializeField] private Material hitMaterial; // Material khi trúng đòn
    private Material originalMaterial; // Material gốc
    private Renderer parentRenderer;
    [SerializeField] private float existenceTime = 0.2f;
    private Coroutine effectCoroutine; // Biến lưu Coroutine hiện tại
    Transform bodyTransform;
    [SerializeField] private GameObject hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        bodyTransform = transform.Find("Body");
        if (bodyTransform!= null)
        {
            parentRenderer = bodyTransform.GetComponent<Renderer>(); 
        }
        else
        {
            parentRenderer = GetComponent<Renderer>(); 
        }
        if (parentRenderer != null)
        {
            originalMaterial = parentRenderer.material; // Lưu lại material ban đầu
        }
    }

    // Hàm gọi khi có event HitEffectEvent
    public void ApplyHitEffect()
    {
        if (effectCoroutine != null)
        {
            StopCoroutine(effectCoroutine);
            parentRenderer.material = originalMaterial; // Đảm bảo trả lại material ban đầu
        }

        if (parentRenderer != null && hitMaterial != null)
        {
            
            effectCoroutine = StartCoroutine(ChangeMaterialTemporarily());
        }
    }

    private IEnumerator ChangeMaterialTemporarily()
    {
        parentRenderer.material = hitMaterial; // Đổi sang material hit

        yield return new WaitForSeconds(existenceTime); 
        parentRenderer.material = originalMaterial; // Đổi lại material ban đầu
    }
}
