using System.Collections.ObjectModel;
namespace Engine.Models
{
    public class Monster : BaseNotificationClass
    {
        private int _hitpoints;
        public string Name { get; private set; }
        public string ImageName { get; set; }
        public int MaximumHitPoints { get; private set; }
        public int HitPoints {
            get { return _hitpoints; }
            set {                               // removed provate as otherwise it will not work  (updated somewhere between lessons)
                _hitpoints = value;
                OnPropertyChanged(nameof(HitPoints));
            }
        }

        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }

        public int RewardExperiencePoints { get; private set; }
        public int RewardGold { get; private set; }

        public ObservableCollection<ItemQuantity> Inventory { get; set; }
        public Monster(string name, string imageName,
            int maximumHitPoints, int hitPoints,
            int minimumDamage, int maximumDamage,
            int rewardExperiencePoints, int rewardGold)
        {
            Name = name;
            ImageName = $"/Engine;component/Images/Monsters/{imageName}";
            MaximumHitPoints = maximumHitPoints;
            HitPoints = hitPoints;
            MinimumDamage = minimumDamage;
            MaximumDamage = maximumDamage;
            RewardExperiencePoints = rewardExperiencePoints;
            RewardGold = rewardGold;

            Inventory = new ObservableCollection<ItemQuantity>();
        }
    }
}