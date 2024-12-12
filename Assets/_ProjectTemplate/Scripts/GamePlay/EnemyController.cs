using UnityEngine;

namespace _ProjectTemplate.Scripts.GamePlay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb2D;
        public float moveSpeed = 8f;

        [Space] public float minPosX;
        public float maxPosX;

        private bool isMoveRight;

        private void OnEnable()
        {
            isMoveRight = true;
            transform.localPosition = new Vector3(minPosX, transform.localPosition.y);
        }

        private void FixedUpdate()
        {
            if (transform.localPosition.x <= minPosX)
            {
                isMoveRight = true;
            }
            else if (transform.localPosition.x >= maxPosX)
            {
                isMoveRight = false;
            }

            if (isMoveRight)
            {
                _rb2D.velocity = Vector2.right * (moveSpeed * Time.fixedDeltaTime);
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                _rb2D.velocity = Vector2.left * (moveSpeed * Time.fixedDeltaTime);
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
}