using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using TMPro;
//using UnityEngine.UI;

public enum lvlType
{
    MAINMENU, TOWN, COMBAT, HOUSE
}

public class OmnisceneScript : MonoBehaviour
{
    private LevelFade lvlFade;
    public GameObject lvlFadeZero;
    private LevelFade lvlFade0;
    public string pastLvl;
    private static OmnisceneScript duplicateFix;
    public bool pause = false;
    public GameObject pauseMenuUI;
    private float escButton;
    public int playerLvl;
    public int playerExp;
    public int playerHealth;
    public int playerMana;
    public int storyCount;
    public string[] quests;
    public List<Spell> spells = new List<Spell>();
    public string[] items;
    public int[] itemQuant;
    public Vector3 playerPos = new Vector3(0, 0, 0);
    private Scene tempScene;
    public string scene;
    public TopDownCharacterControllerScript playerScript;
    public string enemyName;
    public int enemyLvl;
    public lvlType lvl;
    public string[] houses;
    private string currentScene;
    public bool movement;
    public Vector3[] playerTownSpawn;// = new Vector3[5];      //Location to spawn into the town
    public Vector3[] playerSpawn;// = new Vector3[5];          //Location to spawn in scene corresponding to Scene Index
    public bool newGameBool;
    public Vector3 pastLocation;
    public int lvlFadeIndex;
    public bool flee = false;
    private bool combatTimer = true;
    private Vector3 currentPlayerPos = new Vector3(0f, 0f, 0f);
    public Spell[] spellsArray; // = new Spell[3];
    public GameObject interactionKeyPopup;

    //Player Spawn and Transitions

    void Awake()
    {
        //Placeholders for in-game progress
        newGameBool = false;
        enemyName = "Land Mage";
        enemyLvl = 3;
        playerLvl = 1;
        quests = new string[1];
        items = new string[6];                                //Max 6 items
        itemQuant = new int[6];                               //Items have 1 quant by default. You can have multiple potions, but only 1 equitment
        //spells.Add(Spell.);
        //spellsArray = Resources.FindObjectsOfTypeAll(typeof(Spell));
        //spells
        //spells = new string[6];
        //spells[0] = "Wind Scythe";
        items[0] = "Health Potion";
        items[1] = "Mana Potion";
        itemQuant[0] = 1;
        itemQuant[1] = 1;

        //Where the player spawns into different Scenes
        playerSpawn = new Vector3[10];
        playerSpawn[0] = new Vector3(0f, 0f, 0f);             //MainMenu (will not be used)
        playerSpawn[1] = new Vector3(0f, 0f, 0f);             //MainTown (will not be used)
        playerSpawn[2] = new Vector3(0f, -9f, 0f);            //PurpleHouse1
        playerSpawn[3] = new Vector3(6f, 0.5f, 0f);           //Combat
        playerSpawn[4] = new Vector3(0f, -9f, 0f);           //PurpleShop1

        //Where the player spawns into the town from different Scenes
        playerTownSpawn = new Vector3[10];
        playerTownSpawn[0] = new Vector3(0f, 0f, 0f);      //initial player spawn in position (on new game)
        playerTownSpawn[1] = new Vector3(0f, 0f, 0f);         //this can not happen
        playerTownSpawn[2] = new Vector3(-16.5f, 0f, 0f);     //from PurpleHouse1 to MainTown
        playerTownSpawn[3] = new Vector3(0f, 0f, 0f);         //from combat
        playerTownSpawn[4] = new Vector3(16.5f, 0f, 0f);           //from PurpleShop1 to MainTown

        //Array of Spell scriptable objects


        if (duplicateFix == null)
        {
            duplicateFix = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        //lvlFade = GameObject.FindObjectOfType<LevelFade>();
        lvlFadeZero = GameObject.Find("LevelFade0");
        lvlFade0 = lvlFadeZero.GetComponent<LevelFade>();
        playerScript = GameObject.FindObjectOfType<TopDownCharacterControllerScript>();

        pauseMenuUI.SetActive(false);
        //interactionKeyPopup.GetComponentInChildren<TextMeshProUGUI>().color = new Color();
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        movement = true;
        currentScene = SceneManager.GetActiveScene().name;
        if (playerScript != null)
        {
            currentPlayerPos = playerScript.playerTrans.transform.position;
        }



        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if ((!pause) && (lvl != lvlType.MAINMENU))
            {
                pauseGame();
            }
        }
        if (currentScene == "MainMenu")
        {
            lvl = lvlType.MAINMENU;
            movement = false;
        }
        if (currentScene == "Combat")
        {
            lvl = lvlType.COMBAT;
            movement = false;
        }
        if (currentScene.IndexOf("Town") > -1 || currentScene.IndexOf("town") > -1)
        {
            lvl = lvlType.TOWN;
            movement = true;
        }
        if (currentScene.IndexOf("House") > -1 || currentScene.IndexOf("house") > -1)
        {
            lvl = lvlType.HOUSE;
            movement = true;
        }

        if ((lvl == lvlType.TOWN) && combatTimer)
        {
            combatStart();
        }
    }

