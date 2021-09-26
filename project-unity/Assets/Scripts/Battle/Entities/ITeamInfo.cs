using Core.Components;
using UnityEngine;

namespace Battle
{
    public interface ITeamInfo: IComponent
    {
        string Name { get; }
        Color Color { get; }
    }
}