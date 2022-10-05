using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrafabManager : MonoBehaviour
{
    public GameObject[] perfabs;

    private static PrafabManager instance;

    public static PrafabManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PrafabManager>();
            }
            return instance;
        }
    }
}
