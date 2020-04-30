using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{

    public string unitName;
    public int unitLevel;

    //public int enemyDamage;

    public int maxHP;
    public int currentHP;
    public int maxMana;
    public int currentMana;

    private void Start()
    {
        if (gameObject.name == "Player")
        {
            OmnisceneScript dontDestroy = GameObject.FindObjectOfType<OmnisceneScript>();
            unitName = "William Wizardly";
            unitLevel = dontDestroy.playerLvl;
            maxHP = dontDestroy.playerHealth;
            currentHP = dontDestroy.playerHealth;
            maxMana = dontDestroy.playerMana;
            currentMana = dontDestroy.playerMana;
        }
    }

}
