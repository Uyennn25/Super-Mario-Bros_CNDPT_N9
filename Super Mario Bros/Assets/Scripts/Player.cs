using System;
using System.Collections;

using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;

    public DeathAnimation deathAnimation;

    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool dead => deathAnimation.enabled;
    private void Awake()
    {
        bigRenderer.enabled = false;
        deathAnimation = GetComponent<DeathAnimation>();
    }

    public void Hit()
    {
        if (big)
        {
            Shrink();
        }
        else
        { 
            Death();
        }
    }

    private void Shrink()
    {
        
    }

    private void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;
        
        GameManager.Instance.ResetLevel(3f);
    }
}
