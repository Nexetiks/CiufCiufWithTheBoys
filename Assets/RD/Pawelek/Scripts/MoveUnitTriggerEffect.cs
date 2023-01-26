using Entities.Effects;
using UnityEngine;

namespace Pawelek.Testing.ItWillBeChangeLater
{
    public class MoveUnitTriggerEffect : TriggeredEffect<MoveUnitAbilityArgs>
    {
        public MoveUnitTriggerEffect() : base("MoveUnitTriggerEffect", -1)
        {
        }

        protected override void OnPerform(MoveUnitAbilityArgs args)
        {
            base.OnPerform(args);
            Debug.Log("test - dupa");
            //move unit.
        }
    }
}