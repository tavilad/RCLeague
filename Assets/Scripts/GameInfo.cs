using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{
    public Text text;

    private void Start()
    {
        text.text = Camera.main.pixelWidth + " " + Camera.main.pixelHeight;
        Screen.SetResolution(800, 450, true);
    }

    private void Update()
    {
    }
}