using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
                 // slow = 0 element = 8.6f  // normal = 1st element = 10.4f
public enum Speeds { Slow, Normal, Fast, Faster, Fastest};
public enum GameMode { Cube = 0, Ship = 1};
public enum Gravity { Upright = 1, Upside = -1 };
public class Movement : MonoBehaviour
{
    public Speeds CurrentSpeed;
    public Transform GroundCheckTransform;
    public Transform Sprite;
    public LayerMask GroundMask;
    public float GroundCheckRadius;
    public float jumpForce = 26.6581f;
    public GameMode CurrGameMode;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    float[] SpeedValues = { 8.6f, 10.4f, 12.96f, 15.6f, 19.27f };

    private void Update()
    {                                               // 1/10 FPS = 0.1 time.deltatime, so won't move too fast           
        transform.position += Vector3.right * SpeedValues[(int)CurrentSpeed] * Time.deltaTime;

        if (OnGround())
        {
            Vector3 Rotation = Sprite.rotation.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
            Sprite.rotation = Quaternion.Euler(Rotation);

            if(Input.GetMouseButtonDown(0))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        else
        {
            Sprite.Rotate(Vector3.back * 360 * Time.deltaTime); //full turn in 1 second
        }
    }
    bool OnGround()
    {
        return Physics2D.OverlapCircle(GroundCheckTransform.position, GroundCheckRadius, GroundMask);
    }

    public void changeThroughPortal(GameMode gameMode, Speeds speed, Gravity gravity, int state)
    {
        if(state == 0)
        {
            CurrentSpeed = speed;
        }
        else if (state == 1)
        {
            CurrGameMode = gameMode;
        }
        else if(state == 2)
        {
            rb.gravityScale = Mathf.Abs(rb.gravityScale) * (int)gravity;
        }
    }
}
