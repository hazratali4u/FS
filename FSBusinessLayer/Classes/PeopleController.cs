using System;
using FSDatabaseLayer.Classes;
using FSDataAccessLayer.Classes;
using FSCommonLayer.Classes;
using System.Data;

namespace FSBusinessLayer.Classes
{
    /// <summary>
    /// Class For People Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert People
    /// </item>
    /// <item>
    /// Get People
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
    public class PeopleController
    {
        #region Public Methods

        #region Select
             
        /// <summary>
        /// Gets People
        /// </summary>
        /// <param name="p_PEOPLE_ID">People</param>
        /// <param name="p_ORGANIZATION_ID">Organization</param>
        /// <param name="p_FNAME">FirstName</param>
        /// <param name="p_LNAME">LastName</param>
        /// <returns>Datatable</returns>
        public DataTable GetPeople(long p_PEOPLE_ID, long p_ORGANIZATION_ID, string p_FNAME, string p_LNAME)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetPeople ObjSelect = new spGetPeople();
                ObjSelect.Connection = mConnection;
                ObjSelect.PEOPLE_ID = p_PEOPLE_ID;
                ObjSelect.ORGANIZATION_ID = p_ORGANIZATION_ID;
                ObjSelect.FNAME = p_FNAME;
                ObjSelect.LNAME = p_LNAME;
                DataTable dt = ObjSelect.ExecuteTable();
                return dt;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets People
        /// </summary>
        /// <param name="p_PEOPLE_ID">People</param>
        /// <param name="p_ORGANIZATION_ID">Organization</param>
        /// <param name="p_FNAME">FirstName</param>
        /// <param name="p_LNAME">LastName</param>
        /// <returns>Datatable</returns>
        public DataTable GetPeople(long p_PEOPLE_ID, long p_ORGANIZATION_ID,string p_FNAME, string p_LNAME,int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetPeople ObjSelect = new spGetPeople();
                ObjSelect.Connection = mConnection;
                ObjSelect.PEOPLE_ID = p_PEOPLE_ID;
                ObjSelect.ORGANIZATION_ID = p_ORGANIZATION_ID;
                ObjSelect.FNAME = p_FNAME;
                ObjSelect.LNAME = p_LNAME;
                ObjSelect.USER_ID = p_USER_ID;
                DataTable dt = ObjSelect.ExecuteTable();
                return dt;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public DataTable GetPeople(long p_PEOPLE_ID, long p_ORGANIZATION_ID, string p_FNAME, string p_LNAME, int p_USER_ID, int p_TYPE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetPeople ObjSelect = new spGetPeople();
                ObjSelect.Connection = mConnection;
                ObjSelect.PEOPLE_ID = p_PEOPLE_ID;
                ObjSelect.ORGANIZATION_ID = p_ORGANIZATION_ID;
                ObjSelect.FNAME = p_FNAME;
                ObjSelect.LNAME = p_LNAME;
                ObjSelect.USER_ID = p_USER_ID;
                ObjSelect.TYPE = p_TYPE;
                DataTable dt = ObjSelect.ExecuteTable();
                return dt;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets People Detail
        /// </summary>
        /// <param name="p_PEOPLE_ID">People</param>
        /// <returns>Datatable</returns>
        public DataTable GetPeopleDetail(long p_PEOPLE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetPeopleDetail ObjSelect = new spGetPeopleDetail();
                ObjSelect.Connection = mConnection;
                ObjSelect.PEOPLE_ID = p_PEOPLE_ID;
                DataTable dt = ObjSelect.ExecuteTable();
                return dt;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion

        #region Insert, Update, Delete

        /// <summary>
        /// Inserts People
        /// </summary>
        /// <param name="p_ORGANIZATION_ID">Organization</param>
        /// <param name="p_TITLE">Title</param>
        /// <param name="p_FNAME">First Name</param>
        /// <param name="p_LNAME">Last Name</param>
        /// <param name="dtDetail">PeopleDetail</param>
        /// <returns>True on Success And False on Failure</returns>
        public long InsertPeople(long p_ORGANIZATION_ID,int p_TITLE, string p_FNAME, string p_LNAME, DateTime p_DOB, int p_DEPARTMENT, string p_EMAIL,
            string p_PHONE, int p_USER_ID, int p_LOCATION_ID, string p_DESIGNATION_ID, string p_ACCOUNT_NO, DataTable dtDetail)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            long PeopleID = Constants.LongNullValue;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spInsertPeople ObjInsert = new spInsertPeople();
                ObjInsert.Connection = mConnection;
                ObjInsert.Transaction = mTransaction;

                ObjInsert.ORGANIZATION_ID = p_ORGANIZATION_ID;
                ObjInsert.LOCATION_ID = p_LOCATION_ID;
                ObjInsert.DESIGNATION_ID = p_DESIGNATION_ID;
                ObjInsert.TITLE = p_TITLE;
                ObjInsert.FNAME = p_FNAME;
                ObjInsert.LNAME = p_LNAME;
                ObjInsert.DOB = p_DOB;
                ObjInsert.DEPARTMENT = p_DEPARTMENT;
                ObjInsert.EMAIL = p_EMAIL;
                ObjInsert.PHONE = p_PHONE;
                ObjInsert.USER_ID = p_USER_ID;
                ObjInsert.ACCOUNT_NO = p_ACCOUNT_NO;
                ObjInsert.ExecuteQuery();

                PeopleID = ObjInsert.PEOPLE_ID;
                if (dtDetail.Rows.Count > 0)
                {
                    spInsertPeopleDetail ObjInsertDetail = new spInsertPeopleDetail();
                    ObjInsertDetail.Connection = mConnection;
                    ObjInsertDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtDetail.Rows)
                    {
                        ObjInsertDetail.PEOPLE_ID = ObjInsert.PEOPLE_ID;
                        ObjInsertDetail.FIELD_TYPE = dr["FIELD_TYPE"].ToString();
                        ObjInsertDetail.FIELD_VALUE = dr["FIELD_VALUE"].ToString();

                        ObjInsertDetail.ExecuteQuery();
                    }
                }
                mTransaction.Commit();
                return PeopleID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return Constants.LongNullValue;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Updates People
        /// </summary>
        /// <param name="p_PEOPLE_ID">People</param>
        /// <param name="p_ORGANIZATION_ID">Organization</param>
        /// <param name="p_TITLE">Title</param>
        /// <param name="p_FNAME">FirstName</param>
        /// <param name="p_LNAME">LastName</param>
        /// <param name="dtDetail">PeopleDetail</param>
        /// <returns>True on Success And False on Failure</returns>
        public bool UpdatePeople(long p_PEOPLE_ID, long p_ORGANIZATION_ID, int p_TITLE, string p_FNAME, string p_LNAME, DateTime p_DOB, int p_DEPARTMENT, string p_EMAIL,
            string p_PHONE, int p_USER_ID, int p_LOCATION_ID, string p_DESIGNATION_ID, bool p_IS_ACTIVE, string p_ACCOUNT_NO, DataTable dtDetail)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdatePeople ObjUpdate = new spUpdatePeople();
                ObjUpdate.Connection = mConnection;
                ObjUpdate.Transaction = mTransaction;

                ObjUpdate.PEOPLE_ID = p_PEOPLE_ID;
                ObjUpdate.ORGANIZATION_ID = p_ORGANIZATION_ID;
                ObjUpdate.LOCATION_ID = p_LOCATION_ID;
                ObjUpdate.DESIGNATION_ID = p_DESIGNATION_ID;
                ObjUpdate.TITLE = p_TITLE;
                ObjUpdate.FNAME = p_FNAME;
                ObjUpdate.LNAME = p_LNAME;
                ObjUpdate.DOB = p_DOB;
                ObjUpdate.DEPARTMENT = p_DEPARTMENT;
                ObjUpdate.EMAIL = p_EMAIL;
                ObjUpdate.PHONE = p_PHONE;
                ObjUpdate.IS_ACTIVE = p_IS_ACTIVE;
                ObjUpdate.USER_ID = p_USER_ID;
                ObjUpdate.ACCOUNT_NO = p_ACCOUNT_NO;
                ObjUpdate.ExecuteQuery();

                //Delete Old Detail
                spDeletePeopleDetail objDelelete = new spDeletePeopleDetail();
                objDelelete.Connection = mConnection;
                objDelelete.Transaction = mTransaction;

                objDelelete.PEOPLE_ID = p_PEOPLE_ID;

                objDelelete.ExecuteQuery();

                if (dtDetail.Rows.Count > 0)
                {
                    spInsertPeopleDetail ObjInsertDetail = new spInsertPeopleDetail();
                    ObjInsertDetail.Connection = mConnection;
                    ObjInsertDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtDetail.Rows)
                    {
                        ObjInsertDetail.PEOPLE_ID = p_PEOPLE_ID;
                        ObjInsertDetail.FIELD_TYPE = dr["FIELD_TYPE"].ToString();
                        ObjInsertDetail.FIELD_VALUE = dr["FIELD_VALUE"].ToString();

                        ObjInsertDetail.ExecuteQuery();
                    }
                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Deletes People
        /// </summary>
        /// <param name="p_PEOPLE_ID">People</param>
        /// <returns>True on Success And False on Failure</returns>
        public bool DeletePeople(long p_PEOPLE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spDeletePeople ObjUpdate = new spDeletePeople();
                ObjUpdate.Connection = mConnection;

                ObjUpdate.PEOPLE_ID = p_PEOPLE_ID;

                bool Bvalue = ObjUpdate.ExecuteQuery();
                return Bvalue;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion

        #endregion
    }
}
