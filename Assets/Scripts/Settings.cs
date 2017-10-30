using UnityEngine;

public class Settings : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
    }

    public static void ChangeResolution(int screenWidth, int screenHeight, bool isFullscreen)
    {
        Screen.SetResolution(screenWidth, screenHeight, isFullscreen);
    }
}