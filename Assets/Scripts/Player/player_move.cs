using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_move : MonoBehaviour
{
    //movement
    public int playerSpeed = 10;
    private bool facingRight;
    public int PlayerJumpPower = 1250;
    private float moveX;

    //touch mechanic
    private Vector2 fp;
    private Vector2 lp;
    private float dragDistanceH;
    float timer = 30.0f;

    //tap mechanic
    bool tapping = false;
    float tapTimer = 0f;
    public float maxTaps = 4.0f;

    public bool damage = false;
    public bool coin = false;
    public bool bol = true;

    //objects
    private Rigidbody2D characterBody;
    public GameObject player;
    public Text debugText;

    Health hp;
    public Score point;

    public GameObject healthCollider;

    [SerializeField]
    public Animator anim;

    public float maxSpeed = 800;
    public float speed = 800;
    float intervals = 5;




    void Start()
    {
        characterBody = GetComponent<Rigidbody2D>();
        dragDistanceH = Screen.height * 15 / 100; //dragDistance is 5% height of the screen
        RunCharacter(800.0f);
    }

    private void RunCharacter(float horizontalInput)
    {
        characterBody.AddForce(new Vector2(horizontalInput * playerSpeed * Time.deltaTime, 0));
    }

    public void MovePlayer(float moveAmount)
    {
        RunCharacter(moveAmount);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        RunCharacter(speed);
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lp = touch.position;
                tapping = false;
                if (Mathf.Abs(lp.y - fp.y) > dragDistanceH)
                {
                    if (lp.x < fp.x)
                    {
                        debugText.text += "left swipe\n";
                        Debug.Log("left swipe");
                        if (speed - 200 <= 0)
                        {

                        }
                        else
                        {
                            speed -= 200;
                            intervals = 5;
                        }
                    }

                }
            }
            else
            {
                if (!tapping)
                {
                    tapTimer = 0f;
                    Jump();
                    Debug.Log("jump");
                    tapping = true;

                }

            }
        }
        if (tapping)
        {
            tapTimer += Time.deltaTime;
            if (tapTimer > maxTaps)
            {
                tapping = false;
                tapTimer = 0;

            }
        }
        //move player mobile
        if (speed < maxSpeed)
        {
            intervals -= Time.deltaTime;
            debugText.text = intervals.ToString();
            if (intervals <= 1)
            {
                speed += 100;
                if (speed > maxSpeed)
                {
                    speed = maxSpeed;
                }
                intervals = 5;
            }
        }


    }
    private void FixedUpdate()
    {
#if Unity_Editor
        RunCharacter(input.GetAxis("Horizontal"));
#endif
    }

    void PlayerMove()
    {
        //controls
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);

    }


    void Jump()
    {
        //jumping code
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * PlayerJumpPower);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("mine"))
        {
            damage = true;
            Object.Destroy(other.gameObject);
            Debug.Log("I GOT Mined");
        }

        if (other.gameObject.tag.Equals("Coin"))
        {
            point.scoreValue++;
            coin = true;
            Object.Destroy(other.gameObject);
            Debug.Log("I GOT a Coin");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        timer -= Time.deltaTime;
        if (other.gameObject.name.Equals("Detecc"))
        {
            GameObject.FindWithTag("Respawn").GetComponent<BoxCollider2D>().enabled = false;
        }


    }
}
