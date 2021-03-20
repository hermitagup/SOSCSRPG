namespace Engine.Models
{
    public class Monster : LivingEntity
    {

        public string ImageName { get;}
        public int MinimumDamage { get;}
        public int MaximumDamage { get;}

        public int RewardExperiencePoints { get;}
       
        public Monster(string name, string imageName,
            int maximumHitPoints, int currentHitPoints,
            int minimumDamage, int maximumDamage,
            int rewardExperiencePoints, int gold) :
            base(name, maximumHitPoints, currentHitPoints, gold)
        {
            //ImageName = string.Format("/Engine;component/Images/Monsters/{0}", imageName);    <- this was concatenated string now we use string interpolation = '$' + '{var}'
            ImageName = $"/Engine;component/Images/Monsters/{imageName}";
            MinimumDamage = minimumDamage;
            MaximumDamage = maximumDamage;
            RewardExperiencePoints = rewardExperiencePoints;

            //Inventory = new ObservableCollection<ItemQuantity>(); Removed on 10.1 as inheriting from LivingEntity class
        }
    }
}