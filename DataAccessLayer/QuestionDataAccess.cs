using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using testme.Models;

namespace testme.DataAccessLayer
{
    public class QuestionDataAccess
    {
        public DataAccessResult InsertNewQuestion(Questions questions)
        {
            DataAccessResult dataAccessResult = new DataAccessResult();
            string DBErrorMessage = "";

            //Probably need to remove this section and put it in more of a form validation method...
            if (questions.QuestionTitle.Length < 1)
            {
                if (questions.QuestionSubject.Length > 60)
                {
                    questions.QuestionTitle = questions.QuestionSubject.Substring(0, 60);
                }
                else
                {
                    questions.QuestionTitle = questions.QuestionSubject;
                }
            }

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TestMeConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("spInsertIntoQuestions", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("QuestionTitle", questions.QuestionTitle);
            sqlCommand.Parameters.AddWithValue("QuestionSubject", questions.QuestionSubject);
            sqlCommand.Parameters.AddWithValue("Difficulty", questions.Difficulty);
            sqlCommand.Parameters.AddWithValue("UserID", questions.UserID);
            sqlCommand.Parameters.AddWithValue("TestID", questions.TestID);

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
                    dataAccessResult.UserMessage = String.Format("{0} has been succcessfully added!", questions.QuestionTitle);
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
                    dataAccessResult.UserMessage = String.Format("A fatal error has occured! {0} was not successfully added, please try again!", questions.QuestionTitle);
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

        public DataAccessResult UpdateExistingQuestion(Questions questions)
        {
            DataAccessResult dataAccessResult = new DataAccessResult();
            string DBErrorMessage = "";

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TestMeConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("spUpdateQuestionsByRecordID", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("RecordID", questions.RecordID);
            sqlCommand.Parameters.AddWithValue("Active", questions.Active);
            sqlCommand.Parameters.AddWithValue("QuestionTitle", questions.QuestionTitle);
            sqlCommand.Parameters.AddWithValue("QuestionSubject", questions.QuestionSubject);
            sqlCommand.Parameters.AddWithValue("Difficulty", questions.Difficulty);

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
                    dataAccessResult.UserMessage = String.Format("{0} has been succcessfully updated!", questions.QuestionTitle);
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
                    dataAccessResult.UserMessage = String.Format("A fatal error has occured! Update for {0} was not successful, please try again!", questions.QuestionTitle);
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

        #region

        public void InsertNewQuestion_UNITTEST()
        {
            DataAccessResult dataAccessResult = new DataAccessResult();
            Questions questions = new Questions();
            questions.QuestionTitle = "The patient has never been transfused before this incident and has no other serious medical illnesses.";
            questions.QuestionSubject = "The patient has never been transfused before this incident and has no other serious medical illnesses.";
            questions.Difficulty = 1;
            questions.UserID = 1;
            questions.TestID = 3;
            dataAccessResult = InsertNewQuestion(questions);
            bool IsError = dataAccessResult.IsError;
            string UserMessage = dataAccessResult.UserMessage;
            string TransactionDetails = dataAccessResult.TransactionDetails;
        }

        #endregion
    }
}