using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using viveiro_back.Models;

namespace viveiro_back.Data
{
    public class viveiro_backContext : DbContext
    {
        public viveiro_backContext (DbContextOptions<viveiro_backContext> options)
            : base(options)
        {
        }

        public DbSet<viveiro_back.Models.products> products { get; set; } = default!;

        public DbSet<viveiro_back.Models.post>? post { get; set; }

        public DbSet<viveiro_back.Models.users>? users { get; set; }
    }
}
