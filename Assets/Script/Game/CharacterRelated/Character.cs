using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web.Configuration;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    [SerializeField]
    private float speed = 5f;
    //½ÇÉ«·½Ïò
    protected  Vector2 direction;

    protected Animator myAnimator;

    protected Rigidbody2D myRigidbody;

    protected bool isAttacking=false;

    protected bool isHurt=false;

    protected Coroutine attackCoroutine;

    public bool IsMoving
    {
        get
        {
            return direction != Vector2.zero;
        }
    }

    protected virtual void Update()
    {
        HandleLayers();
    }
    protected virtual void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        myRigidbody.velocity = direction.normalized * speed;
    }
    public void HandleLayers()
    {
        /*if(isHurt)
        {
            ActivateLayer("HurtLayer");
            StopAttack();
        }*/
        if (IsMoving)
        {
            ActivateLayer("WalkLayer");

            myAnimator.SetFloat("x", direction.x);
            myAnimator.SetFloat("y", direction.y);

            StopAttack();
        }
        else if(isAttacking)
        {
            ActivateLayer("AttackLayer");
        }
        else
        {
            ActivateLayer("IdleLayer");
        }
    }
     public void ActivateLayer(string layerName)
    {
        for(int i=0;i< myAnimator.layerCount;i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }
        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }

    public virtual void StopAttack()
    {
        isAttacking = false;
        myAnimator.SetBool("attack", isAttacking);
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
    }

    public virtual void TakeDamage(float damage)
    {
    }
}
