using UnityEngine;

[CreateAssetMenu(fileName = "NPC_Description", menuName = "Scriptable Objects/NPC_Description")]
public class NPC_Description : ScriptableObject
{
    [TextArea(2, 5)]
    public string[] lines;

    public NPC_Description[] information;
}
