﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApiProject.Data;

#nullable disable

namespace JwtTest.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231129174443_UserUserParents")]
    partial class UserUserParents
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("integer");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("UserUserChildren", b =>
                {
                    b.Property<int>("UserChildrenId")
                        .HasColumnType("integer");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("UserChildrenId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("UserUserChildren");
                });

            modelBuilder.Entity("WebApiProject.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Name")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("WebApiProject.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Fio")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Pin")
                        .IsRequired()
                        .HasColumnType("varchar(14)");

                    b.Property<int?>("UserParentsId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserParentsId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebApiProject.Models.UserChildren", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Fio")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Pin")
                        .IsRequired()
                        .HasColumnType("varchar(14)");

                    b.HasKey("Id");

                    b.ToTable("UserChildren");
                });

            modelBuilder.Entity("WebApiProject.Models.UserParents", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("PinFather")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PinMother")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserParents");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("WebApiProject.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiProject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserUserChildren", b =>
                {
                    b.HasOne("WebApiProject.Models.UserChildren", null)
                        .WithMany()
                        .HasForeignKey("UserChildrenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiProject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApiProject.Models.User", b =>
                {
                    b.HasOne("WebApiProject.Models.UserParents", "UserParents")
                        .WithOne("User")
                        .HasForeignKey("WebApiProject.Models.User", "UserParentsId");

                    b.Navigation("UserParents");
                });

            modelBuilder.Entity("WebApiProject.Models.UserParents", b =>
                {
                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
