using _ProjectTemplate.Scripts.GamePlay.Player;
using _ProjectTemplate.Scripts.Managers;
using GameToolSample.Scripts.Layers_Tags;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type
    {
        Coin = 0,
        ExtraLife = 1,
        MagicMushroom = 2,
        StarPower = 3,
    }

    public Type type;


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(TagName.Player))
        {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        PlayerController playerScript = player.GetComponent<PlayerController>();
        if (!playerScript)
        {
            return;
        }

        switch (type)
        {
            case Type.Coin:
                GameController.Instance.AddCoin();
                break;
            case Type.ExtraLife:
                GameController.Instance.AddLife();
                break;
            case Type.MagicMushroom:
                playerScript.Grow();
                break;
            case Type.StarPower:
                playerScript.StarPower();
                break;
        }

        DeActiveObject();
    }

    private void DeActiveObject()
    {
        Destroy(gameObject);
    }
}