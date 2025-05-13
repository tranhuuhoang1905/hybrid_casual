using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private float speed;
    private Vector2 direction;
    void Awake()
    {
    }
    public void Initialize(float bulletSpeed)
    {
        speed = bulletSpeed;

        // Lấy hướng bay từ góc quay hiện tại của viên đạn
        float angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad; // Chuyển đổi góc từ độ sang radian
        direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized; // Tạo vector hướng bay
    }

    void Update()
    {
        // Di chuyển viên đạn theo hướng của nó với vận tốc speed
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}