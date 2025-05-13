using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    public Transform parent; // Tâm quỹ đạo (Parent)
    public float radius = 2f; // Bán kính quay
    public float speed = 2f; // Tốc độ quay (radians/s)
    private float angle; // Góc quay hiện tại
    public void Initialize(float initSpeed)
    {
        updateSpeed(initSpeed);
    }

    void Start()
    {
        parent = transform.parent;
        if (parent == null) return; // Kiểm tra nếu Parent không tồn tại

        // Tính toán góc ban đầu dựa trên vị trí hiện tại so với Parent
        Vector3 offset = transform.position - parent.position; // Khoảng cách hiện tại so với Parent
        radius = offset.magnitude; // Cập nhật bán kính dựa trên khoảng cách hiện tại
        angle = Mathf.Atan2(offset.y, offset.x); // Tính góc hiện tại theo Atan2
    }

    void Update()
    {
        if (parent == null) return; // Đảm bảo có Parent

        // Cập nhật góc quay theo thời gian
        angle -= speed * Time.deltaTime; 

        // Tính toán vị trí mới quanh `Parent`
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        // Đặt vị trí mới dựa trên vị trí của `Parent`
        transform.position = parent.position + new Vector3(x, y, 0);

        float angle2 = Mathf.Atan2(y,x)* Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle2);
        transform.rotation = rotation;
    }
    public void updateSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
