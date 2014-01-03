using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using testme.Models;

namespace testme.DataAccessLayer
{
    public class CategoryDataAccess
    {
        public DataAccessResult InsertNewCategory(Categories categories) 
        {
            DataAccessResult dataAccessResult = new DataAccessResult();
            string DBErrorMessage = "";

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TestMeConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("spInsertIntoCategories", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("CategoryName", categories.CategoryName);
            sqlCommand.Parameters.AddWithValue("UserID", categories.UserID);

            try 
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader;
                sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        DBErrorMessage = (string)sqlDataReader["DBErrorMessage"];
                    }
                }

                if (sqlDataReader.RecordsAffected >= 1)
                {
                    dataAccessResult.IsError = false;
                    dataAccessResult.UserMessage = String.Format("{0} has been succcessfully added!", categories.CategoryName);
                    dataAccessResult.TransactionDetails = String.Format("Number of Number of records affected in this transaction: {0}", sqlDataReader.RecordsAffected);
                }
                else if (sqlDataReader.RecordsAffected == -1)
                {
                    dataAccessResult.IsError = true;
                    dataAccessResult.UserMessage = DBErrorMessage;
                    dataAccessResult.TransactionDetails = String.Format("Number of records affected in this transaction: {0}", sqlDataReader.RecordsAffected);
                }
                else 
                {
                    dataAccessResult.IsError = true;
                    dataAccessResult.UserMessage = String.Format("A fatal error has occured! {0} was not successfully added, please try again!", categories.CategoryName);
                    dataAccessResult.TransactionDetails = String.Format("Number of Number of records affected in this transaction: {0}", sqlDataReader.RecordsAffected);
                }
            }
            catch (Exception exception)
            {
                dataAccessResult.IsError = true;
                dataAccessResult.UserMessage = String.Format("Error: {0}", exception.Message);
                dataAccessResult.TransactionDetails = String.Format("StackTrace: {0}", exception.StackTrace);
            }
            finally 
            {
                sqlConnection.Close();
            }

