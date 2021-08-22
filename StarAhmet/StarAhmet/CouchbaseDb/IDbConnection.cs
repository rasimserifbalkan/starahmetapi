using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Couchbase.Linq;

namespace StarAhmet.CouchbaseDb
{
    public interface IDbConnection
    {
        BucketContext GetConnection(string bucketName);
    }
}
