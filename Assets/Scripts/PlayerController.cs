using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;



public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject gunPoint;
    [SerializeField] float movementSpeed;
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject bullet;

    [SerializeField] Vector3 offsetPosBullet = new Vector3(1.15f, -0.21f, 0);

    [SerializeField] float powerUpDuration = 0;
    public float lastTimePowerUp = -100;
    Quaternion faceLeft = new Quaternion(0, 180, 0, 0);
    Quaternion faceRight = new Quaternion(0, 0, 0, 180);
    Animator _animator;
    AudioSource audioSource;


    // Start is called before the first frame update

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _animator.SetBool("isWalking", false);
        _animator.SetBool("isFaceRight", false);


    }

    // Update is called once per frame
    void Update()
    {
        CheckCursor();

    }

    private void FixedUpdate()
    {
        Movement();
        lockZ();
    }
    
    bool isPowerUp(){
        if(Time.time - lastTimePowerUp > powerUpDuration){
            return false;
        }
        else return true;
    }
    private void CheckCursor()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 characterPos = transform.position;
        if (mousePos.x > characterPos.x)
        {
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
            _animator.SetBool("isFaceRight", true);

        }
        else if (mousePos.x < characterPos.x)
        {
            this.transform.rotation = new Quaternion(0, 180, 0, 0);
            _animator.SetBool("isFaceRight", false);

        }
            
        
        // TODO : Implementasi player menembak
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            if(transform.rotation == faceLeft){
                offsetPosBullet.x *= -1;
            }
            GameObject bulletSpawn = Instantiate(bullet, playerTransform.position + offsetPosBullet, playerTransform.rotation);
            Bullet bulletComps = bulletSpawn.GetComponent<Bullet>();
            audioSource.volume = 0.1f;
            audioSource.Play(0);
            Vector3 bulletPos = new Vector3(mousePos.x - characterPos.x, mousePos.y - characterPos.y, 0);
            bulletComps.Init(bulletPos, faceRight == transform.rotation, false, isPowerUp());
            if(transform.rotation == faceLeft){
                offsetPosBullet.x *= -1;
            }

        }
        
    }
    private void Movement()
    {
        // TODO : Implementasi movement player
        if(!Input.anyKey){
            _animator.SetBool("isWalking", false);

        }
        else if(Input.GetKey(KeyCode.W)){
            _animator.SetBool("isWalking", true);
            transform.Translate(Vector3.up * movementSpeed * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.S)){
            _animator.SetBool("isWalking", true);
            transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
        }
        else if(transform.rotation == faceLeft){
            if(Input.GetKey(KeyCode.A)){
                _animator.SetBool("isWalking", true);
                transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
            }
            else if(Input.GetKey(KeyCode.D)){
                _animator.SetBool("isWalking", true);
                transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
            }
        }
        else if(transform.rotation == faceRight){
            if(Input.GetKey(KeyCode.A)){
                _animator.SetBool("isWalking", true);
                transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
            }
            else if(Input.GetKey(KeyCode.D)){
                _animator.SetBool("isWalking", true);
                transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
            }
        }
       
    }

    void lockZ(){
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

}
