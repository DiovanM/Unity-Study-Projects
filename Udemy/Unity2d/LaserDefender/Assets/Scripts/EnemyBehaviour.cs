using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public Path path;

    [SerializeField] private GameObject laser;
    [SerializeField] float timeToStartShooting = 2f;
    [SerializeField] private float minTimeBetweenShots = 0.2f;
    [SerializeField] private float maxTimeBetweenShots = 1f;         
    [SerializeField] float life = 20;
    [SerializeField] int enemyValue = 150;
    [SerializeField] float moveSpeed = 2;
    [SerializeField] GameObject explosionParticle;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] [Range(0,1)] private float shootSoundVolume;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] [Range(0, 1)] private float deathSoundVolume;
    private int initialPoint = 0;
    private int nextWaypoint;
    private List<Transform> waypoints;
    private bool canMove = false;     

    private void Awake()
    {                     
        GetComponent<DeathManager>().life = life;
        GetComponent<DeathManager>().Death += Die;
    }

    private void OnDestroy()
    {
        GetComponent<DeathManager>().Death -= Die;
        explosionParticle = Instantiate(explosionParticle, transform.position, transform.rotation) as GameObject;
        Destroy(explosionParticle, 1f);
    }
                                   
    void Start ()
    {
        nextWaypoint = initialPoint;
        if (path)
        {
            waypoints = path.GetWayPoints();
            transform.position = waypoints[initialPoint].position;
            CanMove();
        }

        Invoke("StartShootCoroutine", timeToStartShooting);

    }
	                                   
	void Update () {
        if (canMove) Move();
    }

    private void Move()
    {
        if (nextWaypoint <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[nextWaypoint].position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                nextWaypoint++;
            }                                
        }
        else { Destroy(gameObject); }

    }

    private void StartShootCoroutine()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {                                                        
        while (true)
        {
            GameObject _laser = Instantiate(laser, transform.position, transform.rotation);
            _laser.GetComponent<LaserBehaviour>().SetDirection(Vector2.down);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(Random.Range(minTimeBetweenShots,maxTimeBetweenShots));
        }         
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Die();
        }
    }

    #region Sets

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void SetPath(Path path)
    {
        this.path = path;
    }

    #endregion

    public void CanMove()
    {
        canMove = true;
    }

    public void Die()
    {
        Debug.Log("KILL ENEMY");
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        Destroy(gameObject);
        FindObjectOfType<GameSession>().AddToScore(enemyValue);
    } 

}
