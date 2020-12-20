using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace efEX
{
    public class EFExSqlDesigner
    {
        public Dictionary<string, EFEXSqlMigration?> Migrations { get; set; }
        public EFExSqlDesigner(EFProject proj, EfEXLog log)
        {
            Migrations = new Dictionary<string, EFEXSqlMigration?>();
            foreach(var file in Directory.GetFiles(proj.MigrationPath))
            {
                if (!file.ToLower().EndsWith(".sql.designer.json"))
                {
                    continue;
                }
                log.LogVerbose($"Processing designer file {file}");
                var jsonString = File.ReadAllText(file);
                var fileName = Path.GetFileName(file);
                var migrationName = fileName.ToLower().Replace(".sql.designer.json","");
                log.LogVerbose($"Migration name found to be {migrationName}");
                var migObject = JsonSerializer.Deserialize<EFEXSqlMigration>(jsonString);
                Migrations.Add(migrationName, migObject);
                log.LogVerbose($"Successfully deserialized migration json file {file} for processing");
            }
        }
    }
    public class EFEXSqlMigration
    {
        public List<SqlObject> Ups { get; set; }
        public List<SqlObject> Downs { get; set; }
        public EFEXSqlMigration()
        {
            Ups = new List<SqlObject>();
            Downs = new List<SqlObject>();
        }
    }
    public class SqlObject
    {
        public string Name { get; set; }
        public string SqlType { get; set; }
        public string Body { get; set; }
        public SqlObject(string name, string sqlType, string body)
        {
            Name = name;
            SqlType = sqlType;
            Body = body;
        }
    }
}
