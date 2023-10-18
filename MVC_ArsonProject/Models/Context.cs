using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MVC_ArsonProject.Models
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<ContactUser> ContactUsers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<CertifiedMember> CertifiedMembers { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Knowledge> Knowledges { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
