﻿// Copyright (c) 2016 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using System;
using DataLayer.EfClasses;
using DataLayer.EfCode;
using DataLayer.EfCode.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Test.Chapter08Listings.EfCode
{
    public class Chapter08EfCoreContext : DbContext
    {
        public DbSet<Book> Books { get; set; }              
        public DbSet<Author> Authors { get; set; }          
        public DbSet<PriceOffer> PriceOffers { get; set; }  
        public DbSet<Order> Orders { get; set; }            

        public Chapter08EfCoreContext(                             
            DbContextOptions<Chapter08EfCoreContext> options)      
            : base(options) {}

        public static double? AverageVotesUdf(int bookId)
        {
            throw new Exception();
        }

        protected override void
            OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfig());
            modelBuilder.ApplyConfiguration(new BookAuthorConfig());
            modelBuilder.ApplyConfiguration(new PriceOfferConfig()); 
            modelBuilder.ApplyConfiguration(new LineItemConfig());

            //needed this to add .HasSchema - see bug https://github.com/aspnet/EntityFrameworkCore/issues/9663
            modelBuilder.HasDbFunction(
                () => AverageVotesUdf(default(int)))
                .HasSchema("dbo");

        }

    }
}

