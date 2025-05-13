using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    // [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] ScoreType scoreType = ScoreType.Score;
    [SerializeField] int pointsForCoinPickup = 5;
    bool wasCollected = false;
    bool isMovingToPlayer = false;
    protected Transform player;
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] private AudioClip pickup;
    Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (isMovingToPlayer)
        {
            Move();
        }
        if (player && Vector2.Distance(transform.position, player.position) < 0.5f)
        {
            CollectItem();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            isMovingToPlayer = true;
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }
    
    private void Move()
    {
        Vector2 direction = (player.position - transform.position ).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
    private void CollectItem()
    {
        wasCollected = true;
        ScoreEntry scoreEntry = new ScoreEntry(scoreType, pointsForCoinPickup);
        ScoreEvent.RaiseScore(scoreEntry);
        gameObject.SetActive(false);
        
        AudioSource.PlayClipAtPoint(pickup, Camera.main.transform.position, 0.1f);
        Destroy(gameObject);
    }
}
