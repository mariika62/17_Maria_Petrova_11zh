using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UsersContext : IDb<User, int>
    {
        private readonly InterestDbContext dbContext;

        public UsersContext(InterestDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Create(User item)
        {
            try
            {
                List<User> friends = new List<User>();

                foreach (User f in item.Friends)
                {
                    User fFromDb = dbContext.Users.Find(f.Id);

                    if (fFromDb != null)
                    {
                        friends.Add(fFromDb);
                    }
                    else
                    {
                        friends.Add(f);
                    }
                }

                List<Interest> interests = new List<Interest>();

                foreach (Interest i in item.Interests)
                {
                    Interest iFromDb = dbContext.Interests.Find(i.Id);

                    if (iFromDb != null)
                    {
                        interests.Add(iFromDb);
                    }
                    else
                    {
                        interests.Add(i);
                    }
                }
                item.Friends = friends;
                item.Interests = interests;
                dbContext.Users.Add(item);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User Read( int key, bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<User> query = dbContext.Users;

                if (useNavigationalProperties)
                {
                    query = query.Include(u => u.Friends)
                        .Include(u => u.Interests);
                        
                }

                return query.FirstOrDefault(u => u.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<User> ReadAll(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<User> query = dbContext.Users;

                if (useNavigationalProperties)
                {
                    query = query.Include(u => u.Friends)
                        .Include(u => u.Interests);
                }

                return query.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(User item, bool useNavigationalProperties = false)
        {
            try
            {
                User userFromDb = Read(item.Id, useNavigationalProperties);

                if (userFromDb == null)
                {
                    Create(item);
                    return;
                }

                userFromDb.LastName = item.LastName;
                userFromDb.FirstName = item.FirstName;
                userFromDb.UserName = item.UserName;
                userFromDb.Password = item.Password;

                if (useNavigationalProperties)
                {

                    List<User> friends = new List<User>();

                    foreach (User f in item.Friends)
                    {
                        User fFromDb = dbContext.Users.Find(f.Id);

                        if (fFromDb != null)
                        {
                            friends.Add(fFromDb);
                        }
                        else
                        {
                            friends.Add(f);
                        }
                    }

                    List<Interest> interests = new List<Interest>();

                    foreach (Interest i in item.Interests)
                    {
                        Interest iFromDb = dbContext.Interests.Find(i.Id);

                        if (iFromDb != null)
                        {
                            interests.Add(iFromDb);
                        }
                        else
                        {
                            interests.Add(i);
                        }
                    }
                    userFromDb.Friends = friends;
                    userFromDb.Interests = interests;
                }
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Delete(int key)
        {
            try
            {
                User userFromDb = Read(key);

                if (userFromDb != null)
                {
                    dbContext.Users.Remove(userFromDb);
                    dbContext.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("User with that id does not exist!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}


