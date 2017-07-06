using System;
using System.Reflection;
using Autofac;
using System.Collections;
using Autofac.Features.Metadata;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Gdot.Care.Common.Dependency
{

    [ExcludeFromCodeCoverage]
    [MetadataAttribute]
    public class WithKeyAttribute : System.Attribute
    {
        /// <summary>
        /// Gets the key the dependency is expected to have to satisfy the parameter.
        /// </summary>
        /// <value>
        /// The <see cref="System.Object"/> corresponding to a registered service
        /// key on a component. Resolved components must be keyed with this value to
        /// satisfy the filter.
        /// </value>
        public object Key { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WithKeyAttribute"/> class,
        /// specifying the <paramref name="key"/> that the
        /// dependency should have in order to satisfy the parameter.
        /// </summary>
        public WithKeyAttribute(object key)
        {
            this.Key = key;
        }
    }
}
