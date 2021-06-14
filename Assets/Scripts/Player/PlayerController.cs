using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public delegate void Player();
    public static event Player onAttack, onBlown, onDie;

    public Animator animator;
    public Rigidbody2D rigidbody;
    public BoxCollider2D boxCollider2D;
    Joystick joystick;

    [SerializeField] ParticleSystem hammerVFX;

    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip footstepSound;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip burnSound;
    [SerializeField] AudioClip collectSound;
    AudioSource audioSource;


    Vector2 runVelocity;

    float runSpeed = 10f;
    float jumpSpeed = 17f;

    public bool isBusy = false;
    public bool freeFall = false;
    public bool isGrounded = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        joystick = FindObjectOfType<FloatingJoystick>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        ExitLevel.onPassThroughTheDoor += SetInactive;
    }

    private void OnDisable()
    {
        ExitLevel.onPassThroughTheDoor -= SetInactive;
    }

    private void Update()
    {
        if (isGrounded && !isBusy)
        {
            TriggerJump();
            Attack();
        }
        else
        {
            CheckYPositionOnAir();
        }
    }

    private void FixedUpdate()
    {
        if (!isBusy)
        {
            Run();      
        }
    }

    public void TriggerJump()
    {
        //bool jump = Input.GetKeyDown(KeyCode.Space);
        bool jump = CustomTouchInput.GetJumpButtonKey();


        if (jump)
        {
            Jump();
            animator.SetTrigger("JumpTrigger");
        }

    }

    private void Run()
    {
        // GET INPUT VALUES

        //float horizontalInput = Input.GetAxis("Horizontal");
        float horizontalInput = joystick.Horizontal;

        float runStep = horizontalInput * runSpeed;

        runVelocity = new Vector2(runStep, rigidbody.velocity.y);

        // SET VELOCITY OF CHARACTER

        if (!freeFall)
        {
            rigidbody.velocity = runVelocity;
        }

        // FLIP CHARACTER

        if (horizontalInput > 0)
        {
            transform.localScale = new Vector2(0.8f, 0.8f);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector2(-0.8f, 0.8f);
        }

        // SET FLOAT PARAMETER OF RUN ANIMATION

        float absInputValue = Mathf.Abs(horizontalInput);

        animator.SetFloat("HorizontalInput", absInputValue);        
    }       

    private void CheckYPositionOnAir()
    {
        // CHECK CHARACTER IF IT'S GOING DOWN

        animator.SetFloat("YVelocity", rigidbody.velocity.y);
        

    }

    private void Jump()
    {
        // APPLY JUMP VELOCITY TO Y AXIS

        if (!isBusy)
        {
            audioSource.PlayOneShot(jumpSound);
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
        }
    }

    private void Attack()
    {
        //bool jumpButton = Input.GetKeyDown(KeyCode.F);
        bool jumpButton = CustomTouchInput.GetAttackButtonKey();

        if (jumpButton)
        {
            SetBusy();
            rigidbody.velocity = Vector2.zero;
            animator.SetTrigger("AttackTrigger");
        }
    }

    public void PlayAttackSound()
    {
        audioSource.PlayOneShot(attackSound);
    }

    public void PlayExplosionSound()
    {
        audioSource.PlayOneShot(explosionSound, 0.6f);
    }

    public void PlayBurnSound()
    {
        audioSource.PlayOneShot(burnSound);
    }

    public void PlayCollectSound()
    {
        audioSource.PlayOneShot(collectSound);
    }

    public void PlayFootstepSound()
    {
        audioSource.PlayOneShot(footstepSound);        
    }

    public IEnumerator Die(DamageType deathAnim)
    {
        yield return new WaitForEndOfFrame();
        rigidbody.velocity = Vector2.zero;
        SetBusy();
        boxCollider2D.enabled = false;
        rigidbody.isKinematic = true;
        string parameterName = animator.GetParameter((int)deathAnim).name;
        animator.SetTrigger(parameterName);
    }

    public void Explode()
    {
        onBlown?.Invoke();
    }

    public void Death()
    {
        onDie?.Invoke();
    }

    private void SetBusy()
    {
        isBusy = true;
    }

    private void SetFree()
    {
        isBusy = false;
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }

    public void DealDamage()
    {
        onAttack?.Invoke();
    }

    public void PlayVFX()
    {
        Vector3 vfxPosition = new Vector3(1.5f * transform.localScale.x, -2f, 0f);
        hammerVFX.transform.position = transform.position + vfxPosition;
        hammerVFX.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            DamageType type = collision.collider.GetComponent<DealDamage>().type;

            StartCoroutine(Die(type));
        }
    }
}
