using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SEI_APP.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DocumentType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Document = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "date", maxLength: 256, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    Bank = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    TypeAccount = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    NumberAccount = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });




            migrationBuilder.CreateTable(
               name: "Departamento",
               columns: table => new
               {
                   IdDepartamento = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                   CodigoDane = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                   Nombre = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                   FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                   UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
                   UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_IdDepartamento", x => x.IdDepartamento);


               });

            migrationBuilder.CreateTable(
               name: "Municipio",
               columns: table => new
               {
                   IdMunicipio = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                   CodigoDane = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                   Nombre = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                   UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                   FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
                   IdDepartamento = table.Column<int>(type: "int", nullable: false),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_IdMunicipio", x => x.IdMunicipio);
                   table.ForeignKey(
                       name: "FK_Municipio_Departamento_IdDepartammento",
                       column: x => x.IdDepartamento,
                       principalTable: "Departamento",
                       principalColumn: "IdDepartamento",
                       onDelete: ReferentialAction.NoAction);
               });

            migrationBuilder.CreateTable(
            name: "Barrio",
            columns: table => new
            {
                IdBarrio = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                CodigoDane = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                Nombre = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
                UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IdMunicipio = table.Column<int>(type: "int", nullable: false),
            },
                constraints: table =>
                     {
                         table.PrimaryKey("PK_IdBarrio", x => x.IdBarrio);
                         table.ForeignKey(
                         name: "FK_Barrio_Municipio_IdMunicipio",
                         column: x => x.IdMunicipio,
                         principalTable: "Municipio",
                         principalColumn: "IdMunicipio",
                         onDelete: ReferentialAction.NoAction);
                     });


            migrationBuilder.CreateTable(
            name: "TipoGarantiaProducto",
            columns: table => new
            {
                IdTipoGarantiaProducto = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                Nombre = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
                FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),

            },
              constraints: table =>
               {
                   table.PrimaryKey("PK_IdTipoGarantiaProducto", x => x.IdTipoGarantiaProducto);

               });

            migrationBuilder.CreateTable(
            name: "TipoMaterialProducto",
            columns: table => new
            {
                IdTipoMaterialProducto = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                Nombre = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
                FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
            },
               constraints: table =>
               {
                   table.PrimaryKey("PK_IdTipoMaterialProducto", x => x.IdTipoMaterialProducto);

               });


            migrationBuilder.CreateTable(
            name: "CaracterizacionProducto",
            columns: table => new
            {
                IdCaracterizacionProducto = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                Marca = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                Modelo = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                Condicion = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                Ancho = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                Alto = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
                FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IdTipoMaterialProducto = table.Column<int>(type: "int", nullable: false),
                IdTipoGarantiaProducto = table.Column<int>(type: "int", nullable: false),
                EnvioGratis = table.Column<string>(type: "nvarchar(max)", nullable: true),

            },
              constraints: table =>
              {
                  table.PrimaryKey("PK_IdCaracterizacionProducto", x => x.IdCaracterizacionProducto);
                  table.ForeignKey(
                   name: "FK_CaracterizacionProducto_TipoMaterialProducto_IdTipoMaterialProducto",
                   column: x => x.IdTipoMaterialProducto,
                   principalTable: "TipoMaterialProducto",
                   principalColumn: "IdTipoMaterialProducto",
                   onDelete: ReferentialAction.NoAction);
                  table.ForeignKey(
                   name: "FK_CaracterizacionProducto_TipoGarantiaProducto_IdTipoGarantiaProducto",
                   column: x => x.IdTipoGarantiaProducto,
                   principalTable: "TipoGarantiaProducto",
                   principalColumn: "IdTipoGarantiaProducto",
                   onDelete: ReferentialAction.NoAction);
              });


            migrationBuilder.CreateTable(
            name: "TipoCategoriaProducto",
            columns: table => new
            {
                IdTipoCategoriaProducto = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                Nombre = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                Activo = table.Column<int>(type: "int", nullable: false),
                UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),

            },
          constraints: table =>
          {
              table.PrimaryKey("PK_IdTipoCategoriaProducto", x => x.IdTipoCategoriaProducto);
          });


            migrationBuilder.CreateTable(
            name: "CategoriaProducto",
            columns: table => new
            {
                IdCategoriaProducto = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                Nombre = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                Activo = table.Column<int>(type: "int", nullable: false),
                UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
                UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IdTipoCategoriaProducto = table.Column<int>(type: "int", nullable: false),

            },
        constraints: table =>
        {
            table.PrimaryKey("PK_IdCategoriaProducto", x => x.IdCategoriaProducto);
            table.ForeignKey(
             name: "FK_TipoCategoriaProducto_IdTipoCategoriaProducto ",
             column: x => x.IdTipoCategoriaProducto,
             principalTable: "TipoCategoriaProducto",
             principalColumn: "IdTipoCategoriaProducto",
             onDelete: ReferentialAction.NoAction);
        });

            migrationBuilder.CreateTable(
          name: "TipoProducto",
          columns: table => new
          {
              IdTipoProducto = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
              NombreTiipoProducto = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
              Activo = table.Column<int>(type: "int", nullable: false),
              FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
              UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
              UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
              FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
              IdCategoriaProducto = table.Column<int>(type: "int", nullable: false),

          },
      constraints: table =>
      {
          table.PrimaryKey("PK_IdTipoProducto", x => x.IdTipoProducto);
          table.ForeignKey(
           name: "FK_CategoriaProducto_IdCategoriaProducto ",
           column: x => x.IdCategoriaProducto,
           principalTable: "CategoriaProducto",
           principalColumn: "IdCategoriaProducto",
           onDelete: ReferentialAction.NoAction);
      });

            migrationBuilder.CreateTable(
         name: "Localizacion",
         columns: table => new
         {
             IdLocalizacion = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
             Direccion = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
             CodigoPostal = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
             DatosAdicionales = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
             FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
             FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
             UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
             UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
             IdMunicipio = table.Column<int>(type: "int", nullable: false),
             IdBarrio = table.Column<int>(type: "int", nullable: false),
             Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
             TelefonoOpc = table.Column<string>(type: "nvarchar(max)", nullable: true),
             Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
             Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
         },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdLocalizacion", x => x.IdLocalizacion);
                table.ForeignKey(
                  name: "FK_Localizacion_Municipio_IdMunicipio",
                  column: x => x.IdMunicipio,
                  principalTable: "Municipio",
                  principalColumn: "IdMunicipio",
                  onDelete: ReferentialAction.NoAction);
                table.ForeignKey(
                  name: "FK_Localizacion_Barrio_IdBarrio",
                  column: x => x.IdBarrio,
                  principalTable: "Barrio",
                  principalColumn: "IdBarrio",
                  onDelete: ReferentialAction.NoAction);
            });


            migrationBuilder.CreateTable(
            name: "EstadoProductoServicio",
            columns: table => new
            {
                IdEstadoProductoServicio = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                Activo = table.Column<int>(type: "int", nullable: false),
                Nombre = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),

            },


            constraints: table =>
            {
                table.PrimaryKey("PK_IdEstadoProductoServicio", x => x.IdEstadoProductoServicio);

            });

            migrationBuilder.CreateTable(
          name: "CaracterizacionServicio",
          columns: table => new
          {
              IdCaracterizacionServicio = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
              Experiencia = table.Column<int>(type: "int", nullable: false),
              Incluye = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
              NoIncluye = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
              FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
              FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
              UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
              UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),


          },
             constraints: table =>
            {
                table.PrimaryKey("PK_IdCaracterizacionServicio", x => x.IdCaracterizacionServicio);

            });


            migrationBuilder.CreateTable(
            name: "TipoCategoriaServicio",
            columns: table => new
            {
                IdTipoCategoriaServicio = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                Nombre = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                Activo = table.Column<int>(type: "int", nullable: false),
                UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
                UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),

            },
        constraints: table =>
        {
            table.PrimaryKey("PK_IdTipoCategoriaServicio", x => x.IdTipoCategoriaServicio);
        });

            migrationBuilder.CreateTable(
            name: "CategoriaServicio",
            columns: table => new
            {
                IdCategoriaServicio = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                Nombre = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                Activo = table.Column<int>(type: "int", nullable: false),
                UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
                UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IdTipoCategoriaServicio = table.Column<int>(type: "int", nullable: false),
            },
      constraints: table =>
      {
          table.PrimaryKey("PK_IdCategoriaServicio", x => x.IdCategoriaServicio);
          table.ForeignKey(
           name: "FK_CategoriaServicio_IdTipoCategoriaServicio",
           column: x => x.IdTipoCategoriaServicio,
           principalTable: "TipoCategoriaServicio",
           principalColumn: "IdTipoCategoriaServicio",
           onDelete: ReferentialAction.NoAction);
      });

            migrationBuilder.CreateTable(
           name: "TipoServicio",
          columns: table => new
          {
              IdTipoServicio = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
              Nombre = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
              Activo = table.Column<int>(type: "int", nullable: false),
              FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
              UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
              UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
              FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
              IdCategoriaServicio = table.Column<int>(type: "int", nullable: false),

          },
             constraints: table =>
        {
            table.PrimaryKey("PK_IdTipoServicio", x => x.IdTipoServicio);
            table.ForeignKey(
              name: "FK_TipoServicio_IdCategoriaServicio",
              column: x => x.IdCategoriaServicio,
              principalTable: "CategoriaServicio",
              principalColumn: "IdCategoriaServicio",
              onDelete: ReferentialAction.NoAction);
        });


            migrationBuilder.CreateTable(
              name: "Servicio",
              columns: table => new
              {
                  IdServicio = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                  Descripcion = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                  NombreServicio = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                  FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                  UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
                  AplicaConvenio = table.Column<bool>(type: "bit", nullable: false),
                  CostoServicio = table.Column<float>(type: "float", nullable: false),
                  IdTipoServicio = table.Column<int>(type: "int", nullable: false),
                  IdCaracterizacionServicio = table.Column<int>(type: "int", nullable: false),
                  IdEstadoProductoServicio = table.Column<int>(type: "int", nullable: false),
                  IdLocalizacion = table.Column<int>(type: "int", nullable: false),
                  Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_IdServicio", x => x.IdServicio);
                  table.ForeignKey(
                   name: "FK_Servicio_TipoServicio_IdTipoServicio",
                   column: x => x.IdTipoServicio,
                   principalTable: "TipoServicio",
                   principalColumn: "IdTipoServicio",
                   onDelete: ReferentialAction.NoAction);
                  table.ForeignKey(
                    name: "FK_Servicio_CaracterizacionServicio_IdCaracterizacionServicio",
                    column: x => x.IdCaracterizacionServicio,
                    principalTable: "CaracterizacionServicio",
                    principalColumn: "IdCaracterizacionServicio",
                    onDelete: ReferentialAction.NoAction);
                  table.ForeignKey(
                   name: "FK_Servicio_EstadoProductoServicio_IdEstadoProductoServicio",
                   column: x => x.IdEstadoProductoServicio,
                   principalTable: "EstadoProductoServicio",
                   principalColumn: "IdEstadoProductoServicio",
                   onDelete: ReferentialAction.NoAction);
                  table.ForeignKey(
                   name: "FK_Servicio_Localizacion_IdLocalizacion",
                   column: x => x.IdLocalizacion,
                   principalTable: "Localizacion",
                   principalColumn: "IdLocalizacion",
                   onDelete: ReferentialAction.NoAction);
              });


            migrationBuilder.CreateTable(
            name: "Producto",
            columns: table => new
            {
                IdProducto = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                Descripcion = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NombreProducto = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
                CostoProducto = table.Column<float>(type: "float", nullable: false),
                UnidadesProducto = table.Column<float>(type: "float", nullable: false),
                IdTipoProducto = table.Column<int>(type: "int", nullable: false),
                IdCaracterizacionProducto = table.Column<int>(type: "int", nullable: false),
                IdEstadoProductoServicio = table.Column<int>(type: "int", nullable: false),
                IdLocalizacion = table.Column<int>(type: "int", nullable: false),
                Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true)
                
            },

            constraints: table =>
            {
                table.PrimaryKey("PK_IdProducto", x => x.IdProducto);
                table.ForeignKey(
                  name: "FK_Producto_IdTipoProducto",
                  column: x => x.IdTipoProducto,
                  principalTable: "TipoProducto",
                  principalColumn: "IdTipoProducto",
                  onDelete: ReferentialAction.NoAction);
                table.ForeignKey(
                 name: "FK_Producto_CaracterizacionProducto_IdCaraterizacionproducto",
                 column: x => x.IdCaracterizacionProducto,
                 principalTable: "CaracterizacionProducto",
                 principalColumn: "IdCaracterizacionProducto",
                 onDelete: ReferentialAction.NoAction);
                table.ForeignKey(
                  name: "FK_Producto_EstadoProductoServicio_IdEstadoProductoServicio",
                  column: x => x.IdEstadoProductoServicio,
                  principalTable: "EstadoProductoServicio",
                  principalColumn: "IdEstadoProductoServicio",
                  onDelete: ReferentialAction.NoAction);
                table.ForeignKey(
                   name: "FK_Producto_Localizacion_IdLocalizacion",
                   column: x => x.IdLocalizacion,
                   principalTable: "Localizacion",
                   principalColumn: "IdLocalizacion",
                   onDelete: ReferentialAction.NoAction);
            });


            migrationBuilder.CreateTable(
            name: "PublicacionProductoServicio",
            columns: table => new
            {
                IdPublicacionProductoServicio = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
                FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdPublicacionProductoServicio", x => x.IdPublicacionProductoServicio);

            });

            migrationBuilder.CreateTable(
            name: "TipoDocumentoIdentidad",
            columns: table => new
            {
                IdTipoDocumentoIdentidad = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Activo = table.Column<int>(type: "int", nullable: false),
                FechaCreacion = table.Column<string>(type: "date", nullable: true),
                FechaModificacion = table.Column<string>(type: "date", nullable: true),
                UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdTipoDocumentoIdentidad", x => x.IdTipoDocumentoIdentidad);

            });

            migrationBuilder.CreateTable(
            name: "EstadoVenta",
            columns: table => new
            {
                IdEstadoVenta = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Activo = table.Column<int>(type: "int", nullable: false),
                FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
                FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdEstadoVenta", x => x.IdEstadoVenta);

            });

            migrationBuilder.CreateTable(
            name: "TipoPago",
            columns: table => new
            {
                IdTipoPago = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Activo = table.Column<int>(type: "int", nullable: false),
                FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
                FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdTipoPago", x => x.IdTipoPago);

            });

            migrationBuilder.CreateTable(
                name: "CalificacionServicio",
                columns: table => new
                {
                    IdCalificacionServicio = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    Calificacion = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdUsuarioCliente = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdUsuarioCalificado = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdServicio = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdCalificacionServicio", x => x.IdCalificacionServicio);
                    table.ForeignKey(
                      name: "FK_CalificacionServicio_IdUsuarioCliente",
                      column: x => x.IdUsuarioCliente,
                      principalTable: "AspNetUsers",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                         name: "FK_CalificacionServicio_IdUsuarioCalificado",
                         column: x => x.IdUsuarioCalificado,
                         principalTable: "AspNetUsers",
                         principalColumn: "Id",
                         onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                         name: "FK_CalificacionServicio_IdServicio",
                         column: x => x.IdServicio,
                         principalTable: "Servicio",
                         principalColumn: "IdServicio",
                         onDelete: ReferentialAction.NoAction);
          });

            migrationBuilder.CreateTable(
            name: "CalificacionProducto",
            columns: table => new
            {
                IdCalificacionProducto = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                Calificacion = table.Column<int>(type: "int", nullable: false),
                Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
                FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IdUsuarioCliente = table.Column<string>(type: "nvarchar(450)", nullable: false),
                IdUsuarioCalificado = table.Column<string>(type: "nvarchar(450)", nullable: false),
                IdProducto = table.Column<int>(type: "int", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdCalificacionProducto", x => x.IdCalificacionProducto);
                table.ForeignKey(
                  name: "FK_CalificacionProducto_IdUsuarioCliente",
                  column: x => x.IdUsuarioCliente,
                  principalTable: "AspNetUsers",
                  principalColumn: "Id",
                  onDelete: ReferentialAction.NoAction);
                table.ForeignKey(
                     name: "FK_CalificacionProducto_IdUsuarioCalificado",
                     column: x => x.IdUsuarioCalificado,
                     principalTable: "AspNetUsers",
                     principalColumn: "Id",
                     onDelete: ReferentialAction.NoAction);
                table.ForeignKey(
                     name: "FK_CalificacionProducto_IdProducto",
                     column: x => x.IdProducto,
                     principalTable: "Producto",
                     principalColumn: "IdProducto",
                     onDelete: ReferentialAction.NoAction);
            });

            migrationBuilder.CreateTable(
            name: "MotivoFinalizacionServicio",
            columns: table => new
            {
                IdMotivoFinalizacionServicio = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Activo = table.Column<int>(type: "int", nullable: false),
                FechaCreacion = table.Column<string>(type: "date", nullable: true),
                FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                UsuarioModificacion = table.Column<DateTime>(type: "nvarchar(max)", nullable: true),
                UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdMotivoFinalizacionServicio", x => x.IdMotivoFinalizacionServicio);

            });

            migrationBuilder.CreateTable(
            name: "VentasServicios",
            columns: table => new
            {
                IdVentasServicios = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                ValoTotal = table.Column<float>(type: "float", nullable: true),
                FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
                FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IdServicio = table.Column<int>(type: "int", nullable: false),
                IdUsuarioComprador = table.Column<string>(type: "nvarchar(450)", nullable: false),
                IdUsuarioVendedor = table.Column<string>(type: "nvarchar(450)", nullable: false),
                IdTipoPago = table.Column<int>(type: "int", nullable: false),
                IdEstadoVenta = table.Column<int>(type: "int", nullable: false),
                IdMotivoFinalizacionServicio = table.Column<int>(type: "int", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdVentasServicios", x => x.IdVentasServicios);
                table.ForeignKey(
                      name: "FK_VentasServicios_IdServicio",
                      column: x => x.IdServicio,
                      principalTable: "Servicio",
                      principalColumn: "IdServicio",
                      onDelete: ReferentialAction.NoAction);
                table.ForeignKey(
                     name: "FK_VentasServicios_IdUsuarioComprador",
                     column: x => x.IdUsuarioComprador,
                     principalTable: "AspNetUsers",
                     principalColumn: "Id",
                     onDelete: ReferentialAction.NoAction);
                table.ForeignKey(
                     name: "FK_VentasServicios_IdUsuarioVendedor",
                     column: x => x.IdUsuarioVendedor,
                     principalTable: "AspNetUsers",
                     principalColumn: "Id",
                     onDelete: ReferentialAction.NoAction);
                table.ForeignKey(
                     name: "FK_VentasServicios_IdTipoPago",
                     column: x => x.IdTipoPago,
                     principalTable: "TipoPago",
                     principalColumn: "IdTipoPago",
                     onDelete: ReferentialAction.NoAction);
                table.ForeignKey(
                     name: "FK_VentasServicios_IdEstadoVenta",
                     column: x => x.IdEstadoVenta,
                     principalTable: "EstadoVenta",
                     principalColumn: "IdEstadoVenta",
                     onDelete: ReferentialAction.NoAction);
                table.ForeignKey(
                     name: "FK_VentasServicios_IdMotivoFinalizacionServicio",
                     column: x => x.IdMotivoFinalizacionServicio,
                     principalTable: "MotivoFinalizacionServicio",
                     principalColumn: "IdMotivoFinalizacionServicio",
                     onDelete: ReferentialAction.NoAction);
            });

            migrationBuilder.CreateTable(
            name: "AdjuntosVentasServicios",
            columns: table => new
            {
                IdAdjuntosVentasServicios = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                NombreDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Documento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Activo = table.Column<int>(type: "int", nullable: false),
                FechaCreacion = table.Column<string>(type: "date", nullable: true),
                FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                UsuarioModificacion = table.Column<DateTime>(type: "nvarchar(max)", nullable: true),
                UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IdVentasServicios = table.Column<int>(type: "int", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdAdjuntosVentasServicios", x => x.IdAdjuntosVentasServicios);
                table.ForeignKey(
                 name: "FK_AdjuntosVentasServicios_IdVentasServicios",
                 column: x => x.IdVentasServicios,
                 principalTable: "VentasServicios",
                 principalColumn: "IdVentasServicios",
                 onDelete: ReferentialAction.NoAction);

            });

            migrationBuilder.CreateTable(
            name: "MensajesVentaServicio",
            columns: table => new
            {
                IdMensajesVentaServicio = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                FechaCreacion = table.Column<string>(type: "date", nullable: false),
                UsuarioVendedor = table.Column<DateTime>(type: "nvarchar(max)", nullable: false),
                UsuarioComprador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                IdVentasServicios = table.Column<int>(type: "int", nullable: false),
                IdMensajeRespuesta = table.Column<int>(type: "int", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdMensajesVentaServicio", x => x.IdMensajesVentaServicio);
                table.ForeignKey(
                 name: "FK_IdMensajesVentaServicio_IdVentasServicios",
                 column: x => x.IdVentasServicios,
                 principalTable: "VentasServicios",
                 principalColumn: "IdVentasServicios",
                 onDelete: ReferentialAction.NoAction);
            });

            migrationBuilder.CreateTable(
            name: "NotificacionMasiva",
            columns: table => new
            {
                IdNotificacionMasiva = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                FechaMensaje = table.Column<DateTime>(type: "datetime", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdNotificacionMasiva", x => x.IdNotificacionMasiva);
            });

            migrationBuilder.CreateTable(
            name: "VentasProductos",
            columns: table => new
            {
                IdVentasProductos = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                UnidadesCompradas = table.Column<float>(type: "float", nullable: true),
                ValorPorUnidad = table.Column<float>(type: "float", nullable: true),
                ValorTotal = table.Column<float>(type: "float", nullable: false),
                FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
                FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IdProducto = table.Column<int>(type: "int", nullable: false),
                IdUsuarioComprador = table.Column<string>(type: "nvarchar(450)", nullable: false),
                IdUsuarioVendedor = table.Column<string>(type: "nvarchar(450)", nullable: false),
                IdTipoPago = table.Column<int>(type: "int", nullable: false),
                IdEstadoVenta = table.Column<int>(type: "int", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdVentasProductos", x => x.IdVentasProductos);
                table.ForeignKey(
                 name: "FK_VentasProductos_Producto_IdProducto",
                 column: x => x.IdProducto,
                 principalTable: "Producto",
                 principalColumn: "IdProducto",
                 onDelete: ReferentialAction.NoAction);

                table.ForeignKey(
                     name: "FK_VentasProductos_Usuario_IdUsuarioComprador",
                     column: x => x.IdUsuarioComprador,
                     principalTable: "AspNetUsers",
                     principalColumn: "Id",
                     onDelete: ReferentialAction.NoAction);

                table.ForeignKey(
                     name: "FK_VentasProductos_Usuario_IdUsuarioVendedor",
                     column: x => x.IdUsuarioVendedor,
                     principalTable: "AspNetUsers",
                     principalColumn: "Id",
                     onDelete: ReferentialAction.NoAction);

                table.ForeignKey(
                     name: "FK_VentasProductos_TipoPago_IdTipoPago",
                     column: x => x.IdTipoPago,
                     principalTable: "TipoPago",
                     principalColumn: "IdTipoPago",
                     onDelete: ReferentialAction.NoAction);

                table.ForeignKey(
                     name: "FK_VentasProductos_EstadoVenta_IdEstadoVenta",
                     column: x => x.IdEstadoVenta,
                     principalTable: "EstadoVenta",
                     principalColumn: "IdEstadoVenta",
                     onDelete: ReferentialAction.NoAction);
            });


            migrationBuilder.CreateTable(
            name: "Envios",
            columns: table => new
            {
                IdEnvios = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                NombreCompradors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                DireccionEnvio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DatosAdicionales = table.Column<string>(type: "nvarchar(max)", nullable: true),
                EnvioGratis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                FechaCreacion = table.Column<DateTime>(type: "date", nullable: true),
                FechaModificacion = table.Column<DateTime>(type: "date", nullable: true),
                UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IdVentasProductos = table.Column<int>(type: "int", nullable: false),
                IdUsuarioComprador = table.Column<string>(type: "nvarchar(450)", nullable: false),
                IdMunicipio = table.Column<int>(type: "int", nullable: true),
                IdBarrio = table.Column<int>(type: "int", nullable: true),
            },

            constraints: table =>
            {
                table.PrimaryKey("PK_IdEnvios", x => x.IdEnvios);
                table.ForeignKey(
                 name: "FK_Envios_VentasProducto_IdVentasProducto",
                 column: x => x.IdVentasProductos,
                 principalTable: "VentasProductos",
                 principalColumn: "IdVentasProductos",
                 onDelete: ReferentialAction.NoAction);

                table.ForeignKey(
                 name: "FK_Envios_Usuario_IdUsuarioComprador",
                 column: x => x.IdUsuarioComprador,
                 principalTable: "AspNetUsers",
                 principalColumn: "Id",
                 onDelete: ReferentialAction.NoAction);


                table.ForeignKey(
                 name: "FK_Envios_Barrio_IdBarrio",
                 column: x => x.IdBarrio,
                 principalTable: "Barrio",
                 principalColumn: "IdBarrio",
                 onDelete: ReferentialAction.NoAction);

                table.ForeignKey(
                 name: "FK_Envios_Municipio_IdMunicipio",
                 column: x => x.IdMunicipio,
                 principalTable: "Municipio",
                 principalColumn: "IdMunicipio",
                 onDelete: ReferentialAction.NoAction);
            });



            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
              name: "Servicio");

            migrationBuilder.DropTable(
              name: "Producto");

            migrationBuilder.DropTable(
              name: "EstadoProductoServicio");

            migrationBuilder.DropTable(
              name: "TipoCategoriaProducto");

            migrationBuilder.DropTable(
              name: "CategoriaProducto");

            migrationBuilder.DropTable(
              name: "TipoProducto");

            migrationBuilder.DropTable(
              name: "CaracterizacionProducto");

            migrationBuilder.DropTable(
              name: "TipoMaterialProducto");

            migrationBuilder.DropTable(
              name: "TipoGarantiaProducto");

            migrationBuilder.DropTable(
              name: "TipoCategoriaServicio");

            migrationBuilder.DropTable(
              name: "CategoriaServicio");

            migrationBuilder.DropTable(
              name: "TipoServicio");

            migrationBuilder.DropTable(
              name: "CaracterizacionServicio");

            migrationBuilder.DropTable(
              name: "Localizacion");

            migrationBuilder.DropTable(
              name: "PublicacionProductoServicio");

            migrationBuilder.DropTable(
              name: "Departamento");

            migrationBuilder.DropTable(
             name: "Municipio");

            migrationBuilder.DropTable(
              name: "Barrio");
            migrationBuilder.DropTable(
              name: "NotificacionMasiva");

            migrationBuilder.DropTable(
              name: "TipoDocumentoIdentidad");

            migrationBuilder.DropTable(
              name: "EstadoVenta");

            migrationBuilder.DropTable(
              name: "TipoPago");

            migrationBuilder.DropTable(
              name: "CalificacionProducto");

            migrationBuilder.DropTable(
              name: "CalificacionServicio");

            migrationBuilder.DropTable(
              name: "MotivoFinalizacionServicio");

            migrationBuilder.DropTable(
              name: "VentasServicios");

            migrationBuilder.DropTable(
              name: "AdjuntosVentasServicios");

            migrationBuilder.DropTable(
             name: "VentasProductos");

            migrationBuilder.DropTable(
             name: "Envios"); 

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

        }
    }
}
