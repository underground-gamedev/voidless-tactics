using System;

public class ManaStore
{
    private int capacity;
    private int limit;

    public int Capacity => capacity;
    public int Limit => limit;

    public ManaStore(int capacity, int limit)
    {
        this.capacity = capacity;
        this.limit = limit; 
    }

    public int Refill(int current)
    {
        if (current >= limit) {
            return current;
        }

        var needCount = limit - current;
        var wouldTake = Math.Min(needCount, capacity);
        var newValue = current + wouldTake;
        capacity -= wouldTake;
        return newValue;
    }
}
