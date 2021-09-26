using System;

namespace Core.Components
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RequireAttribute: Attribute
    {
        public readonly Type[] Requirements;
        
        public RequireAttribute(params Type[] requirements)
        {
            Requirements = requirements ?? Type.EmptyTypes;
        }
    }
}