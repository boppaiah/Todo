﻿using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.Data
{
    public class TodoDb: DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options) : base(options) { }

        public DbSet<TodoItem> Todos { get; set; }
    }
}
