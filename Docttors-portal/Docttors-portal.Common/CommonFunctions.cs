using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Docttors_portal.Common
{
    public class CommonFunctions
    {
        /// <summary>
        /// Validate Date
        /// </summary>
        /// <param name="vDate"></param>
        /// <returns></returns>
        /// 
        public static String SingleSpacedTrim(String inString)
        {
            StringBuilder sb = new StringBuilder();
            Boolean inBlanks = false;
            foreach (Char c in inString)
            {
                switch (c)
                {
                    case '\r':
                    case '\n':
                    case '\t':
                    case ' ':
                        if (!inBlanks)
                        {
                            inBlanks = true;
                            sb.Append(' ');
                        }
                        continue;
                    default:
                        inBlanks = false;
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString().Trim();
        }
        public static bool IsDate(string vDate)
        {
            try
            {
                Convert.ToDateTime(vDate);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validate Numeric
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsNumeric(object o)
        {
            try
            {
                Convert.ToDouble(o);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validate Null
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNull(object s)
        {
            try
            {
                if (s == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static string BytesToFileSize(long bytes)
        {
            string[] sizes = new string[] { "Bytes", "KB", "MB", "GB", "TB" };
            if (bytes == 0) return "0 Byte";
            var i = Convert.ToInt64(Math.Floor(Math.Log(bytes) / Math.Log(1024)));
            string Res = Convert.ToString(System.Math.Round(bytes / System.Math.Pow(1024, i), 2)) + ' ' + sizes[i];
            return Res;
        }

        /// <summary>
        /// Validate IsEmpty
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsEmpty(string s)
        {
            try
            {
                if (string.IsNullOrEmpty(s))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validate IsBoolean
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsBoolean(object o)
        {
            try
            {
                Convert.ToBoolean(o);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validate Regular expression
        /// </summary>
        /// <param name="stringToMatch"></param>
        /// <param name="validationType"></param>
        /// <returns></returns>

        /// <summary>
        /// Convert Single Column To ArrayList
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static ArrayList ConvertSingleColumnToArrayList(DataTable dataTable)
        {
            return ConvertSingleColumnToArrayList(dataTable, 0);
        }

        /// <summary>
        /// Convert Single Column To ArrayList
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public static ArrayList ConvertSingleColumnToArrayList(DataTable dataTable, int columnIndex)
        {
            ArrayList arrayList = new ArrayList();
            if (dataTable != null)
            {
                for (int rowCount = 0; rowCount < dataTable.Rows.Count; rowCount++)
                {
                    arrayList.Add(dataTable.Rows[rowCount][columnIndex]);
                }
            }
            return arrayList;
        }

        /// <summary>
        /// Convert Data Row To Hashtable
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static Hashtable ConvertDataRowToHashtable(DataTable dataTable)
        {
            Hashtable hashTable = new Hashtable();
            if (dataTable != null)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    hashTable.Add(dataTable.Columns[i].ColumnName, Convert.ToString(dataTable.Rows[0][i]));
                }
            }
            return hashTable;
        }

        /// <summary>
        /// Convert Data Row To Hashtable
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="rowNumber"></param>
        /// <returns></returns>
        public static Hashtable ConvertDataRowToHashtable(DataTable dataTable, int rowNumber)
        {
            Hashtable hashTable = new Hashtable();
            if (dataTable != null)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    hashTable.Add(dataTable.Columns[i].ColumnName, Convert.ToString(dataTable.Rows[rowNumber][i]));
                }
            }
            return hashTable;
        }


        /// <summary>
        /// Copy Selected rows from DataSet To Datatable
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="filterString"></param>
        /// <param name="tableIndex"></param>
        /// <returns></returns>
        public static DataTable CopySelectedRowsFromDataSetToDatatable(DataSet ds, string filterString, int tableIndex)
        {
            try
            {
                if (ds != null)
                {
                    DataRow[] dr;
                    using (DataTable dt = ds.Tables[tableIndex].Clone())
                    {
                        dr = ds.Tables[tableIndex].Select(filterString);
                        for (int j = 0; j < dr.Length; j++)
                        {
                            dt.ImportRow(dr[j]);
                        }
                        dt.AcceptChanges();
                        return dt;
                    }
                }
                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Add new Row To DataTable
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="propertyArray"></param>
        /// <returns></returns>
        public static DataTable AddNewRowToDataTable(DataTable dataTable, Object[] propertyArray)
        {
            if (dataTable != null)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow.ItemArray = propertyArray;
                dataTable.Rows.Add(dataRow);
                dataTable.AcceptChanges();
            }
            return dataTable;
        }

        /// <summary>
        /// Update Data Table Row
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="propertyArray"></param>
        /// <param name="condition"></param>
        /// <param name="DataTableKeyField"></param>
        /// <returns></returns>
        public static DataTable UpdateDataTableRow(DataTable dataTable, Object[] propertyArray, int condition, String data)
        {
            if (dataTable != null && propertyArray != null)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (Convert.ToString(dataTable.Rows[i][data]).Equals(Convert.ToString(condition)))
                    {
                        dataTable.Rows[i].BeginEdit();
                        for (int j = 0; j < propertyArray.Length; j++)
                        {
                            dataTable.Rows[i][j] = propertyArray[j];
                        }
                        dataTable.Rows[i].EndEdit();
                        dataTable.Rows[i].AcceptChanges();
                        break;
                    }
                }
            }
            return dataTable;
        }

        /// <summary>
        /// Delete Data Table Row
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="condition"></param>
        /// <param name="DataTableKeyField"></param>
        /// <returns></returns>
        public static DataTable DeleteDataTableRow(DataTable dataTable, int condition, String data)
        {
            if (dataTable != null)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (Convert.ToString(dataTable.Rows[i][data]).Equals(Convert.ToString(condition)))
                    {
                        dataTable.Rows[i].Delete();
                        dataTable.Rows[i].AcceptChanges();
                        break;
                    }
                }
            }
            return dataTable;
        }

        /// <summary>
        /// Language Converter
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int LanguageConverter(object obj)
        {
            int iLanguageID = 0;
            if (Convert.ToString(obj) == "fr")
            {
                iLanguageID = 1;
            }
            else if (Convert.ToString(obj) == "en-US")
            {
                iLanguageID = 2;
            }
            else if (Convert.ToString(obj) == "ar")
            {
                iLanguageID = 3;
            }
            return iLanguageID;
        }

        /// <summary>
        /// This function will validate credit card
        /// </summary>
        /// <param name="creditCardNumber"></param>
        /// <returns></returns>
        public bool Mod10Check(string creditCardNumber)
        {
            if (string.IsNullOrEmpty(creditCardNumber))
                return false;

            int sumOfDigits = creditCardNumber.Where((e) => e >= '0' && e <= '9')
            .Reverse()
            .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
            .Sum((e) => e / 10 + e % 10);
            return sumOfDigits % 10 == 0;
        }

        #region Get Limited String

        public static string GetLimitedString(string text, Int32? length)
        {
            string finalstring = string.Empty;
            if (!string.IsNullOrEmpty(text) && text.Length > 0)
            {
                if (length != null && length > 0)
                {
                    if (text.Trim().Length >= length)
                        finalstring = text.Trim().Substring(0, length.Value - 1) + "...";
                    else
                        finalstring = text.Trim();
                }
                else
                    finalstring = text.Trim();
            }
            return finalstring;
        }

        #endregion

        #region Get Integer Value from Object
        /// <summary>
        /// Get Integer Value.
        /// </summary>
        /// <param name="obj">Object type obj</param>
        /// <returns></returns>
        public int GetIntegerValue(object obj)
        {
            return GetIntegerValue(obj, 0);
        }
        public int GetIntegerValue(object obj, int defaultReturnValue)
        {
            try
            {
                defaultReturnValue = Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                throw;
            }
            return defaultReturnValue;
        }
        /// <summary>
        /// Get Long Value.
        /// </summary>
        /// <param name="obj">Object type obj</param>
        /// <returns></returns>
        public long GetLontValue(object obj)
        {
            return GetLontValue(obj, 0);
        }
        public long GetLontValue(object obj, long defaultReturnValue)
        {
            try
            {
                defaultReturnValue = Convert.ToInt64(obj);
            }
            catch (Exception)
            {
                throw;
            }
            return defaultReturnValue;
        }
        #endregion

        #region Get Decimel Value from Object
        /// <summary>
        /// Get Decimal Value
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public decimal GetDecimelValue(object obj)
        {
            return GetDecimelValue(obj, 0);
        }
        public decimal GetDecimelValue(object obj, decimal defaultReturnValue)
        {
            try
            {
                defaultReturnValue = Convert.ToDecimal(obj);
            }
            catch (Exception)
            {
                throw;
            }
            return defaultReturnValue;
        }
        #endregion

        #region Get Long Value from Object
        /// <summary>
        /// Get Long Value
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public long GetLongValue(object obj)
        {
            return GetLongValue(obj, 0);
        }
        public long GetLongValue(object obj, long defaultValue)
        {
            if (!(obj == null || obj == DBNull.Value))
                try
                {
                    defaultValue = Convert.ToInt64(obj);
                }
                catch (Exception) { throw; }
            return defaultValue;
        }
        #endregion

        #region Get Boolean Value from Object
        /// <summary>
        /// Get Boolean Value
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool GetBooleanValue(object obj)
        {
            return GetBooleanValue(obj, false);
        }
        public bool GetBooleanValue(object obj, bool defaultValue)
        {
            try
            {
                if (!(obj == null || obj == DBNull.Value))
                    defaultValue = Convert.ToBoolean(obj);
            }
            catch (Exception)
            {
                throw;
            }
            return defaultValue;
        }
        #endregion

        #region Get DateTime Value from Object
        /// <summary>
        /// Get GetDateTime Value
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DateTime GetDateTimeValue(object obj)
        {
            return GetDateTimeValue(obj, Convert.ToDateTime("1900-01-01"));
        }
        public DateTime GetDateTimeValue(object obj, DateTime defaultValue)
        {
            try
            {
                if (!(obj == null || obj == DBNull.Value))
                    defaultValue = Convert.ToDateTime(obj);
            }
            catch (Exception)
            {
                throw;
            }
            return defaultValue;
        }
        #endregion

        #region Get String Value from Object
        /// <summary>
        /// Get String Value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public string GetStringValue(object obj, string defaultValue)
        {
            if (!(obj == null || obj == DBNull.Value))
                defaultValue = Convert.ToString(obj);
            return defaultValue;
        }
        #endregion

        #region Decimal Value Handling
        /// <summary>
        /// This Method  Can make decimal value with desiger 
        /// length with updating last number (if last number after decimel point >=5 then 6 and lessthan <5 then 
        /// the same value
        /// Ex:- 10.012547 will be 10.01255 if i call this method with GetDecimalPlaceValue("10.012547",5)
        /// and 12.012351 will be 12.01235 if i cakll this method with GetDecimalPlaceValue("12.012351",5)
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="DecimalPlace"></param>
        /// <returns></returns>
        public decimal GetDecimalPlaceValue(string value, int decimalPlace)
        {
            decimal RetunValue = 0.00M;

            if (!string.IsNullOrEmpty(value))
            {
                int NextToDecimalPlaceValue = 0;
                string InputValue = value;
                try
                {
                    RetunValue = Convert.ToDecimal(InputValue.Substring(0, (InputValue.IndexOf('.') + decimalPlace + 1)));
                    NextToDecimalPlaceValue = GetIntegerValue(InputValue.Substring(Convert.ToString(RetunValue).Length, 1));
                    if (NextToDecimalPlaceValue > 4)
                    {
                        RetunValue = RetunValue + Convert.ToDecimal(AddOne(decimalPlace));
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return RetunValue;
        }
        /// <summary>
        /// This Methods for round off item weight for(USPS) 
        /// EX: .15 Pound =1 Pound
        /// </summary>
        /// <param name="inPutVal"></param>
        /// <returns></returns>
        public int MakeRoundOffDecimal(string inPutVal)
        {
            int contenerVal = 0;
            int intValAfterPoint = 0;

            if (!string.IsNullOrEmpty(inPutVal))
            {
                string ValBeforePoint = inPutVal.Substring(0, inPutVal.IndexOf('.'));
                string valAfterPoint = inPutVal.Substring(inPutVal.IndexOf('.') + 1);

                try
                {
                    contenerVal = Convert.ToInt32(ValBeforePoint);
                    intValAfterPoint = Convert.ToInt32(valAfterPoint);
                    if (intValAfterPoint > 0)
                    {
                        contenerVal += 1;
                    }
                }
                catch
                {
                    contenerVal += 1;
                }
            }
            return contenerVal;
        }
        private string AddOne(int DecimalPlace)
        {
            string Output = ".";
            for (int i = 1; i < DecimalPlace; i++)
            {
                Output = Output + "0";
            }
            Output = Output + "1";
            return Output;
        }
        #endregion

        #region GUID - String Handler
        /// <summary>
        /// Convert trring to GUID
        /// </summary>
        /// <param name="sValue"></param>
        /// <returns></returns>
        public Guid StringToGuid(string sValue)
        {
            System.Guid guid = new Guid(sValue);
            return guid;
        }
        #endregion

        #region Other Methods
        public string FcnRemoveSingleItems(string strVlaue)
        {
            strVlaue = FcnCleanSearchString(strVlaue);
            string strTemp = strVlaue.Replace("\"", "");
            if (strTemp.Length < 2)
                strVlaue = "";

            string strReturn = strVlaue;

            if (strVlaue.Trim().IndexOf(" ") > 0)
            {
                strReturn = "";
                string[] strSplit = strVlaue.Split(' ');
                for (int intCnt = 0; intCnt < strSplit.Length; intCnt++)
                {
                    if (strSplit[intCnt].Trim().Length > 1)
                    {
                        if (strReturn.Length < 1)
                            strReturn += strSplit[intCnt].Trim();
                        else
                            strReturn += " " + strSplit[intCnt].Trim();
                    }
                }
            }

            return strReturn;
        }

        #region for clean search string
        public string FcnCleanSearchString(string strSearchString)
        {
            if (!string.IsNullOrEmpty(strSearchString))
            {
                strSearchString = strSearchString.Trim();

                try
                {
                    string _pattern = @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
                    Regex reg = new Regex(_pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    bool _isMatch = reg.IsMatch(strSearchString);
                    if (_isMatch == true)
                    {
                        strSearchString = Regex.Replace(strSearchString, "http://", "", RegexOptions.IgnoreCase);
                        strSearchString = Regex.Replace(strSearchString, "https://", "", RegexOptions.IgnoreCase);
                        strSearchString = Regex.Replace(strSearchString, "ftp://", "", RegexOptions.IgnoreCase);
                        strSearchString = Regex.Replace(strSearchString, "www.", "", RegexOptions.IgnoreCase);
                        strSearchString = Regex.Replace(strSearchString, ".com", "", RegexOptions.IgnoreCase);
                        strSearchString = Regex.Replace(strSearchString, ".org", "", RegexOptions.IgnoreCase);
                        strSearchString = Regex.Replace(strSearchString, ".net", "", RegexOptions.IgnoreCase);
                        strSearchString = Regex.Replace(strSearchString, ".co.in", "", RegexOptions.IgnoreCase);
                        strSearchString = Regex.Replace(strSearchString, ".aspx", "", RegexOptions.IgnoreCase);
                        strSearchString = Regex.Replace(strSearchString, ".html", "", RegexOptions.IgnoreCase);
                        strSearchString = Regex.Replace(strSearchString, ".php", "", RegexOptions.IgnoreCase);
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                strSearchString = strSearchString.Replace("-", " ");
                strSearchString = strSearchString.Replace("<", " ");
                strSearchString = strSearchString.Replace(">", " ");
                strSearchString = strSearchString.Replace("*", " ");
                strSearchString = strSearchString.Replace(".", " ");
                strSearchString = strSearchString.Replace("?", " ");
                strSearchString = strSearchString.Replace("!", " ");
                strSearchString = strSearchString.Replace("$", " ");
                strSearchString = strSearchString.Replace("&", " ");
                strSearchString = strSearchString.Replace("~", " ");
                strSearchString = strSearchString.Replace("#", " ");
                strSearchString = strSearchString.Replace("@", " ");
                strSearchString = strSearchString.Replace("%", " ");
                strSearchString = strSearchString.Replace("^", " ");
                strSearchString = strSearchString.Replace("'", " ");
                strSearchString = strSearchString.Replace(":", " ");
                strSearchString = strSearchString.Replace("(", " ");
                strSearchString = strSearchString.Replace(")", " ");
                strSearchString = strSearchString.Replace("+", " ");
                strSearchString = strSearchString.Replace("=", " ");
                strSearchString = strSearchString.Replace("[", " ");
                strSearchString = strSearchString.Replace("]", " ");
                strSearchString = strSearchString.Replace("{", " ");
                strSearchString = strSearchString.Replace("}", " ");
                strSearchString = strSearchString.Replace("\"", " ");
                strSearchString = strSearchString.Replace(";", " ");
                strSearchString = strSearchString.Replace("/", " ");
                strSearchString = strSearchString.Replace("  ", " ");
                strSearchString = strSearchString.Replace("   ", " ");
            }
            return strSearchString;
        }
        #endregion

        public string FormatPublicationTitle(string inputString)
        {
            string outputString = string.Empty;

            if (!string.IsNullOrEmpty(inputString))
            {
                string word = string.Empty;
                string pubTitle = string.Empty;
                string[] strWord = inputString.Trim().Split('-');
                for (int i = 0; i < strWord.Length; i++)
                {
                    word = UpperCaseFirstChar(strWord[i]);
                    if (string.IsNullOrEmpty(pubTitle))
                    { pubTitle = word; }
                    else { pubTitle = pubTitle + " " + word; }
                }
                outputString = pubTitle;
            }
            return outputString;
        }

        public string FormatPublicationTitleforUrl(string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                string outputString = inputString.Trim();
                string lowinputString = outputString.ToLower();
                if (lowinputString.Substring(lowinputString.Length - 3, 3) == "the")
                {
                    if (lowinputString.Substring(lowinputString.LastIndexOf("the")) == "the")
                    {
                        outputString = outputString.Substring(0, outputString.Length - 3);
                        outputString = outputString.Trim();
                        if (outputString.Substring(outputString.LastIndexOf(",")) == ",")
                        {
                            outputString = outputString.Substring(0, outputString.Length - 1);
                        }
                        outputString = "The " + outputString;
                    }
                }
                outputString = outputString.Replace("    ", " ").Replace("   ", " ").Replace("  ", " ").Replace(" - ", "-").Replace("  -  ", "-").Replace(" ", "-").Replace("\\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("|", "").Replace(",", "").Replace("'", "").Replace(".", "").TrimEnd(new char[] { '-', ',', '_' });
                return outputString;
            }
            else
                return string.Empty;
        }

        public string UpperCaseFirstChar(string word)
        {
            if (string.IsNullOrEmpty(word)) { return string.Empty; }
            return char.ToUpper(word[0]) + word.Substring(1);
        }
        #endregion


        #region:Phone Format
        /// <summary>
        /// This function will format Phone no.
        /// </summary>
        /// <param name="myNumber"></param>
        /// <returns></returns>
        public string FormatPhoneNumber(string myNumber)
        {
            string mynewNumber = string.Empty;

            if (!string.IsNullOrEmpty(myNumber) && myNumber.Length > 10)
                mynewNumber = myNumber.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            else if (!string.IsNullOrEmpty(myNumber) && myNumber.Length == 10)
                mynewNumber = myNumber.Substring(0, 3) + "-" + myNumber.Substring(3, 3) + "-" + myNumber.Substring(6, 4);

            return mynewNumber;

        }
        #endregion

        #region Payment (Subscription)

        #region Payment Gateway


        #endregion

        #endregion

        #region ConvertDateToDDMMYY

        /// <summary>
        /// Created By : Akriti Joshi
        /// Date : 17-06-2015
        /// Details : Returns the date in dd/MM/yyyy format
        /// </summary>
        /// <param name="date"></param>
        /// <param name="objDate"></param>
        /// <returns></returns>
        public static DateTime ConvertDateToDDMMYY(string date, DateTime objDate)
        {
            if (!string.IsNullOrEmpty(date.Trim()))
                return DateTime.ParseExact(date.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            else
                return DateTime.ParseExact(objDate.ToString("dd/MM/yyyy"), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }

        #endregion
    }
}
