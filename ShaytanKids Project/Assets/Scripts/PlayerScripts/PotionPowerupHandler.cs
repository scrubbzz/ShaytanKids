using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A state machine which manages the player's power-up state and use of potions.
/// Note: this class needs all references existing in the scene to function properly.
/// </summary>
public class PotionPowerupHandler : MonoBehaviour
{
    public float powerUpDuration = 10;     // how long should the buff/debuff be active?
    public float powerUpStrength = 1.5f;   // how much should we increase or decrease the player's stats? (in multiples)

    //public ItemCounter itemDisplay;

    GameObject player;                     // references to the player's stats
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;
    public PlayerShoot playerShoot;

    public enum PowerUpType                // each possible powerup type (corresponds to stat buffs / debuffs).
    {
        speedBoost = 0,
        jumpBoost = 1,
        meleeBoost = 2,
        firingBoost = 3
    }
    public PowerUpType powerUpType;
    public int numberOfStates = 4;

    public MeterManager meters;
    public int maxRandomness;             // this represents the amount of randomness involved in
                                          // deciding the state switching to poweredUp or depowered.


    public float contingencyTimer;

    PowerupState powerupState;           

    public NonPoweredState nonPowered = new NonPoweredState();
    public PoweredUpState poweredUp = new PoweredUpState();
    public DepoweredState depowered = new DepoweredState();


    // ensure we have our powerup state and all references set.
    void Start()
    {
        powerupState = nonPowered;
        powerupState.EnterState(this);
        
        //itemDisplay = GetComponent<ItemCounter>();

        player = GameObject.FindWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        playerAttack = player.GetComponent<PlayerAttack>();
        playerShoot = player.GetComponent<PlayerShoot>();

        meters = player.GetComponent<MeterManager>(); 
    }

    void Update()
    {
        powerupState.UpdateState(this);
    }

    public void SwitchToState(PowerupState nextState)
    {
        powerupState = nextState;
        powerupState.EnterState(this);
    }

}
