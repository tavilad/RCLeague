using Assets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public bool isPressedLeft;
    public bool isPressedRight;

    public GameObject obj;
    private Movement movement;
    public WheelCollider[] wheels = new WheelCollider[2];
    public float steerAngle;
    public Canvas canvas;

    private void Start()
    {
        movement = obj.GetComponent<Movement>();
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

    public void flip(GameObject obj)
    {
        GameObjectUtil.flip(obj.transform);
    }

    public void jump(Rigidbody rb)
    {
        Movement.Jump(rb);
    }

    public void onPointerDown(GameObject obj)
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

    public void onPointerUp(GameObject obj)
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

    public void Quality(GameObject obj)
    {
        if (obj.transform.tag == "Fastest")
        {
            QualitySettings.SetQualityLevel(0, true);
            Debug.Log("fastest");
        }
        if (obj.transform.tag == "Fast")
        {
            QualitySettings.SetQualityLevel(1, true);
            Debug.Log("fast");
        }
        if (obj.transform.tag == "Simple")
        {
            QualitySettings.SetQualityLevel(2, true);
            Debug.Log("simple");
        }
        if (obj.transform.tag == "Good")
        {
            QualitySettings.SetQualityLevel(3, true);
            Debug.Log("good");
        }
        if (obj.transform.tag == "Beautiful")
        {
            QualitySettings.SetQualityLevel(4, true);
            Debug.Log("beautiful");
        }
        if (obj.transform.tag == "Fantastic")
        {
            QualitySettings.SetQualityLevel(5, true);
            Debug.Log("Fantastic");
        }
        if (obj.transform.tag == "Resolution1")
        {
            Screen.SetResolution(896, 504, true);
            Debug.Log("resolution1");
        }
        if (obj.transform.tag == "Resolution2")
        {
            Screen.SetResolution(960, 540, true);
            Debug.Log("resolution2");
        }
        if (obj.transform.tag == "Resolution3")
        {
            Screen.SetResolution(1024, 576, true);
            Debug.Log("resolution3");
        }
        if (obj.transform.tag == "Resolution4")
        {
            Screen.SetResolution(1152, 648, true);
            Debug.Log("resolution4");
        }
    }
}