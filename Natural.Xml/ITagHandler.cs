﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Xml
{
    /// <summary>
    /// Interface for reading the child tags for a parent tag.
    /// This can be reused or created-per.
    /// </summary>
    public interface ITagHandler
    {
        /// <summary>Called when a child tag is hit. Return a handler for a child tag, or null to skip further children.</summary>
        ITagHandler HandleStartChildTag(string tagName, ITagAttributes attributes);

        /// <summary>Called when this handler is done with.</summary>
        void HandleEndTag();
    }
}
