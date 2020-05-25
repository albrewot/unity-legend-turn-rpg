using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] public string newGameScene;
    [SerializeField] public GameObject continueButton;

    //Methods
    public void NewGame() {
        SceneManager.LoadScene(newGameScene);
    }

    public void Continue() {
        SceneManager.LoadScene("Loading");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void CheckSavedGame() {
        if (PlayerPrefs.HasKey("CURRENT_SCENE")) {
            continueButton.SetActive(true);
        } else {
            continueButton.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckSavedGame();
    }
}
