using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFade : MonoBehaviour
{
    private Animator animator;
    private string level2load;
    private Scene scene;
    private OmnisceneScript dontDestroy;
    public string loadLvl;
    string pastLvl;
    public int num;
    private BoxCollider2D thisCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        dontDestroy = GameObject.FindObjectOfType<OmnisceneScript>();
        thisCollider = GetComponent<BoxCollider2D>();
        if (num == 0)
        {
            animator.SetTrigger("FadeIn");
        }
        //dontDestroy.NewScene();
    }

    void Update()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter Collider");
        dontDestroy.newLvlFade(this, num);
        pastLvl = SceneManager.GetActiveScene().name;
        dontDestroy.pastLvl = SceneManager.GetActiveScene().name;
        FadeToLevel(loadLvl);
    }

    public void FadeToLevel(string levelIndex)
    {
        Debug.Log("Begin Fade");
        level2load = levelIndex;
        if (animator != null)
        {
            animator.SetTrigger("FadeOut");
        } else
        {
            Debug.Log("Null");
        }
    }

    public void OnFadeComplete()
    {
        Debug.Log("Fade Complete");
        SceneManager.LoadScene(level2load);
    }

    public void fadeIn()
    {
        Debug.Log("Fade In");
        if (SceneManager.GetActiveScene().name == "MainTown")
        {
            dontDestroy.spawnCharacterInMainTown();
        }
        else if (SceneManager.GetActiveScene().name == "LandTown")
        {
            dontDestroy.spawnCharacterInLandTown();
        }
        else if (SceneManager.GetActiveScene().name == "SeaTown")
        {
            dontDestroy.spawnCharacterInSeaTown();
        }
        else if (SceneManager.GetActiveScene().name == "SkyTown")
        {
            dontDestroy.spawnCharacterInSkyTown();
        }
        else if (SceneManager.GetActiveScene().name == "FireTown")
        {
            dontDestroy.spawnCharacterInFireTown();
        } else {
            dontDestroy.spawnCharacter();
        }
    }

    public void combatFade()
    {
        FadeToLevel(dontDestroy.pastLvl);
    }
}
