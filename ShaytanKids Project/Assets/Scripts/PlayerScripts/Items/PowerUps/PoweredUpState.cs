using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredUpState : PowerupState
{
    public override void EnterState(PotionPowerupHandler manager)
    {
        Debug.Log("Entered powered up mode.");
        manager.StartCoroutine(BoostPlayer(manager));
        //manager.contingencyTimer = 0;
    }

    public override void UpdateState(PotionPowerupHandler manager)
    {
        /*manager.contingencyTimer+= Time.fixedDeltaTime;
        if (manager.contingencyTimer > manager.powerUpDuration + 1)
        {
            Debug.Log("Something went wrong with the powerup. Refunding your potion.");

            ItemCounter.potionCount++;
            manager.itemDisplay.UpdateUI();

            manager.SwitchToState(manager.nonPowered);
        }*/
    }


    // sets one of the player's stats with a switch case according to what the powerUpType enum is set to. eg: enum = speedboost, increase speed
    // if there are powerups to be added or removed, the manager's enum variable list should be updated to match.
    IEnumerator BoostPlayer(PotionPowerupHandler manager)
    {
        float powerUpStrength = manager.powerUpStrength;
        WaitForSeconds buffDuration = new WaitForSeconds(manager.powerUpDuration);
        //float powerUpDuration = manager.powerUpDuration; // slightly easier to read


        switch (manager.powerUpType)
        {

            case PotionPowerupHandler.PowerUpType.speedBoost: // speed up

                manager.playerMovement.maxMoveSpeed = manager.playerMovement.maxMoveSpeed * powerUpStrength;
                yield return buffDuration;

                manager.playerMovement.maxMoveSpeed = manager.playerMovement.maxMoveSpeed / powerUpStrength;

                break;

            case PotionPowerupHandler.PowerUpType.jumpBoost: // jump height up

                manager.playerMovement.jumpHeight = manager.playerMovement.jumpHeight * powerUpStrength;
                yield return buffDuration;

                manager.playerMovement.jumpHeight = manager.playerMovement.jumpHeight / powerUpStrength;

                break;

            case PotionPowerupHandler.PowerUpType.meleeBoost: // attack up

                manager.playerAttack.attackDamage = manager.playerAttack.attackDamage * powerUpStrength;
                yield return buffDuration;

                manager.playerAttack.attackDamage = manager.playerAttack.attackDamage / powerUpStrength;

                break;

            case PotionPowerupHandler.PowerUpType.firingBoost: // faster arrows

                manager.playerShoot.projectileSpeed = manager.playerShoot.projectileSpeed * powerUpStrength;
                yield return buffDuration;

                manager.playerShoot.projectileSpeed = manager.playerShoot.projectileSpeed / powerUpStrength;

                break;

            default: // in case the enum is set to a value not accounted for.

                Debug.Log("Something went wrong with the powerup. Refunding your potion.");
                ItemCounter.potionCount++;
                ItemCounter.itemCounter.UpdateUI();
                break;
        }


        manager.SwitchToState(manager.nonPowered); // end function by switching back to non-powered.

        #region
        /*
        if (manager.powerUpType == PotionPowerupHandler.PowerUpType.speedBoost)
        {
            manager.playerMovement.maxMoveSpeed = manager.playerMovement.maxMoveSpeed * manager.powerUpStrength;
            yield return new WaitForSeconds(manager.powerUpDuration);

            manager.playerMovement.maxMoveSpeed = manager.playerMovement.maxMoveSpeed / manager.powerUpStrength;
        }

        else if (manager.powerUpType == PotionPowerupHandler.PowerUpType.jumpBoost)
        {
            manager.playerMovement.jumpHeight = manager.playerMovement.jumpHeight * manager.powerUpStrength;
            yield return new WaitForSeconds(manager.powerUpDuration);

            manager.playerMovement.jumpHeight = manager.playerMovement.jumpHeight / manager.powerUpStrength;
        }

        else if (manager.powerUpType == PotionPowerupHandler.PowerUpType.meleeBoost)
        {
            manager.playerAttack.attackDamage = manager.playerAttack.attackDamage * manager.powerUpStrength;
            yield return new WaitForSeconds(manager.powerUpDuration);

            manager.playerAttack.attackDamage = manager.playerAttack.attackDamage / manager.powerUpStrength;
        }

        else if (manager.powerUpType == PotionPowerupHandler.PowerUpType.firingBoost)
        {
            manager.playerShoot.projectileSpeed = manager.playerShoot.projectileSpeed * manager.powerUpStrength;
            yield return new WaitForSeconds(manager.powerUpDuration);

            manager.playerShoot.projectileSpeed = manager.playerShoot.projectileSpeed / manager.powerUpStrength;
        }
        yield return null;
        */
        #endregion
    }
}
