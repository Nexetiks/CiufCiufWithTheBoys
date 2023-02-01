using EarthEater.Abilities.MoveEngine;
using Entities;
using Entities.Abilities;
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
            if (context.Entity.TryGetComponent(out AbilitiesHandler abilitiesHandler))
            {
                abilitiesHandler.TryGetAbility(out MoveEngineAbility moveEngineAbility);
                abilitiesHandler.PerformAbility<MoveEngineAbility>(new MoveEngineAbilityArgs(-dir));
            }
        }
    }
}