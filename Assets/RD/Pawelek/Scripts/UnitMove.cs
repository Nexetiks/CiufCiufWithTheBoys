using Entities;
using Entities.Abilities;
using UnityEngine;

namespace Pawelek.Testing.ItWillBeChangeLater
{
    public class UnitMove : MonoBehaviour
    {
        [SerializeField]
        private EntityContext context;

        public void TestMove()
        {
            if (context.Entity.TryGetComponent(out AbilitiesHandler abilitiesHandler))
            {
                abilitiesHandler.TryGetAbility(out MoveUnitAbility moveUnitAbility);
            }
        }
    }
}