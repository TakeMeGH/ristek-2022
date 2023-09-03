using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    private Rigidbody2D rb2d;
    [SerializeField] Transform playerTransform;
    [SerializeField] float chaseDistance;
    [SerializeField] float FollowSpeed;
    [SerializeField] ParticleSystem deadParticle;
    [SerializeField] GameObject gunPowerUp;
    [SerializeField] GameObject healthPowerUp;

    public bool isChasing = false;
    bool isCollide = false;
    // [SerializeField] PathFinding pathFinding;
    [SerializeField] setPathFinding pathFinding;
    Vector3 sizeEnemy = new Vector3(0, 0.75f, 0);

    int curPathIdx = -1;
    Vector3 lastPlayerPos;
    List<Vector3> pathToPlayer;
    float lastExe;

    Animator _animator;

    Coroutine nextMoveCoroutine;
    float moveTimer = 0f;


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); 
        _animator = GetComponent<Animator>();
        _animator.SetBool("isWalking", false);
        _animator.SetBool("isFaceRight", true);

        lastExe = Time.time;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            isCollide = true;
            _animator.SetBool("isWalking", false);        
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            isCollide = false;
        }

    }

    public void turnOnChasing(){
        isChasing = true;
    }
    private void CheckPlayer()
    {
        // TODO : Implementasi enemy mengecek apakah player berada di dalam radius
        
        if(moveTimer <= 0){
            Vector3 playerPos = playerTransform.position;
            float diffX = Math.Abs(transform.position.x - playerPos.x);
            float diffY = Math.Abs(transform.position.y - playerPos.y);
            float diffDistance = diffX + diffY;
            if((diffDistance - chaseDistance <= Mathf.Epsilon || isChasing) && !isCollide){
                findPlayer();
                isChasing = true;
                _animator.SetBool("isWalking", true);
                moveTimer = 0.4f;
            }
        }
    }

    public void enemyDead(){
        int dropChance = UnityEngine.Random.Range(1, 16);
        if(dropChance == 1){
            Instantiate(gunPowerUp, transform.position, Quaternion.identity);
        }
        else if(dropChance == 2){
            Instantiate(healthPowerUp, transform.position, Quaternion.identity);
        }
        Instantiate(deadParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void findPlayer(){
        pathToPlayer = pathFinding.bfs(transform.position, playerTransform.position);
        curPathIdx = (int)pathToPlayer.Count - 1;
    }
    private void MoveEnemy()
    {
        // TODO : Implementasi enemy mengikuti player jika masuk dalam jangkauan radius
        if(Vector3.Distance(playerTransform.position + sizeEnemy, transform.position) <= 1){
            transform.position = Vector3.Lerp(transform.position, playerTransform.position + sizeEnemy, FollowSpeed * Time.deltaTime);
        }
        else if(pathFinding != null && curPathIdx >= 0){ 
            if(Vector3.Distance(pathToPlayer[curPathIdx] + sizeEnemy, transform.position) >= 0.7f){
                Vector3 dir = (pathToPlayer[curPathIdx] + sizeEnemy - transform.position).normalized;
                transform.position = transform.position + dir * FollowSpeed * Time.deltaTime;
            }
            else{
                curPathIdx--;
            }
        }
        // transform.position = Vector3.Lerp(transform.position, playerTransform.position, FollowSpeed*Time.deltaTime);

    }

    void CheckDirection(){
        if(playerTransform == null) return;
        Vector3 playerPos = playerTransform.position;
        float diffX = (transform.position.x - playerPos.x);
        if(diffX >= 0){
            transform.rotation = new Quaternion(0, 180, 0, 0);
            _animator.SetBool("isFaceRight", false);

        }
        else{
            transform.rotation = new Quaternion(0, 0, 0, 0);
            _animator.SetBool("isFaceRight", true);

        }
    }

    private void Update() {
        moveTimer -= Time.deltaTime;
        if(playerTransform == null) return;
        CheckDirection();
        MoveEnemy();
    }

    private void FixedUpdate()
    {
        if(playerTransform == null) return;
        CheckPlayer();
        lockZ();
    }

    void lockZ(){
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
