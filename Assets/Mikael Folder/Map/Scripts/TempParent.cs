using System.Collections.Generic;
using UnityEngine;

public class TempParent : MonoBehaviour
{
    public static TempParent Instance { get; private set; }
    //Makes it so there is only one TempParent item in the scene
    private void Awake()
    {
        if(Instance == null) 
        {
            Instance = this;
        }
        else 
        {
            Destroy(this);
        }
    }
}
