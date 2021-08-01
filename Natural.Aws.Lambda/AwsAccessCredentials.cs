using System;
using System.Collections.Generic;
using System.Text;

namespace Natural.Aws
{
    public class AwsAccessCredentials
    {
        /// <summary>The access key.</summary>
        public string AccessKeyId { get; set; }

        /// <summary>The access secret.</summary>
        public string AccessKeySecret { get; set; }

        /// <summary>The region</summary>
        public Amazon.RegionEndpoint Region { get; set; }
    }
}
