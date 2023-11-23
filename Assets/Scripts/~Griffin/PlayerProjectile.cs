using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private GameObject player;
    private float playerDamage;
    private GameObject projectile;

    private void Start()
    {
        projectile = this.gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<IDamageable>().GetDamage(playerDamage);
        }
        else if(!collision.gameObject.CompareTag("Player"))
        {
            var projectileRb = projectile.GetComponent<Rigidbody>();
            projectileRb.freezeRotation = true;
            projectileRb.isKinematic = true;
            //projectile.transform.SetParent(collision.transform);
        }
    }
}
