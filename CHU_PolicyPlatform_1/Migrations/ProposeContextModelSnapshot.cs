﻿// <auto-generated />
using System;
using CHU_PolicyPlatform_1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CHU_PolicyPlatform_1.Migrations
{
    [DbContext(typeof(ProposeContext))]
    partial class ProposeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CHU_PolicyPlatform_1.Models.Category", b =>
                {
                    b.Property<string>("CategoryId")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("CategoryID");

                    b.Property<int>("CategoryGerentReview")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int>("CategoryMaxDay")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int>("CategoryMinDay")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("CHU_PolicyPlatform_1.Models.Gerent", b =>
                {
                    b.Property<string>("GerentId")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("GerentID");

                    b.Property<string>("Gpassword")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("GPassword");

                    b.HasKey("GerentId");

                    b.ToTable("Gerent");
                });

            modelBuilder.Entity("CHU_PolicyPlatform_1.Models.Proposal", b =>
                {
                    b.Property<string>("ProposalId")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("ProposalID");

                    b.Property<int>("CategoryDay")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("CategoryID");

                    b.Property<int>("CategoryReview")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("GainsInfluential")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Gains&Influential");

                    b.Property<bool?>("Pass")
                        .HasColumnType("bit");

                    b.Property<string>("Pcontent")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("PContent");

                    b.Property<DateTime>("Pdate")
                        .HasColumnType("datetime")
                        .HasColumnName("PDate");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("Underways")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("UserID");

                    b.HasKey("ProposalId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Proposal");
                });

            modelBuilder.Entity("CHU_PolicyPlatform_1.Models.ToRepond", b =>
                {
                    b.Property<string>("ProposalId")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("ProposalID");

                    b.Property<string>("GerentId")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("GerentID");

                    b.Property<string>("Rcontent")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("RContent");

                    b.HasKey("ProposalId", "GerentId");

                    b.HasIndex("GerentId");

                    b.ToTable("ToRepond");
                });

            modelBuilder.Entity("CHU_PolicyPlatform_1.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("UserID");

                    b.Property<string>("Upassword")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("UPassword");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CHU_PolicyPlatform_1.Models.Vote", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("UserID");

                    b.Property<string>("ProposalId")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("ProposalID");

                    b.Property<bool>("Crucial")
                        .HasColumnType("bit");

                    b.Property<string>("Vcontent")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("VContent");

                    b.Property<DateTime>("Vdate")
                        .HasColumnType("datetime")
                        .HasColumnName("VDate");

                    b.HasKey("UserId", "ProposalId");

                    b.HasIndex("ProposalId");

                    b.ToTable("Vote");
                });

            modelBuilder.Entity("CHU_PolicyPlatform_1.Models.Proposal", b =>
                {
                    b.HasOne("CHU_PolicyPlatform_1.Models.Category", "Category")
                        .WithMany("Proposals")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_Proposal_Category")
                        .IsRequired();

                    b.HasOne("CHU_PolicyPlatform_1.Models.User", "User")
                        .WithMany("Proposals")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Proposal_User")
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CHU_PolicyPlatform_1.Models.ToRepond", b =>
                {
                    b.HasOne("CHU_PolicyPlatform_1.Models.Gerent", "Gerent")
                        .WithMany("ToReponds")
                        .HasForeignKey("GerentId")
                        .HasConstraintName("FK_ToRepond_Gerent")
                        .IsRequired();

                    b.HasOne("CHU_PolicyPlatform_1.Models.Proposal", "Proposal")
                        .WithMany("ToReponds")
                        .HasForeignKey("ProposalId")
                        .HasConstraintName("FK_ToRepond_Proposal")
                        .IsRequired();

                    b.Navigation("Gerent");

                    b.Navigation("Proposal");
                });

            modelBuilder.Entity("CHU_PolicyPlatform_1.Models.Vote", b =>
                {
                    b.HasOne("CHU_PolicyPlatform_1.Models.Proposal", "Proposal")
                        .WithMany("Votes")
                        .HasForeignKey("ProposalId")
                        .HasConstraintName("FK_Vote_Proposal")
                        .IsRequired();

                    b.HasOne("CHU_PolicyPlatform_1.Models.User", "User")
                        .WithMany("Votes")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Vote_User")
                        .IsRequired();

                    b.Navigation("Proposal");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CHU_PolicyPlatform_1.Models.Category", b =>
                {
                    b.Navigation("Proposals");
                });

            modelBuilder.Entity("CHU_PolicyPlatform_1.Models.Gerent", b =>
                {
                    b.Navigation("ToReponds");
                });

            modelBuilder.Entity("CHU_PolicyPlatform_1.Models.Proposal", b =>
                {
                    b.Navigation("ToReponds");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("CHU_PolicyPlatform_1.Models.User", b =>
                {
                    b.Navigation("Proposals");

                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}
