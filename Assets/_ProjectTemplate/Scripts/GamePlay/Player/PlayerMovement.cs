using GameToolSample.Scripts.Layers_Tags;
using UnityEngine;

namespace _ProjectTemplate.Scripts.GamePlay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        // private void OnCollisionEnter2D(Collision2D collision)
        // {
        //     if (collision.gameObject.layer == LayerMask.NameToLayer(LayerName.Enemy))
        //     {
        //         if (transform.DotTest(collision.transform, Vector2.down))
        //         {
        //             velocity.y = jumpForce / 2f;
        //             isJumping = true;
        //         }
        //     }
        //
        //     if (collision.gameObject.layer != LayerMask.NameToLayer(LayerName.PowerUp))
        //     {
        //         if (transform.DotTest(collision.transform, Vector2.up))
        //         {
        //             velocity.y = 0f;
        //         }
        //     }
        // }
    }
}