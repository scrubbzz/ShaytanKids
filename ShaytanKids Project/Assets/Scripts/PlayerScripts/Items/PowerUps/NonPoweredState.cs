using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this class represents the player in the neutral state (not using a potion).
// once they press the "use potion" button they will switch to the powered or depowered state, 
// depending on their current level of greed and trust.
public class NonPoweredState : PowerupState
{
    
    // decide which powerup type the player gets before switching them to the next state.
    public override void EnterState(PotionPowerupHandler manager)
    {
        Debug.Log("Returned to non powered mode.");
        int selectPowerUp = Random.Range(0, manager.numberOfStates);
        manager.powerUpType = (PotionPowerupHandler.PowerUpType)selectPowerUp; // need to cast the random number to the enum type here.
    } 

    public override void UpdateState(PotionPowerupHandler manager)
    {
        if (Input.GetKeyDown(KeyCode.Comma) && ItemCounter.potionCount > 0)
        {
            ItemCounter.potionCount--;
            ItemCounter.itemCounter.UpdateUI();


            // returns true or false depending on whether the likelihood of being a good potion turned out greater.
            if (IsGoodPotion(manager)) 
                manager.SwitchToState(manager.poweredUp);
            
            else
                manager.SwitchToState(manager.depowered);
        }
    }

    // randonly calculate whether the player should recieve a good potion or bad potion,
    // using the trust and greed meters as reference and comparing the result values.
    bool IsGoodPotion(PotionPowerupHandler manager) 
    {
        float goodPotionLikelihood = (Random.Range(0, manager.maxRandomness) * manager.meters.currentTrust);
        float badPotionLikelihood = (Random.Range(0, manager.maxRandomness) * manager.meters.currentGreed);

        if (goodPotionLikelihood >= badPotionLikelihood)
        {
            return true;
        }
        else return false;
    }
}
