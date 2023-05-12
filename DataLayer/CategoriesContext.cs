using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CategoriesContext : IDb<Category, int>
    {
        private readonly InterestDbContext dbContext;

        public CategoriesContext(InterestDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Create(Category item)
        {
            try
            { 
                List<User> users = new List<User>();

                foreach (User u in item.Users)
                {
                    User uFromDb = dbContext.Users.Find(u.Id);

                    if (uFromDb != null)
                    {
                        users.Add(uFromDb);
                    }
                    else
                    {
                        users.Add(u);
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
                item.Users = users;
                item.Interests = interests;
                dbContext.Categories.Add(item);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Category Read(int key, bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Category> query = dbContext.Categories;

                if (useNavigationalProperties)
                {
                    query = query.Include(c => c.Users)
                        .Include(u => u.Interests);
                }

                return query.FirstOrDefault(c => c.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Category> ReadAll(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Category> query = dbContext.Categories;

                if (useNavigationalProperties)
                {
                    query = query.Include(c => c.Users)
                    .Include(u => u.Interests);
                }

                return query.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Category item, bool useNavigationalProperties = false)
        {
            try
            {
                Category categoryFromDb = Read(item.Id, useNavigationalProperties);

                if (categoryFromDb == null)
                {
                    Create(item);
                    return;
                }

                categoryFromDb.Name = item.Name;

                if (useNavigationalProperties)
                {

                    List<User> users = new List<User>();

                    foreach (User u in item.Users)
                    {
                        User uFromDb = dbContext.Users.Find(u.Id);

                        if (uFromDb != null)
                        {
                            users.Add(uFromDb);
                        }
                        else
                        {
                            users.Add(u);
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
                    categoryFromDb.Users = users;
                    categoryFromDb.Interests = interests;
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
                Category categoryFromDb = Read(key);

                if (categoryFromDb != null)
                {
                    dbContext.Categories.Remove(categoryFromDb);
                    dbContext.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("Category with that id does not exist!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
