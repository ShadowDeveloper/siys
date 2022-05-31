using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.UnloadSceneAsync("Game");
        SceneManager.LoadSceneAsync("Start Menu");
    }

    // Update is called once per frame
    public void StartupGame(){
        transform.localScale = new Vector2(transform.localScale.x * 0.75f, transform.localScale.y * 0.75f);
        SceneManager.UnloadSceneAsync("Start Menu");
        SceneManager.LoadSceneAsync("Game");
        
    }
}
