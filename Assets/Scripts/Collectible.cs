using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collectible : MonoBehaviour
{
    public GameManager gameManager;
    public float respawnDelay = 2.0f;
    public Transform spawnPoint;

    private Collider coinCollider;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
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
            coinCollider.enabled = true;
            StartCoroutine(RespawnCoin());
        }
    } 

    private IEnumerator RespawnCoin()
    {
        yield return new WaitForSeconds(respawnDelay);
        coinCollider.enabled = true;
        transform.position = spawnPoint.position;
    }
    
}

