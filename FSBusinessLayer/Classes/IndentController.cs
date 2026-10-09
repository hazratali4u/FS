using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDatabaseLayer.Classes;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections;
using FSDataAccessLayer.Classes;

namespace FSBusinessLayer.Classes
{
    /// <summary>
    /// Class For Indent Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert Indent
    /// </item>
    /// <term>
    /// Update Indent
    /// </term>
    /// <item>
    /// Get Indent
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
    public class IndentController
    {
        #region Constructor

        /// <summary>
        /// Constructor for IndentController
        /// </summary>
        public IndentController()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion

        #region Private Variables

        IDbTransaction mTransaction;
        IDbConnection mConnection;

        #endregion

        #region Select
               
        public DataTable GetIndentMaster(int p_CUSTOMER_ID, int p_SUPPLIER_ID, long p_INDENT_MASTER_ID, long p_ENQUIREY_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetIndentMaster mIndentMaster = new uspGetIndentMaster();
                mIndentMaster.Connection = mConnection;
                mIndentMaster.CUSTOMER_ID = p_CUSTOMER_ID;
                mIndentMaster.SUPPLIER_ID = p_SUPPLIER_ID;
                mIndentMaster.INDENT_MASTER_ID = p_INDENT_MASTER_ID;
                mIndentMaster.ENQUIREY_ID = p_ENQUIREY_ID;
                DataTable dt = mIndentMaster.ExecuteTable();
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

        public DataTable GetIndentDetail(long p_INDENT_MASTER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetIndentDetail mIndentDetail = new uspGetIndentDetail();
                mIndentDetail.Connection = mConnection;
                mIndentDetail.INDENT_MASTER_ID = p_INDENT_MASTER_ID;
                DataTable dt = mIndentDetail.ExecuteTable();
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

        #region Insert, Update, Deleted

        /// <summary>
        /// Inserts Indent
        /// </summary>
        /// <param name="p_ENQUIRY_ID">Enquiry</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <param name="p_CUSTOMER_ID">Customer</param>
        /// <param name="p_REMARKS">Remarks</param>
        /// <param name="p_USER_ID">User</param>
        /// <param name="dtIndentDetail">IndentDetail</param>
        /// <returns>True on success and False on failure</returns>
        public bool InsertIndent(long p_ENQUIRY_ID, int p_DISTRIBUTOR_ID, DateTime p_DOCUMENT_DATE, int p_CUSTOMER_ID,int p_SUPPLIER_ID, string p_REMARKS, int p_USER_ID,
            int p_DAY1, int p_DAY2, string p_AccountNo , string p_BRANCH, string p_BRANCH_ADDRESS, string p_BRANCH_CODE, string p_RTGS_CODE, string p_WIFT_CODE, DataTable dtIndentDetail)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                uspInsertIndentMaster mIndentMaster = new uspInsertIndentMaster();
                mIndentMaster.Connection = mConnection;
                mIndentMaster.Transaction = mTransaction;
                mIndentMaster.ENQUIRY_ID = p_ENQUIRY_ID;
                mIndentMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mIndentMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mIndentMaster.CUSTOMER_ID = p_CUSTOMER_ID;
                mIndentMaster.SUPPLIER_ID = p_SUPPLIER_ID;
                mIndentMaster.REMARKS = p_REMARKS;
                mIndentMaster.USER_ID = p_USER_ID;
                mIndentMaster.DAY1 = p_DAY1;
                mIndentMaster.DAY2 = p_DAY2;
                mIndentMaster.AccountNo = p_AccountNo;
                mIndentMaster.BRANCH = p_BRANCH;
                mIndentMaster.BRANCH_ADDRESS = p_BRANCH_ADDRESS;
                mIndentMaster.BRANCH_CODE = p_BRANCH_CODE;
                mIndentMaster.RTGS_CODE = p_RTGS_CODE;
                mIndentMaster.WIFT_CODE = p_WIFT_CODE;

                mIndentMaster.ExecuteQuery();

                uspInsertIndentDetail mIndentDetail = new uspInsertIndentDetail();
                mIndentDetail.Connection = mConnection;
                mIndentDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtIndentDetail.Rows)
                {
                    mIndentDetail.INDENT_MASTER_ID = mIndentMaster.INDENT_MASTER_ID;
                    mIndentDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mIndentDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mIndentDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mIndentDetail.PAYMENT_ID = int.Parse(dr["PAYMENT_ID"].ToString());
                    mIndentDetail.UOM_ID = int.Parse(dr["UOM_ID"].ToString());
                    mIndentDetail.SHIPMENT_ID = int.Parse(dr["SHIPMENT_ID"].ToString());
                    mIndentDetail.SHIPMENTMODE_ID = int.Parse(dr["SHIPMENTMODE_ID"].ToString());
                    mIndentDetail.DELIVERYLOCATION_ID = int.Parse(dr["DELIVERYLOCATION_ID"].ToString());
                    mIndentDetail.LEAD_TIME = Convert.ToDateTime(dr["LEAD_TIME"]);
                    mIndentDetail.CURRENCY_ID = int.Parse(dr["CURRENCY_ID"].ToString());
                    mIndentDetail.SKU_CODE = dr["SKU_CODE"].ToString();
                    mIndentDetail.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
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
        /// Updates Enquiry
        /// </summary>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <param name="p_CUSTOMER_ID">Customer</param>
        /// <param name="p_REMARKS">Remarks</param>
        /// <param name="p_USER_ID">User</param>
        /// <param name="dtEnquiryDetail">EnquiryDetail</param>
        /// <returns>True on success and False on failure</returns>
        public bool UpdateIndent(long p_INDENT_MASTER_ID,long p_ENQUIRY_ID, int p_DISTRIBUTOR_ID, DateTime p_DOCUMENT_DATE, int p_CUSTOMER_ID, int p_SUPPLIER_ID, string p_REMARKS, int p_USER_ID,
            int p_DAY1, int p_DAY2, string p_AccountNo, string p_BRANCH, string p_BRANCH_ADDRESS, string p_BRANCH_CODE, string p_RTGS_CODE, string p_WIFT_CODE, DataTable dtIndentDetail)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                uspUpdateIndentMaster mIndentMaster = new uspUpdateIndentMaster();
                mIndentMaster.Connection = mConnection;
                mIndentMaster.Transaction = mTransaction;
                mIndentMaster.INDENT_MASTER_ID = p_INDENT_MASTER_ID;
                mIndentMaster.ENQUIRY_ID = p_ENQUIRY_ID;
                mIndentMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mIndentMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mIndentMaster.CUSTOMER_ID = p_CUSTOMER_ID;
                mIndentMaster.SUPPLIER_ID = p_SUPPLIER_ID;
                mIndentMaster.REMARKS = p_REMARKS;
                mIndentMaster.USER_ID = p_USER_ID;
                mIndentMaster.IS_ACTIVE = true;

                mIndentMaster.DAY1 = p_DAY1;
                mIndentMaster.DAY2 = p_DAY2;
                mIndentMaster.AccountNo = p_AccountNo;
                mIndentMaster.BRANCH = p_BRANCH;
                mIndentMaster.BRANCH_ADDRESS = p_BRANCH_ADDRESS;
                mIndentMaster.BRANCH_CODE = p_BRANCH_CODE;
                mIndentMaster.RTGS_CODE = p_RTGS_CODE;
                mIndentMaster.WIFT_CODE = p_WIFT_CODE;

                mIndentMaster.ExecuteQuery();

                uspDeleteIndentDetail mDelIndentDetail = new uspDeleteIndentDetail();
                mDelIndentDetail.Connection = mConnection;
                mDelIndentDetail.Transaction = mTransaction;
                mDelIndentDetail.INDENT_MASTER_ID = p_INDENT_MASTER_ID;
                mDelIndentDetail.ExecuteQuery();

                uspInsertIndentDetail mIndentDetail = new uspInsertIndentDetail();
                mIndentDetail.Connection = mConnection;
                mIndentDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtIndentDetail.Rows)
                {
                    mIndentDetail.INDENT_MASTER_ID = p_INDENT_MASTER_ID;
                    mIndentDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mIndentDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mIndentDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mIndentDetail.PAYMENT_ID = int.Parse(dr["PAYMENT_ID"].ToString());
                    mIndentDetail.UOM_ID = int.Parse(dr["UOM_ID"].ToString());
                    mIndentDetail.SHIPMENT_ID = int.Parse(dr["SHIPMENT_ID"].ToString());
                    mIndentDetail.SHIPMENTMODE_ID = int.Parse(dr["SHIPMENTMODE_ID"].ToString());
                    mIndentDetail.DELIVERYLOCATION_ID = int.Parse(dr["DELIVERYLOCATION_ID"].ToString());
                    mIndentDetail.LEAD_TIME = Convert.ToDateTime(dr["LEAD_TIME"]);
                    mIndentDetail.CURRENCY_ID = int.Parse(dr["CURRENCY_ID"].ToString());
                    mIndentDetail.SKU_CODE = dr["SKU_CODE"].ToString();
                    mIndentDetail.ExecuteQuery();
                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
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
    }
}

