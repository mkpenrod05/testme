using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using testme.Models;

namespace testme.DataAccessLayer
{
    public class TestDataAccess
    {
        public DataAccessResult InsertNewTest(Tests tests)
        {
            DataAccessResult dataAccessResult = new DataAccessResult();
            string DBErrorMessage = "";

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TestMeConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("spInsertIntoTests", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("UserID", tests.UserID);
            sqlCommand.Parameters.AddWithValue("CategoryID", tests.CategoryID);
            sqlCommand.Parameters.AddWithValue("TestName", tests.TestName);

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
                    dataAccessResult.UserMessage = String.Format("{0} has been succcessfully added!", tests.TestName);
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
                    dataAccessResult.UserMessage = String.Format("A fatal error has occured! {0} was not successfully added, please try again!", tests.TestName);
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

        public DataAccessResult UpdateExistingTest(Tests tests)
        {
            DataAccessResult dataAccessResult = new DataAccessResult();
            string DBErrorMessage = "";

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TestMeConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("spUpdateTestsByRecordID", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("RecordID", tests.RecordID);
            sqlCommand.Parameters.AddWithValue("Active", tests.Active);
            sqlCommand.Parameters.AddWithValue("TestName", tests.TestName);

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
                    dataAccessResult.UserMessage = String.Format("{0} has been succcessfully updated!", tests.TestName);
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
                    dataAccessResult.UserMessage = String.Format("A fatal error has occured! Update for {0} was not successful, please try again!", tests.TestName);
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

        public Tuple<List<Tests>, DataAccessResult> SelectAllTestsByUser(int UserID)
        {
            List<Tests> listOfTests = new List<Tests>();
            DataAccessResult dataAccessResult = new DataAccessResult();

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TestMeConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("spSelectAllTestsByUserID", sqlConnection);
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
                    dataAccessResult.UserMessage = "All tests have been successfully retrieved";
                    dataAccessResult.TransactionDetails = String.Format("Number of records returned in this transaction: {0}", sqlDataReader.RecordsAffected);

                    while (sqlDataReader.Read())
                    {
                        Tests tests = new Tests();
                        tests.RecordID = (int)sqlDataReader["ID"];
                        tests.Active = (bool)sqlDataReader["Active"];
                        tests.UserID = (int)sqlDataReader["UserID"];
                        tests.CategoryID = (int)sqlDataReader["CategoryID"];
                        tests.TestName = (string)sqlDataReader["TestName"];
                        tests.AddedDate = (DateTime)sqlDataReader["AddedDate"];
                        tests.LastModifiedByID = (int)sqlDataReader["LastModifiedByID"];
                        tests.LastModifiedDate = (DateTime)sqlDataReader["LastModifiedDate"];

                        listOfTests.Add(tests);
                    }
                }
                else
                {
                    dataAccessResult.IsError = true;
                    dataAccessResult.UserMessage = "You do not have any tests saved at this time";
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

            Tuple<List<Tests>, DataAccessResult> tuple = new Tuple<List<Tests>, DataAccessResult>(listOfTests, dataAccessResult);

            return tuple;

        }

        #region TestDataAccess_UnitTesting

        public void SelectAllTestsByUser_UNITTEST()
        {
            Tuple<List<Tests>, DataAccessResult> tuple;
            List<Tests> listOfTests;
            DataAccessResult dataAccessResult;

            tuple = SelectAllTestsByUser(3);
            listOfTests = tuple.Item1;
            dataAccessResult = tuple.Item2;
            string skdfkshdkfhskdjf = "sdfsdfsdfsdf";
        }

        public void UpdateExistingTest_UNITTEST()
        {
            DataAccessResult dataAccessResult = new DataAccessResult();
            Tests tests = new Tests();
            tests.RecordID = 7;
            tests.Active = true;
            tests.TestName = "Test 2";
            dataAccessResult = UpdateExistingTest(tests);
            bool IsError = dataAccessResult.IsError;
            string UserMessage = dataAccessResult.UserMessage;
            string TransactionDetails = dataAccessResult.TransactionDetails;
        }

        public void InsertNewTest_UNITTEST()
        {
            DataAccessResult dataAccessResult = new DataAccessResult();
            Tests tests = new Tests();
            tests.UserID = 1;
            tests.CategoryID = 1;
            tests.TestName = "Test 1";
            dataAccessResult = InsertNewTest(tests);
            bool IsError = dataAccessResult.IsError;
            string UserMessage = dataAccessResult.UserMessage;
            string TransactionDetails = dataAccessResult.TransactionDetails;
        }
        
        #endregion
    }
}