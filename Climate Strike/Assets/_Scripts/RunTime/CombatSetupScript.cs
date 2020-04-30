using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { PLAYERTURN, ENEMYTURN, WON, LOST }


public class CombatSetupScript : MonoBehaviour
{
    private GameObject playerHUD;
    private GameObject enemyHUD;
    private GameObject magicHUD;
    private GameObject mainHUD;
    private GameObject itemHUD;
    private GameObject combatTextHUD;
    private GameObject go1;
    private GameObject go2;
    private GameObject go3;
    private GameObject go4;
    private GameObject backButton;
    private GameObject spell1;
    private GameObject spell2;
    private GameObject spell3;
    private GameObject spell4;
    private GameObject spell5;
    private GameObject spell6;
    private GameObject item1;
    private GameObject item2;
    private GameObject item3;
    private GameObject item4;
    private GameObject item5;
    private GameObject item6;
    private Slider playerHPSlider;
    private Slider playerManaSlider;
    public string playerName;
    public string playerLevel;
    private Slider enemyHPSlider;
    private Slider enemyManaSlider;
    public string enemyName;
    public string enemyLevel;
    public string combatText;
    private OmnisceneScript dontDestroy;
    private LevelFade lvlFade0;
    private GameObject lvlFadeZero;
    public int playerCombatHealth;
    public int playerCombatMana;
    public int enemyCombatHealth;
    public int enemyCombatMana;
    private int enemyMaxHealth;
    private int enemyMaxMana;
    public TextMeshProUGUI Player_Name;
    public TextMeshProUGUI Player_HP_Text;
    public TextMeshProUGUI Player_Mana_Text;
    public TextMeshProUGUI Player_Level_Text;
    public TextMeshProUGUI Enemy_Name;
    public TextMeshProUGUI Enemy_HP_Text;
    public TextMeshProUGUI Enemy_Mana_Text;
    public TextMeshProUGUI Enemy_Level_Text;
    public TextMeshProUGUI combatText_Text;
    public TextMeshProUGUI spell1_Text;
    public TextMeshProUGUI spell2_Text;
    public TextMeshProUGUI spell3_Text;
    public TextMeshProUGUI spell4_Text;
    public TextMeshProUGUI spell5_Text;
    public TextMeshProUGUI spell6_Text;
    public TextMeshProUGUI item1_Text;
    public TextMeshProUGUI item2_Text;
    public TextMeshProUGUI item3_Text;
    public TextMeshProUGUI item4_Text;
    public TextMeshProUGUI item5_Text;
    public TextMeshProUGUI item6_Text;
    private int playerMaxHealth;
    private int playerMaxMana;
    public List<Spell> spells;
    public string[] items;
    public BattleState state;
    private string lastCombatText = " ";
    public GameObject enemyObject;
    public GameObject playerObject;
    private UnitScript enemyScript;
    private UnitScript playerScript;


