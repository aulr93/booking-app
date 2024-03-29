﻿// <auto-generated />
using System;
using Booking.Presistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Booking.Presistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231110024627_InitializeDB")]
    partial class InitializeDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Booking.Domain.Entities.HotelRoom", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Floor")
                        .HasColumnType("integer");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<string>("RoomNumber")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserIn")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("UserUp")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("DateIn");

                    b.HasIndex("DateUp");

                    b.HasIndex("UserIn");

                    b.HasIndex("UserUp");

                    b.ToTable("msHotelRoom", (string)null);
                });

            modelBuilder.Entity("Booking.Domain.Entities.Masters.Administrator", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("UserIn")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("UserUp")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DateIn");

                    b.HasIndex("DateUp");

                    b.HasIndex("UserIn");

                    b.HasIndex("UserUp");

                    b.ToTable("msAdministrator", (string)null);
                });

            modelBuilder.Entity("Booking.Domain.Entities.Masters.Visitor", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NIK")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("UserIn")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("UserUp")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DateIn");

                    b.HasIndex("DateUp");

                    b.HasIndex("UserIn");

                    b.HasIndex("UserUp");

                    b.ToTable("msVisitor", (string)null);
                });

            modelBuilder.Entity("Booking.Domain.Entities.Transactions.HotelRoomBooking", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime?>("ActualCheckInDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ActualCheckOutDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserIn")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("UserUp")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("VisitorId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DateIn");

                    b.HasIndex("DateUp");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserIn");

                    b.HasIndex("UserUp");

                    b.HasIndex("VisitorId");

                    b.ToTable("trHotelRoomBooking", (string)null);
                });

            modelBuilder.Entity("Booking.Domain.Entities.Transactions.HotelRoomBooking", b =>
                {
                    b.HasOne("Booking.Domain.Entities.HotelRoom", "HotelRoom")
                        .WithMany("HotelRoomBookings")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Booking.Domain.Entities.Masters.Visitor", "Visitor")
                        .WithMany("HotelRoomBookings")
                        .HasForeignKey("VisitorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HotelRoom");

                    b.Navigation("Visitor");
                });

            modelBuilder.Entity("Booking.Domain.Entities.HotelRoom", b =>
                {
                    b.Navigation("HotelRoomBookings");
                });

            modelBuilder.Entity("Booking.Domain.Entities.Masters.Visitor", b =>
                {
                    b.Navigation("HotelRoomBookings");
                });
#pragma warning restore 612, 618
        }
    }
}
