using System;
using Engine.Models;

namespace Engine.Actions
{
    public class AttackWithWeapon : BaseAction, IAction {   // Interface implementation of IAction interface | this allows to treay "AttackWithWeapon" obj as AttackWithWeapon obj or as IAction object
                                                // when treat as IAction obj  - the only thing available are the one listed within interface IAction file.
        private readonly int _maximumDamage;
        private readonly int _minimumDamage;

        public event EventHandler<string> OnActionPerformed;    // public event that will notify UI of any messages that result from executing this command object

        public AttackWithWeapon(GameItem itemInUse, int minimumDamage, int maximumDamage) : base(itemInUse) {  // constructor with parameter validation and to private variables pass
            if (itemInUse.Category != GameItem.ItemCategory.Weapon) {
                throw new ArgumentException($"{itemInUse.Name} is not a weapon");
            }

            if (_minimumDamage < 0) {
                throw new ArgumentException("minimumDamage must be 0 or larger");
            }

            if (_maximumDamage < _minimumDamage) {
                throw new ArgumentException("maximumDamage must be >= minimumDamage");
            }
            _minimumDamage = minimumDamage;
            _maximumDamage = maximumDamage;
        }

        public void Execute(LivingEntity actor, LivingEntity target) {   // command that accepts two parameters 1) actor (who is performing the action) 2) target (who is having the action done to them)
            int damage = RandomNumberGenerator.NumberBetween(_minimumDamage, _maximumDamage);

            string actorName = (actor is Player) ? "You" : $"The {actor.Name.ToLower()}";
            string targetName = (target is Player) ? "you" : $"the {target.Name.ToLower()}";

            if (damage == 0) {
                ReportResult($"{actorName} missed {targetName}.");
            }
            else {
                ReportResult($"{actorName} hit {targetName} for {damage} point{(damage > 1 ? "s" : "")}.");     // singular plural "s" at the end trick
                target.TakeDamage(damage);
            }
        }
    }
}