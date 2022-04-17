using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Stores the items the player has picked up and can update UI to match.
/// </summary>
public class ItemCounter : MonoBehaviour
{

    public static int gateKeyCount;  // since there are only two collectibles and both are stackable,
    public static int potionCount;   // these counters will function as the player's collectibles.

    [SerializeField] TextMeshProUGUI onscreenKeyNumber;
    [SerializeField] TextMeshProUGUI onscreenPotionNumber;


    public static ItemCounter itemCounter; 
    void Awake() // sets up the class to make sure there's only one ItemCounter in the scene
    {
        if (itemCounter == null)
            itemCounter = this;
        else
            Destroy(this);
        

        onscreenKeyNumber = GameObject.Find("Gate Key Counter").GetComponent<TextMeshProUGUI>();
        onscreenPotionNumber = GameObject.Find("Potion Counter").GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }

    public void UpdateUI() // update the item number onscreen. should be called in PickUp and ItemUse.
    {
        onscreenKeyNumber.text = "" + gateKeyCount;
        onscreenPotionNumber.text = "" + potionCount;
        //Debug.Log("UI updated.");
    }

}