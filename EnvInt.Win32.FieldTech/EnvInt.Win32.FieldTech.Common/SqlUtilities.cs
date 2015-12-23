using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Common.Containers;

namespace EnvInt.Win32.FieldTech.Common
{
    /// <summary>
    /// Helper class for common SQL Functions
    /// </summary>
    public static class SqlUtilities
    {
        public static ExtendedResult TestSqlConnection(string connectionString, string testSql, int timeout)
        {
            ExtendedResult er = new ExtendedResult();
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                if (String.IsNullOrEmpty(testSql))
                {
                    testSql = "select 1";
                }
                SqlCommand command = new SqlCommand(testSql, conn);
                if (timeout > 0)
                {
                    command.CommandTimeout = timeout;
                }
                object count = command.ExecuteScalar();
                er.Result = count;
                er.Success = true;
            }
            catch (Exception ex)
            {
                er.Message = ex.Message;
            }
            return er;
        }

        public static ExtendedResult TestSqlConnection(string connectionString, string testSql)
        {
            return TestSqlConnection(connectionString, testSql, 0);
        }

        public static ExtendedResult TestSqlConnection(string connectionString)
        {
            return TestSqlConnection(connectionString, null, 0);
        }
    }
}
