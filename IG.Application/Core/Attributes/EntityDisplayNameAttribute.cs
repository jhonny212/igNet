using System;

namespace IG.Application.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class EntityDisplayNameAttribute : Attribute
    {
        public string Name { get; }

        public EntityDisplayNameAttribute(string name)
        {
            Name = name;
        }
    }
}