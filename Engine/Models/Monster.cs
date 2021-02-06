namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        //private int _hitpoints; Removed on 10.1 as inheriting from LivingEntity class
        //public string Name { get; private set; } Removed on 10.1 as inheriting from LivingEntity class
        public string ImageName { get; set; }
        //public int MaximumHitPoints { get; private set; } Removed on 10.1 as inheriting from LivingEntity class
        /*public int HitPoints {
            get { return _hitpoints; }
            set {                               // removed provate as otherwise it will not work  (updated somewhere between lessons)
                _hitpoints = value;
                OnPropertyChanged(nameof(HitPoints));
            }
        }*/

        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }

        public int RewardExperiencePoints { get; private set; }
        //public int RewardGold { get; private set; } Removed on 10.1 as inheriting from LivingEntity class

        //public ObservableCollection<ItemQuantity> Inventory { get; set; } Removed on 10.1 as inheriting from LivingEntity class
        public Monster(string name, string imageName,
            int maximumHitPoints, int hitPoints,
            int minimumDamage, int maximumDamage,
            int rewardExperiencePoints, int rewardGold)
        {
            Name = name;
            //ImageName = string.Format("/Engine;component/Images/Monsters/{0}", imageName);    <- this was concatenated string now we use string interpolation = '$' + '{var}'
            ImageName = $"/Engine;component/Images/Monsters/{imageName}";
            MaximumHitPoints = maximumHitPoints;
            CurrentHitPoints = hitPoints;
            MinimumDamage = minimumDamage;
            MaximumDamage = maximumDamage;
            RewardExperiencePoints = rewardExperiencePoints;
            Gold = rewardGold;

            //Inventory = new ObservableCollection<ItemQuantity>(); Removed on 10.1 as inheriting from LivingEntity class
        }
    }
}