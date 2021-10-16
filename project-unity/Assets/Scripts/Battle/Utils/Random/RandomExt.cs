using System;
using System.Collections.Generic;

namespace Battle.Systems.RandomSystem
{
    public static class RandomExt
    {
        private const double ToDouble = 1.0 / 4294967296.0;

        /// <summary>
        /// Get random int in range [Int32.MinValue;Int32.MaxValue]
        /// </summary>
        public static int Next(this IRandomGenerator rand)
        {
            return (int) rand.NextUInt();
        }

        /// <summary>
        /// Get random int in range [0;max)
        /// </summary>
        public static int Next(this IRandomGenerator rand, int max)
        {
            if (max < 0)
            {
                throw new ArgumentException("max <= 0");
            }

            if (max == 0)
            {
                rand.NextUInt(); // mutate state for consistance
                return 0;
            }
            
            var result = rand.NextUInt();
            return Math.Abs((int) (result >> 16) % max);
        }

        /// <summary>
        /// Get random int in range [min;max)
        /// </summary>
        public static int Next(this IRandomGenerator rand, int min, int max)
        {
            if (max < min)
            {
                throw new ArgumentException("Max must be larger than min");
            }

            return rand.Next(max - min) + min;
        }

        /// <summary>
        /// Get random float in range [0;1]
        /// </summary>
        public static float NextFloat(this IRandomGenerator rand)
        {
            return (float) (rand.NextUInt() * ToDouble);
        }

        /// <summary>
        /// Get random float in range [0;max]
        /// </summary>
        public static float NextFloat(this IRandomGenerator rand, float max)
        {
            if (max < 0)
            {
                throw new ArgumentException("Max must be larger than 0");
            }

            if (max == 0)
            {
                rand.NextFloat(); // mutate for consisttance
                return 0;
            }

            return rand.NextFloat() * max;
        }

        /// <summary>
        /// Get random float in range [min;max]
        /// </summary>
        public static float NextFloat(this IRandomGenerator rand, float min, float max)
        {
            if (max < min)
            {
                throw new ArgumentException("Max must be larger than min");
            }

            return rand.NextFloat(max - min) + min;
        }

        public static bool NextBool(this IRandomGenerator rand)
        {
            return (rand.NextUInt()&1) == 1;
        }

        /// <summary>
        /// Choice random element from non empty list
        /// </summary>
        public static T Choice<T>(this IRandomGenerator rand, List<T> collection)
        {
            if (collection.Count <= 0)
            {
                throw new ArgumentException("Collection must contains elements");
            }

            return collection[rand.Next(collection.Count)];
        }

    }
}