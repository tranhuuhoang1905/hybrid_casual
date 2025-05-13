using UnityEngine;

public class AutoDestroyEffect : MonoBehaviour
{
    private ParticleSystem ps;
    [SerializeField] private float countdown = 1f;

    void Start()
    {
        ps = GetComponent<ParticleSystem>(); // Lấy Particle System trên object
        Destroy(gameObject, countdown);
    }

    void Update()
    {
        if (ps != null && !ps.IsAlive()) // Kiểm tra nếu hiệu ứng đã kết thúc
        {
            Destroy(gameObject); // Hủy object khi hiệu ứng kết thúc
        }
    }
}