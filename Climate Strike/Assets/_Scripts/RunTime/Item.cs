using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    string itemName = "";
    string type = "";
    float multiplier = 0f;

    public Item(string tempItemName, string tempType, float tempMultiplier)
    {
        itemName = tempItemName;
        type = tempType;
        multiplier = tempMultiplier;
    }
    
    public string getItemName()
    {
        return itemName;
    }

    public string getType()
    {
        return type;
    }

    public float getMultiplier()
    {
        return multiplier;
    }

}
