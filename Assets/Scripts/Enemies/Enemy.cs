using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    private RoundManager _roundManager;
    public GameObject player;
    public float speed = 2f;
    public int hitCount = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
        //!!!!!REFACTOR
        _roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move our position a step closer to the target.
        var step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, -1.05f, player.transform.position.z), step);
        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, player.transform.position) < 0.001f)
        {
            // Swap the position of the cylinder.
            player.transform.position *= -1.0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Im here");
        //simple destroy enemy to see round flow
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            Debug.Log("Im here");
            //substract an enemy from 
            _roundManager.KillEnemy();
            Destroy(this.gameObject);
        }
    }
}
