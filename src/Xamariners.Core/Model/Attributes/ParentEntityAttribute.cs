using System;

namespace Xamariners.Core.Model.Attributes
{
    public class ParentEntityAttribute : Attribute
    {
        public ParentEntityAttribute(params Type[] parentTypes)
        {
            ParentTypes = parentTypes;
        }

        public Type[] ParentTypes { get; set; }
    }
}