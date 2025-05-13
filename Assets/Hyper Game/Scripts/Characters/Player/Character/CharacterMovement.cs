using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    private SpriteRenderer spriteRenderer;
    private Character playerCharacter;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;
    bool isAlive = true;
    public bool IsAlive => isAlive; // Trả về trạng thái sống
    void Awake()
    {
        StatsRefresh.OnRefresh += RunSpeedRefresh; // Đăng ký sự kiện
    }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gravityScaleAtStart = myRigidbody.gravityScale;
        playerCharacter = GetComponent<Character>();
    }
    void OnDestroy()
    {
        StatsRefresh.OnRefresh -= RunSpeedRefresh; // Đăng ký sự kiện
        
    }
    
    void Update()
    {
        if (!isAlive) return;
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) return;
        moveInput = value.Get<Vector2>();
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) return;
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

        if (value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        // Lấy input di chuyển
        Vector3 moveDirection = new Vector3(moveInput.x, moveInput.y, 0);

        // Chuẩn hóa hướng di chuyển để tránh tốc độ lớn hơn 5px/s khi di chuyển chéo
        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }
        // Di chuyển với tốc độ 5px/s
        transform.Translate(moveDirection * runSpeed * Time.deltaTime);

        bool playerHasHorizontalSpeed = moveInput.x !=0 || moveInput.y != 0;
        myAnimator.SetBool("IsRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(moveInput.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            spriteRenderer.flipX = moveInput.x < 0; // Flip chỉ sprite
        }
    }

    void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }


    public void Die()
    {
        if (!isAlive) return;
        isAlive = false;
        myAnimator.SetTrigger("Dying");
        // if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        // {
        //     isAlive = false;
        //     myRigidbody.velocity = deathKick;
        //     // FindObjectOfType<GameSession>().ProcessPlayerDeath();
        // }
    }
    void RunSpeedRefresh( Attr totalStats)
    {
        runSpeed = totalStats.moveSpeed;
    }
}