using UnityEngine;

namespace Assets
{
    internal class GameObjectUtil : MonoBehaviour
    {
        public static float distanceToGround = 0.5f;

        private static Vector3 defaultRotation = new Vector3(0f, 0f, 0f);

        public static bool isGrounded(Transform transform)
        {
            return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f);
        }

        public static void respawn(Transform transform)
        {
            Vector3 spawn = new Vector3(0f, 2f, 0f);

            transform.position = spawn;
            transform.rotation = Quaternion.Euler(defaultRotation);
        }

        public static void flip(Transform transform)
        {
            if (isGrounded(transform) && transform.eulerAngles.x > 70 || transform.eulerAngles.z > 70)
            {
                transform.position = new Vector3(transform.position.x, 2f, transform.position.z);
                transform.rotation = Quaternion.Euler(defaultRotation);
            }
        }
    }
}