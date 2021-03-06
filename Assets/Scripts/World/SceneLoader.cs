﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Attributes
    public static SceneLoader instance;
    [SerializeField] public Animator transition;
    [SerializeField] public float transitionTime = 1f;

    //Methods
    public void LoadNextScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadStartScene() {
        SceneManager.LoadScene(0);
    }

    public void LoadScene(string sceneToLoad) {
        StartCoroutine(LoadSceneCo(sceneToLoad));
    }

    public void LoadSceneWithIndex(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame() {
        Application.Quit();
    }

    //Coroutines
    IEnumerator LoadSceneCo(string sceneToLoad) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneToLoad);
    }

    // Start is called before the first frame update
    void Start()
    {
        //if (instance == null) {
        //    instance = this;
        //} else {
        //    Destroy(gameObject);
        //}
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
