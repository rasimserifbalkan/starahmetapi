using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Linq;
using StarAhmet.DbModels;
using StarAhmet.Helpers;

namespace StarAhmet.CouchbaseDb
{
    public class UserRepository : IUserRepository
    {
        private readonly string bucketName = "users";
        readonly BucketContext _context;
        private readonly IUserTokenRepository _userTokenRepository;
        public UserRepository(IDbConnection db, IUserTokenRepository userTokenRepository)
        {
            _userTokenRepository = userTokenRepository;
            _context = db.GetConnection(bucketName);
        }

        public IDocumentResult Add(Users user)
        {
            IDocument<Users> u = new Document<Users>();
            u.Content = user;
            u.Id = Guid.NewGuid().ToString();
            return _context.Bucket.Upsert(u);
        }
        public bool CheckUserEmail(string email)
        {
           return  _context.Query<Users>().Any(x => x.Email.Equals(email));
        }
        public Users GetUserByEmail(string email)
        {
            return _context.Query<Users>().FirstOrDefault(x => x.Email.Equals(email));
        }
        public string UserLogin(string email,string password)
        {
            string token = null;
            var loginResult =  _context.Query<Users>().Any(x => x.Email.Equals(email) && x.Password.Equals(password.ToMd5()));
            if (loginResult)
            {
                var user = GetUserByEmail(email);
                token = Guid.NewGuid().ToString()+"-"+new Random().Next(1000,9999);
                _userTokenRepository.Add(new UserTokens
                {
                    CreateDate = DateTime.Now,
                    ExpireDate = DateTime.Now.AddMonths(1),
                    IsExit = false,
                    Token = token,
                    UserId = user.Id
                });
            }

            return token;
        }
    }
}
