using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SamuraiApp.Data.Migrations
{
    public partial class AddSprocs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
            name: "IX_SamuraiBattle_SamuraiId",
            table: "SamuraiBattle");

            migrationBuilder.Sql(
                  @"CREATE PROCEDURE FilterSamuraiByNamePart
              @namepart varchar(50) 
              AS
              select * from samurais where name like '%'+@namepart+'%'");

            migrationBuilder.Sql(
              @"create procedure FindLongestName
              @procResult varchar(50) OUTPUT
              AS
              BEGIN
              SET NOCOUNT ON;
              select top 1 @procResult= name from samurais order by len(name) desc
              END"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
               name: "IX_SamuraiBattle_SamuraiId",
               table: "SamuraiBattle",
               column: "BattleId");
            migrationBuilder.Sql("DROP PROCEDURE FindLongestName");
            migrationBuilder.Sql("drop procedure FilterSamuraiByNamePart");
        }
    }
}
