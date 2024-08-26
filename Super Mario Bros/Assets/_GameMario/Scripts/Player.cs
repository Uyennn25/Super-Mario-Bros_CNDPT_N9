using System.Collections;
using System;
using UnityEngine;
using Random = System.Random;


public class Player : MonoBehaviour
{ 
    public PlayerSpriteRenderer smallPlayerSpriteRenderer; 
    public PlayerSpriteRenderer bigPlayerSpriteRenderer;
    private PlayerSpriteRenderer activeRenderer;
    
    public DeathAnimation deathAnimation;
    private CapsuleCollider2D capsuleCollider;
    public bool big => bigPlayerSpriteRenderer.enabled;
    public bool small => smallPlayerSpriteRenderer.enabled;
    public bool dead => deathAnimation.enabled;
    public bool starpower { get; private set; }
    private void Awake()
    {
        bigPlayerSpriteRenderer.enabled = false;
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeRenderer = smallPlayerSpriteRenderer;

    }

    public void Hit()
    {
        if (!dead && !starpower)
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
       
    }
    
    private void Death()
    {
        smallPlayerSpriteRenderer.enabled = false;
        bigPlayerSpriteRenderer.enabled = false;
        deathAnimation.enabled = true;
        GameManager.Instance.ResetLevel(3f);
    }

    public void Grow()
    {
        
        smallPlayerSpriteRenderer.enabled = false;
        bigPlayerSpriteRenderer.enabled = true; 
        activeRenderer = bigPlayerSpriteRenderer;
        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);
        
        StartCoroutine(ScaleAnimation());
    }
    private void Shrink()
    {
      
        smallPlayerSpriteRenderer.enabled = true;
        bigPlayerSpriteRenderer.enabled = false;
        activeRenderer = smallPlayerSpriteRenderer;
        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                smallPlayerSpriteRenderer.enabled = !smallPlayerSpriteRenderer.enabled;
                bigPlayerSpriteRenderer.enabled = !smallPlayerSpriteRenderer.enabled;
            }

            yield return null;
        }

        smallPlayerSpriteRenderer.enabled = false;
        bigPlayerSpriteRenderer.enabled = false;
        activeRenderer.enabled = true;
    }

    public void Starpower(float duration = 10f)
    {
        StartCoroutine(starpowerAnimation(duration));
    }

    private IEnumerator starpowerAnimation(float duration)
    {
        starpower = true;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if (Time.frameCount % 4 == 0)
            {
                activeRenderer.spriteRenderer.color = Color.red;
            }

            yield return null;
        }

        activeRenderer.spriteRenderer.color = Color.white;
        starpower = false;
    }
}
