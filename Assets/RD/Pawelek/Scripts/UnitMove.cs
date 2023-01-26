using Entities;
using UnityEngine;

namespace Pawelek.Testing.ItWillBeChangeLater
{
    public class UnitMove : MonoBehaviour
    {
        [SerializeField]
        private EntityContext context;
        
        public void TestMove()
        {
            context.Entity.AbilitiesHandler.TryGetAbility(out MoveUnitAbility moveUnitAbility);
           // moveUnitAbility.Args.
        }
    }
}