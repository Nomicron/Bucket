using UnityEngine;

public class mopTriggerScript : MonoBehaviour
{
    [SerializeField] private mopScript mop;

    private void OnTriggerEnter(Collider other)
    {
        mop.HandleTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        mop.HandleTriggerStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        mop.HandleTriggerExit(other);
    }
}
