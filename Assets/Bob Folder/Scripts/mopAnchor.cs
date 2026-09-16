using UnityEngine;

public class mopAnchor : MonoBehaviour
{
    [SerializeField] private Transform player;

    [Header("Offset from player")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 1f, 2f);

    private void Update()
    {
        if (player == null)
        {
            Debug.LogError("Mop Anchor has no Player assigned!");
            return;
        }

        // Only use horizontal player rotation.
        Quaternion yawRotation = Quaternion.Euler(0f, player.eulerAngles.y, 0f);

        // Follow player position.
        transform.position = player.position + yawRotation * offset;

        // Follow player yaw.
        transform.rotation = yawRotation;
    }
}
