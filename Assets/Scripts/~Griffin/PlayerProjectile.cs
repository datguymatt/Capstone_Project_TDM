using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float playerDamage;
    private GameObject projectile;
    private void Start()
    {
        projectile = this.gameObject;
    }
    private void Awake()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.GetDamage(playerDamage);
                //play hit sound
                var projectileRb = projectile.GetComponent<Rigidbody>();
                projectileRb.freezeRotation = true;
                projectileRb.isKinematic = true;
                projectile.transform.SetParent(collision.transform);                
            }
        }
        else if (!collision.gameObject.CompareTag("Player"))
        {
            var projectileRb = projectile.GetComponent<Rigidbody>();
            projectileRb.freezeRotation = true;
            projectileRb.isKinematic = true;
            Destroy(projectile, 3f);
        }
        GetComponent<AudioSource>().Play();
    }
}
