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
    /// Class For Enquiry Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert Enquiry
    /// </item>
    /// <term>
    /// Update Enquiry
    /// </term>
    /// <item>
    /// Get Enquiry
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
    public class EnquiryController
    {
        #region Constructor

        /// <summary>
        /// Constructor for EnquiryController
        /// </summary>
        public EnquiryController()
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

        /// <summary>
        /// Get Enquiry Detail
        /// </summary>
        /// <remarks>
        /// Returns Enquiry Detail as Datatable
        /// </remarks>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="PConnection">Connection</param>
        /// <param name="PTransaction">Transaction</param>
        /// <returns>Enquiry Detail as Datatable</returns>
        public DataTable GetEnquiryDetail(long p_ENQUIREY_MASTER_ID, IDbConnection PConnection, IDbTransaction PTransaction)
        {
            try
            {
                uspGetEnquiryDetail mEnquiryDetail = new uspGetEnquiryDetail();
                mEnquiryDetail.Connection = PConnection;
                mEnquiryDetail.Transaction = PTransaction;
                mEnquiryDetail.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
                DataTable dt = mEnquiryDetail.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
        }

        /// <summary>
        /// Get Enquiry Detail
        /// </summary>
        /// <remarks>
        /// Returns Enquiry Detail as Datatable
        /// </remarks>
        /// <param name="p_ENQUIREY_MASTER_ID">Enquiry</param>
        /// <returns>Enquiry Detail as Datatable</returns>
        public DataTable GetEnquiryDetail(long p_ENQUIREY_MASTER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetEnquiryDetail mEnquiryDetail = new uspGetEnquiryDetail();
                mEnquiryDetail.Connection = mConnection;
                mEnquiryDetail.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
                DataTable dt = mEnquiryDetail.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
        }

        /// <summary>
        /// Gets Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_User_Id">InsertedBy</param>
        /// <param name="p_Posting">Posting</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage  Document No as Datatable</returns>
        public DataTable GetEnquiryMaster(int p_DISTRIBUTOR_ID, long p_ENQUIREY_MASTER_ID, int p_User_Id, int p_TYPE_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetEnquiryMaster mEnquiryMaster = new uspGetEnquiryMaster();
                mEnquiryMaster.Connection = mConnection;
                mEnquiryMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mEnquiryMaster.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
                mEnquiryMaster.USER_ID = p_User_Id;
                mEnquiryMaster.TYPE_ID = p_TYPE_ID;
                DataTable dt = mEnquiryMaster.ExecuteTable();
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
        /// Get Enquiry 
        /// </summary>
        /// <remarks>
        /// Returns Enquiry as Datatable
        /// </remarks>
        /// <param name="p_ENQUIREY_MASTER_ID">Enquiry</param>
        /// <returns>Enquiry as Datatable</returns>
        public DataTable GetEnquiry(long p_ENQUIREY_MASTER_ID, int p_DISTRIBUTOR_ID, int p_USER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetEnquiry mEnquiry = new uspGetEnquiry();
                mEnquiry.Connection = mConnection;
                mEnquiry.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
                mEnquiry.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mEnquiry.USER_ID = p_USER_ID;
                DataTable dt = mEnquiry.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
        }

        public DataTable GetEmailReciepant(long p_ENQUIREY_MASTER_ID, int p_PRODUCT_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetEmailReceipant mSkuHierarchy = new uspGetEmailReceipant();
                mSkuHierarchy.Connection = mConnection;

                mSkuHierarchy.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
                mSkuHierarchy.PRODUCT_ID = p_PRODUCT_ID;
                DataTable dt = new DataTable();
                dt = mSkuHierarchy.ExecuteTable();
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
        /// Get Enquiry Payment
        /// </summary>
        /// <remarks>
        /// Returns Enquiry as Datatable
        /// </remarks>
        /// <param name="p_ENQUIREY_ID">Enquiry</param>
        /// <returns>Enquiry as Datatable</returns>
        public DataTable GetEnquiryPayment(long p_ENQUIREY_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetEnquiryPayment mEnquiry = new uspGetEnquiryPayment();
                mEnquiry.Connection = mConnection;
                mEnquiry.ENQUIRY_ID = p_ENQUIREY_ID;
                DataTable dt = mEnquiry.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
        }

        #endregion

        #region Insert, Update, Deleted

        /// <summary>
        /// Inserts Enquiry 
        /// </summary>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <param name="p_CUSTOMER_ID">Customer</param>
        /// <param name="p_REMARKS">Remarks</param>
        /// <param name="p_USER_ID">User</param>
        /// <param name="dtEnquiryDetail">EnquiryDetail</param>
        /// <returns>True on success and False on failure</returns>
        public long InsertEnquiry(int p_TYPE_ID,int p_DISTRIBUTOR_ID, DateTime p_DOCUMENT_DATE, int p_CUSTOMER_ID,string p_REMARKS,int p_USER_ID,long p_PEOPLE_ID, DataTable dtEnquiryDetail)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                uspInsertEnquiryMaster mEnqMaster = new uspInsertEnquiryMaster();
                mEnqMaster.Connection = mConnection;
                mEnqMaster.Transaction = mTransaction;
                mEnqMaster.TYPE_ID = p_TYPE_ID;
                mEnqMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mEnqMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mEnqMaster.CUSTOMER_ID = p_CUSTOMER_ID;
                mEnqMaster.REMARKS = p_REMARKS;
                mEnqMaster.USER_ID = p_USER_ID;
                mEnqMaster.PEOPLE_ID = p_PEOPLE_ID;

                mEnqMaster.ExecuteQuery();

                uspInsertEnquiryDetail mPurchaseDetail = new uspInsertEnquiryDetail();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtEnquiryDetail.Rows)
                {
                    mPurchaseDetail.ENQUIREY_MASTER_ID = mEnqMaster.ENQUIREY_MASTER_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.DOCUMENT_DATE = Convert.ToDateTime(dr["DOCUMENT_DATE"]);
                    mPurchaseDetail.ENQUIREY_DETAIL_ID2 = Convert.ToInt64(dr["ENQUIREY_DETAIL_ID2"]);
                    mPurchaseDetail.ExecuteQuery();
                }
                mTransaction.Commit();
                return mEnqMaster.ENQUIREY_MASTER_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
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
        /// Updates Enquiry
        /// </summary>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <param name="p_CUSTOMER_ID">Customer</param>
        /// <param name="p_REMARKS">Remarks</param>
        /// <param name="p_USER_ID">User</param>
        /// <param name="dtEnquiryDetail">EnquiryDetail</param>
        /// <returns>True on success and False on failure</returns>
        public bool UpdateEnquiry(int p_TYPE_ID,long p_ENQUIREY_MASTER_ID, int p_DISTRIBUTOR_ID, DateTime p_DOCUMENT_DATE, int p_CUSTOMER_ID, string p_REMARKS, int p_USER_ID,int p_STATUS_ID,long p_PEOPLE_ID, DataTable dtEnquiryDetail)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                uspUpdateEnquiryMaster mEnquiryMaster = new uspUpdateEnquiryMaster();
                mEnquiryMaster.Connection = mConnection;
                mEnquiryMaster.Transaction = mTransaction;
                mEnquiryMaster.TYPE_ID = p_TYPE_ID;
                mEnquiryMaster.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
                mEnquiryMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mEnquiryMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mEnquiryMaster.CUSTOMER_ID = p_CUSTOMER_ID;
                mEnquiryMaster.REMARKS = p_REMARKS;
                mEnquiryMaster.USER_ID = p_USER_ID;
                mEnquiryMaster.STATUS_ID = p_STATUS_ID;
                mEnquiryMaster.IS_ACTIVE = true;
                mEnquiryMaster.PEOPLE_ID = p_PEOPLE_ID;
                mEnquiryMaster.ExecuteQuery();

                DataTable dt = GetEnquiryDetail(p_ENQUIREY_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    uspDeleteEnquiryDetail mDelEnquiryDetail = new uspDeleteEnquiryDetail();
                    mDelEnquiryDetail.Connection = mConnection;
                    mDelEnquiryDetail.Transaction = mTransaction;
                    mDelEnquiryDetail.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
                    mDelEnquiryDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mDelEnquiryDetail.ExecuteQuery();
                }

                uspInsertEnquiryDetail mEnquiryDetail = new uspInsertEnquiryDetail();
                mEnquiryDetail.Connection = mConnection;
                mEnquiryDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtEnquiryDetail.Rows)
                {
                    mEnquiryDetail.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
                    mEnquiryDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mEnquiryDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mEnquiryDetail.DOCUMENT_DATE = Convert.ToDateTime(dr["DOCUMENT_DATE"]);
                    mEnquiryDetail.ENQUIREY_DETAIL_ID2 = Convert.ToInt64(dr["ENQUIREY_DETAIL_ID2"]);
                    mEnquiryDetail.ExecuteQuery();
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
        /// <param name="p_STATUS_ID">Status</param>
        /// <returns>True on success and False on failure</returns>
        public bool UpdateEnquiry(long p_ENQUIREY_MASTER_ID, int p_DISTRIBUTOR_ID, DateTime p_DOCUMENT_DATE, int p_CUSTOMER_ID, string p_REMARKS, int p_USER_ID, int p_STATUS_ID, bool p_IS_ACTIVE)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                uspUpdateEnquiryMaster mEnquiryMaster = new uspUpdateEnquiryMaster();
                mEnquiryMaster.Connection = mConnection;
                mEnquiryMaster.Transaction = mTransaction;
                mEnquiryMaster.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
                mEnquiryMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mEnquiryMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mEnquiryMaster.CUSTOMER_ID = p_CUSTOMER_ID;
                mEnquiryMaster.REMARKS = p_REMARKS;
                mEnquiryMaster.USER_ID = p_USER_ID;
                mEnquiryMaster.STATUS_ID = p_STATUS_ID;
                mEnquiryMaster.IS_ACTIVE = p_IS_ACTIVE;
                mEnquiryMaster.PEOPLE_ID = Constants.LongNullValue;
                mEnquiryMaster.ExecuteQuery();                

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

        public bool InsertCustomerProductApproval(int p_DISTRIBUTOR_ID, int p_CUSTOMER_ID, int p_PRODUCT_ID,int p_SUPPLIER_ID, int p_USER_ID, int p_IS_APPROVE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspInsertCustomerApprovedProduct ObjInsert = new uspInsertCustomerApprovedProduct();
                ObjInsert.Connection = mConnection;
                ObjInsert.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                ObjInsert.CUSTOMER_ID = p_CUSTOMER_ID;
                ObjInsert.PRODUCT_ID = p_PRODUCT_ID;
                ObjInsert.SUPPLIER_ID = p_SUPPLIER_ID;
                ObjInsert.USER_ID = p_USER_ID;
                ObjInsert.USER_ID = p_USER_ID;
                ObjInsert.IS_APPROVE = p_IS_APPROVE;
                bool Bvalue = ObjInsert.ExecuteQuery();
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

        public long InsertEnquiryPayment(long p_ENQUIRY_ID, long p_INVOICE_ID,long p_ENQUIREY_DETAIL_ID, string p_PAYMENT_REFERENCE,int p_PAYMENT_MODE,
            string p_CHEQUE_NO, string p_BANK_NAME, DateTime p_CHEQUE_DATE, string p_SLIP_NO, decimal p_AMOUNT, DateTime p_PAYMENT_DATE, string p_REMARKS, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspInsertEnquiryPayment ObjInsert = new uspInsertEnquiryPayment();
                ObjInsert.Connection = mConnection;
                ObjInsert.ENQUIRY_ID = p_ENQUIRY_ID;
                ObjInsert.INVOICE_ID = p_INVOICE_ID;
                ObjInsert.ENQUIREY_DETAIL_ID = p_ENQUIREY_DETAIL_ID;
                ObjInsert.PAYMENT_REFERENCE = p_PAYMENT_REFERENCE;
                ObjInsert.PAYMENT_MODE = p_PAYMENT_MODE;
                ObjInsert.CHEQUE_NO = p_CHEQUE_NO;
                ObjInsert.BANK_NAME = p_BANK_NAME;
                ObjInsert.CHEQUE_DATE = p_CHEQUE_DATE;
                ObjInsert.SLIP_NO = p_SLIP_NO;
                ObjInsert.AMOUNT = p_AMOUNT;
                ObjInsert.PAYMENT_DATE = p_PAYMENT_DATE;
                ObjInsert.REMARKS = p_REMARKS;
                ObjInsert.USER_ID = p_USER_ID;

                ObjInsert.ExecuteQuery();

                return ObjInsert.ENQUIRY_PAYMENT_ID;
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


        public bool UpdateEnquiryPayment(long p_ENQUIRY_PAYMENT_ID, long p_ENQUIRY_ID, long p_INVOICE_ID,long p_ENQUIREY_DETAIL_ID, string p_PAYMENT_REFERENCE, int p_PAYMENT_MODE,
            string p_CHEQUE_NO, string p_BANK_NAME, DateTime p_CHEQUE_DATE, string p_SLIP_NO, decimal p_AMOUNT, DateTime p_PAYMENT_DATE, string p_REMARKS, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspUpdateEnquiryPayment ObjInsert = new uspUpdateEnquiryPayment();
                ObjInsert.Connection = mConnection;
                ObjInsert.ENQUIRY_PAYMENT_ID = p_ENQUIRY_PAYMENT_ID;
                ObjInsert.ENQUIRY_ID = p_ENQUIRY_ID;
                ObjInsert.INVOICE_ID = p_INVOICE_ID;
                ObjInsert.ENQUIREY_DETAIL_ID = p_ENQUIREY_DETAIL_ID;
                ObjInsert.PAYMENT_REFERENCE = p_PAYMENT_REFERENCE;
                ObjInsert.PAYMENT_MODE = p_PAYMENT_MODE;
                ObjInsert.CHEQUE_NO = p_CHEQUE_NO;
                ObjInsert.BANK_NAME = p_BANK_NAME;
                ObjInsert.CHEQUE_DATE = p_CHEQUE_DATE;
                ObjInsert.SLIP_NO = p_SLIP_NO;
                ObjInsert.AMOUNT = p_AMOUNT;
                ObjInsert.PAYMENT_DATE = p_PAYMENT_DATE;
                ObjInsert.REMARKS = p_REMARKS;
                ObjInsert.USER_ID = p_USER_ID;

                ObjInsert.ExecuteQuery();

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
        /// Delete Enquiry Payment
        /// </summary>
        /// <param name="p_ENQUIRY_PAYMENT_ID">PyamentID</param>
        /// <param name="p_USER_ID">User</param>
        /// <returns>True on success and False on failure</returns>
        public bool DeleteEnquiryPayment(long p_ENQUIRY_PAYMENT_ID, int p_USER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                uspDeleteEnquiryPayment mEnquiryMaster = new uspDeleteEnquiryPayment();
                mEnquiryMaster.Connection = mConnection;
                mEnquiryMaster.Transaction = mTransaction;
                mEnquiryMaster.ENQUIRY_PAYMENT_ID = p_ENQUIRY_PAYMENT_ID;
                mEnquiryMaster.USER_ID = p_USER_ID;
                mEnquiryMaster.ExecuteQuery();

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

