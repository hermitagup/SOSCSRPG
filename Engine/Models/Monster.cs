namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        public string ImageName { get; set; }
        
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }

        public int RewardExperiencePoints { get; private set; }
       
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
        }
    }
}