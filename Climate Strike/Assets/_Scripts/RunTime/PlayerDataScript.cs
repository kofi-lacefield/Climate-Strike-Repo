using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDataScript
{
    public int level;
    public int health;
    public int experience;
    public int mana;
    public int storyCount;
    public string[] quests;
    public List<Spell> spells;
    public string[] items;
    public int[] itemQuant;
    public float[] position;
    public string scene;
    public bool newGame;

    public PlayerDataScript(OmnisceneScript player)
    {
        level = player.playerLvl;
        health = player.playerHealth;
        mana = player.playerMana;
        experience = player.playerExp;
        storyCount = player.storyCount;
        quests = player.quests;
        spells = player.spells;
        items = player.items;
        itemQuant = player.itemQuant;
        scene = player.scene;
        position = new float[3];
        position[0] = player.playerPos.x;
        position[1] = player.playerPos.y;
        position[2] = player.playerPos.z;
        newGame = player.newGameBool;
    }
}
