namespace Battle
{
    public static class Archtypes
    {
        public static readonly IArchtype Character =
            Archtype.New()
                .With<IStatComponent>()
                .Build();
    }
}