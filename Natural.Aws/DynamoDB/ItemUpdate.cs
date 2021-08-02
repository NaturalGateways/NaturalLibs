using System;
using System.Collections.Generic;
using System.Text;

namespace Natural.Aws.DynamoDB
{
    /// <summary>the attributes to update on an item.</summary>
    public class ItemUpdate
    {
        /// <summary>The string attributes.</summary>
        public Dictionary<string, string> StringAttributes { get; set; }
    }
}
