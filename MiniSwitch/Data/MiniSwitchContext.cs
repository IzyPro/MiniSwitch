using System;
using MiniSwitch.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MiniSwitch.Data
{
    public class MiniSwitchContext : IdentityDbContext<User>
    {
        public MiniSwitchContext(DbContextOptions options) : base(options)
        {
        }

        public new DbSet<User> Users { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Scheme> Schemes { get; set; }
        public DbSet<SinkNode> SinkNodes { get; set; }
        public DbSet<SourceNode> SourceNodes { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
