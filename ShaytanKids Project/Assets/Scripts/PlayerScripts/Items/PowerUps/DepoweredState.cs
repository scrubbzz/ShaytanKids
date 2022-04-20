using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepoweredState : PowerupState
{
    public override void EnterState(PotionPowerupHandler manager)
    {
        Debug.Log("Entered depowered mode.");
        manager.StartCoroutine(DebuffPlayer(manager));
    }

    public override void UpdateState(PotionPowerupHandler manager)
    {

    }

    IEnumerator DebuffPlayer(PotionPowerupHandler manager)
    {
        float powerUpStrength = manager.powerUpStrength;
        WaitForSeconds debuffDuration = new WaitForSeconds(manager.powerUpDuration);
        //float powerUpDuration = manager.powerUpDuration; // slightly easier to read


        switch (manager.powerUpType)
        {

            case PotionPowerupHandler.PowerUpType.speedBoost: // speed down

                manager.playerMovement.maxMoveSpeed = manager.playerMovement.maxMoveSpeed / powerUpStrength;
                yield return debuffDuration;

                manager.playerMovement.maxMoveSpeed = manager.playerMovement.maxMoveSpeed * powerUpStrength;

                break;

            case PotionPowerupHandler.PowerUpType.jumpBoost: // jump height down

                manager.playerMovement.jumpHeight = manager.playerMovement.jumpHeight / powerUpStrength;
                yield return debuffDuration;

                manager.playerMovement.jumpHeight = manager.playerMovement.jumpHeight * powerUpStrength;

                break;

            case PotionPowerupHandler.PowerUpType.meleeBoost: // attack down

                manager.playerAttack.attackDamage = manager.playerAttack.attackDamage / powerUpStrength;
                yield return debuffDuration;

                manager.playerAttack.attackDamage = manager.playerAttack.attackDamage * powerUpStrength;

                break;

            case PotionPowerupHandler.PowerUpType.firingBoost: // slower arrows

                manager.playerShoot.projectileSpeed = manager.playerShoot.projectileSpeed / powerUpStrength;
                yield return debuffDuration;

                manager.playerShoot.projectileSpeed = manager.playerShoot.projectileSpeed * powerUpStrength;

                break;

            default: // in case the enum is set to a value not accounted for.

                Debug.Log("Something went wrong with the powerup. Refunding your potion.");
                ItemCounter.potionCount++;
                manager.itemDisplay.UpdateUI();
                break;
        }


        manager.SwitchToState(manager.nonPowered); // end function with switching back to non-powered.

    }
}