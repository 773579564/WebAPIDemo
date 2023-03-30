using System;
using System.Text;
using System.Data.Common;

namespace FY.Common.Database
{
    public class DatabasePageExtension
    {
        /// <summary>
        /// SqlServer 分页查询
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <param name="sort"></param>
        /// <param name="isAsc"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static StringBuilder SqlServerPageSql(string strSql, DbParameter[] dbParameter, string sort, bool isAsc, int pageSize, int pageIndex)
        {
            CheckSqlParam(sort);

            StringBuilder sb = new StringBuilder();
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            int startNum = (pageIndex - 1) * pageSize;
            int endNum = (pageIndex) * pageSize;
            string orderBy = string.Empty;

            if (!string.IsNullOrEmpty(sort))
            {
                if (sort.ToUpper().IndexOf("ASC") + sort.ToUpper().IndexOf("DESC") > 0)
                {
                    orderBy = " ORDER BY " + sort;
                }
                else
                {
                    orderBy = " ORDER BY " + sort + " " + (isAsc ? "ASC" : "DESC");
                }
            }
            else
            {
                orderBy = "ORDERE BY (SELECT 0)";
            }
            sb.Append("SELECT * FROM (SELECT ROW_NUMBER() Over (" + orderBy + ")");
            sb.Append(" AS ROWNUM, * From (" + strSql + ") t ) AS N WHERE ROWNUM > " + startNum + " AND ROWNUM <= " + endNum + "");
            return sb;
        }

        public static StringBuilder OraclePageSql(string strSql, DbParameter[] dbParameter, string sort, bool isAsc, int pageSize, int pageIndex)
        {
            CheckSqlParam(sort);

            StringBuilder sb = new StringBuilder();
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            int startNum = (pageIndex - 1) * pageSize;
            int endNum = (pageIndex) * pageSize;
            string orderBy = string.Empty;

            if (!string.IsNullOrEmpty(sort))
            {
                if (sort.ToUpper().IndexOf("ASC") + sort.ToUpper().IndexOf("DESC") > 0)
                {
                    orderBy = " ORDER BY " + sort;
                }
                else
                {
                    orderBy = " ORDER BY " + sort + " " + (isAsc ? "ASC" : "DESC");
                }
            }
            sb.Append("SELECT * From (SELECT ROWNUM AS n,");
            sb.Append(" T.* From (" + strSql + orderBy + ") t )  N WHERE n > " + startNum + " AND n <= " + endNum + "");
            return sb;
        }

        public static StringBuilder MySqlPageSql(string strSql, DbParameter[] dbParameter, string sort, bool isAsc, int pageSize, int pageIndex)
        {
            CheckSqlParam(sort);

            StringBuilder sb = new StringBuilder();
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            int num = (pageIndex - 1) * pageSize;
            string orderBy = string.Empty;

            if (!string.IsNullOrEmpty(sort))
            {
                if (sort.ToUpper().IndexOf("ASC") + sort.ToUpper().IndexOf("DESC") > 0)
                {
                    orderBy = " ORDER BY " + sort;
                }
                else
                {
                    orderBy = " ORDER BY " + sort + " " + (isAsc ? "ASC" : "DESC");
                }
            }
            sb.Append(strSql + orderBy);
            sb.Append(" LIMIT " + num + "," + pageSize + "");
            return sb;
        }

        /// <summary>
        /// 获取查询总行数语句
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static string GetCountSql(string strSql)
        {
            string countSql = "SELECT COUNT(1) FROM (" + strSql + ") t";
            return countSql;
        }

        private static void CheckSqlParam(string param)
        {
            if (!SecurityHelper.IsSafeSqlParam(param))
            {
                throw new ArgumentException("含有Sql注入的参数");
            }
        }
    }
}