    // Start is called before the first frame update
    void Start()
    {
        //spells = new string[6];
        items = new string[6];
        dontDestroy = GameObject.FindObjectOfType<OmnisceneScript>();
        enemyObject = GameObject.Find("Enemy");
        playerObject = GameObject.Find("Player");
        enemyScript = enemyObject.GetComponent<UnitScript>();
        playerScript = playerObject.GetComponent<UnitScript>();
        lvlFadeZero = GameObject.Find("LevelFade0");
        lvlFade0 = lvlFadeZero.GetComponent<LevelFade>();
        playerHUD = GameObject.Find("PlayerHUD");
        enemyHUD = GameObject.Find("EnemyHUD");
        go1 = GameObject.Find("Player_HP_Slider");
        go2 = GameObject.Find("Player_Mana_Slider");
        go3 = GameObject.Find("Enemy_HP_Slider");
        go4 = GameObject.Find("Enemy_Mana_Slider");
        magicHUD = GameObject.Find("MagicHUD");
        mainHUD = GameObject.Find("MainHUD");
        itemHUD = GameObject.Find("ItemsHUD");
        combatTextHUD = GameObject.Find("Combat_Text");
        backButton = GameObject.Find("BackButton");
        spell1 = GameObject.Find("Spell 1");
        spell2 = GameObject.Find("Spell 2");
        spell3 = GameObject.Find("Spell 3");
        spell4 = GameObject.Find("Spell 4");
        spell5 = GameObject.Find("Spell 5");
        spell6 = GameObject.Find("Spell 6");
        item1 = GameObject.Find("Item 1");
        item2 = GameObject.Find("Item 2");
        item3 = GameObject.Find("Item 3");
        item4 = GameObject.Find("Item 4");
        item5 = GameObject.Find("Item 5");
        item6 = GameObject.Find("Item 6");
        playerHPSlider = go1.GetComponent<Slider>();
        playerManaSlider = go2.GetComponent<Slider>();
        enemyHPSlider = go3.GetComponent<Slider>();
        enemyManaSlider = go4.GetComponent<Slider>();
        playerMaxHealth = (dontDestroy.playerLvl * 100);
        playerMaxMana = (dontDestroy.playerLvl * 100);
        playerName = "William Wizardly";
        playerLevel = "Level " + dontDestroy.playerLvl;
        enemyName = dontDestroy.enemyName;
        enemyLevel = "Level " + dontDestroy.enemyLvl;
        playerCombatHealth = dontDestroy.playerLvl * 100;
        playerCombatMana = dontDestroy.playerLvl * 100;
        enemyCombatHealth = dontDestroy.enemyLvl * 100 + (int)Random.Range(-50, 50);
        enemyCombatMana = dontDestroy.enemyLvl * 100 + (int)Random.Range(-50, 50);
        enemyMaxHealth = enemyCombatHealth;
        enemyMaxMana = enemyCombatMana;
        Player_Name.text = playerName;
        Player_HP_Text.text = playerCombatHealth + "/" + playerMaxHealth;
        Player_Mana_Text.text = playerCombatMana + "/" + playerMaxMana;
        Enemy_Name.text = enemyName;
        Enemy_HP_Text.text = enemyCombatHealth + "/" + enemyMaxHealth;
        Enemy_Mana_Text.text = enemyCombatMana + "/" + enemyMaxMana;
        Player_Level_Text.text = playerLevel;
        Enemy_Level_Text.text = enemyLevel;
        combatText = "A Battle Begins... a " + enemyName + " approaches.";
        playerHPSlider.maxValue = playerCombatHealth;
        playerManaSlider.maxValue = playerCombatMana;
        enemyHPSlider.maxValue = enemyCombatHealth;
        enemyManaSlider.maxValue = enemyCombatMana;

        mainHUD.SetActive(true);
        magicHUD.SetActive(true);
        itemHUD.SetActive(true);
        combatTextHUD.SetActive(true);
        backButton.SetActive(true);
        dontDestroy.flee = false;

        //for (int i = 0; i < spells.Length; i++)
        //{
        //    spells[i] = dontDestroy.spells[i];
        //    if (spells[i] == null)
        //    {
        //        spells[i] = "Empty Slot";
        //    }
        //}

        for (int i = 0; i < items.Length; i++)
        {
            items[i] = dontDestroy.items[i];
            if (items[i] == null)
            {
                items[i] = "Empty Slot";
            }
        }

        spell1_Text.text = spells[0].ToString();
        spell2_Text.text = spells[1].ToString();
        spell3_Text.text = spells[2].ToString();
        spell4_Text.text = spells[3].ToString();
        spell5_Text.text = spells[4].ToString();
        spell6_Text.text = spells[5].ToString();
        item1_Text.text = items[0];
        item2_Text.text = items[1];
        item3_Text.text = items[2];
        item4_Text.text = items[3];
        item5_Text.text = items[4];
        item6_Text.text = items[5];
        if (spells[0] == null)
        {
            spell1.SetActive(false);
        }
        if (spells[1] == null)
        {
            spell2.SetActive(false);
        }
        if (spells[2] == null)
        {
            spell3.SetActive(false);
        }
        if (spells[3] == null)
        {
            spell4.SetActive(false);
        }
        if (spells[4] == null)
        {
            spell5.SetActive(false);
        }
        if (spells[5] == null)
        {
            spell6.SetActive(false);
        }
        if (items[0] == "Empty Slot")
        {
            item1.SetActive(false);
        }
        if (items[1] == "Empty Slot")
        {
            item2.SetActive(false);
        }
        if (items[2] == "Empty Slot")
        {
            item3.SetActive(false);
        }
        if (items[3] == "Empty Slot")
        {
            item4.SetActive(false);
        }
        if (items[4] == "Empty Slot")
        {
            item5.SetActive(false);
        }
        if (items[5] == "Empty Slot")
        {
            item6.SetActive(false);
        }

        mainHUD.SetActive(true);
        magicHUD.SetActive(false);
        itemHUD.SetActive(false);
        combatTextHUD.SetActive(true);
        backButton.SetActive(false);

        enemyScript.unitName = enemyName;
        enemyScript.unitLevel = dontDestroy.enemyLvl;
        enemyScript.maxHP = enemyCombatHealth;
        enemyScript.currentHP = enemyCombatHealth;

        playerScript.unitName = playerName;
        playerScript.unitLevel = dontDestroy.playerLvl;
        playerScript.maxHP = playerCombatHealth;
        playerScript.currentHP = playerCombatHealth;

        StartCoroutine(combatSetup(2f));
    }

