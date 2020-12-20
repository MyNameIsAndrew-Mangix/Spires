namespace Spire.Core
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

    [System.Serializable]
    public class StatBlock
    {
        public enum characterState
        {
            Roaming,
            Ambush,
            Combat,
            Dead
        }
        public enum characterCombatState
        {
            Slowed,
            Stunned,
            Feared,
            Exhausted
        }
        public string name;

        public int memberId;
        public int baseHP;
        public int curHP;

        public int baseStamina;
        public int curStamina;

        //CRITS WILL BE DETERMINED BY THE AMOUNT OF KNOWLEDGE OF AN ENEMY, WITH A SOFT CAP TO AVOID GAME-BREAKING CRITS.

        // Determines melee damage and weight capacity.
        public int strength;

        //Determines gun accuracy and damage.
        public int agility;

        //Determines bonus stamina, bonus health, and corrosion/poison mitigation.
        public int endurance;

        //How smart and perseptive a character is. Tracking, explosive damage. Crits(?).
        public int wits;

        //works against perseverance for fear mechanic.
        public int intimidation;

        // CC/status resist. if low willpower might run away in fear OR frozen in fear or reluctant to advance (slow).
        public int perseverance;
    }
}