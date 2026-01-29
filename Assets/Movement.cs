using System.Collections;
using System.Collections.Generic;
using UnityEngine;
                 // slow = 0 element = 8.6f  // normal = 1st element = 10.4f
public enum Speeds { Slow, Normal, Fast, Faster, Fastest};
public class Movement : MonoBehaviour
{
    public Speeds CurrentSpeed; 

    float[] SpeedValues = { 8.6f, 10.4f, 12.96f, 15.6f, 19.27f };

    private void Update()
    {                                               // 1/10 FPS = 0.1 time.deltatime, so won't move too fast           
        transform.position += Vector3.right * SpeedValues[(int)CurrentSpeed] * Time.deltaTime;
    }
}
