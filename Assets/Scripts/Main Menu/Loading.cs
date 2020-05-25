using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] public float waitToLoad;
    //Methods
    public void Load() {
        if(waitToLoad > 0) {
            waitToLoad -= Time.fixedDeltaTime;
            if(waitToLoad <= 0) {
                LoadGame();
            }
        } else {
            LoadGame();
        }
    }

    public void LoadGame() {
        //SceneManager.LoadScene(PlayerPrefs.GetString("CURRENT_SCENE"));
        GameManager.instance.LoadData();
        QuestManager.instance.LoadQuestData();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Load");
        Load();
    }
}
