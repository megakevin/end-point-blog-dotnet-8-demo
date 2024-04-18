﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VehicleQuotes.WebApi;

namespace VehicleQuotes.WebApi.Migrations
{
    [DbContext(typeof(VehicleQuotesContext))]
    [Migration("20210627230039_AddSeedDataForSizesAndBodyTypes")]
    partial class AddSeedDataForSizesAndBodyTypes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("VehicleQuotes.Models.BodyType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("ID")
                        .HasName("pk_body_types");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_body_types_name");

                    b.ToTable("body_types");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Coupe"
                        },
                        new
                        {
                            ID = 2,
                            Name = "Sedan"
                        },
                        new
                        {
                            ID = 3,
                            Name = "Hatchback"
                        },
                        new
                        {
                            ID = 4,
                            Name = "Wagon"
                        },
                        new
                        {
                            ID = 5,
                            Name = "Convertible"
                        },
                        new
                        {
                            ID = 6,
                            Name = "SUV"
                        },
                        new
                        {
                            ID = 7,
                            Name = "Truck"
                        },
                        new
                        {
                            ID = 8,
                            Name = "Mini Van"
                        },
                        new
                        {
                            ID = 9,
                            Name = "Roadster"
                        });
                });

            modelBuilder.Entity("VehicleQuotes.Models.Make", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("ID")
                        .HasName("pk_makes");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_makes_name");

                    b.ToTable("makes");
                });

            modelBuilder.Entity("VehicleQuotes.Models.Model", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("MakeID")
                        .HasColumnType("integer")
                        .HasColumnName("make_id");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("ID")
                        .HasName("pk_models");

                    b.HasIndex("MakeID")
                        .HasDatabaseName("ix_models_make_id");

                    b.HasIndex("Name", "MakeID")
                        .IsUnique()
                        .HasDatabaseName("ix_models_name_make_id");

                    b.ToTable("models");
                });

            modelBuilder.Entity("VehicleQuotes.Models.ModelStyle", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BodyTypeID")
                        .HasColumnType("integer")
                        .HasColumnName("body_type_id");

                    b.Property<int>("ModelID")
                        .HasColumnType("integer")
                        .HasColumnName("model_id");

                    b.Property<int>("SizeID")
                        .HasColumnType("integer")
                        .HasColumnName("size_id");

                    b.HasKey("ID")
                        .HasName("pk_model_styles");

                    b.HasIndex("BodyTypeID")
                        .HasDatabaseName("ix_model_styles_body_type_id");

                    b.HasIndex("SizeID")
                        .HasDatabaseName("ix_model_styles_size_id");

                    b.HasIndex("ModelID", "BodyTypeID", "SizeID")
                        .IsUnique()
                        .HasDatabaseName("ix_model_styles_model_id_body_type_id_size_id");

                    b.ToTable("model_styles");
                });

            modelBuilder.Entity("VehicleQuotes.Models.ModelStyleYear", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ModelStyleID")
                        .HasColumnType("integer")
                        .HasColumnName("model_style_id");

                    b.Property<string>("Year")
                        .HasColumnType("text")
                        .HasColumnName("year");

                    b.HasKey("ID")
                        .HasName("pk_model_style_years");

                    b.HasIndex("ModelStyleID")
                        .HasDatabaseName("ix_model_style_years_model_style_id");

                    b.HasIndex("Year", "ModelStyleID")
                        .IsUnique()
                        .HasDatabaseName("ix_model_style_years_year_model_style_id");

                    b.ToTable("model_style_years");
                });

            modelBuilder.Entity("VehicleQuotes.Models.Quote", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BodyTypeID")
                        .HasColumnType("integer")
                        .HasColumnName("body_type_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<bool>("HasAllTires")
                        .HasColumnType("boolean")
                        .HasColumnName("has_all_tires");

                    b.Property<bool>("HasAllWheels")
                        .HasColumnType("boolean")
                        .HasColumnName("has_all_wheels");

                    b.Property<bool>("HasAlloyWheels")
                        .HasColumnType("boolean")
                        .HasColumnName("has_alloy_wheels");

                    b.Property<bool>("HasCompleteInterior")
                        .HasColumnType("boolean")
                        .HasColumnName("has_complete_interior");

                    b.Property<bool>("HasEngine")
                        .HasColumnType("boolean")
                        .HasColumnName("has_engine");

                    b.Property<bool>("HasKey")
                        .HasColumnType("boolean")
                        .HasColumnName("has_key");

                    b.Property<bool>("HasTitle")
                        .HasColumnType("boolean")
                        .HasColumnName("has_title");

                    b.Property<bool>("HasTransmission")
                        .HasColumnType("boolean")
                        .HasColumnName("has_transmission");

                    b.Property<bool>("ItMoves")
                        .HasColumnType("boolean")
                        .HasColumnName("it_moves");

                    b.Property<string>("Make")
                        .HasColumnType("text")
                        .HasColumnName("make");

                    b.Property<string>("Message")
                        .HasColumnType("text")
                        .HasColumnName("message");

                    b.Property<string>("Model")
                        .HasColumnType("text")
                        .HasColumnName("model");

                    b.Property<int?>("ModelStyleYearID")
                        .HasColumnType("integer")
                        .HasColumnName("model_style_year_id");

                    b.Property<int>("OfferedQuote")
                        .HasColumnType("integer")
                        .HasColumnName("offered_quote");

                    b.Property<bool>("RequiresPickup")
                        .HasColumnType("boolean")
                        .HasColumnName("requires_pickup");

                    b.Property<int>("SizeID")
                        .HasColumnType("integer")
                        .HasColumnName("size_id");

                    b.Property<string>("Year")
                        .HasColumnType("text")
                        .HasColumnName("year");

                    b.HasKey("ID")
                        .HasName("pk_quotes");

                    b.HasIndex("BodyTypeID")
                        .HasDatabaseName("ix_quotes_body_type_id");

                    b.HasIndex("ModelStyleYearID")
                        .HasDatabaseName("ix_quotes_model_style_year_id");

                    b.HasIndex("SizeID")
                        .HasDatabaseName("ix_quotes_size_id");

                    b.ToTable("quotes");
                });

            modelBuilder.Entity("VehicleQuotes.Models.QuoteOverride", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ModelStyleYearID")
                        .HasColumnType("integer")
                        .HasColumnName("model_style_year_id");

                    b.Property<int>("Price")
                        .HasColumnType("integer")
                        .HasColumnName("price");

                    b.HasKey("ID")
                        .HasName("pk_quote_overides");

                    b.HasIndex("ModelStyleYearID")
                        .IsUnique()
                        .HasDatabaseName("ix_quote_overides_model_style_year_id");

                    b.ToTable("quote_overides");
                });

            modelBuilder.Entity("VehicleQuotes.Models.QuoteRule", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FeatureType")
                        .HasColumnType("text")
                        .HasColumnName("feature_type");

                    b.Property<string>("FeatureValue")
                        .HasColumnType("text")
                        .HasColumnName("feature_value");

                    b.Property<int>("PriceModifier")
                        .HasColumnType("integer")
                        .HasColumnName("price_modifier");

                    b.HasKey("ID")
                        .HasName("pk_quote_rules");

                    b.HasIndex("FeatureType", "FeatureValue")
                        .IsUnique()
                        .HasDatabaseName("ix_quote_rules_feature_type_feature_value");

                    b.ToTable("quote_rules");
                });

            modelBuilder.Entity("VehicleQuotes.Models.Size", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("ID")
                        .HasName("pk_sizes");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_sizes_name");

                    b.ToTable("sizes");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Subcompact"
                        },
                        new
                        {
                            ID = 2,
                            Name = "Compact"
                        },
                        new
                        {
                            ID = 3,
                            Name = "Mid Size"
                        },
                        new
                        {
                            ID = 5,
                            Name = "Full Size"
                        });
                });

            modelBuilder.Entity("VehicleQuotes.Models.Model", b =>
                {
                    b.HasOne("VehicleQuotes.Models.Make", "Make")
                        .WithMany()
                        .HasForeignKey("MakeID")
                        .HasConstraintName("fk_models_makes_make_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Make");
                });

            modelBuilder.Entity("VehicleQuotes.Models.ModelStyle", b =>
                {
                    b.HasOne("VehicleQuotes.Models.BodyType", "BodyType")
                        .WithMany()
                        .HasForeignKey("BodyTypeID")
                        .HasConstraintName("fk_model_styles_body_types_body_type_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VehicleQuotes.Models.Model", "Model")
                        .WithMany("ModelStyles")
                        .HasForeignKey("ModelID")
                        .HasConstraintName("fk_model_styles_models_model_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VehicleQuotes.Models.Size", "Size")
                        .WithMany()
                        .HasForeignKey("SizeID")
                        .HasConstraintName("fk_model_styles_sizes_size_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BodyType");

                    b.Navigation("Model");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("VehicleQuotes.Models.ModelStyleYear", b =>
                {
                    b.HasOne("VehicleQuotes.Models.ModelStyle", "ModelStyle")
                        .WithMany("ModelStyleYears")
                        .HasForeignKey("ModelStyleID")
                        .HasConstraintName("fk_model_style_years_model_styles_model_style_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ModelStyle");
                });

            modelBuilder.Entity("VehicleQuotes.Models.Quote", b =>
                {
                    b.HasOne("VehicleQuotes.Models.BodyType", "BodyType")
                        .WithMany()
                        .HasForeignKey("BodyTypeID")
                        .HasConstraintName("fk_quotes_body_types_body_type_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VehicleQuotes.Models.ModelStyleYear", "ModelStyleYear")
                        .WithMany()
                        .HasForeignKey("ModelStyleYearID")
                        .HasConstraintName("fk_quotes_model_style_years_model_style_year_id");

                    b.HasOne("VehicleQuotes.Models.Size", "Size")
                        .WithMany()
                        .HasForeignKey("SizeID")
                        .HasConstraintName("fk_quotes_sizes_size_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BodyType");

                    b.Navigation("ModelStyleYear");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("VehicleQuotes.Models.QuoteOverride", b =>
                {
                    b.HasOne("VehicleQuotes.Models.ModelStyleYear", "ModelStyleYear")
                        .WithMany()
                        .HasForeignKey("ModelStyleYearID")
                        .HasConstraintName("fk_quote_overides_model_style_years_model_style_year_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ModelStyleYear");
                });

            modelBuilder.Entity("VehicleQuotes.Models.Model", b =>
                {
                    b.Navigation("ModelStyles");
                });

            modelBuilder.Entity("VehicleQuotes.Models.ModelStyle", b =>
                {
                    b.Navigation("ModelStyleYears");
                });
#pragma warning restore 612, 618
        }
    }
}
