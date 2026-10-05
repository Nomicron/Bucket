using UnityEngine;

public class MakeBed : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject bed;
    [SerializeField] private RayController ray;

    MeshFilter meshFilter;
    void Start()
    {
        meshFilter = bed.GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(ray.TryGetComponent<Bed>(out Bed bedComponent))
        //{
        //    if (bedComponent != null)
        //    {
        //        if (Input.GetKeyDown(KeyCode.E))
        //        {
        //          bedComponent.MakeBed();
        //        }
        //    }
        //}
    }
}
