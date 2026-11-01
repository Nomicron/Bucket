using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Evidence : MonoBehaviour
{
    public string name;
    public string description;
}

public class Wrench : Evidence { }

public class Tie : Evidence { }