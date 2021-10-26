using Sirenix.OdinInspector;
using Sirenix.Serialization;
using VoidLess.Game.Setup.Base;

namespace VoidLess.Game.Setup.SetupSteps
{
    public abstract class SerializableSetupStep : SerializedScriptableObject, IBattleStateSetupStep
    {
        [OdinSerialize, ShowInInspector, PropertyOrder(-1), LabelWidth(50), HorizontalGroup]
        public bool Enabled { get; private set; } = true;
        
        public int Order => (int) SetupOrder;
        
        public abstract void Setup(BattleState state);
        protected abstract SetupOrder SetupOrder { get; }
    }
}