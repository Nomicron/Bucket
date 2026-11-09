using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class KeyInventoryManager : MonoBehaviour
{
    public static KeyInventoryManager Instance;

    // Keeps track of keys only allowing one copy of it
    HashSet<string> collectedKeys = new HashSet<string>();

    void Awake()
    {
        // Allows only one instance of S_KeyInventory
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddKey(string keyID) 
    {
        if (!string.IsNullOrEmpty(keyID) && !collectedKeys.Contains(keyID)) 
        {
            collectedKeys.Add(keyID);
            Debug.Log($"Key collected and added to inbentory : {keyID}");
        }
    }

    public bool HasKey(string keyID) 
    {
        if (string.IsNullOrEmpty(keyID)) 
        {
            return true;
        }
        return collectedKeys.Contains(keyID);
    }
}
