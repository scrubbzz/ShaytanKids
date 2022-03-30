using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrustBar : MonoBehaviour
{
    private float distToPlayer;
    public int maxTrust = 100;
    public int maxGreed = 100;
    public int currentGreed;
    public int currentTrust;
    public GameObject player;
    public float range;
    public TrustManager TrustManager;
    public GreedManager GreedManager;
    public bool killKid;
    public bool saveKid;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentTrust = 50;
        currentGreed = 50;
        TrustManager.SetMaxBar(100);
        GreedManager.SetMaxBar(100);
        
    }

 
    void Update()
    {
        distToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distToPlayer <= range && Input.GetKeyDown(KeyCode.L))
        {
            saveKid = true;
            DecreaseGreed(10);
            IncreaseTrust(10);
        }
        else
        {
            saveKid = false;
        }
        if (distToPlayer <= range && Input.GetKeyDown(KeyCode.K))
        {
            killKid = true;
            DecreaseTrust(10);
            IncreaseGreed(10);
        }
        else
        {
            killKid = false;
        }

        void IncreaseTrust(int saveKids)
        {
            currentTrust += saveKids;
            if (currentTrust > maxTrust)
            {
                currentTrust = maxTrust;
            }
            TrustManager.SetBar(currentTrust);
        }

        void DecreaseTrust(int saveKids)
        {
            currentTrust -= saveKids;
            if (currentTrust < 0)
            {
                currentTrust = 0;
            }
            TrustManager.SetBar(currentTrust);
        }

        void IncreaseGreed(int killKids)
        {
            currentGreed += killKids;
            if(currentGreed > maxGreed)
            {
                currentGreed = maxGreed;
            }
            GreedManager.SetBar(currentGreed);
        }

        void DecreaseGreed(int killKids)
        {
            currentGreed -= killKids;
            if(currentGreed < 0)
            {
                currentGreed = 0;
            }
            GreedManager.SetBar(currentGreed);

        }


    }
}
