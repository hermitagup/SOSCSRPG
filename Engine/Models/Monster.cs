namespace Engine.Models
{
    public class Monster : LivingEntity
    {

        public string ImageName { get;}
        public int RewardExperiencePoints { get;}
       
        public Monster(string name, string imageName,
            int maximumHitPoints, int currentHitPoints,
            int rewardExperiencePoints, int gold) :
            base(name, maximumHitPoints, currentHitPoints, gold)
        {
            //ImageName = string.Format("/Engine;component/Images/Monsters/{0}", imageName);    <- this was concatenated string now we use string interpolation = '$' + '{var}'
            ImageName = $"/Engine;component/Images/Monsters/{imageName}";
            RewardExperiencePoints = rewardExperiencePoints;

            //Inventory = new ObservableCollection<ItemQuantity>(); Removed on 10.1 as inheriting from LivingEntity class
        }
    }
}