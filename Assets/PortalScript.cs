using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public GameMode gameMode;
    public Speeds speed;
    public Gravity gravity;
    public int state;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Movement player))
        {
            player.changeThroughPortal(gameMode, speed, gravity, state);
        }
    }
}
