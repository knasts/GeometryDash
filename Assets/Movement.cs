using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
                 // slow = 0 element = 8.6f  // normal = 1st element = 10.4f
public enum Speeds { Slow, Normal, Fast, Faster, Fastest};
public enum GameMode { Cube, Ship};
public class Movement : MonoBehaviour
{
    public Speeds CurrentSpeed;
    public Transform Sprite;
    public LayerMask GroundMask;
    public float GroundCheckRadius;
    public float jumpForce = 26.6581f;
    public GameMode CurrGameMode;

    public int Gravity = 1;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    float[] SpeedValues = { 8.6f, 10.4f, 12.96f, 15.6f, 19.27f };

    private void Update()
    {                                               // 1/10 FPS = 0.1 time.deltatime, so won't move too fast           
        transform.position += Vector3.right * SpeedValues[(int)CurrentSpeed] * Time.deltaTime;

        if (rb.velocity.y < -24.2f) //wont fall too deep, just original game logic
            rb.velocity = new Vector2(rb.velocity.x, -24.2f);

        if(CurrGameMode == GameMode.Cube)
        {
            Cube();
        }
        else if(CurrGameMode == GameMode.Ship)
        {
            Ship();
        }
    }

    void Cube()
    {
        rb.gravityScale = 12.41f * Gravity;
        if (OnGround())
        {
            Vector3 Rotation = Sprite.rotation.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
            Sprite.rotation = Quaternion.Euler(Rotation);

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce * Gravity, ForceMode2D.Impulse);
            }
        }
        else
        {
            Sprite.Rotate(Vector3.back * 360 * Time.deltaTime * Gravity); //full turn in 1 second
        }
    }

    void Ship()
    {
        Sprite.rotation = Quaternion.Euler(0, 0, rb.velocity.y * 2);

        if (Input.GetMouseButton(0))
            rb.gravityScale = -4.314969f;
        else
            rb.gravityScale = 4.314969f;

        rb.gravityScale = rb.gravityScale * Gravity;
    }


    bool OnGround()
    {
        return Physics2D.OverlapBox(transform.position + Vector3.down * Gravity * 0.5f, Vector2.right * 1.1f + Vector2.up * GroundCheckRadius, 0, GroundMask);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            foreach(ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y < 0.7f) GameOver();
            }
        }
    }

    bool TouchingWall()
    {
        return Physics2D.OverlapBox((Vector2)transform.position + (Vector2.right * 0.55f), Vector2.up * 0.8f + (Vector2.right * GroundCheckRadius), 0, GroundMask);
    }

    public void PortalChanges(GameMode newGameMode, bool changeMode, Speeds newSpeed, bool changeSpeed, int gravityUP, bool changeGravity)
    {
        if(changeMode)
        {
            CurrGameMode = newGameMode;
            Sprite.rotation = Quaternion.identity;
        }
        
        if (changeSpeed)
        {
            CurrentSpeed = newSpeed;
        }
        if(changeGravity)
        {
            Gravity = gravityUP;
            rb.gravityScale = Mathf.Abs(rb.gravityScale) * gravityUP;

            if(gravityUP == -1) Sprite.localScale = new Vector3(1, -1 , 1);
            else Sprite.localScale = new Vector3(1, 1, 1);
        }
    }

    void GameOver()
    {
        Time.timeScale = 0;
        enabled = false;
    }
}
