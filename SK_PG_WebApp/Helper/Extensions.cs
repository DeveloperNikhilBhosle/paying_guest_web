using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace SK_PG_WebApp.Helper
{
    public static class Extensions
    {

        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        #region DataRow.IsEmpty()
        /// <summary>
        /// Returns false if value of any of columns of the row are not empty or NULL. 
        /// </summary>
        /// <param name="ODataRow">DataRow</param>
        /// <returns>bool</returns>
        public static bool IsEmpty(this DataRow ODataRow)
        {
            bool Result = true;

            foreach (object OValue in ODataRow.ItemArray)
                if (OValue.ToString() != string.Empty)
                { Result = false; break; }

            return Result;
        }
        #endregion

        #region String.IsEmpty()
        /// <summary>
        /// Returns true if value is NULL or empty. 
        /// </summary>
        /// <param name="ODataRow">DataRow</param>
        /// <returns>bool</returns>
        public static bool IsNullOrEmpty(this string Ostring)
        {
            return (Ostring == null || Ostring.Trim().Length == 0);
        }
        #endregion

        #region DateTime.Compare(bool IsAscending, DateTime ComparedValue)
        /// <summary>
        /// Returns comparison between two datetime values depending on IsAscending. 
        /// </summary>
        /// <param name="ODataRow">DataRow</param>
        /// <returns>bool</returns>
        public static int Compare(this DateTime ODateTime, bool IsAscending, DateTime ComparedValue)
        {
            if (IsAscending)
                return ODateTime.CompareTo(ComparedValue);
            else
                return ComparedValue.CompareTo(ODateTime);
        }
        #endregion

        #region DataTable.AddColumns(string[] ColumnNames)
        /// <summary>
        /// Adds the list of columns to the Datatable.
        /// </summary>
        /// <param name="ODataTable">DataTable</param>
        /// <param name="ColumnNames">string[]</param>
        public static void AddColumns(this DataTable ODataTable, string[] ColumnNames)
        {
            foreach (string OColumnName in ColumnNames)
                ODataTable.Columns.Add(OColumnName);
        }
        #endregion

        #region DataTable.RemoveRowsWithValues(string[] columnNames)
        /// <summary>
        /// Removes rows with the values from the datatable.
        /// </summary>
        /// <param name="oDataTable">DataTable</param>
        /// <param name="columnName">string</param>
        /// <param name="values">string[]</param>
        public static void RemoveRowsWithValues(this DataTable oDataTable, string columnName, string[] values)
        {
            if (oDataTable.Columns.Contains(columnName))
            {
                int RowCount = oDataTable.Rows.Count - 1;
                for (int Counter = RowCount; Counter >= 0; Counter--)
                    if (values.Contains<string>(oDataTable.Rows[Counter][columnName].ToString()))
                        oDataTable.Rows[Counter].Delete();
                oDataTable.AcceptChanges();
            }
            else
                throw new Exception("Column with name " + columnName + " not found.");
        }
        #endregion

        #region Object.IsNull()
        /// <summary>
        /// Returns true if the object is null.
        /// </summary>
        /// <param name="oObject">object</param>
        /// <returns>bool</returns>
        public static bool IsNull(this object oObject)
        {
            return oObject == null ? true : false;
        }
        #endregion

        #region Object.IsNotNull()
        /// <summary>
        /// Returns true if the object is not null.
        /// </summary>
        /// <param name="oObject">object</param>
        /// <returns>bool</returns>
        public static bool IsNotNull(this object oObject)
        {
            return oObject == null ? false : true;
        }
        #endregion
        /// <summary>
        /// Returns true if string consist of Only Alphabets
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsAlphabet(this string source)
        {
            Regex rg = new Regex(@"^[A-Za-z\s]+$");
            return rg.IsMatch(source);
        }

        /// <summary>
        /// Reutrn true id the string consist of alphabets & numbers
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsAlphaNumeric(this string source)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9_\s]*$");
            return rg.IsMatch(source);
        }
        /// <summary>
        /// Returns true if string consisit of only numbers
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string source)
        {
            Regex rg = new Regex(@"^[0-9]+$");
            return rg.IsMatch(source);
        }

        /// <summary>
        /// Returns true if string is consisit of only number or decimal numbers
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNumericOrDecimal(this string source)
        {
            Regex rg = new Regex(@"^[1-9]\d*(\.\d+)?$");
            return rg.IsMatch(source);
        }

        #region Object[].IsNullOrEmpty()
        /// <summary>
        /// Returns true if the object array is null or empty.
        /// </summary>
        /// <param name="oObject">object</param>
        /// <returns>bool</returns>
        public static bool IsNullOrEmpty(this object[] oObject)
        {
            return (oObject == null || oObject.Length == 0);
        }
        #endregion

        #region DataRowView.IsEmpty()
        /// <summary>
        /// Returns false if value of any of columns of the row view are not empty or NULL. 
        /// </summary>
        /// <param name="oDataRowView">DataRowView</param>
        /// <returns>bool</returns>
        public static bool IsEmpty(this DataRowView oDataRowView)
        {
            bool Result = true;

            foreach (object OValue in oDataRowView.Row.ItemArray)
                if (OValue.ToString() != string.Empty)
                { Result = false; break; }

            return Result;
        }
        #endregion

        #region DataTable.AddBlankRow()
        /// <summary>
        /// Adds a blank row to the datatable.
        /// </summary>
        /// <param name="oDataTable">DataTable</param>
        public static void AddBlankRow(this DataTable oDataTable)
        {
            oDataTable.Rows.Add(new object[] { });
        }
        #endregion


        #region DataTable.EnsureRow()
        /// <summary>
        /// Adds a blank row to the datatable if no rows exist.
        /// </summary>
        /// <param name="oDataTable">DataTable</param>
        public static void EnsureRow(this DataTable oDataTable)
        {
            if (oDataTable.Rows.Count == 0)
                oDataTable.Rows.Add(new object[] { });
        }
        #endregion

        #region DataTable.IsEmpty()
        /// <summary>
        /// Returns false if at least one row exists in the table.
        /// </summary>
        /// <param name="oDataTable"></param>
        /// <returns></returns>
        public static bool IsNullorEmpty(this DataTable oDataTable)
        {
            if (oDataTable.IsNull() || oDataTable.Rows.Count == 0)
                return true;
            else
                return false;
        }
        #endregion

        #region string.ReplacePrefixWildCard()
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oString">string</param>
        public static string ReplacePrefixWildCard(this string oString)
        {
            if (!string.IsNullOrEmpty(oString) && oString.IndexOf('*') == 0)
            {
                char[] modifiedString = oString.ToCharArray();
                modifiedString.SetValue('%', 0);
                oString = new string(modifiedString);
            }
            return oString.Replace("*", "");
        }
        #endregion

        #region AddReplace
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyList"></param>
        /// <param name="customProperty"></param>
        public static void AddReplace(this List<Attribute> propertyList, Attribute customProperty)
        {
            foreach (Attribute listedProperty in propertyList)
            {
                if (listedProperty.Equals(customProperty))
                {
                    propertyList.Remove(listedProperty);
                    break;
                }
            }
            propertyList.Add(customProperty);
        }
        #endregion

        #region Contains
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyList"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool Contains(this Attribute[] propertyList, string propertyName)
        {
            bool contains = false;
            for (int Counter = 0; Counter <= propertyList.Length - 1; Counter++)
            {
                if (((Attribute)propertyList.GetValue(Counter)).ToString() == propertyName)
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
        #endregion

    }

    public enum PAYMENT_TYPE
    {
        Deposit = 1,
        Rent = 2,
        PG_compensation_for_damages = 3,
        Other = 4
    }


    public enum PAYMENT_GATEWAY
    {
        CASH = 1,
        Debit_Or_Credit_Card = 2,
        UPI_ID = 3
    }
}
