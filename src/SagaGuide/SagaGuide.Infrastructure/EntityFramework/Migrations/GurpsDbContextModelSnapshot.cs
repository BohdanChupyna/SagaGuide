﻿// <auto-generated />
using System;
using SagaGuide.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SagaGuide.Infrastructure.EntityFramework.Migrations
{
    [DbContext(typeof(GurpsDbContext))]
    partial class GurpsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SagaGuide.Core.Domain.Attribute", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("BookReference")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double?>("DefaultValue")
                        .HasColumnType("double precision");

                    b.Property<string>("DependOnAttributeType")
                        .HasColumnType("text");

                    b.Property<int>("PointsCostPerLevel")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("ValueIncreasePerLevel")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("Type")
                        .IsUnique();

                    b.ToTable("Attribute", (string)null);
                });

            modelBuilder.Entity("SagaGuide.Core.Domain.CharacterAggregate.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<double>("Age")
                        .HasColumnType("double precision");

                    b.Property<string>("Attributes")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Campaign")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Equipments")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FpLose")
                        .HasColumnType("integer");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Handedness")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Height")
                        .HasColumnType("double precision");

                    b.Property<int>("HpLose")
                        .HasColumnType("integer");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Player")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Race")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Religion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Size")
                        .HasColumnType("double precision");

                    b.Property<string>("Skills")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TechLevel")
                        .HasColumnType("integer");

                    b.Property<string>("Techniques")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalPoints")
                        .HasColumnType("integer");

                    b.Property<string>("Traits")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Character", (string)null);
                });

            modelBuilder.Entity("SagaGuide.Core.Domain.EquipmentAggregate.Equipment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Attacks")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BookReferences")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double?>("Cost")
                        .HasColumnType("double precision");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Features")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IgnoreWeightForSkills")
                        .HasColumnType("boolean");

                    b.Property<string>("LegalityClass")
                        .HasColumnType("text");

                    b.Property<int?>("MaxUses")
                        .HasColumnType("integer");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Modifiers")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<string>("Prerequisites")
                        .HasColumnType("text");

                    b.Property<int?>("RatedStrength")
                        .HasColumnType("integer");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TechLevel")
                        .HasColumnType("text");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.Property<string>("Weight")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Equipment", (string)null);
                });

            modelBuilder.Entity("SagaGuide.Core.Domain.EquipmentAggregate.EquipmentModifier", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("BookReferences")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Cost")
                        .HasColumnType("text");

                    b.Property<string>("CostType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Features")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TechLevel")
                        .HasColumnType("text");

                    b.Property<string>("Weight")
                        .HasColumnType("text");

                    b.Property<string>("WeightType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EquipmentModifier");
                });

            modelBuilder.Entity("SagaGuide.Core.Domain.SkillAggregate.Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("AttributeType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BookReferences")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Defaults")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DifficultyLevel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("EncumbrancePenaltyMultiplier")
                        .HasColumnType("integer");

                    b.Property<string>("LocalNotes")
                        .HasColumnType("text");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PointsCost")
                        .HasColumnType("integer");

                    b.Property<string>("Prerequisites")
                        .HasColumnType("text");

                    b.Property<string>("Specialization")
                        .HasColumnType("text");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("TechLevel")
                        .HasColumnType("integer");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Skill", (string)null);
                });

            modelBuilder.Entity("SagaGuide.Core.Domain.TechniqueAggregate.Technique", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("BookReferences")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Default")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DifficultyLevel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PointsCost")
                        .HasColumnType("integer");

                    b.Property<string>("Prerequisites")
                        .HasColumnType("text");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("TechniqueLimitModifier")
                        .HasColumnType("integer");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Technique", (string)null);
                });

            modelBuilder.Entity("SagaGuide.Core.Domain.TraitAggregate.Trait", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("BasePointsCost")
                        .HasColumnType("integer");

                    b.Property<string>("BookReferences")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("CanLevel")
                        .HasColumnType("boolean");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Features")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LocalNotes")
                        .HasColumnType("text");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifierGroups")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Modifiers")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PointsCostPerLevel")
                        .HasColumnType("integer");

                    b.Property<string>("Prerequisites")
                        .HasColumnType("text");

                    b.Property<bool>("RoundCostDown")
                        .HasColumnType("boolean");

                    b.Property<int>("SelfControlRoll")
                        .HasColumnType("integer");

                    b.Property<string>("SelfControlRollAdjustment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Trait", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}