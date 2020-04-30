using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
//[Serializable]
//[]
public class Spell : ScriptableObject
{
    // Start is called before the first frame update

    public string type;
    public new string name;
    public string description;
    public string effect;
    public int effectNum;

    public Sprite artwork;
    public RuntimeAnimatorController animation;

    public int manaCost;
    public int baseDamage;

    public void print()
    {
        Debug.Log(name + ": " + description + ". Element: " + type +", Base Damage: " + baseDamage + ", Mana Cost: " + manaCost + ", Effect Type: " + effect + "(" + effectNum + ").");
    }

    public void OnEnable()
    {
        GameObject playerSpell = GameObject.Find("Player Spell");

        SpriteRenderer sprite = playerSpell.GetComponent<SpriteRenderer>();
        sprite.sprite = artwork;
        Animator anim = playerSpell.GetComponent<Animator>();
        anim.runtimeAnimatorController = animation;
        anim.Play("Cast", 0, 0);
        while (playerSpell.GetComponent<Transform>().position.x <= 6)
        {
            playerSpell.GetComponent<Transform>().position = new Vector3(playerSpell.GetComponent<Transform>().position.x + 0.1f, 0f, 0f);
        }
    }


}
