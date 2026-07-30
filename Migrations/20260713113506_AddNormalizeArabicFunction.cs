using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class AddNormalizeArabicFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // CREATE OR ALTER so this migration is safe to apply against databases
            // where the function was already created by hand (the pre-existing
            // workaround this migration replaces).
            //
            // Edit both together: AribONE's Services/ArabicText.cs is the in-memory
            // mirror of these rules, used where the text being searched is already
            // loaded and no query can be issued. If the folding rules below change,
            // that class must change identically, or the same search text will match
            // different rows depending on where it was resolved.
            migrationBuilder.Sql(
                """
                CREATE OR ALTER FUNCTION [dbo].[NormalizeArabic] (@input NVARCHAR(MAX))
                RETURNS NVARCHAR(MAX)
                AS
                BEGIN
                    IF @input IS NULL RETURN NULL;

                    DECLARE @result NVARCHAR(MAX) = @input;

                    -- توحيد أشكال الألف
                    SET @result = REPLACE(@result, N'أ', N'ا');
                    SET @result = REPLACE(@result, N'إ', N'ا');
                    SET @result = REPLACE(@result, N'آ', N'ا');

                    -- توحيد الألف المقصورة
                    SET @result = REPLACE(@result, N'ى', N'ي');

                    -- توحيد ؤ و ئ
                    SET @result = REPLACE(@result, N'ؤ', N'و');
                    SET @result = REPLACE(@result, N'ئ', N'ي');

                    -- توحيد ة
                    SET @result = REPLACE(@result, N'ة', N'ه');

                    RETURN @result;
                END;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS [dbo].[NormalizeArabic];");
        }
    }
}
