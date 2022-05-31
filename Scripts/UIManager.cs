using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject UIContainer;
    private GameObject difficultyButton;
    private GameObject fpsSlider;
    private GameObject currentFPStext;
    private int cooldown;
    private int cooldownFrames;
    [HideInInspector]
    public bool showUI = false;
    [HideInInspector]
    public List<string> difficulties = new List<string>()
{
    "Hard",
    "Impossible"
};
    [HideInInspector]
    public string difficulty;
    private void Start() {
        difficulty = difficulties[0];
        difficultyButton = GetComponentInChildren<UnityEngine.UI.Button>().gameObject;
        difficultyButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(changeDifficulty);
        fpsSlider =  GetComponentInChildren<UnityEngine.UI.Slider>().gameObject;
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren){
        GameObject cgo = child.gameObject;
            if(cgo.name == "Current FPS"){
            currentFPStext = cgo.GetComponent<TMP_Text>().gameObject;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        cooldownFrames = Application.targetFrameRate/8;
        if (cooldown > 0 && !Keyboard.current.escapeKey.isPressed)
        {
            cooldown--;
        }
        if (Keyboard.current.escapeKey.isPressed && cooldown == 0){
            showUI = !showUI;
            cooldown = cooldownFrames;
        }
       UIContainer.SetActive(showUI);
       difficultyButton.GetComponentInChildren<TMP_Text>().text = difficulty;
       Application.targetFrameRate = (int)fpsSlider.GetComponent<UnityEngine.UI.Slider>().value;
       fpsSlider.GetComponentInChildren<TMP_Text>().text = "Target FPS: "+Application.targetFrameRate;
       currentFPStext.GetComponentInChildren<TMP_Text>().text = "Current FPS: "+(Time.frameCount / Time.time).ToString();
    }
    void changeDifficulty(){
        int nextDifficultyIndex = difficulties.IndexOf(difficulty)+1;
        if (nextDifficultyIndex >= difficulties.Count){
            string nextDifficulty = difficulties[0];
            difficulty = nextDifficulty;
        }else{
            string nextDifficulty = difficulties[nextDifficultyIndex];
            difficulty = nextDifficulty;
        }
    }
}