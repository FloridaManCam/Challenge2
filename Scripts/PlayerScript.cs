using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text scoreText;
    public Text liveText;
    private int livesValue = 3;
    private int scoreValue = 0;
    public Text winText;
    public GameObject player;
    private bool facingRight = true;
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        SetScoreText();
        SetLivesText();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
        Flip();
        }
    }

    void OnTriggerEnter2D(Collider2D other) //Checking tags on impact with prefabs, then setting count accordingly
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            scoreValue += 1;
            SetScoreText();
            
            if (scoreValue % 4 == 0)
            {
                transform.position = new Vector2(50f, 0f);
                livesValue = 3;
                liveText.text = "Lives: " + livesValue.ToString();
            }
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            livesValue -= 1;
            SetLivesText();
        }
    }
    

    void SetScoreText() //Creating the win text by checking how many boxes have been picked up
    {
        scoreText.text = "Score: " + scoreValue.ToString();
        if (scoreValue >= 8)
        {
            winText.text = "You Win! Made By: Cameron Cavossa";
            Destroy(player);
        }
    }

    void SetLivesText()
    {
        liveText.text = "Lives: " + livesValue;
        if (livesValue <= 0)
        {
            winText.text = "YOU LOSE! MAde By Cam Cavossa";
            Destroy(player);
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground" && isOnGround)
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

    void Update()
    {
        if (isOnGround == true)
        {
            {
            if (Input.GetKeyDown(KeyCode.D))
                anim.SetInteger("State", 1);
            }
            
            if (Input.GetKeyUp(KeyCode.D))
            {
                anim.SetInteger("State", 0);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                anim.SetInteger("State", 1);
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetInteger("State", 0);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                anim.SetInteger("State", 2);
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetInteger("State", 3);
            }
        }
        if (isOnGround == false)
            {
                anim.SetInteger("State", 2);
            }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        
    }
}