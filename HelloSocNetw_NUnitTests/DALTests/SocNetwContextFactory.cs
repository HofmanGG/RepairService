using HelloSocNetw_DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloSocNetw_NUnitTests.DALTests
{
    public static class SocNetwContextFactory
    {
        public static SocNetwContext Create()
        {
            var options = new DbContextOptionsBuilder<SocNetwContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new SocNetwContext(options);

            context.Database.EnsureCreated();

            SeedSampleData(context);

            return context;
        }

        public static void SeedSampleData(SocNetwContext context)
        {/*
            context.TodoLists.AddRange(
                new TodoList { Id = 1, Title = "Shopping" }
            );

            context.TodoItems.AddRange(
                new TodoItem { Id = 1, ListId = 1, Title = "Bread", Done = true },
                new TodoItem { Id = 2, ListId = 1, Title = "Butter", Done = true },
                new TodoItem { Id = 3, ListId = 1, Title = "Milk" },
                new TodoItem { Id = 4, ListId = 1, Title = "Sugar" },
                new TodoItem { Id = 5, ListId = 1, Title = "Coffee" }
            );

            context.SaveChanges();
            */
        }

        public static void Destroy(SocNetwContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
