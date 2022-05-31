using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public GameObject UI;
    public Sprite playerSkin;
    public AudioSource jumpSFX;
    private bool canJump = true;
    private Rigidbody2D rb;
    public float speed;
    public float jump;
    public int randJumpTime;
    public float randJumpThreshold;
    public float difficultyDeviation;
    public float jumpMin;
    public float jumpMax;
    private string difficulty;
    private int cooldown;
    private int cooldownFrames;
    private float tempRandJumpThreshold;
    private void Jump(float mult){
        rb.AddForce(new Vector2(0, jump * mult), ForceMode2D.Impulse);
        jumpSFX.Play();
    }
    private void ResetPos(){
        transform.position = new Vector2(-10, -3);
        rb.velocity = new Vector2(0, 0);
        rb.angularVelocity = 0;
        rb.rotation = 0;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
    private void KillPlayer(){
        ResetPos();
    }
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        difficulty = UI.GetComponent<UIManager>().difficulty;
}
    void FixedUpdate()
    {
        // Sets up necessary components
        this.gameObject.GetComponent<SpriteRenderer>().sprite = playerSkin;
        difficulty = UI.GetComponent<UIManager>().difficulty;
        Keyboard keyboard = Keyboard.current;
        bool wPressed = keyboard.wKey.isPressed;
        bool spacePressed = keyboard.spaceKey.isPressed;
        bool aPressed = keyboard.aKey.isPressed;
        bool dPressed = keyboard.dKey.isPressed;
        bool upPressed = keyboard.upArrowKey.isPressed;
        bool leftPressed = keyboard.leftArrowKey.isPressed;
        bool rightPressed = keyboard.rightArrowKey.isPressed;
        // Jumping
        cooldownFrames =  (int) Mathf.Round(Application.targetFrameRate * (randJumpTime/difficultyDeviation));
        if (cooldown > 0)
        {
            cooldown--;
        }
        if (cooldown == 0){
            cooldown = cooldownFrames;
            if(difficulty=="Impossible"){
                tempRandJumpThreshold = randJumpThreshold * difficultyDeviation;
            }else{
                tempRandJumpThreshold = randJumpThreshold;
            }
            if(canJump && UnityEngine.Random.Range(1f, 10f) < tempRandJumpThreshold){
                Jump(UnityEngine.Random.Range(jumpMin, jumpMax));
            }
        }
        // Movement
        if ((aPressed || leftPressed) && difficulty == "Hard")
        {
            rb.AddForce(new Vector2(-speed, 0f), ForceMode2D.Force);
        }if ((dPressed || rightPressed) && difficulty == "Hard")
        {
            rb.AddForce(new Vector2(speed, 0f), ForceMode2D.Force);
        }
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0));
        Vector2 worldPoint2d = new Vector2(worldPoint.x, worldPoint.y);
        if(Mouse.current.leftButton.isPressed && difficulty == "Impossible"){
            rb.AddForce(new Vector2(-(worldPoint.x - transform.position.x)*(speed/2), 0), ForceMode2D.Force);
        }
        // UI & boring stuff
        if(UI.GetComponent<UIManager>().showUI){
            ResetPos();
        }
        if(this.transform.position.y <= -30 || -275 > this.transform.position.x || this.transform.position.x > 275){
            KillPlayer();
        }
        
    }
    // Allows/disallows jumping
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            canJump = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            canJump = false;
        }
    }
}