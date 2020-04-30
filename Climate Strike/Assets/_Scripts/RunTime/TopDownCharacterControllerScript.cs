using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TopDownCharacterControllerScript : MonoBehaviour
{
    private Rigidbody2D body;
    public Animator animator;
    public Transform playerTrans;
    float horizontal;
    float vertical;
    float sprint;
    float moveLimiter = 0.7f;
    public float initialRunSpeed = 8.0f;
    public float currentRunSpeed;
    public float sprintSpeed;
    private OmnisceneScript dontDestroy;
    public PolygonCollider2D leftCollider;
    public PolygonCollider2D rightCollider;
    private int inputNum = 0;
    AudioSource backgroundMusic;
    public AudioClip outerworldMusic;
    public AudioClip combatMusic;

    void Start()
    {
        playerTrans = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
        dontDestroy = GameObject.FindObjectOfType<OmnisceneScript>();
        backgroundMusic = GetComponent<AudioSource>();
        leftCollider.enabled = false;
        rightCollider.enabled = true;

        currentRunSpeed = initialRunSpeed;
        sprintSpeed = (initialRunSpeed) + (initialRunSpeed / 2);

        if (dontDestroy.lvl == lvlType.COMBAT)
        {
            backgroundMusic.clip = combatMusic;
        }
        else
        {
            if (dontDestroy.lvl != lvlType.MAINMENU)
            {
                backgroundMusic.clip = outerworldMusic;
            }
            else
            {
                backgroundMusic.clip = null;
            }
        }
    }

    void Update()
    {
        inputNum = 0;
        currentRunSpeed = initialRunSpeed;
        sprintSpeed = (currentRunSpeed) + (currentRunSpeed / 2);
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
        animator.SetBool("Up", false);

        // Gives a value between -1 and 1
        if (dontDestroy.movement)
        {
            horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
            vertical = Input.GetAxisRaw("Vertical"); // -1 is down
            sprint = Input.GetAxisRaw("Sprint");
        }

        if (dontDestroy.pause)
        {
            backgroundMusic.Pause();
        }
        else
        {
            if (!backgroundMusic.isPlaying)
            {
                backgroundMusic.Play();
            }
        }

        if (dontDestroy.movement)
        {
            if ((sprint <= 1) && (sprint > 0))
            {
                currentRunSpeed = sprintSpeed;
            }
            if ((horizontal >= -1) && (horizontal < 0))
            {
                inputNum++;
                leftCollMethod();
                animator.SetBool("Left", true);
            }
            if ((horizontal <= 1) && (horizontal > 0))
            {
                inputNum++;
                rightCollMethod();
                animator.SetBool("Right", true);
            }
            if ((vertical <= 1) && (vertical > 0))
            {
                inputNum++;
                leftCollMethod();
                animator.SetBool("Up", true);
            }
            if (inputNum == 0)
            {
                rightCollMethod();
            }
        }
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        body.velocity = new Vector2(horizontal * currentRunSpeed, vertical * currentRunSpeed);
    }

    private void leftCollMethod()
    {
        leftCollider.enabled = true;
        rightCollider.enabled = false;
    }

    private void rightCollMethod()
    {
        leftCollider.enabled = false;
        rightCollider.enabled = true;
    }

    private void noCollMethod()
    {
        leftCollider.enabled = false;
        rightCollider.enabled = false;
    }
}
