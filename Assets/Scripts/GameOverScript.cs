using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverScript : MonoBehaviour
{

    GameObject sceneObject;
    SceneHandler sceneHandler;
    public void restartButton(){
        sceneHandler.restartButton();
    }

    private void Start() {
        sceneObject = GameObject.FindWithTag("SceneHandler");
        sceneHandler = sceneObject.GetComponent<SceneHandler>();
    }
}
