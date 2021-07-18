using System;

[Flags]
public enum ConsumeTag
{
    None = 0,
    FireMana = 1 << 0,
    NatureMana = 1 << 1,
    WaterMana = 1 << 2,
    AnyMana = FireMana | NatureMana | WaterMana,

    FullAction = 1 << 3,
}