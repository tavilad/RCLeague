using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{
    public Text text;

    // Use this for initialization
    private void Start()
    {
        text.text = Camera.main.pixelWidth + " " + Camera.main.pixelHeight;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}