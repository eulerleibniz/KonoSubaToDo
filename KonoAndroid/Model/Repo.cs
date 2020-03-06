using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace KonoAndroid.Model
{
    public class DatabaseToDo : DbContext
    {
        private readonly string _dbPath;

        public DatabaseToDo(string dbPath) : base()
        {
            _dbPath = dbPath;
            Database.EnsureCreated();
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlite($"Filename={_dbPath}");
            //base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new NullReferenceException();

            }
            modelBuilder.Entity<Item>().HasKey(p => p.Id);
            modelBuilder.Entity<Item>().Property(p => p.Text).IsRequired();
            modelBuilder.Entity<Item>().Property(p => p.Text).HasMaxLength(250);
            modelBuilder.Entity<Item>().Property(p => p.ItemCategory).HasConversion(
                // Going to DB
                v => v.ToString(),
                // Coming from DB
                v => (ItemCategory)Enum.Parse(typeof(ItemCategory), v));
            modelBuilder.Entity<Attachment>().HasKey(p => p.Id);
            modelBuilder.Entity<Attachment>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Attachment>().Property(p => p.ParentTaskId).IsRequired();
            modelBuilder.Entity<Attachment>().Property(p => p.FilePath).IsRequired();

            // OR for this specific Task
            //modelBuilder.Entity<Item>().Property(p => p.ItemCategory).HasConversion(new EnumToStringConverter<ItemCategory>());

#if DEBUG
            modelBuilder.Entity<Item>()
                .HasData(
                new Item { Id = Guid.NewGuid(), Text = "First Item", Description = "First Item Description", ItemCategory = ItemCategory.Private },
                new Item { Id = Guid.NewGuid(), Text = "Second Item", Description = "Second Item Description", ItemCategory = ItemCategory.Shopping },
                new Item { Id = Guid.NewGuid(), Text = "Third Item", Description = "Third Item Description", ItemCategory = ItemCategory.Work });
            //Database.EnsureCreated();
#endif
        }

        public async Task<Item> GetTodoItemAsync(Guid id)
            => await Items.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

        public async Task<IEnumerable<Item>> GetTodoItemsAsync(bool forceRefresh = false)
        {
            // How to force refresh?
            this.ChangeTracker.DetectChanges();
            // Don't Know

            return await Items.ToListAsync().ConfigureAwait(false);
        }

        public async Task<bool> AddTodoItemsAsync(Item todoItem)
        {
            await Items.AddAsync(todoItem).ConfigureAwait(false);
            await SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public async Task<bool> UpdateTodoItemsAsync(Item todoItem)
        {
            Items.Update(todoItem);
            await SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public async Task<bool> DeleteTodoItemsAsync(Guid id)
        {
            var itemToRemove = Items.FirstOrDefault(x => x.Id == id);
            if (itemToRemove != null)
            {
                Items.Remove(itemToRemove);
                await SaveChangesAsync().ConfigureAwait(false);
            }
            return true;
        }
    }
}