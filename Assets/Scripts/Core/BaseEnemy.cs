namespace Spire.Core
{
    public class BaseEnemy
    {
        public enum EnemyType
        {
            Bipedal_Screecher,
            Quadrapedal_Harasser,
            Quadralpedal_Hunter,
            Hexapedal_Sentinel,
            BOSS
            //more later?
        }


        public string name;

        public int baseHP;
        public int curHP;

        public int baseStamina;
        public int curStamina;

        public int strength;
        public int agility;
        public int endurance;
        public int wits;
        public int intimidation;
        //works with willpower for fear mechanic.
        public int tenacity;
        //CC/status resist.
    }
}