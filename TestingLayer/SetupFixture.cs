﻿using NUnit.Framework;
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
namespace TestingLayer
{
    [SetUpFixture]
     public static class SetupFixture
        {
            public static InterestDbContext dbContext;

            [OneTimeSetUp]
            public static void OneTimeSetUp()
            {
                DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
                builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
                dbContext = new InterestDbContext(builder.Options);
            }

            [OneTimeTearDown]
            public static void OneTimeTearDown()
            {
                dbContext.Dispose();
            }
        }
    }