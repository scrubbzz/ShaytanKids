using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ability : ScriptableObject
{
    //public string abilityName;
    public  float cooldownTime;
    public float activeTime;
   
  /*  public enum AbilityType {Float, Lunge, Supress, Regenerate, Teleport, Hypnotise};
    public AbilityType abilityType;*/

    private void Awake()
    {
        activeTime = 6;
        cooldownTime = 3;
    }
    public virtual void SetAbilityType(KeyCode keyCode)
    {

    }
    public virtual void Activate()
    {
       
    }

}

