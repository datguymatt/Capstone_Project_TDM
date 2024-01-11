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
            //play sfx
            int randomFileNumber = Random.Range(1, 2);
            GameObject.FindAnyObjectByType<AudioManager>().PlaySFXAudio("enemy-hit-" + randomFileNumber.ToString());
            //
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.GetDamage(playerDamage);
                //play hit enemy sound
                var projectileRb = projectile.GetComponent<Rigidbody>();
                projectileRb.freezeRotation = true;
                projectileRb.isKinematic = true;
                projectile.transform.SetParent(collision.transform);                
            }
        }
        else if (!collision.gameObject.CompareTag("Player"))
        {
            //play sfx
            GameObject.FindAnyObjectByType<AudioManager>().PlaySFXAudio("bolt-hit-wood");
            //
            var projectileRb = projectile.GetComponent<Rigidbody>();
            projectileRb.freezeRotation = true;
            projectileRb.isKinematic = true;
            Destroy(projectile, 3f);
            Debug.Log($"{collision.gameObject.name}");
        }
    }
}
