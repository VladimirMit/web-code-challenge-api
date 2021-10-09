﻿// <auto-generated />
using System;
using Cognizant.CodeChallenge.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cognizant.CodeChallenge.Api.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20211009135422_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("Cognizant.CodeChallenge.Domain.Entities.CodeTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CodeTask");
                });

            modelBuilder.Entity("Cognizant.CodeChallenge.Domain.Entities.Participant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("UserName")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("Cognizant.CodeChallenge.Domain.Entities.Solution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("LanguageName")
                        .HasColumnType("text");

                    b.Property<int?>("ParticipantId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParticipantId");

                    b.HasIndex("TaskId");

                    b.ToTable("Solutions");
                });

            modelBuilder.Entity("Cognizant.CodeChallenge.Domain.Entities.CodeTask", b =>
                {
                    b.OwnsMany("Cognizant.CodeChallenge.Domain.ValueObjects.CodeTaskTestCase", "TestCases", b1 =>
                        {
                            b1.Property<int>("CodeTaskId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<string>("InputValue")
                                .HasColumnType("text");

                            b1.Property<string>("OutputValue")
                                .HasColumnType("text");

                            b1.HasKey("CodeTaskId", "Id");

                            b1.ToTable("CodeTaskTestCases");

                            b1.WithOwner()
                                .HasForeignKey("CodeTaskId");
                        });

                    b.Navigation("TestCases");
                });

            modelBuilder.Entity("Cognizant.CodeChallenge.Domain.Entities.Solution", b =>
                {
                    b.HasOne("Cognizant.CodeChallenge.Domain.Entities.Participant", "Participant")
                        .WithMany("Solutions")
                        .HasForeignKey("ParticipantId");

                    b.HasOne("Cognizant.CodeChallenge.Domain.Entities.CodeTask", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId");

                    b.Navigation("Participant");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("Cognizant.CodeChallenge.Domain.Entities.Participant", b =>
                {
                    b.Navigation("Solutions");
                });
#pragma warning restore 612, 618
        }
    }
}
