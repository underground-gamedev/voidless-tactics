using System;

public class ManaStore
{
    private int storedValue;
    private int limit;

    public int StoredValue => storedValue;
    public int Limit => limit;

    public ManaStore(int storedValue, int limit)
    {
        this.storedValue = storedValue;
        this.limit = limit; 
    }

    public int Refill(int current)
    {
        if (current >= limit) {
            return current;
        }

        var needCount = limit - current;
        var wouldTake = Math.Min(needCount, storedValue);
        var newValue = current + wouldTake;
        storedValue -= wouldTake;
        return newValue;
    }
}
