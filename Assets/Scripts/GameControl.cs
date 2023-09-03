using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameControl : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject infantry;

    [SerializeField] WaveSpawner waveSpawner;
    GameObject sceneObject;
    SceneHandler sceneHandler;

    int differentSpwanerCount;

    public void GameOver(){

        sceneHandler.loadLoseScene();
    }
    void Start()
    {
        differentSpwanerCount = waveSpawner.enemies.Count;
        sceneObject = GameObject.FindWithTag("SceneHandler");
        sceneHandler = sceneObject.GetComponent<SceneHandler>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // nextLevel();
    }

    public bool isFinishLevel(){
        if(infantry.transform.childCount == differentSpwanerCount && waveSpawner.isFinishRunning()){
            return true;
        }
        return false;

    }

    // void nextLevel(){
    //     // sisa dummy object
    //     if(infantry.transform.childCount == differentSpwanerCount && waveSpawner.isFinishRunning() && !isNext){
    //         isNext = true;
    //         Invoke("nextLevelExe", 0.5f);
    //     }
    // }

    // public void nextLevelExe(){

    //     sceneHandler.loadNextLevel();
    // }
}
