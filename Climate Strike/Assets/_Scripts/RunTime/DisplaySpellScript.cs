using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplaySpellScript : MonoBehaviour
{

    public Spell spell;
    OmnisceneScript dontDestroy;
    public int index;
    List<Spell> spells;
    string mySpellName;
    GameObject playerSpell;



    // Start is called before the first frame update
    void Start()
    {
        dontDestroy = GameObject.FindObjectOfType<OmnisceneScript>();
        playerSpell = GameObject.Find("PlayerSpell");
        spells = dontDestroy.spells;
        if (spells[index] != null)
        {
            mySpellName = spells[index].ToString();
        } 
    }

    public void castSpell() //set sprite, set animation, do damage, consume mana, change turn
    {

    }

}
