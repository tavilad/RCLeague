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
            RaycastHit hit;
            print(isGrounded(transform) + " " + transform.localRotation.x + " " + transform.localRotation.z);

            if (Physics.Raycast(transform.position, Vector3.down, out hit, distanceToGround + 0.1f))
            {
                transform.position = hit.point + new Vector3(0f, 2f, 0f);
                transform.rotation = Quaternion.Euler(defaultRotation);
            }
        }
    }
}