using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenu : MonoBehaviour
{
    GameObject sceneObject;
    SceneHandler sceneHandler;

    public void nextLevel(){
        sceneHandler.loadNextLevel();
    }

    public void backMainMenu(){
        sceneHandler.restartMainMenu();
    }

    public void QuitGame(){
        sceneHandler.QuitGame();
    }

    public void restartFirstLevel(){
        sceneHandler.restartFirstLevel();
    }
    private void Start() {
        sceneObject = GameObject.FindWithTag("SceneHandler");
        sceneHandler = sceneObject.GetComponent<SceneHandler>();
    }

    
}
