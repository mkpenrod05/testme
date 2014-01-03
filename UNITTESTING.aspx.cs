using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using testme.DataAccessLayer;
using testme.Models;

namespace testme
{
    public partial class UNITTESTING : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CategoryDataAccess categoryDataAccess = new CategoryDataAccess();
            //categoryDataAccess.UpdateExistingCategoryTest();
            //categoryDataAccess.InsertNewCategoryTest();
            //categoryDataAccess.SelectAllCategoriesByUserTest();

            TestDataAccess testDataAccess = new TestDataAccess();
            //testDataAccess.InsertNewTest_UNITTEST();
            //testDataAccess.UpdateExistingTest_UNITTEST();
            //testDataAccess.SelectAllTestsByUser_UNITTEST();

            QuestionDataAccess questionDataAccess = new QuestionDataAccess();
            questionDataAccess.InsertNewQuestion_UNITTEST();
        }
    }
}