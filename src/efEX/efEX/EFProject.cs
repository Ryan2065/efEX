using System;
using System.Collections.Generic;
using System.CommandLine;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efEX
{
    public class EFProject
    {
        private EfEXLog _efLog;
        public string ProjectPath { get; set; }
        public string MigrationPath { get; set; }
        public string SqlPath { get; set; }
        private string GetProjectPath(string? projectPath)
        {
            if (string.IsNullOrEmpty(projectPath))
            {
                return Directory.GetCurrentDirectory();
            }
            if (!Directory.Exists(projectPath))
            {
                // if a file was provided, Directory.Exists will return null - so check if it's a file and get parent
                if (File.Exists(projectPath))
                {
                    var pPath = Directory.GetParent(projectPath)?.FullName;
                    if (pPath != null)
                    {
                        return pPath;
                    }
                }
            }
            _efLog.Log($"Could not find project path at {projectPath}");
            throw new DirectoryNotFoundException();
        }
        private string GetMigrationPath()
        {
            string mPath = Path.Combine(ProjectPath, "Migration");
            if (!Directory.Exists(mPath))
            {
                _efLog.Log($"Could not find migration path at {mPath}");
                throw new DirectoryNotFoundException();
            }
            return mPath;
        }
        private string GetSqlPath()
        {
            string sqlPath = Path.Combine(ProjectPath, "SQL");
            if (!Directory.Exists(sqlPath))
            {
                _efLog.Log("Could not find SQL path at {sqlPath}");
                throw new DirectoryNotFoundException();
            }
            return sqlPath;
        }
        public EFProject(string? projectPath, EfEXLog efLog)
        {
            _efLog = efLog;
            ProjectPath = GetProjectPath(projectPath);
            MigrationPath = GetMigrationPath();
            SqlPath = GetSqlPath();
        }
    }
}
