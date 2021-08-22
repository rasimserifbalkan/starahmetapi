using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Couchbase;
using StarAhmet.DbModels;

namespace StarAhmet.CouchbaseDb
{
    public interface IUserRepository
    {
        IDocumentResult Add(Users user);
        bool CheckUserEmail(string email);
        Users GetUserByEmail(string email);
        string UserLogin(string email, string password);
    }
}
