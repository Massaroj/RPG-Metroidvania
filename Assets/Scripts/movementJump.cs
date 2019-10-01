using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementJump : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private Rigidbody2D rb;
    private float moveInput;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    public BulletType bulletType;
    public GameObject fireBullet;
    public GameObject iceBullet;
    public bool facingRight;
    Vector2 bulletPos;
    public float fireRate = .5f;
    float nextFire = 0;

    public int health = 3;
    private bool invincible = false;
    public float invincibilityTime = 2f;
    public LayerMask whatIsEnemy;
    private BoxCollider2D playerDamaged;

    public int mana = 5;
    public float manaChargeTime = 3000f;
    public bool fireEnabled;
    public bool iceEnabled;

    public Canvas UI;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        facingRight = true;
        playerDamaged = GetComponent<BoxCollider2D>();
        fireEnabled = false;
        iceEnabled = false;
    }

    //Will need to be more fine tuned
    /*
     *Player Movement 
    */
    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (rb.velocity.x < 0)
            facingRight = false;

        else if(rb.velocity.x > 0)
            facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }

        //Flip player based on direction
        if(facingRight)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        else
            transform.localRotation = Quaternion.Euler(0, 180, 0);

        //Makes a check to see if the player is grounded or not for jumping
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        //Initial jump
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        //Extend jump if the button is held down longer
        if(Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
                isJumping = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
            isJumping = false;

        //Shoot projectile **(Add a boolean for when shooting is enabled)**
        if(Input.GetKey(KeyCode.X) && Time.time > nextFire && mana > 0)
        {
            nextFire = Time.time + fireRate;

            if (fireEnabled)
                bulletType = BulletType.fireBullet;
            else if (iceEnabled)
                bulletType = BulletType.iceBullet;
            else
                bulletType = BulletType.none;

            Fire();
        }
    }

    //Instatiates and fires projectile
   void Fire()
    {
        bulletPos = transform.position;
        if(facingRight)
            bulletPos += new Vector2(+2.5f, .4f);
        else
            bulletPos += new Vector2(-2.5f, .4f);

        switch(bulletType)
        {
            case BulletType.fireBullet:
                Instantiate(fireBullet, bulletPos, Quaternion.identity);
                break;

            case BulletType.iceBullet:
                Instantiate(iceBullet, bulletPos, Quaternion.identity);
                break;

            case BulletType.none:
                return;
        }

        mana -= 1;
        UI.GetComponent<UIManager>().updateMana(mana);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!invincible)
        {
            if (collision.gameObject.tag == "enemy")
            {
                health -= 1;
                UI.GetComponent<UIManager>().updateHealth(health);
                StartCoroutine(Invulnerability());
            }
        }
    }

    IEnumerator Invulnerability()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }
}

public enum BulletType
{
    fireBullet,
    iceBullet,
    none
}
