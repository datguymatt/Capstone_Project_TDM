using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Components, children & references
    private Transform shootPoint;
    [SerializeField] private GameObject projectilePrefab;

    //Shooting variables
    [SerializeField] private float fireDelay; // This is long the player must wait between shots, can be the focus of an upgrade, perhaps?
    private float fireTimer; // This is for keeping track of time between shots, acts as a simple timer
    [SerializeField] private float shootForce = 1000f;




    private void Start()
    {
        shootPoint = GameObject.Find("ShootPoint").transform;
    }

    private void Update()
    {
        ShootWeapon();
    }

    private void ShootWeapon()
    {
        // Shot timer functionality, maybe worth separating
        fireTimer = Mathf.Clamp(fireTimer - (1 * Time.deltaTime), 0, fireDelay);

        // Shoot funcionality
        if (Input.GetMouseButton(0) && fireTimer == 0 && !PauseMenuManager.Instance.isPaused && !RestartMenu.Instance.isPaused)
        {
            // Gather playerHeadTransform for rotation base
            var playerHeadTransform = shootPoint.GetComponentInParent<Transform>();

            // Create projectile rotation by flipping playerHead forward and upward (because default rotation for projectile is 90 deg off what we need)
            Quaternion rotation = Quaternion.LookRotation(playerHeadTransform.forward, playerHeadTransform.up);

            //Create projctile and get rb for adding force, then destroy
            var projectile = Instantiate(projectilePrefab, shootPoint.transform.position, rotation);
            var projectileRb = projectile.GetComponent<Rigidbody>();
            projectileRb.AddForce(shootPoint.forward * shootForce);

            // reset fire delay
            fireTimer = fireDelay;
            //playsfx
            int randomFileNumber = Random.Range(1, 4);
            GameObject.FindAnyObjectByType<AudioManager>().PlaySFXAudio("crossbow-fire-" + randomFileNumber.ToString());

        }
    }
}
