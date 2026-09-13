using UnityEngine;

public class NPCFacesPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform player;
    [SerializeField] public float Smoothness = 1.1f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Smoothness * Time.deltaTime);
    }
}
