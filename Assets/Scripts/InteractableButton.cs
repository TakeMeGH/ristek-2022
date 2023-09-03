using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButton : MonoBehaviour
{
    // Start is called before the first frame update
    Animator _animator;
    bool isTrigger = false;
    bool isNext = false;
    bool isWaveSpawn = false;
    [SerializeField] WaveSpawner waveSpawner;
    [SerializeField] GameControl gameControl;
    [SerializeField] GameObject textSpawnWave;
    [SerializeField] GameObject textNextLevel;
    GameObject sceneObject;
    SceneHandler sceneHandler;
    AudioSource audioSource;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("isPressed", false);
        audioSource = GetComponent<AudioSource>();
        sceneObject = GameObject.FindWithTag("SceneHandler");
        sceneHandler = sceneObject.GetComponent<SceneHandler>();

    }

    // Update is called once per frame
    void Update()
    {
        cekAnimation();
        cekText();
    }

    void cekText(){
        if(isTrigger){
            if(isWaveSpawn == false){
                textSpawnWave.SetActive(true);
            }
            else{
                textSpawnWave.SetActive(false);
            }
            if(gameControl.isFinishLevel()){
                textNextLevel.SetActive(true);
            }
            else{
                textNextLevel.SetActive(false);
            }
        }
        else{
            textSpawnWave.SetActive(false);
            textNextLevel.SetActive(false);
        }
    }
    void cekAnimation(){
        if(Input.GetKeyDown(KeyCode.F) && isTrigger){
            audioSource.Play();
            _animator.SetBool("isPressed", true);
            isWaveSpawn = true;
            waveSpawner.runWave();
            nextLevel();  
        }
        else{
            _animator.SetBool("isPressed", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            isTrigger = true;   
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            isTrigger = false;   
        }
    }

    void nextLevel(){
        if(gameControl.isFinishLevel() && !isNext){
            isNext = true;
            sceneHandler.loadNextLevel();
        }
    }
}
