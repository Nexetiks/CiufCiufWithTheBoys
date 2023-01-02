using System;
using System.Collections;
using System.Collections.Generic;
using EarthEater.Abilities.MoveEngine;
using Entities;
using UnityEngine;

public class PlayerAbilitiesTrigger : MonoBehaviour
{
    private EntityContext context;

    private void Awake()
    {
        context = GetComponent<EntityContext>();
    }

    // Update is called once per frame
    void Update()
    {
        int dir = 0;
        if (Input.GetKeyDown(KeyCode.A))
        {
            dir = -1;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            dir = 1;
        }
        
        if (dir != 0)
        {
            context.Entity.AbilitiesHandler.TryGetAbility(out MoveEngineAbility moveEngineAbility);
            moveEngineAbility.Args.Dir = -dir;
            context.Entity.AbilitiesHandler.PerformAbility<MoveEngineAbility>();
        }
    }
}
