using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;


public class Collectible : MonoBehaviour
{
    public GameManager gameManager;
    public float respawnDelay = 2.0f;
    // public Transform spawnPoint;
    public GameObject coinage;
    
    private Collider coinCollider;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        coinCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            gameManager.AddPoints(1);
            Destroy(gameObject);
            // random post + respawn

            Debug.Log("fak");

            Vector3 randomSpawnPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            Instantiate(coinage, randomSpawnPosition, Quaternion.identity);
        }

    } 

    
    // coin pos
    /* 
    


    */




    // private IEnumerator RespawnCoin()
    // {
    //     yield return new WaitForSeconds(respawnDelay);
    //     coinCollider.enabled = true;
    //     transform.position = spawnPoint.position;
    //     Debug.Log("lasklaskasl");
    // }
    
}

