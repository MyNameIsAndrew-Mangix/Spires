namespace Spire.Stats
{
    public enum StatModType
    {
        Flat = 100,
        PercentAdd = 200,
    }
    public class StatMod
    {

        public readonly float value;
        public readonly int order;
        public readonly StatModType statModType;
        public readonly object source;

        // Main constructor
        public StatMod(float value, StatModType type, int order, object source)
        {
            this.value = value;
            this.order = order;
            statModType = type;
            this.source = source;
        }

        // Requires value and type. Calls the "Main" constructor and sets order and source to their default values (int)type and null, respectively.
        public StatMod(float value, StatModType type) : this(value, type, (int)type, null) { }
        // Requires value, type, and order. Sets source to its default value: null.
        public StatMod(float value, StatModType type, int order) : this(value, type, order, null) { }
        // Requires value, type, and source. Sets order to its default value: (int)type.
        public StatMod(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
    }
}