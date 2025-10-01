using System;
using System.Collections.Generic;
using System.Text;

namespace Natural.Aws
{
    /// <summary>Interface for accessing AWS resources.</summary>
    public interface IAwsService : IDisposable
    {
        /// <summar>Getter for the DynamoDB service.</summary>
        DynamoDB.IDynamoService DynamoService { get; }

        /// <summar>Getter for the S3 service.</summary>
        S3.IS3Service S3Service { get; }
    }
}
