using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net.PeerToPeer.Collaboration;
using System.Web.Mvc;
using Unity.VisualScripting;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    private void Start()
    {
        gameObject.name = gameObject.name.Remove(name.Length - 7, 7);
    }
    private void Update()
    {
        if(Vector3.Distance(transform.position,Player.MyInstance.transform.position)<4.0f)
        {
            transform.DOMove(Player.MyInstance.transform.position, 1.0f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            InventoryManager.MyInstance.AddItem(gameObject.name);
            Destroy(gameObject);
        }
    }
}
