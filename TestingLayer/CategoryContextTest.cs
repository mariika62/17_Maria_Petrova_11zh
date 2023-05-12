using BusinessLayer;
using DataLayer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestingLayer
{
    [TestFixture]
    public class CategoryContextTest
    {
        private CategoriesContext context = new CategoriesContext(SetupFixture.dbContext);
        private Category category;
        private User u1, u2;
        private Interest i1, i2;

        [SetUp]
        public void CreateCategory()
        {
            category = new Category("Music");

            i1 = new Interest("Singing");
            i2 = new Interest("Dancing");

            u1 = new User("Maria", "Petrova", 18, "mariika62", "26102005", "mariyapetrova_zh19@schoolmath.eu");
            u2 = new User("Todor", "Demirov", 18, "toshkoo", "16112005", "todordemirov_zh19@schoolmath.eu");

            category.Interests.Add(i1);
            category.Interests.Add(i2);

            category.Users.Add(u1);
            category.Users.Add(u2);
            context.Create(category);
        }
        [TearDown]
        public void DropCategory()
        {
            foreach (Category item in SetupFixture.dbContext.Categories)
            {
                SetupFixture.dbContext.Categories.Remove(item);
            }

            SetupFixture.dbContext.SaveChanges();
        }
        [Test]
        public void Create()
        {
            Category newCategory = new Category("Sport");

            int categoriesBefore = SetupFixture.dbContext.Categories.Count();
            context.Create(newCategory);

            int categoriesAfter = SetupFixture.dbContext.Categories.Count();
            Assert.IsTrue(categoriesBefore + 1 == categoriesAfter, "Create() does not work!");
        }
        [Test]
        public void Read()
        {
            Category readCategory = context.Read(category.Id);

            Assert.AreEqual(category, readCategory, "Read does not return the same object!");
        }

        [Test]
        public void ReadWithNavigationalProperties()
        {
            Category readCategory = context.Read(category.Id, true);

            Assert.That(readCategory.Interests.Contains(i1)
                && readCategory.Interests.Contains(i2)
                && readCategory.Users.Contains(u1) 
                && readCategory.Users.Contains(u2),
                "I1 and I2 are not in the Interests list and U1 and U2 are not in the users list!");
        }
        [Test]
        public void ReadAll()
        {
            List<Category> categories = (List<Category>)context.ReadAll();

            Assert.That(categories.Count != 0, "ReadAll() does not return categories!");
        }
        [Test]
        public void Update()
        {
            Category changedCategory = context.Read(category.Id);

            changedCategory.Name = "Updated " + category.Name;

            context.Update(changedCategory);

            category = context.Read(category.Id);

            Assert.AreEqual(changedCategory, category, "Update() does not work!");
        }
        [Test]
        public void Delete()
        {
            int categoriesBefore = SetupFixture.dbContext.Categories.Count();

            context.Delete(category.Id);
            int categoriesAfter = SetupFixture.dbContext.Categories.Count();

            Assert.IsTrue(categoriesBefore - 1 == categoriesAfter, "Delete() does not work! 👎🏻");
        }
    }
}
