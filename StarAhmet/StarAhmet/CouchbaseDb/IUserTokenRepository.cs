using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Couchbase;
using StarAhmet.DbModels;

namespace StarAhmet.CouchbaseDb
{
    public interface IUserTokenRepository
    {
        IDocumentResult<UserTokens> Add(UserTokens token);
        UserTokens TokenUser(string token);
    }
}
