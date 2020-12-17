namespace Spire.Core
{
    [System.Serializable]
    public class BaseSquadMember
    {
        public string name;

        public int baseHP;
        public int curHP;

        public int baseStamina;
        public int curStamina;

        //CRITS WILL BE DETERMINED BY THE AMOUNT OF KNOWLEDGE OF AN ENEMY, WITH A SOFT CAP TO AVOID GAME-BREAKING CRITS.

        public int strength;
        // Determines melee damage, weight capacity, and what kind of armor can be worn.
        public int agility;
        //Determines gun damage.
        public int endurance;
        //Determines stamina and what kind of armor can be worn.
        public int wits;
        //How smart and observant a character is. Research speed, tracking, explosive damage.
        public int willpower;
        // if low willpower might run away in fear OR frozen in fear or reluctant to advance (slow). Could be cool mechanic but not required
        public int perseverance;
        // CC/status resist.

    }
}