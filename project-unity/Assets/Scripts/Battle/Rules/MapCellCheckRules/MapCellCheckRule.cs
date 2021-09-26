namespace Battle.Rules.MapCellCheckRules
{
    public static class MapCellCheckRule
    {
        public static IMapCellCheckRule OutOfBounds()
        {
            return new OutOfBoundsCheck();
        }

        public static IMapCellCheckRule IsSolid()
        {
            return new SolidCellCheck();
        }

        public static IMapCellCheckRule CharacterExists()
        {
            return new CharacterCheckRule();
        }

        public static IMapCellCheckRule Inverse(IMapCellCheckRule rule)
        {
            return new InverseCheckRule(rule);
        }

        public static IMapCellCheckRule AndSequence(params IMapCellCheckRule[] rules)
        {
            var andSeq = new AndSequenceCheck();
            for (var i = 0; i < rules.Length; i++)
            {
                andSeq = andSeq.AddRule(rules[i]);
            }
            return andSeq;
        }

        public static IMapCellCheckRule OrSequence(params IMapCellCheckRule[] rules)
        {
            var orSeq = new OrSequenceCheck();
            for (var i = 0; i < rules.Length; i++)
            {
                orSeq = orSeq.AddRule(rules[i]);
            }
            return orSeq;
        }
    }
}