    IEnumerator combatSetup(float time)
    {
        yield return new WaitForSeconds(time);
        /*if (dontDestroy.playerSpeed < enemySpeed)
        {
            state = BattleState.ENEMYTURN;
            enemyTurn();
        } else
        {*/
            state = BattleState.PLAYERTURN;
            playerTurn();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        playerHPSlider.value = playerCombatHealth;
        playerManaSlider.value = playerCombatMana;
        enemyHPSlider.value = enemyCombatHealth;
        enemyManaSlider.value = enemyCombatMana;
        combatText_Text.text = combatText;
        Enemy_HP_Text.text = enemyCombatHealth + "/" + enemyMaxHealth;
        Enemy_Mana_Text.text = enemyCombatMana + "/" + enemyMaxMana;
        Player_HP_Text.text = playerCombatHealth + "/" + playerMaxHealth;
        Player_Mana_Text.text = playerCombatMana + "/" + playerMaxMana;
    }

    public void magicMenu()
    {
        lastCombatText = combatText;
        mainHUD.SetActive(false);
        magicHUD.SetActive(true);
        itemHUD.SetActive(false);
        combatTextHUD.SetActive(false);
        backButton.SetActive(true);
    }

    public void itemMenu()
    {
        lastCombatText = combatText;
        mainHUD.SetActive(false);
        magicHUD.SetActive(false);
        itemHUD.SetActive(true);
        combatTextHUD.SetActive(false);
        backButton.SetActive(true);
    }

    public void mainMenu()
    {
        combatText = lastCombatText;
        mainHUD.SetActive(true);
        magicHUD.SetActive(false);
        itemHUD.SetActive(false);
        combatTextHUD.SetActive(true);
        backButton.SetActive(false);
        //combatText = "";
    }

    public void fleeMenu()
    {
        lastCombatText = combatText;
        mainHUD.SetActive(false);
        magicHUD.SetActive(false);
        itemHUD.SetActive(false);
        combatTextHUD.SetActive(true);
        backButton.SetActive(true);
        int roll = (int)Random.Range(0, 2);
        string YoN = "";
        if (roll == 1)
        {
            YoN = "not "; //no
        }
        combatText = "You attempt to flee...\nYou are " + YoN + "able to escape!";
        if (roll == 1) //no
        {

        }
        else
        { //yes
            dontDestroy.flee = true;
            backButton.SetActive(false);
            StartCoroutine(leaveCombat(2f));
        }
    }

    IEnumerator leaveCombat(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        lvlFade0.combatFade();
    }

    void playerTurn ()
    {
        combatText = "It is your turn. Choose an action.";
    }

    IEnumerator playerMagic(int index)
    {
        yield return new WaitForSeconds(1);

        
    }

    public void onSpellOne()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(playerMagic(0));
    }

    public void onSpellTwo()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(playerMagic(1));
    }

    public void onSpellThree()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(playerMagic(2));
    }

    public void onSpellFour()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(playerMagic(3));
    }

    public void onSpellFive()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(playerMagic(4));
    }

    public void onSpellSix()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(playerMagic(5));
    }

    IEnumerator playerItem(int index)
    {
        yield return new WaitForSeconds(1);


    }

    public void onItemOne()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(playerItem(0));
    }

    public void onItemTwo()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(playerItem(1));
    }

    public void onItemThree()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(playerItem(2));
    }

    public void onItemFour()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(playerItem(3));
    }

    public void onItemFive()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(playerItem(4));
    }

    public void onItemSix()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(playerItem(5));
    }

}
