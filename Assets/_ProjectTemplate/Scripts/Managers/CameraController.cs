using GameTool.Assistants.DesignPattern;
using UnityEngine;

namespace _ProjectTemplate.Scripts.Managers
{
    public class CameraController : SingletonMonoBehaviour<CameraController>
    {
        [SerializeField] protected Transform playerTransform;

        [Header("CONFIG CAM")] [SerializeField]
        private float minX;

        [SerializeField] private float maxX;
        [SerializeField] private float minY;
        [SerializeField] private float maxY;
        private Vector3 targetPoint, newPoint;

        // Limitations for camera movement
        // public Vector3 minLimits;
        // public Vector3 maxLimits;
        public float speedMoth;

        private void CheckPlayer()
        {
            if (!playerTransform)
            {
                playerTransform = GameController.Instance.PlayerController.transform;
            }
        }

        private void LateUpdate()
        {
            if (!GameController.Instance.IsPlayingGame)
            {
                return;
            }
            Follow();
        }

        private void Follow()
        {
            CheckPlayer();
            targetPoint = playerTransform.position;
            targetPoint.x = Mathf.Clamp(targetPoint.x, minX, maxX);
            targetPoint.y = Mathf.Clamp(targetPoint.y, minY, maxY);

            targetPoint.z = -10f;
            // Vector3 dir = new Vector3(Mathf.Clamp(targetPoint.x, minLimits.x, maxLimits.x),
            //     Mathf.Clamp(targetPoint.y, minLimits.y, maxLimits.y),
            //     Mathf.Clamp(targetPoint.z, minLimits.z, maxLimits.z));
            newPoint = Vector3.Lerp(transform.position, targetPoint, speedMoth * Time.deltaTime);
            transform.position = newPoint;
        }
    }
}