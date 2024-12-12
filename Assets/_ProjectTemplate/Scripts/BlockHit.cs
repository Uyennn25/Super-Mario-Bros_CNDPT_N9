using System.Collections;
using GameToolSample.Scripts.Layers_Tags;
using Unity.Mathematics;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    public GameObject item;
    public Sprite emptyBlock;
    public int maxHits = -1;

    private bool animating;

    //hàm va chạm OncollisionEnter2D có sẵn 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!animating && maxHits != 0 && collision.gameObject.CompareTag(TagName.Player))
        {
            if (collision.transform.DotTest(transform, Vector2.up))
            {
                Hit();
            }
        }
    }

    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        maxHits--;
        if (maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
        }

        if (item != null)
        {
            Instantiate(item, transform.position, quaternion.identity);
        }

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        animating = true;

        Vector3 restingPosition = transform.localPosition;
        Vector3 animatePosition = restingPosition + Vector3.up * 0.5f;

        yield return Move(restingPosition, animatePosition);
        yield return Move(animatePosition, restingPosition);

        animating = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
        Destroy(gameObject);
    }
}