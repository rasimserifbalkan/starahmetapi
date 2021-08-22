using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Linq;
using StarAhmet.DbModels;

namespace StarAhmet.CouchbaseDb
{
    public class UserTokenRepository : IUserTokenRepository
    {
        private readonly string bucketName = "usertokens";
        readonly BucketContext _context;
        public UserTokenRepository(IDbConnection db)
        {
            _context = db.GetConnection(bucketName);
        }

        public IDocumentResult<UserTokens> Add(UserTokens token)
        {
            IDocument<UserTokens> u = new Document<UserTokens>();
            u.Content = token;
            u.Id = Guid.NewGuid().ToString();
            return _context.Bucket.Upsert(u);
        }
        public UserTokens TokenUser(string token)
        {
            var result = _context.Query<UserTokens>().FirstOrDefault(x => x.Token.Equals(token) && x.ExpireDate > DateTime.Now && x.IsExit != true);
            return result;
        }
    }
}
