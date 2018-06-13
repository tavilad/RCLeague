using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "PickUps", order = 1)]
public class PickupInfo : ScriptableObject
{
    public string objectName;
    public Texture texture;
}