            return dataAccessResult;

        }

        public DataAccessResult UpdateExistingCategory(Categories categories)
        {
            DataAccessResult dataAccessResult = new DataAccessResult();
            string DBErrorMessage = "";

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TestMeConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("spUpdateCategoriesByRecordID", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("RecordID", categories.RecordID);
            sqlCommand.Parameters.AddWithValue("Active", categories.Active);
            sqlCommand.Parameters.AddWithValue("CategoryName", categories.CategoryName);
            
            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader;
                sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    { 
                        DBErrorMessage = (string)sqlDataReader["DBErrorMessage"];
                    }
                }

                if (sqlDataReader.RecordsAffected >= 1)
                {
                    dataAccessResult.IsError = false;
                    dataAccessResult.UserMessage = String.Format("{0} has been succcessfully updated!", categories.CategoryName);
                    dataAccessResult.TransactionDetails = String.Format("Number of records affected in this transaction: {0}", sqlDataReader.RecordsAffected);
                }
                else if (sqlDataReader.RecordsAffected == -1)
                {
                    dataAccessResult.IsError = true;
                    dataAccessResult.UserMessage = DBErrorMessage;
                    dataAccessResult.TransactionDetails = String.Format("Number of records affected in this transaction: {0}", sqlDataReader.RecordsAffected);
                }
                else
                {
                    dataAccessResult.IsError = true;
                    dataAccessResult.UserMessage = String.Format("A fatal error has occured! Update for {0} was not successful, please try again!", categories.CategoryName);
                    dataAccessResult.TransactionDetails = String.Format("Number of records affected in this transaction: {0}", sqlDataReader.RecordsAffected);
                }
            }
            catch (Exception exception)
            {
                dataAccessResult.IsError = true;
                dataAccessResult.UserMessage = String.Format("Error: {0}", exception.Message);
                dataAccessResult.TransactionDetails = String.Format("StackTrace: {0}", exception.StackTrace);
            }
            finally
            {
                sqlConnection.Close();
            }

            return dataAccessResult;

        }

        public Tuple<List<Categories>, DataAccessResult> SelectAllCategoriesByUser(int UserID)
        {
            List<Categories> listOfCategories = new List<Categories>();
            DataAccessResult dataAccessResult = new DataAccessResult();
            
            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TestMeConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("spSelectAllCategoriesByUserID", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("UserID", UserID);
            
            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader;
                sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                if (sqlDataReader.HasRows)
                {
                    dataAccessResult.IsError = false;
                    dataAccessResult.UserMessage = "All categories have been successfully retrieved";
                    dataAccessResult.TransactionDetails = String.Format("Number of records returned in this transaction: {0}", sqlDataReader.RecordsAffected);

                    while (sqlDataReader.Read())
                    {
                        Categories categories = new Categories();
                        categories.Active = (bool)sqlDataReader["Active"];
                        categories.AddedDate = (DateTime)sqlDataReader["AddedDate"];
                        categories.CategoryName = (string)sqlDataReader["CategoryName"];
                        categories.LastModifiedByID = (int)sqlDataReader["LastModifiedByID"];
                        categories.LastModifiedDate = (DateTime)sqlDataReader["LastModifiedDate"];
                        categories.RecordID = (int)sqlDataReader["ID"];
                        categories.UserID = (int)sqlDataReader["UserID"];

                        listOfCategories.Add(categories);
                    }
                }
                else
                {
                    dataAccessResult.IsError = true;
                    dataAccessResult.UserMessage = "You do not have any categories saved at this time";
                    dataAccessResult.TransactionDetails = String.Format("Number of records affected in this transaction: {0}", sqlDataReader.RecordsAffected);
                }
            }
            catch (Exception exception)
            {
                dataAccessResult.IsError = true;
                dataAccessResult.UserMessage = String.Format("Error: {0}", exception.Message);
                dataAccessResult.TransactionDetails = String.Format("StackTrace: {0}", exception.StackTrace);
            }
            finally
            {
                sqlConnection.Close();
            }

            Tuple<List<Categories>, DataAccessResult> tuple = new Tuple<List<Categories>, DataAccessResult>(listOfCategories, dataAccessResult);

            return tuple;

        }

        #region CategoryDataAccess_UnitTesting

        public void SelectAllCategoriesByUserTest()
        {
            Tuple<List<Categories>, DataAccessResult> tuple;
            List<Categories> listOfCategories;
            DataAccessResult dataAccessResult;

            tuple = SelectAllCategoriesByUser(1);
            listOfCategories = tuple.Item1;
            dataAccessResult = tuple.Item2;
            string skdfkshdkfhskdjf = "sdfsdfsdfsdf";
        }

        public void UpdateExistingCategoryTest()
        {
            DataAccessResult dataAccessResult = new DataAccessResult();
            Categories categories = new Categories();

            categories.UserID = 1;
            categories.RecordID = 1;
            categories.Active = true;
            categories.CategoryName = "New Cat Name 4";
            categories.LastModifiedDate = DateTime.Now;

            dataAccessResult = UpdateExistingCategory(categories);
            bool IsError = dataAccessResult.IsError;
            string UserMessage = dataAccessResult.UserMessage;
            string TransactionDetails = dataAccessResult.TransactionDetails;
        }

        public void InsertNewCategoryTest()
        {
            DataAccessResult dataAccessResult = new DataAccessResult();
            Categories categories = new Categories();
            categories.CategoryName = "XYZASD";
            categories.UserID = 1;
            dataAccessResult = InsertNewCategory(categories);
            bool IsError = dataAccessResult.IsError;
            string UserMessage = dataAccessResult.UserMessage;
            string TransactionDetails = dataAccessResult.TransactionDetails;
        }
        
        #endregion
    }
}