using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour {

    public bool isFacingRight;
    Rigidbody2D rb;
    public Rigidbody2D rb2;
    public float speed;
    public float jumpForce;
    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;
    public Transform projectileSpawnPoint;
    public Projectile projectilePrefab;
    public float projectileForce;
    public float jumpBoost = 5.0f;
    public float jumpBoostTime = 4f;
    public float speedBoost = 5.0f;
    public float speedBoostTime = 4f;

    Animator anim;

    public IEnumerator StopJumpForce()
    {
        yield return new WaitForSeconds(jumpBoostTime);
        jumpForce -= jumpBoost;
    }
    public IEnumerator StopSpeed()
    {
        yield return new WaitForSeconds(speedBoostTime);
        speed -= speedBoost;
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaleFactor = transform.localScale;
        scaleFactor.x *= -1;
        transform.localScale = scaleFactor;
    }


    void Start() {


        rb = GetComponent<Rigidbody2D>();

        if (!rb) 
        {
            Debug.LogError("Rigidbody2D not found on " + name);
        }

        if (!rb2)
        {
            Debug.LogError("Rigidbody2D not found on " + name);
        }


        if (speed <= 0)
        {

            speed = 5.0f;


            Debug.LogWarning("Speed not set on " + name + ". Defaulting to " + speed);
        }

        if (jumpForce <= 0)
        {
 
            jumpForce = 5.0f;

            Debug.LogWarning("JumpForce not set on " + name + ". Defaulting to " + jumpForce);
        }


        if (!groundCheck)
        {
            Debug.LogError("GroundCheck not found on " + name);
        }


        if (groundCheckRadius <= 0)
        {
 
            groundCheckRadius = 0.2f;

        
            Debug.LogWarning("GroundCheckRadius not set on " + name + ". Defaulting to " + groundCheckRadius);
        }

        anim = GetComponent<Animator>();

       
        if (!anim)
        {
            Debug.LogError("Animator not found on " + name);
        }

        if (!projectileSpawnPoint)
            Debug.LogError("Missing projectileSpawn");
        if (!projectilePrefab)
            Debug.LogError("Missing projectilePrefab");
        if (projectileForce == 0)
        {
            projectileForce = 7.0f;
            Debug.Log("projectileForce was not set. Defaulting to" + projectileForce);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Mushy")
        {
            jumpForce += jumpBoost;
            StartCoroutine(StopJumpForce());

            Debug.Log("High");

            Destroy(other.gameObject);
        }
        else if (other.tag == "Bally")
        {
            speed += speedBoost;
            StartCoroutine(StopSpeed());

            Debug.Log("Fast");

            Destroy(other.gameObject);
        }
    }


    // Update is called once per frame


    void Fire()
    {
        Projectile temp =
            Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        temp.speed = projectileForce;

        if (isFacingRight)
        {
            temp.speed = -projectileForce;
        }
        else
        {
            temp.speed = projectileForce;
        }
    }


    void Update()
    {


        float moveValue = Input.GetAxisRaw("Horizontal");

        if ((isFacingRight && moveValue > 0) || (!isFacingRight && moveValue < 0))
        {
            Flip();
        }


        isGrounded = Physics2D.OverlapCircle(groundCheck.position,
            groundCheckRadius, isGroundLayer);

        if (transform.localRotation.z != 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (isGrounded)
        {
            anim.SetBool("Jump", false);
            anim.SetBool("SpinJump", false);
        
        }


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
     
            Debug.Log("Jump");

            anim.SetBool("Jump", true);

            rb.velocity = Vector2.zero;


            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        bool spin = Input.GetButtonDown("Spin");
        bool jump = Input.GetButtonDown("Jump");

        if (spin && jump)
        {
            anim.SetBool("Jump", false);
            anim.SetBool("SpinJump", true);
            Debug.Log("Spin");
        }

 
        if (Input.GetButtonDown("Fire1"))

        {
            anim.SetBool("Fire", true);
       
            Debug.Log("Pew pew");
            Fire();
        }
        else
        {
            anim.SetBool("Fire", false);
        }

        rb.velocity = new Vector2(moveValue * speed, rb.velocity.y);

 
        anim.SetFloat("moveValue", Mathf.Abs(moveValue));
    }
  


}