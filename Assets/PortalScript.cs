using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [Header("Change speed")]
    public bool ChangeSpeed;
    public Speeds newSpeed;

    [Header("Change gamemode")]
    public bool ChangeMode;
    public GameMode newGameMode;

    [Header("Change gravity")]
    public bool ChangeGravity;
    public bool gravityUP;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Movement player))
        {
            player.PortalChanges(newGameMode, ChangeMode, newSpeed, ChangeSpeed, gravityUP ? 1 : -1 , ChangeGravity);
        }
    }
}
