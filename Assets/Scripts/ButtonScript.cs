using Assets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public bool isPressedLeft;
    public bool isPressedRight;

    //public static GameObject car;
    public static Movement movement;
    public static WheelCollider[] wheels = new WheelCollider[4];
    public float steerAngle;

    private void Start()
    {

        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            //canvas.enabled = false;
        }
    }

    public void Update()
    {

        if (movement != null)
        {
            if (isPressedLeft)
            {
                movement.MoveLeft(wheels, steerAngle);
            }
            else

                if (isPressedRight)
                {
                    movement.MoveRight(wheels, steerAngle);
                }
                else
                {
                    wheels[0].steerAngle = 0;
                    wheels[1].steerAngle = 0;
                }
        }
        else
        {
            //print("cant find movement component");
        }
    }

    public void Flip(GameObject obj)
    {
        GameObjectUtil.Flip(obj.transform);
    }

    public void Jump(Rigidbody rb)
    {
        Movement.Jump(rb, 40);
    }

    public void OnPointerDown(GameObject obj)
    {
        if (obj.transform.tag == "Left")
        {
            isPressedLeft = true;
        }

        if (obj.transform.tag == "Right")
        {
            isPressedRight = true;
        }
    }

    public void OnPointerUp(GameObject obj)
    {
        if (obj.transform.tag == "Left")
        {
            isPressedLeft = false;
        }

        if (obj.transform.tag == "Right")
        {
            isPressedRight = false;
        }
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

}