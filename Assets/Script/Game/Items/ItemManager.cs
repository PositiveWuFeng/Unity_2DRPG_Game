using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    public Item[] items;

    private static ItemManager instance;

    public static ItemManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ItemManager>();
            }
            return instance;
        }
    }
}
