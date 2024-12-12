using System.Collections;
using _ProjectTemplate.Scripts.Managers;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{
    private void Start()
    {
        GameController.Instance.AddCoin();
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        Vector3 restingPosition = transform.localPosition;
        Vector3 animatePosition = restingPosition + Vector3.up * 2f;

        yield return Move(restingPosition, animatePosition);
        yield return Move(animatePosition, restingPosition);

        Destroy(gameObject);
    }
    

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.25f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
    }
}