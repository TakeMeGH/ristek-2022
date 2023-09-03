using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    bool isPaused = false;
    [SerializeField] GameObject pauseObject;
    GameObject sceneObject;
    SceneHandler sceneHandler;
    void Start()
    {
        pauseObject.SetActive(false);
        sceneObject = GameObject.FindWithTag("SceneHandler");
        sceneHandler = sceneObject.GetComponent<SceneHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                resumeGame();
            }
            else{
                pauseGame();
            }
        }
    }
    public void pauseGame(){
        isPaused = true;
        pauseObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void resumeGame(){
        isPaused = false;
        pauseObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void mainMenu(){
        pauseObject.SetActive(false);
        Time.timeScale = 1f;
        sceneHandler.restartMainMenu();
    }

    public void QuitGame(){
        pauseObject.SetActive(false);
        Time.timeScale = 1f;
        sceneHandler.QuitGame();
    }

    
}
