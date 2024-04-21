using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restoran.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Musterija",
                columns: table => new
                {
                    MusterijaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImeMusterije = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrezimeMusterije = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdresaMusterije = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradMusterije = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KontaktMusterije = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musterija", x => x.MusterijaID);
                });

            migrationBuilder.CreateTable(
                name: "Porudzbina",
                columns: table => new
                {
                    PorudzbinaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IznosPorudzbine = table.Column<double>(type: "float", nullable: true),
                    NacinPlacanja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DodeljeniSto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusPorudzbine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MusterijaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZaposleniID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porudzbina", x => x.PorudzbinaID);
                });

            migrationBuilder.CreateTable(
                name: "Proizvod",
                columns: table => new
                {
                    ProizvodID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Hrana = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NazivProizvoda = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cena = table.Column<double>(type: "float", nullable: true),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvod", x => x.ProizvodID);
                });

            migrationBuilder.CreateTable(
                name: "StavkaPorudzbine",
                columns: table => new
                {
                    StavkaPorudzbineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PorudzbinaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProizvodID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkaPorudzbine", x => x.StavkaPorudzbineID);
                });

            migrationBuilder.CreateTable(
                name: "Zaposleni",
                columns: table => new
                {
                    ZaposleniID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImeZaposlenog = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrezimeZaposlenog = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdresaZaposlenog = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradZaposlenog = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KontaktZaposlenog = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposleni", x => x.ZaposleniID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Musterija");

            migrationBuilder.DropTable(
                name: "Porudzbina");

            migrationBuilder.DropTable(
                name: "Proizvod");

            migrationBuilder.DropTable(
                name: "StavkaPorudzbine");

            migrationBuilder.DropTable(
                name: "Zaposleni");
        }
    }
}
