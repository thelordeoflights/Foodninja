using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target : MonoBehaviour
{
    private Rigidbody targetRb;  
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float YPos = -2;  
    public int pointvalue;
    public int lives = 3;
    private GameManager gameManager;
    
    public ParticleSystem explosionParticle;
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
       
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
       
        targetRb.AddTorque(RandomTorque(),RandomTorque(),RandomTorque(), ForceMode.Impulse); //apply the force required to rotate
       
        transform.position = RandomSpawnPos();
        
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // Debug.Log(gameManager.lives);
    }

    private void OnMouseDown()
    {
        if(gameManager.isGameActive)
        {   
            Destroy(gameObject);
            gameManager.UpdateScore(pointvalue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if(!gameObject.CompareTag("Bad"))
        //if(gameManager.score < 0)
        {
           // gameManager.GameOver();
           gameManager.UpdateLives();
        }
    }
    void Update()
    {
        
    }
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed,maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque,maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange,xRange), YPos);
    }
}
