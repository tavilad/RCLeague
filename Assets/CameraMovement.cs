using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject cube;

    private Vector3 offset;

    // Use this for initialization
    private void Start()
    {
        offset = transform.position - cube.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = cube.transform.position + offset;
    }
}