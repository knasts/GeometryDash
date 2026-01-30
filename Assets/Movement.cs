using System.Collections;
using System.Collections.Generic;
using UnityEngine;
                 // slow = 0 element = 8.6f  // normal = 1st element = 10.4f
public enum Speeds { Slow, Normal, Fast, Faster, Fastest};
public class Movement : MonoBehaviour
{
    public Speeds CurrentSpeed;
    public Transform GroundCheckTransform;
    public float GroundCheckRadius;
    public LayerMask GroundMask;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    float[] SpeedValues = { 8.6f, 10.4f, 12.96f, 15.6f, 19.27f };

    private void Update()
    {                                               // 1/10 FPS = 0.1 time.deltatime, so won't move too fast           
        transform.position += Vector3.right * SpeedValues[(int)CurrentSpeed] * Time.deltaTime;
        float jumpForce = 26.6581f;
        if (Input.GetMouseButtonDown(0))
        {
            if(OnGround())
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
    bool OnGround()
    {
        return Physics2D.OverlapCircle(GroundCheckTransform.position, GroundCheckRadius, GroundMask);
    }
}
