using EarthEater.Abilities.MoveEngine;
using Entities;
using UnityEngine;

public class PlayerAbilitiesTrigger : MonoBehaviour
{
    [SerializeField]
    private EntityContext context;

    private void Awake()
    {
        if (context == null)
        {
            context = GetComponent<EntityContext>();
        }
    }

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