using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Battle
{
    public interface IBattleStateSetupStep
    {
        void Setup(BattleState state);
    }
}