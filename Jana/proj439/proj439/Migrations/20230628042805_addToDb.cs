using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proj439.Migrations
{
    /// <inheritdoc />
    public partial class addToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORY",
                columns: table => new
                {
                    CATEGORY_ID = table.Column<int>(type: "int", nullable: false),
                    TYPE = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORY", x => x.CATEGORY_ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "PERMISSIONS",
                columns: table => new
                {
                    PERMISSIONID = table.Column<int>(type: "int", nullable: false),
                    DELETE_ATTENDENCE = table.Column<int>(type: "int", nullable: true),
                    UPDATE_ATTENDENCE = table.Column<int>(type: "int", nullable: true),
                    ADD_ATTENDENCE = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERMISSIONS", x => x.PERMISSIONID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "DOCTOR",
                columns: table => new
                {
                    DOCTOR_ID = table.Column<int>(type: "int", nullable: false),
                    CATEGORY_ID = table.Column<int>(type: "int", nullable: true),
                    FIRSTNAME = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    LASTNAME = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    AGE = table.Column<int>(type: "int", nullable: true),
                    EMAIL = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    CITY = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOCTOR", x => x.DOCTOR_ID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_DOCTOR_IS_OF_TYP_CATEGORY",
                        column: x => x.CATEGORY_ID,
                        principalTable: "CATEGORY",
                        principalColumn: "CATEGORY_ID");
                });

            migrationBuilder.CreateTable(
                name: "DEPARTMENT",
                columns: table => new
                {
                    DEP_ID = table.Column<int>(type: "int", nullable: false),
                    DOCTOR_ID = table.Column<int>(type: "int", nullable: true),
                    NUMBER = table.Column<int>(type: "int", nullable: true),
                    NBDOCTORS = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEPARTMENT", x => x.DEP_ID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_DEPARTME_COORDINAT_DOCTOR",
                        column: x => x.DOCTOR_ID,
                        principalTable: "DOCTOR",
                        principalColumn: "DOCTOR_ID");
                });

            migrationBuilder.CreateTable(
                name: "FACULTY",
                columns: table => new
                {
                    FACULTYID = table.Column<int>(type: "int", nullable: false),
                    DOCTOR_ID = table.Column<int>(type: "int", nullable: true),
                    NAME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FACULTY", x => x.FACULTYID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_FACULTY_MANAGES_DOCTOR",
                        column: x => x.DOCTOR_ID,
                        principalTable: "DOCTOR",
                        principalColumn: "DOCTOR_ID");
                });

            migrationBuilder.CreateTable(
                name: "SECTION",
                columns: table => new
                {
                    SECTIONID = table.Column<int>(type: "int", nullable: false),
                    DOCTOR_ID = table.Column<int>(type: "int", nullable: true),
                    NUMBER = table.Column<int>(type: "int", nullable: true),
                    PHONE_NUMBER = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    LOCATION = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SECTION", x => x.SECTIONID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_SECTION_DIRECTS_DOCTOR",
                        column: x => x.DOCTOR_ID,
                        principalTable: "DOCTOR",
                        principalColumn: "DOCTOR_ID");
                });

            migrationBuilder.CreateTable(
                name: "ATTENDENCE",
                columns: table => new
                {
                    ATT_ID = table.Column<int>(type: "int", nullable: false),
                    DEP_ID = table.Column<int>(type: "int", nullable: false),
                    DOCTOR_ID = table.Column<int>(type: "int", nullable: false),
                    DATE = table.Column<DateTime>(type: "date", nullable: true),
                    NB_HOURS = table.Column<int>(type: "int", nullable: true),
                    COMMENTS = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ATTEND = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATTENDANCE", x => x.ATT_ID);
                    table.ForeignKey(
                        name: "FK_ATTENDAN_ATTENDANC_DEPARTME",
                        column: x => x.DEP_ID,
                        principalTable: "DEPARTMENT",
                        principalColumn: "DEP_ID");
                    table.ForeignKey(
                        name: "FK_ATTENDAN_ATTENDANC_DOCTOR",
                        column: x => x.DOCTOR_ID,
                        principalTable: "DOCTOR",
                        principalColumn: "DOCTOR_ID");
                });

            migrationBuilder.CreateTable(
                name: "EMPLOYEE",
                columns: table => new
                {
                    EMP_ID = table.Column<int>(type: "int", nullable: false),
                    DEP_ID = table.Column<int>(type: "int", nullable: true),
                    FIRSTNAME = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    LASTNAME = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    AGE = table.Column<int>(type: "int", nullable: true),
                    EMAIL = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    CITY = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEE", x => x.EMP_ID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_EMPLOYEE_WORK_IN_DEPARTME",
                        column: x => x.DEP_ID,
                        principalTable: "DEPARTMENT",
                        principalColumn: "DEP_ID");
                });

            migrationBuilder.CreateTable(
                name: "TEACHES",
                columns: table => new
                {
                    DOCTOR_ID = table.Column<int>(type: "int", nullable: false),
                    DEP_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEACHES", x => new { x.DOCTOR_ID, x.DEP_ID });
                    table.ForeignKey(
                        name: "FK_TEACHES_TEACHES2_DEPARTME",
                        column: x => x.DEP_ID,
                        principalTable: "DEPARTMENT",
                        principalColumn: "DEP_ID");
                    table.ForeignKey(
                        name: "FK_TEACHES_TEACHES_DOCTOR",
                        column: x => x.DOCTOR_ID,
                        principalTable: "DOCTOR",
                        principalColumn: "DOCTOR_ID");
                });

            migrationBuilder.CreateTable(
                name: "BELONG_TO",
                columns: table => new
                {
                    DEP_ID = table.Column<int>(type: "int", nullable: false),
                    SECTIONID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BELONG_TO", x => new { x.DEP_ID, x.SECTIONID });
                    table.ForeignKey(
                        name: "FK_BELONG_T_BELONG_TO_DEPARTME",
                        column: x => x.DEP_ID,
                        principalTable: "DEPARTMENT",
                        principalColumn: "DEP_ID");
                    table.ForeignKey(
                        name: "FK_BELONG_T_BELONG_TO_SECTION",
                        column: x => x.SECTIONID,
                        principalTable: "SECTION",
                        principalColumn: "SECTIONID");
                });

            migrationBuilder.CreateTable(
                name: "HAS",
                columns: table => new
                {
                    FACULTYID = table.Column<int>(type: "int", nullable: false),
                    SECTIONID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HAS", x => new { x.FACULTYID, x.SECTIONID });
                    table.ForeignKey(
                        name: "FK_HAS_HAS2_SECTION",
                        column: x => x.SECTIONID,
                        principalTable: "SECTION",
                        principalColumn: "SECTIONID");
                    table.ForeignKey(
                        name: "FK_HAS_HAS_FACULTY",
                        column: x => x.FACULTYID,
                        principalTable: "FACULTY",
                        principalColumn: "FACULTYID");
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    EMP_ID2 = table.Column<int>(type: "int", nullable: false),
                    DOCTOR_ID = table.Column<int>(type: "int", nullable: true),
                    EMP_ID = table.Column<int>(type: "int", nullable: true),
                    PERMISSIONID = table.Column<int>(type: "int", nullable: true),
                    FIRSTNAME = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    LASTNAME = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    AGE = table.Column<int>(type: "int", nullable: true),
                    EMAIL = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    CITY = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    DATECREATED = table.Column<DateTime>(type: "datetime", nullable: true),
                    LASTMODIFIED = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.EMP_ID2)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_USER_HAS_PERMI_PERMISSI",
                        column: x => x.PERMISSIONID,
                        principalTable: "PERMISSIONS",
                        principalColumn: "PERMISSIONID");
                    table.ForeignKey(
                        name: "FK_USER_RELATIONS_EMPLOYEE",
                        column: x => x.EMP_ID,
                        principalTable: "EMPLOYEE",
                        principalColumn: "EMP_ID");
                    table.ForeignKey(
                        name: "FK_USER_USES_DOCTOR",
                        column: x => x.DOCTOR_ID,
                        principalTable: "DOCTOR",
                        principalColumn: "DOCTOR_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ATTENDENCE_DEP_ID",
                table: "ATTENDENCE",
                column: "DEP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ATTENDENCE_DOCTOR_ID",
                table: "ATTENDENCE",
                column: "DOCTOR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BELONG_TO_SECTIONID",
                table: "BELONG_TO",
                column: "SECTIONID");

            migrationBuilder.CreateIndex(
                name: "COORDINATES_FK",
                table: "DEPARTMENT",
                column: "DOCTOR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DOCTOR_CATEGORY_ID",
                table: "DOCTOR",
                column: "CATEGORY_ID");

            migrationBuilder.CreateIndex(
                name: "WORK_IN_FK",
                table: "EMPLOYEE",
                column: "DEP_ID");

            migrationBuilder.CreateIndex(
                name: "MANAGES_FK",
                table: "FACULTY",
                column: "DOCTOR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_HAS_SECTIONID",
                table: "HAS",
                column: "SECTIONID");

            migrationBuilder.CreateIndex(
                name: "DIRECTS_FK",
                table: "SECTION",
                column: "DOCTOR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TEACHES_DEP_ID",
                table: "TEACHES",
                column: "DEP_ID");

            migrationBuilder.CreateIndex(
                name: "HAS_PERMISISON_FK",
                table: "USER",
                column: "PERMISSIONID");

            migrationBuilder.CreateIndex(
                name: "RELATIONSHIP_9_FK",
                table: "USER",
                column: "EMP_ID");

            migrationBuilder.CreateIndex(
                name: "USES_FK",
                table: "USER",
                column: "DOCTOR_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ATTENDENCE");

            migrationBuilder.DropTable(
                name: "BELONG_TO");

            migrationBuilder.DropTable(
                name: "HAS");

            migrationBuilder.DropTable(
                name: "TEACHES");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "SECTION");

            migrationBuilder.DropTable(
                name: "FACULTY");

            migrationBuilder.DropTable(
                name: "PERMISSIONS");

            migrationBuilder.DropTable(
                name: "EMPLOYEE");

            migrationBuilder.DropTable(
                name: "DEPARTMENT");

            migrationBuilder.DropTable(
                name: "DOCTOR");

            migrationBuilder.DropTable(
                name: "CATEGORY");
        }
    }
}
