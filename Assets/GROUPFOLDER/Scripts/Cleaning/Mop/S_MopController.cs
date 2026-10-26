using UnityEngine;

public class MopController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

   [SerializeField] Cleaning cleanScript;
    [SerializeField] GameObject mop;

    void Start()
    {
        mop.SetActive(false);
    }

    void Update()
    {
        mop.SetActive(cleanScript.hasMop);
    }
}
