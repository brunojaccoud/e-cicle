﻿// <auto-generated />
using EletronicPartsCatalog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EletronicPartsCatalog.Api.Migrations
{
    [DbContext(typeof(EletronicPartsCatalogContext))]
    [Migration("20180607153420_Includes-Projects")]
    partial class IncludesProjects
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

            modelBuilder.Entity("EletronicPartsCatalog.Domain.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AuthorPersonId");

                    b.Property<string>("Body");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("ProjectId");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("CommentId");

                    b.HasIndex("AuthorPersonId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("EletronicPartsCatalog.Domain.FollowedPeople", b =>
                {
                    b.Property<int>("ObserverId");

                    b.Property<int>("TargetId");

                    b.HasKey("ObserverId", "TargetId");

                    b.HasIndex("TargetId");

                    b.ToTable("FollowedPeople");
                });

            modelBuilder.Entity("EletronicPartsCatalog.Domain.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bio");

                    b.Property<string>("Email");

                    b.Property<byte[]>("Hash");

                    b.Property<string>("Image");

                    b.Property<byte[]>("Salt");

                    b.Property<string>("Username");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("EletronicPartsCatalog.Domain.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AuthorPersonId");

                    b.Property<string>("Body");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<string>("Slug");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("ProjectId");

                    b.HasIndex("AuthorPersonId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("EletronicPartsCatalog.Domain.ProjectFavorite", b =>
                {
                    b.Property<int>("ProjectId");

                    b.Property<int>("PersonId");

                    b.HasKey("ProjectId", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("ProjectFavorites");
                });

            modelBuilder.Entity("EletronicPartsCatalog.Domain.ProjectTag", b =>
                {
                    b.Property<int>("ProjectId");

                    b.Property<string>("TagId");

                    b.HasKey("ProjectId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ProjectTags");
                });

            modelBuilder.Entity("EletronicPartsCatalog.Domain.Tag", b =>
                {
                    b.Property<string>("TagId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("EletronicPartsCatalog.Domain.Comment", b =>
                {
                    b.HasOne("EletronicPartsCatalog.Domain.Person", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorPersonId");

                    b.HasOne("EletronicPartsCatalog.Domain.Project", "Project")
                        .WithMany("Comments")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("EletronicPartsCatalog.Domain.FollowedPeople", b =>
                {
                    b.HasOne("EletronicPartsCatalog.Domain.Person", "Observer")
                        .WithMany("Followers")
                        .HasForeignKey("ObserverId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EletronicPartsCatalog.Domain.Person", "Target")
                        .WithMany("Following")
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EletronicPartsCatalog.Domain.Project", b =>
                {
                    b.HasOne("EletronicPartsCatalog.Domain.Person", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorPersonId");
                });

            modelBuilder.Entity("EletronicPartsCatalog.Domain.ProjectFavorite", b =>
                {
                    b.HasOne("EletronicPartsCatalog.Domain.Person", "Person")
                        .WithMany("ProjectFavorites")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EletronicPartsCatalog.Domain.Project", "Project")
                        .WithMany("ProjectFavorites")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EletronicPartsCatalog.Domain.ProjectTag", b =>
                {
                    b.HasOne("EletronicPartsCatalog.Domain.Project", "Project")
                        .WithMany("ProjectTags")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EletronicPartsCatalog.Domain.Tag", "Tag")
                        .WithMany("ProjectTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
