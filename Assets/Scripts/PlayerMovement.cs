using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float Movespeed = 5;
    [SerializeField] float JumpSpeed = 4;
    [SerializeField] float ClimbSpeed = 4;
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform BulletSpawnPoint;

    Vector2 MoveInput;
    Rigidbody2D PlayerRigidbody;
    Animator PlayerAnimator;
    CapsuleCollider2D PlayerBodyCollider;
    BoxCollider2D PlayerFeetCollider;
    PlayerInput ThePlayerInput;
    public MainUIController uiController;
    float DefaultGravity;
    float ImpulseAmount;
    int JumpCount = 2;
    bool HasClimb = false;
    public bool IsAlive = true;

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), false);
    }

    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        PlayerBodyCollider = GetComponent<CapsuleCollider2D>();
        PlayerFeetCollider = GetComponent<BoxCollider2D>();
        DefaultGravity = PlayerRigidbody.gravityScale;
        ThePlayerInput = GetComponent<PlayerInput>();
        uiController = FindObjectOfType<MainUIController>();
    }


    void Update()
    {
        if (uiController.pauseUI.isPaused) return;

        Run();
        FlipSprite();
        ClimbLadder();
        ResetJumpCount();
    }

    void OnMove(InputValue Value)
    {
        MoveInput = Value.Get<Vector2>();
    }

    void Run()
    {
        Vector2 PlayerVelocity = new Vector2(MoveInput.x * Movespeed, PlayerRigidbody.velocity.y);
        PlayerRigidbody.velocity = PlayerVelocity;
        bool PlayerHasHorizontalSpeed = Mathf.Abs(PlayerRigidbody.velocity.x) > Mathf.Epsilon;
        PlayerAnimator.SetBool("IsRunning", PlayerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool PlayerHasHorizontalSpeed = Mathf.Abs(PlayerRigidbody.velocity.x) > Mathf.Epsilon;
        if (PlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(MoveInput.x), 1f);
        }
    }

    void OnJump(InputValue Value)
    {

        if(Value.isPressed && JumpCount > 0)
        {
            PlayerRigidbody.velocity = new Vector2(PlayerRigidbody.velocity.x, 0f);
            PlayerRigidbody.velocity += new Vector2(0f, JumpSpeed);
            JumpCount--;
        }
    }

    void ResetJumpCount()
    {
        if (IsGrounded() || (HasClimb && CanClimb()))
        {
            JumpCount = 1;
        }
    }

    bool IsGrounded()
    {
        return PlayerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    bool CanClimb()
    {
        return PlayerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"));
    }

    void ClimbLadder()
    {
        if (!CanClimb())
        {
            HasClimb = false;
            PlayerRigidbody.gravityScale = DefaultGravity;
            PlayerAnimator.SetBool("IsClimbing", false);
            return;
        }
        
        if (Mathf.Abs(MoveInput.y) > Mathf.Epsilon || HasClimb)
        {
            HasClimb = true;
            PlayerAnimator.SetBool("IsClimbing", true);
            Climbing();
        }
    }

    void Climbing()
    {
        PlayerRigidbody.gravityScale = 0;
        Vector2 ClimbVelocity = new Vector2(PlayerRigidbody.velocity.x, MoveInput.y * ClimbSpeed);
        PlayerRigidbody.velocity = ClimbVelocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsOnHazard() && IsAlive)
        {
            Die();
        }
        else if(collision.gameObject.tag == "Enemy" && IsAlive)
        {
            Die();
        }

    }

    void OnFire(InputValue Value)
    {
        if (uiController.pauseUI.isPaused) return;

        Instantiate(Bullet, BulletSpawnPoint.position, transform.rotation);
    }

    private bool IsOnHazard()
    {
        return PlayerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")) || PlayerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazard"));
    }

    public void Die()
    {
        IsAlive = false;
        ThePlayerInput.enabled = false;
        PlayerAnimator.SetTrigger("Die");
        ImpulseAmount = Random.Range(4f, 10f);
        PlayerRigidbody.velocity = new Vector2(ImpulseAmount, ImpulseAmount);
        Invoke("StopSliding", 2f);
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"));
        FindObjectOfType<GameSession>().Invoke("ProcessPlayerDeath", 2);
    }

    void StopSliding()
    {
        PlayerRigidbody.velocity = new Vector2(0f, 0f);
    }
}
