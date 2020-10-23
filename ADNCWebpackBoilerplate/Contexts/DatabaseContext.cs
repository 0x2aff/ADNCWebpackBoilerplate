/*
 * MIT License
 * 
 * Copyright (c) 2020 Stanislaw Schlosser <http://github.com/0x2aff>
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using ADNCWebpackBoilerplate.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ADNCWebpackBoilerplate.Contexts
{
    /// <summary>
    ///     Main database context for EF Core.
    /// </summary>
    public class
        DatabaseContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Instantiates the database context for this application.
        /// </summary>
        /// <param name="options"></param>
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("User");
            builder.Entity<Role>().ToTable("Role");
            builder.Entity<UserClaim>().ToTable("UserClaim");
            builder.Entity<UserRole>().ToTable("UserRole");
            builder.Entity<UserLogin>().ToTable("UserLogin");
            builder.Entity<RoleClaim>().ToTable("RoleClaim");
            builder.Entity<UserToken>().ToTable("UserToken");
        }
    }
}