    // Pause Menu

    public void pauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        pause = true;
    }

    public void resumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        pause = false;
    }

    // Save/Load System

    public void newGame()
    {
        playerLvl = 1;
        playerHealth = 100;
        playerMana = 100;
        playerExp = 0;
        storyCount = 0;
        newGameBool = true;
        quests = new string[1];
        items = new string[6];
        itemQuant = new int[6];
        //spells = new string[6];
        //spells[0] = "Wind Scythe";
        items[0] = "Health Potion";
        items[1] = "Mana Potion";
        itemQuant[0] = 1;
        itemQuant[1] = 1;
        lvlFade0.FadeToLevel("MainTown");
    }

    public void saveGame()
    {
        playerPos = playerScript.playerTrans.transform.position;
        tempScene = SceneManager.GetActiveScene();
        scene = tempScene.name;
        SaveScript.savePlayer(this);
    }

    public void loadGame()
    {
        PlayerDataScript data = SaveScript.loadPlayer();
        playerLvl = data.level;
        playerHealth = data.health;
        playerMana = data.mana;
        playerExp = data.experience;
        storyCount = data.storyCount;
        quests = data.quests;
        spells = data.spells;
        items = data.items;
        itemQuant = data.itemQuant;
        scene = data.scene;
        newGameBool = data.newGame;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        playerScript.transform.position = position;

        lvlFade0.FadeToLevel(scene);
    }

    public void newLvlFade(LevelFade lvl, int lvlIndex)
    {
        lvlFade = lvl;
        lvlFadeIndex = lvlIndex;
    }

    public void combatStart()
    {
        combatTimer = false;
        //StartCoroutine(startToLoadCombat(Random.Range(0f,10f)));//100f,200f)));
        //combatTimer = true;
    }

    IEnumerator startToLoadCombat(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        combatTime();
    }

    public void combatTime()
    {
        playerPos = currentPlayerPos;
        pastLvl = currentScene;
        playerTownSpawn[3] = playerPos;
        lvlFade0.FadeToLevel("Combat");
        spawnCharacter();
    }

    public void spawnCharacter()
    {
        lvlFadeZero = GameObject.Find("LevelFade0");
        lvlFade0 = lvlFadeZero.GetComponent<LevelFade>();
        playerScript = GameObject.FindObjectOfType<TopDownCharacterControllerScript>();
        playerScript.transform.position = playerSpawn[lvlFadeIndex];
        saveGame();
    }

    public void spawnCharacterInMainTown()
    {
        lvlFadeZero = GameObject.Find("LevelFade0");
        lvlFade0 = lvlFadeZero.GetComponent<LevelFade>();
        playerScript = GameObject.FindObjectOfType<TopDownCharacterControllerScript>();
        if (newGameBool)
        {
            playerScript.transform.position = playerTownSpawn[0];
        } else {
            playerScript.transform.position = playerTownSpawn[lvlFadeIndex];
        }
        saveGame();
    }

    public void spawnCharacterInSeaTown()
    {
        
    }
    public void spawnCharacterInSkyTown()
    {

    }
    public void spawnCharacterInLandTown()
    {

    }
    public void spawnCharacterInFireTown()
    {

    }

    public void addSpell (Spell spell)
    {
        spells.Add(spell);
    }

    public void removeSpell(Spell spell)
    {
        spells.Remove(spell);
    }
}