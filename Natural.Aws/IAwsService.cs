using System;
using System.Collections.Generic;
using System.Text;

namespace Natural.Aws
{
    /// <summary>Interface for accessing AWS resources.</summary>
    public interface IAwsService
    {
        /// <summar>Creates a disposable DynamoDB service.</summary>
        DynamoDB.IDynamoService CreateDynamoService();
    }
}
