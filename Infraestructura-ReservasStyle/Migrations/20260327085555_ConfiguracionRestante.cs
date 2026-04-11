using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infraestructura_ReservasStyle.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguracionRestante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comprobantes",
                columns: table => new
                {
                    IdComprobante = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPago = table.Column<int>(type: "integer", nullable: false),
                    Folio = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comprobantes", x => x.IdComprobante);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    IdEmpleado = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdLocal = table.Column<int>(type: "integer", nullable: false),
                    Especialidad = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Estado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.IdEmpleado);
                });

            migrationBuilder.CreateTable(
                name: "HorarioLocal",
                columns: table => new
                {
                    IdHorarioLocal = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdLocal = table.Column<int>(type: "integer", nullable: false),
                    DiaSemana = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    HoraAbierto = table.Column<TimeSpan>(type: "interval", nullable: false),
                    HoraCerrado = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioLocal", x => x.IdHorarioLocal);
                });

            migrationBuilder.CreateTable(
                name: "HorariosDisponibles",
                columns: table => new
                {
                    IdHorario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdEmpleado = table.Column<int>(type: "integer", nullable: false),
                    DiaSemana = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    HoraFin = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorariosDisponibles", x => x.IdHorario);
                });

            migrationBuilder.CreateTable(
                name: "Locales",
                columns: table => new
                {
                    IdLocal = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Direccion = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Cuidad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CodigoPostal = table.Column<string>(type: "text", nullable: true),
                    Telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    EstadoActivo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locales", x => x.IdLocal);
                });

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    IdNotificacion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    Mensaje = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Leida = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.IdNotificacion);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    IdPago = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdCita = table.Column<int>(type: "integer", nullable: false),
                    Precio = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    MetodoPago = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    FechaPago = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    EstadoPago = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true, defaultValue: "Pendiente"),
                    ReferenciaTransaccion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.IdPago);
                });

            migrationBuilder.CreateTable(
                name: "Promociones",
                columns: table => new
                {
                    IdPromocion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    PorcentajeDescuento = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promociones", x => x.IdPromocion);
                });

            migrationBuilder.CreateTable(
                name: "ServicioLocal",
                columns: table => new
                {
                    IdServicioLocal = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdServicio = table.Column<int>(type: "integer", nullable: false),
                    IdLocal = table.Column<int>(type: "integer", nullable: false),
                    Precio = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicioLocal", x => x.IdServicioLocal);
                });

            migrationBuilder.CreateTable(
                name: "ServicioPromocion",
                columns: table => new
                {
                    IdServicioLocal = table.Column<int>(type: "integer", nullable: false),
                    IdPromocion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicioPromocion", x => new { x.IdServicioLocal, x.IdPromocion });
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    IdServicio = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Duracion = table.Column<int>(type: "integer", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Estado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.IdServicio);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ContraseñaHash = table.Column<string>(type: "text", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Estado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    IdRol = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "citas",
                columns: table => new
                {
                    IdCita = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdCliente = table.Column<int>(type: "integer", nullable: false),
                    IdEmpleado = table.Column<int>(type: "integer", nullable: false),
                    IdServicioLocal = table.Column<int>(type: "integer", nullable: false),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    hora_inicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    hora_fin = table.Column<TimeSpan>(type: "interval", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_citas", x => x.IdCita);
                    table.ForeignKey(
                        name: "FK_citas_Empleados_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleados",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_citas_ServicioLocal_IdServicioLocal",
                        column: x => x.IdServicioLocal,
                        principalTable: "ServicioLocal",
                        principalColumn: "IdServicioLocal",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_citas_Usuarios_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_citas_IdCliente",
                table: "citas",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_citas_IdEmpleado",
                table: "citas",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_citas_IdServicioLocal",
                table: "citas",
                column: "IdServicioLocal");

            migrationBuilder.CreateIndex(
                name: "IX_Comprobantes_Folio",
                table: "Comprobantes",
                column: "Folio",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comprobantes_IdPago",
                table: "Comprobantes",
                column: "IdPago");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_IdLocal",
                table: "Empleados",
                column: "IdLocal");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_IdUsuario",
                table: "Empleados",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_HorarioLocal_IdLocal",
                table: "HorarioLocal",
                column: "IdLocal");

            migrationBuilder.CreateIndex(
                name: "IX_HorariosDisponibles_IdEmpleado",
                table: "HorariosDisponibles",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_IdUsuario",
                table: "Notificaciones",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_IdCita",
                table: "Pagos",
                column: "IdCita");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioLocal_IdServicio_IdLocal",
                table: "ServicioLocal",
                columns: new[] { "IdServicio", "IdLocal" });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "citas");

            migrationBuilder.DropTable(
                name: "Comprobantes");

            migrationBuilder.DropTable(
                name: "HorarioLocal");

            migrationBuilder.DropTable(
                name: "HorariosDisponibles");

            migrationBuilder.DropTable(
                name: "Locales");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Promociones");

            migrationBuilder.DropTable(
                name: "ServicioPromocion");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "ServicioLocal");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
