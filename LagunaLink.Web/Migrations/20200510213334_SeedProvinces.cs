using Microsoft.EntityFrameworkCore.Migrations;

namespace LagunaLink.Web.Migrations
{
    public partial class SeedProvinces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO dbo.Provinces (Name) VALUES ('Álava')," +
                "('Albacete')," +
                "('Alicante')," +
                "('Almería')," +
                "('Ávila')," +
                "('Badajoz')," +
                "('Baleares (Illes)')," +
                "('Barcelona')," +
                "('Burgos')," +
                "('Cáceres')," +
                "('Cádiz')," +
                "('Castellón')," +
                "('Ciudad Real')," +
                "('Córdoba')," +
                "('A Coruña')," +
                "('Cuenca')," +
                "('Girona')," +
                "('Granada')," +
                "('Guadalajara')," +
                "('Guipúzcoa')," +
                "('Huelva')," +
                "('Huesca')," +
                "('Jaén')," +
                "('León')," +
                "('Lleida')," +
                "('La Rioja')," +
                "('Lugo')," +
                "('Madrid')," +
                "('Málaga')," +
                "('Murcia')," +
                "('Navarra')," +
                "('Ourense')," +
                "('Asturias')," +
                "('Palencia')," +
                "('Las Palmas')," +
                "('Pontevedra')," +
                "('Salamanca')," +
                "('Santa Cruz de Tenerife')," +
                "('Cantabria')," +
                "('Segovia')," +
                "('Sevilla')," +
                "('Soria')," +
                "('Tarragona')," +
                "('Teruel')," +
                "('Toledo')," +
                "('Valencia')," +
                "('Valladolid')," +
                "('Vizcaya')," +
                "('Zamora')," +
                "('Zaragoza')," +
                "('Ceuta')," +
                "('Melilla');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
