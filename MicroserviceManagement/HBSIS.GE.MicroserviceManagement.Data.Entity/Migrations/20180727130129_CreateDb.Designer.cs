﻿// <auto-generated />
using HBSIS.GE.MicroserviceManagement.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HBSIS.GE.MicroserviceManagement.Data.Entity.Migrations
{
    [DbContext(typeof(MicroserviceManagerDbContext))]
    [Migration("20180727130129_CreateDb")]
    partial class CreateDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HBSIS.GE.MicroserviceManagement.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("InnerException");

                    b.Property<string>("Message");

                    b.HasKey("Id");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("HBSIS.GE.MicroserviceManagement.Microservice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Directory");

                    b.Property<string>("DisplayName");

                    b.Property<string>("FileExtension");

                    b.Property<string>("FileName");

                    b.Property<int>("Priority");

                    b.HasKey("Id");

                    b.ToTable("Microservice");
                });

            modelBuilder.Entity("HBSIS.GE.MicroserviceManagement.Model.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BaseDirectory");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("HBSIS.GE.MicroserviceManagement.Model.CustomerMicroservice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("CustomerId");

                    b.Property<string>("Directory");

                    b.Property<string>("FullPath");

                    b.Property<bool>("HasVisibleWindow");

                    b.Property<int>("MicroserviceId");

                    b.Property<string>("ProgramArguments");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("MicroserviceId");

                    b.ToTable("CustomerMicroservice");
                });

            modelBuilder.Entity("HBSIS.GE.MicroserviceManagement.Model.CustomerMicroservice", b =>
                {
                    b.HasOne("HBSIS.GE.MicroserviceManagement.Model.Customer", "Customer")
                        .WithMany("CustomerMicroservices")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HBSIS.GE.MicroserviceManagement.Microservice", "Microservice")
                        .WithMany("CustomersMicroservice")
                        .HasForeignKey("MicroserviceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
