using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Chatroom.Domain;

namespace Chatroom.Data
{
    public class ChatroomContext : DbContext
    {
        public ChatroomContext()
        {

        }

        public DbSet<Message> Messages { get; set ; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

                var connectionString = configuration.GetConnectionString("ChatroomConnectionString");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
