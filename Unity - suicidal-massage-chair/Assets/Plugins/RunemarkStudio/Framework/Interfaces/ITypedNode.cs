namespace Runemark.VisualEditor
{
    using System;
    using System.Collections.Generic;

    public interface ITypedNode 
    {
        List<Type> AllowedTypes { get; }
        Type Type { get; set; }

        Action OnTypeChanged { get; set; }

    }
}
