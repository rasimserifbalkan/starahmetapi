using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Configuration.Client;
using Couchbase.Configuration.Server.Providers;
using Couchbase.Linq;

namespace StarAhmet.CouchbaseDb
{
    public class DbConnection : IDbConnection
    {
        public  BucketContext GetConnection(string bucketName)
        {
           
            var cluster = new Cluster(new ClientConfiguration()
            {
                Servers = new List<Uri>{new Uri("couchbase://185.242.162.113:8091") },
                ConfigurationProviders = ServerConfigurationProviders.HttpStreaming
            });
            cluster.Authenticate("rasim", "@Amelecan123");
            var bucket = cluster.OpenBucket(bucketName);
            return new BucketContext(bucket);
         
        }
     
       


    }
}
