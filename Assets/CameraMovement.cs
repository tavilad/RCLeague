using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;

    private Vector3 positionOffset;

    private Space offsetPositionSpace = Space.Self;

    private bool lookAt = true;

    private void Start()
    {
        positionOffset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = player.transform.TransformPoint(positionOffset);
        }
        else
        {
            transform.position = player.transform.position + positionOffset;
        }

        if (lookAt)
        {
            transform.LookAt(player.transform);
        }
        else
        {
            transform.rotation = player.transform.rotation;
        }
    }
}