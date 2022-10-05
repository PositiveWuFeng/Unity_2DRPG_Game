using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public ImageSprite[] sprites;

    private static SpriteManager instance;

    public static SpriteManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpriteManager>();
            }
            return instance;
        }
    }
}

