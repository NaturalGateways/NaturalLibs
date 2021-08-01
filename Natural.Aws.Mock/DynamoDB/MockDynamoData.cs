using System;
using System.Collections.Generic;
using System.Text;

namespace Natural.Aws.DynamoDB
{
    public class MockDynamoData
    {
        /// <summary>the tables in thge DynamoDB service.</summary>
        public Dictionary<string, MockDynamoTable> TablesByName { get; } = new Dictionary<string, MockDynamoTable>();
    }
}
