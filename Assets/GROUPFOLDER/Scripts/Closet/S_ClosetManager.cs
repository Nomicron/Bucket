using System.Collections.Generic;
using UnityEngine;

public class S_ClosetManager : MonoBehaviour
{
    public static S_ClosetManager Instance;

    [SerializeField]
    S_ClosetItem[] items;

    readonly HashSet<string> unlocked = new HashSet<string>();
    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (S_ClosetItem item in items)
        {
            item.gameObject.SetActive(false);
        }
    }

    public bool IsUnlocked(string id) => unlocked.Contains(id);

    public void Unlock(string evidenceId)
    {
        unlocked.Add(evidenceId);
        foreach(S_ClosetItem item in items) 
        {
            if(item.evidenceId == evidenceId) 
            {
                item.gameObject.SetActive(true);
            }
        }
    }
}
