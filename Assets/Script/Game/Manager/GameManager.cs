using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private NPC currentTarget;
    void Start()
    {
        
    }

    void Update()
    {
       // ClickTarget();
    }

    private void ClickTarget()
    {
       /* RaycastHit2D hit = Physics2D.Raycast(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0), Vector2.zero, Mathf.Infinity);
        if (hit.collider != null)
        {
            if (currentTarget != null)
            {
                currentTarget.DeSelect();
            }
            currentTarget = hit.collider.GetComponent<NPC>();
        }*/

    }

}
