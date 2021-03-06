﻿using System;
using Engine.Models;

namespace Engine.Actions
{
    public class AttackWithWeapon {
        private readonly GameItem _weapon;
        private readonly int _maximumDamage;
        private readonly int _minimumDamage;

        public event EventHandler<string> OnActionPerformed;    // public event that will notify UI of any messages that result from executing this command object

        public AttackWithWeapon(GameItem weapon, int minimumDamage, int maximumDamage){  // constructor with parameter validation and to private variables pass
            if (weapon.Category != GameItem.ItemCategory.Weapon) {
                throw new ArgumentException($"{weapon.Name} is not a weapon");
            }

            if (_minimumDamage < 0) {
                throw new ArgumentException("minimumDamage must be 0 or larger");
            }

            if (_maximumDamage < _minimumDamage) {
                throw new ArgumentException("maximumDamage must be >= minimumDamage");
            }

            _weapon = weapon;
            _minimumDamage = minimumDamage;
            _maximumDamage = maximumDamage;
        }

        public void Execute(LivingEntity actor, LivingEntity target) {   // command that accepts two parameters 1) actor (who is performing the action) 2) target (who is having the action done to them)
            int damage = RandomNumberGenerator.NumberBetween(_minimumDamage, _maximumDamage);

            if (damage == 0) {
                ReportResult($"You missed the {target.Name.ToLower()}.");
            }
            else {
                ReportResult($"You hit the {target.Name.ToLower()} for {damage} points.");
                target.TakeDamage(damage);
            }
        }

        // Function that raises the event notification - if anything subscribed to the OnActionPerformed event
        private void ReportResult(string result) {
            OnActionPerformed?.Invoke(this, result);
        }
    }
}