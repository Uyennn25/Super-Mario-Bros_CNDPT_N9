using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type
    {
        Coin,
        ExtraLife,
        MagicMushroom,
        Starpower,
    }

    public Type type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            collect(other.gameObject);
        }
    }

    private void collect(GameObject player)
    {
        switch (type)
        {
            case Type.Coin:
                GameManager.Instance.AddCoin();
                break;
            
            case Type.ExtraLife:
                GameManager.Instance.AddLife();
                break;
            
            case Type.MagicMushroom:
                player.GetComponent<Player>().Grow();
                break;
            
            case Type.Starpower:
                player.GetComponent<Player>().Starpower();
                break;
        }
        Destroy(gameObject);
    }
}
