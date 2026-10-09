using System;
using FSDatabaseLayer.Classes;
using FSDataAccessLayer.Classes;
using FSCommonLayer.Classes;
using System.Data;

namespace FSBusinessLayer.Classes
{
    /// <summary>
    /// Class For Quotation Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert Quotation
    /// </item>
    /// <item>
    /// Get Quotation
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
    public class QuotationController
    {
        #region Private Variables

        IDbTransaction mTransaction;
        IDbConnection mConnection;

        #endregion

        #region Public Methods
        
        #region Select

        /// <summary>
        /// Gets Quotation Data
        /// </summary>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_CUSTOMER_ID">Customer</param>
        /// <returns>DataTable</returns>
        public DataTable GetQuotationProducts(int p_DISTRIBUTOR_ID, long p_CUSTOMER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetQuotationProducts ObjSelect = new uspGetQuotationProducts();
                ObjSelect.Connection = mConnection;
                ObjSelect.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                ObjSelect.CUSTOMER_ID = p_CUSTOMER_ID;
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
        /// Gets Approval Data
        /// </summary>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_CUSTOMER_ID">Customer</param>
        /// <returns>DataTable</returns>
        public DataTable GetApprovalProducts(int p_DISTRIBUTOR_ID, long p_CUSTOMER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetApproveProducts ObjSelect = new uspGetApproveProducts();
                ObjSelect.Connection = mConnection;
                ObjSelect.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                ObjSelect.CUSTOMER_ID = p_CUSTOMER_ID;
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
        /// Gets Approval Data
        /// </summary>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_CUSTOMER_ID">Customer</param>
        /// <returns>DataTable</returns>
        public DataTable GetApprovalProductsNew(int p_DISTRIBUTOR_ID, long p_CUSTOMER_ID, long p_ENQUIREY_MASTER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetApproveProductsNew ObjSelect = new uspGetApproveProductsNew();
                ObjSelect.Connection = mConnection;
                ObjSelect.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                ObjSelect.CUSTOMER_ID = p_CUSTOMER_ID;
                ObjSelect.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
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
        /// Gets Customer Quotation Data
        /// </summary>
        /// <param name="ENQUIREY_MASTER_ID">Enquiry</param>
        /// <returns>DataTable</returns>
        public DataTable GetCustomerQuotation(long p_ENQUIREY_MASTER_ID, bool p_IS_QUOTATION)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetCustomerQuotationData ObjSelect = new uspGetCustomerQuotationData();
                ObjSelect.Connection = mConnection;
                ObjSelect.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
                ObjSelect.IS_QUOTATION = p_IS_QUOTATION;
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
        /// Gets Email Templates
        /// </summary>
        /// <param name="p_EMAIL_TEMPLATE_ID">EmailTemplate</param>
        /// <param name="p_TYPE_ID">Type</param>
        /// <returns>DataTable</returns>
        public DataTable GetEmailTemplate(int p_EMAIL_TEMPLATE_ID, int p_TYPE_ID, int p_CATEGORY_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetEmailTemplate ObjSelect = new uspGetEmailTemplate();
                ObjSelect.Connection = mConnection;
                ObjSelect.EMAIL_TEMPLATE_ID = p_EMAIL_TEMPLATE_ID;
                ObjSelect.TYPE_ID = p_TYPE_ID;
                ObjSelect.CATEGORY_ID = p_CATEGORY_ID;
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

        public DataTable GetQuotationSubDetail(long p_QUOTATION_MASTER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetQuotationDetailSub ObjSelect = new uspGetQuotationDetailSub();
                ObjSelect.Connection = mConnection;
                ObjSelect.QUOTATION_MASTER_ID = p_QUOTATION_MASTER_ID;
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
        /// Gets Invoice Data Data
        /// </summary>
        /// <param name="ENQUIREY_MASTER_ID">Enquiry</param>
        /// <returns>DataTable</returns>
        public DataTable GetInvoiceData(long p_ENQUIREY_MASTER_ID,long p_INVOICE_MASTER_ID, bool p_IS_INVOICE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetInvoiceData ObjSelect = new uspGetInvoiceData();
                ObjSelect.Connection = mConnection;
                ObjSelect.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
                ObjSelect.INVOICE_MASTER_ID = p_INVOICE_MASTER_ID;
                ObjSelect.IS_INVOICE = p_IS_INVOICE;
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

        public DataTable GetMaxInvoiceNo(long p_ENQUIREY_MASTER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetMaxInvoiceNo ObjSelect = new uspGetMaxInvoiceNo();
                ObjSelect.Connection = mConnection;
                ObjSelect.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
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
        /// Gets Invoice Data Data
        /// </summary>
        /// <param name="ENQUIREY_MASTER_ID">Enquiry</param>
        /// <returns>DataTable</returns>
        public DataTable GetEnquiryInvoice(long p_ENQUIREY_MASTER_ID, int p_CUSTOMER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetEnquirInvoice ObjSelect = new uspGetEnquirInvoice();
                ObjSelect.Connection = mConnection;
                ObjSelect.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
                ObjSelect.CUSTOMER_ID = p_CUSTOMER_ID;
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
        /// Updates Quotation
        /// </summary>
        /// <param name="p_PURCHASE_DETAIL_ID">PurchaseDetail</param>
        /// <param name="p_SKU_ID">SKU</param>
        /// <param name="p_ISQUOTATION">IsQuotation</param>
        /// <param name="p_ISAPPROVED">IsApprove</param>
        /// <returns>True on Success and False on Failue</returns>
        public bool UpdateQuotation(long p_PURCHASE_DETAIL_ID, int p_SKU_ID, bool p_ISQUOTATION, bool p_ISAPPROVED)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspUpdateQuotation ObjUpdate = new uspUpdateQuotation();
                ObjUpdate.Connection = mConnection;

                ObjUpdate.PURCHASE_DETAIL_ID = p_PURCHASE_DETAIL_ID;
                ObjUpdate.SKU_ID = p_SKU_ID;
                ObjUpdate.ISQUOTATION = p_ISQUOTATION;
                ObjUpdate.ISAPPROVED = p_ISAPPROVED;
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

        /// <summary>
        /// Updates Approve
        /// </summary>
        /// <param name="p_PURCHASE_DETAIL_ID">PurchaseDetail</param>
        /// <param name="p_SKU_ID">SKU</param>
        /// <param name="p_ISAPPROVED">IsApprove</param>
        /// <returns>True on Success and False on Failue</returns>
        public bool UpdateApprove(long p_PURCHASE_DETAIL_ID, int p_SKU_ID,bool p_ISAPPROVED)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspUpdateApprove ObjUpdate = new uspUpdateApprove();
                ObjUpdate.Connection = mConnection;

                ObjUpdate.PURCHASE_DETAIL_ID = p_PURCHASE_DETAIL_ID;
                ObjUpdate.SKU_ID = p_SKU_ID;
                ObjUpdate.IsApproved = p_ISAPPROVED;
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

        public bool InsertQuotation(long p_ENQUIREY_MASTER_ID, int p_DISTRIBUTOR_ID, long p_CUSTOMER_ID,int p_TEMPLATE_ID, DateTime p_DOCUMENT_DATE, int p_USER_ID, DataTable dtQuotationDetail, DataTable dtQuotationDetailSub)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                
                uspInsertQuotionMaster mQuotation = new uspInsertQuotionMaster();
                mQuotation.Connection = mConnection;
                mQuotation.Transaction = mTransaction;
                mQuotation.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
                mQuotation.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mQuotation.CUSTOMER_ID = p_CUSTOMER_ID;
                mQuotation.TEMPLATE_ID = p_TEMPLATE_ID;
                mQuotation.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mQuotation.USER_ID = p_USER_ID;
                mQuotation.ExecuteQuery();


                uspDeleteQuotationDetail mDelQuotationDetail = new uspDeleteQuotationDetail();
                mDelQuotationDetail.Connection = mConnection;
                mDelQuotationDetail.Transaction = mTransaction;
                mDelQuotationDetail.QUOTATION_MASTER_ID = mQuotation.QUOTATION_MASTER_ID;
                mDelQuotationDetail.ExecuteQuery();
                
                uspInsertQuotionDetail mQuotationDetail = new uspInsertQuotionDetail();
                mQuotationDetail.Connection = mConnection;
                mQuotationDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtQuotationDetail.Rows)
                {
                    mQuotationDetail.QUOTATION_MASTER_ID = mQuotation.QUOTATION_MASTER_ID;
                    mQuotationDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mQuotationDetail.QUANTITY = Convert.ToInt32(dr["QUANTITY"]);
                    mQuotationDetail.CURRENCY_ID = int.Parse(dr["CURRENCY_ID"].ToString());
                    mQuotationDetail.RATE = decimal.Parse(dr["RATE"].ToString());
                    mQuotationDetail.DURATION = dr["DURATION"].ToString();
                    mQuotationDetail.IS_QUOTATION = Convert.ToBoolean(dr["IS_QUOTATION"]);
                    mQuotationDetail.ENQUIREY_DETAIL_ID = Convert.ToInt64(dr["ENQUIREY_DETAIL_ID"]);
                    mQuotationDetail.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
                    mQuotationDetail.ExecuteQuery();
                }


                uspDeleteQuotationDetailSub mDelQuotationDetailSub = new uspDeleteQuotationDetailSub();
                mDelQuotationDetailSub.Connection = mConnection;
                mDelQuotationDetailSub.Transaction = mTransaction;
                mDelQuotationDetailSub.QUOTATION_MASTER_ID = mQuotation.QUOTATION_MASTER_ID;
                mDelQuotationDetailSub.ExecuteQuery();


                uspInsertQuotionDetailSub mQuotationDetailSub = new uspInsertQuotionDetailSub();
                mQuotationDetailSub.Connection = mConnection;
                mQuotationDetailSub.Transaction = mTransaction;

                foreach (DataRow drSub in dtQuotationDetailSub.Rows)
                {
                    mQuotationDetailSub.QUOTATION_MASTER_ID = mQuotation.QUOTATION_MASTER_ID;
                    mQuotationDetailSub.ENQUIREY_DETAIL_ID = int.Parse(drSub["ENQUIREY_DETAIL_ID"].ToString());
                    mQuotationDetailSub.PRODUCT_DETAIL = drSub["PRODUCT_DETAIL"].ToString();
                    mQuotationDetailSub.ExecuteQuery();
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

        public bool InsertQuotation(long p_ENQUIREY_MASTER_ID, int p_DISTRIBUTOR_ID, long p_CUSTOMER_ID, DateTime p_DOCUMENT_DATE, int p_USER_ID, DataTable dtQuotationDetail)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                uspInsertQuotionMaster mQuotation = new uspInsertQuotionMaster();
                mQuotation.Connection = mConnection;
                mQuotation.Transaction = mTransaction;
                mQuotation.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
                mQuotation.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mQuotation.CUSTOMER_ID = p_CUSTOMER_ID;
                mQuotation.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mQuotation.USER_ID = p_USER_ID;
                mQuotation.ExecuteQuery();


                uspDeleteQuotationDetail mDelQuotationDetail = new uspDeleteQuotationDetail();
                mDelQuotationDetail.Connection = mConnection;
                mDelQuotationDetail.Transaction = mTransaction;
                mDelQuotationDetail.QUOTATION_MASTER_ID = mQuotation.QUOTATION_MASTER_ID;
                mDelQuotationDetail.ExecuteQuery();

                uspInsertQuotionDetail mQuotationDetail = new uspInsertQuotionDetail();
                mQuotationDetail.Connection = mConnection;
                mQuotationDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtQuotationDetail.Rows)
                {
                    mQuotationDetail.QUOTATION_MASTER_ID = mQuotation.QUOTATION_MASTER_ID;
                    mQuotationDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mQuotationDetail.CURRENCY_ID = int.Parse(dr["CURRENCY_ID"].ToString());
                    mQuotationDetail.RATE = decimal.Parse(dr["RATE"].ToString());
                    mQuotationDetail.DURATION = dr["DURATION"].ToString();
                    mQuotationDetail.IS_QUOTATION = Convert.ToBoolean(dr["IS_QUOTATION"]);
                    mQuotationDetail.ExecuteQuery();
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

        public bool InsertQuotationReceipient(long p_ENQUIRY_MASTER_ID,int p_PRODUCT_ID, int p_RECEIPIENT_TYPE_ID, long p_RECEIPIENT_ID, string p_RECEIPIENT_EMAIL, int p_EMAIL_ADDRESS_TYPE, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspInsertQuotionReceipient ObjInsert = new uspInsertQuotionReceipient();
                ObjInsert.Connection = mConnection;
                ObjInsert.ENQUIRY_MASTER_ID = p_ENQUIRY_MASTER_ID;
                ObjInsert.PRODUCT_ID = p_PRODUCT_ID;
                ObjInsert.RECEIPIENT_TYPE_ID = p_RECEIPIENT_TYPE_ID;
                ObjInsert.RECEIPIENT_ID = p_RECEIPIENT_ID;
                ObjInsert.RECEIPIENT_EMAIL = p_RECEIPIENT_EMAIL;
                ObjInsert.EMAIL_ADDRESS_TYPE = p_EMAIL_ADDRESS_TYPE;
                ObjInsert.USER_ID = p_USER_ID;
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

        /// <summary>
        /// Updates Email Template
        /// </summary>
        /// <param name="p_EMAIL_TEMPLATE_ID">EmailTemplate</param>
        /// <param name="p_SUBJECT">Subject</param>
        /// <param name="p_BODY">Body</param>
        /// <param name="p_USER_ID">User</param>
        /// <param name="p_IS_ACTIVE">IsActive</param>
        /// <returns>True on Success and False on Failue</returns>
        public bool UpdateEmailTemplate(int p_EMAIL_TEMPLATE_ID,int p_CATEGORY_ID, string p_SUBJECT, string p_BODY, string p_SIGNATURE,string p_TERMS, int p_USER_ID, bool p_IS_ACTIVE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspUpdateEmailTemplate ObjUpdate = new uspUpdateEmailTemplate();
                ObjUpdate.Connection = mConnection;

                ObjUpdate.EMAIL_TEMPLATE_ID = p_EMAIL_TEMPLATE_ID;
                ObjUpdate.CATEGORY_ID = p_CATEGORY_ID;
                ObjUpdate.SUBJECT = p_SUBJECT;
                ObjUpdate.BODY = p_BODY;
                ObjUpdate.SIGNATURE = p_SIGNATURE;
                ObjUpdate.TERMS = p_TERMS;
                ObjUpdate.USER_ID = p_USER_ID;
                ObjUpdate.IS_ACTIVE = p_IS_ACTIVE;
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

        public int InsertEmailTemplate(int p_CATEGORY_ID,int p_TYPE_ID, string p_SUBJECT, string p_BODY, string p_SIGNATURE, string p_TERMS, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspInsertEmailTemplate ObjInsert = new uspInsertEmailTemplate();
                ObjInsert.Connection = mConnection;
                ObjInsert.CATEGORY_ID = p_CATEGORY_ID;
                ObjInsert.TYPE_ID = p_TYPE_ID;
                ObjInsert.SUBJECT = p_SUBJECT;
                ObjInsert.BODY = p_BODY;
                ObjInsert.SIGNATURE = p_SIGNATURE;
                ObjInsert.TERMS = p_TERMS;
                ObjInsert.USER_ID = p_USER_ID;
                ObjInsert.ExecuteQuery();

                return ObjInsert.EMAIL_TEMPLATE_ID;
                
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return Constants.IntNullValue;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public long InsertInvoice(long p_ENQUIREY_MASTER_ID, int p_DISTRIBUTOR_ID, string p_REFERENCE_NO, int p_CUSTOMER_ID, DateTime p_DOCUMENT_DATE, decimal p_CONVERSION_RATE, decimal p_INCOME_TAX, decimal p_SERVICE_TAX, int p_UOM, int p_CURRENCY_ID, int p_USER_ID, string p_PDFFileName
            ,string p_PONo, DateTime p_PODate, DataTable dtInvoiceDetail)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                uspInsertInvoiceMaster mInvoice = new uspInsertInvoiceMaster();
                mInvoice.Connection = mConnection;
                mInvoice.Transaction = mTransaction;
                mInvoice.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
                mInvoice.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mInvoice.REFERENCE_NO = p_REFERENCE_NO;
                mInvoice.CUSTOMER_ID = p_CUSTOMER_ID;
                mInvoice.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mInvoice.CONVERSION_RATE = p_CONVERSION_RATE;
                mInvoice.INCOME_TAX = p_INCOME_TAX;
                mInvoice.SERVICE_TAX = p_SERVICE_TAX;
                mInvoice.UOM = p_UOM;
                mInvoice.CURRENCY_ID = p_CURRENCY_ID;
                mInvoice.USER_ID = p_USER_ID;
                mInvoice.PDFFileName = p_PDFFileName;
                mInvoice.PONo = p_PONo;
                mInvoice.PODate = p_PODate;
                mInvoice.ExecuteQuery();

                uspInsertInvoiceDetail mInvoiceDetail = new uspInsertInvoiceDetail();
                mInvoiceDetail.Connection = mConnection;
                mInvoiceDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtInvoiceDetail.Rows)
                {
                    mInvoiceDetail.INVOICE_MASTER_ID = mInvoice.INVOICE_MASTER_ID;
                    mInvoiceDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mInvoiceDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mInvoiceDetail.RATE = decimal.Parse(dr["RATE"].ToString());
                    mInvoiceDetail.IS_INVOICE = Convert.ToBoolean(dr["IS_INVOICE"]);
                    mInvoiceDetail.PAYMENT_TYPE = Convert.ToInt32(dr["PAYMENT_TYPE"]);
                    mInvoiceDetail.SUBSCRIPTION_DATE = Convert.ToDateTime(ConvertDate.British_To_American(dr["SUBSCRIPTION_DATE"].ToString()));
                    mInvoiceDetail.EXPIRY_DATE = Convert.ToDateTime(ConvertDate.British_To_American(dr["EXPIRY_DATE"].ToString()));
                    mInvoiceDetail.ENQUIREY_DETAIL_ID = Convert.ToInt64(dr["ENQUIREY_DETAIL_ID"]);
                    mInvoiceDetail.REMARKS = dr["REMARKS"].ToString();

                    mInvoiceDetail.ExecuteQuery();
                }

                mTransaction.Commit();
                return mInvoice.INVOICE_MASTER_ID;
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

        public bool UpdateInvoice(long p_ENQUIREY_MASTER_ID, long p_INVOICE_MASTER_ID, int p_DISTRIBUTOR_ID, int p_CUSTOMER_ID,DateTime p_DOCUMENT_DATE, decimal p_CONVERSION_RATE, decimal p_INCOME_TAX, decimal p_SERVICE_TAX, int p_UOM, int p_CURRENCY_ID, int p_USER_ID, bool p_IS_ACTIVE,
            string p_PONo, DateTime p_PODate , DataTable dtInvoiceDetail)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                uspUpdateInvoiceMaster mInvoice = new uspUpdateInvoiceMaster();
                mInvoice.Connection = mConnection;
                mInvoice.Transaction = mTransaction;
                mInvoice.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
                mInvoice.INVOICE_MASTER_ID = p_INVOICE_MASTER_ID;
                mInvoice.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mInvoice.CUSTOMER_ID = p_CUSTOMER_ID;
                mInvoice.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mInvoice.CONVERSION_RATE = p_CONVERSION_RATE;
                mInvoice.SERVICE_TAX = p_SERVICE_TAX;
                mInvoice.INCOME_TAX = p_INCOME_TAX;
                mInvoice.UOM = p_UOM;
                mInvoice.CURRENCY_ID = p_CURRENCY_ID;
                mInvoice.USER_ID = p_USER_ID;
                mInvoice.IS_ACTIVE = p_IS_ACTIVE;
                mInvoice.PONo = p_PONo;
                mInvoice.PODate = p_PODate;
                mInvoice.ExecuteQuery();


                uspDeleteINVOICEDetail mDelInvoiceDetail = new uspDeleteINVOICEDetail();
                mDelInvoiceDetail.Connection = mConnection;
                mDelInvoiceDetail.Transaction = mTransaction;
                mDelInvoiceDetail.INVOICE_MASTER_ID = mInvoice.INVOICE_MASTER_ID;
                mDelInvoiceDetail.ExecuteQuery();

                uspInsertInvoiceDetail mInvoiceDetail = new uspInsertInvoiceDetail();
                mInvoiceDetail.Connection = mConnection;
                mInvoiceDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtInvoiceDetail.Rows)
                {
                    mInvoiceDetail.INVOICE_MASTER_ID = mInvoice.INVOICE_MASTER_ID;
                    mInvoiceDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mInvoiceDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mInvoiceDetail.RATE = decimal.Parse(dr["RATE"].ToString());
                    mInvoiceDetail.IS_INVOICE = Convert.ToBoolean(dr["IS_INVOICE"]);
                    mInvoiceDetail.PAYMENT_TYPE = Convert.ToInt32(dr["PAYMENT_TYPE"]);
                    mInvoiceDetail.SUBSCRIPTION_DATE = Convert.ToDateTime(ConvertDate.British_To_American(dr["SUBSCRIPTION_DATE"].ToString()));
                    mInvoiceDetail.EXPIRY_DATE = Convert.ToDateTime(ConvertDate.British_To_American(dr["EXPIRY_DATE"].ToString()));
                    mInvoiceDetail.ENQUIREY_DETAIL_ID = Convert.ToInt64(dr["ENQUIREY_DETAIL_ID"]);
                    mInvoiceDetail.REMARKS = dr["REMARKS"].ToString();

                    mInvoiceDetail.ExecuteQuery();
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

        public bool UpdateInvoice(long p_ENQUIREY_MASTER_ID, long p_INVOICE_MASTER_ID, int p_DISTRIBUTOR_ID, int p_CUSTOMER_ID, DateTime p_DOCUMENT_DATE, int p_CURRENCY_ID, int p_USER_ID, bool p_IS_ACTIVE)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                uspUpdateInvoiceMaster mInvoice = new uspUpdateInvoiceMaster();
                mInvoice.Connection = mConnection;
                mInvoice.Transaction = mTransaction;
                mInvoice.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
                mInvoice.INVOICE_MASTER_ID = p_INVOICE_MASTER_ID;
                mInvoice.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mInvoice.CUSTOMER_ID = p_CUSTOMER_ID;
                mInvoice.CURRENCY_ID = p_CURRENCY_ID;
                mInvoice.USER_ID = p_USER_ID;
                mInvoice.IS_ACTIVE = p_IS_ACTIVE;                
                mInvoice.ExecuteQuery();

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

        public string GenerateInvoice(long p_INVOICE_MASTER_ID, string p_PDFFileName, DateTime p_DOCUMENT_DATE, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGenerateNewInvoice ObjInsert = new uspGenerateNewInvoice();
                ObjInsert.Connection = mConnection;
                ObjInsert.INVOICE_MASTER_ID = p_INVOICE_MASTER_ID;
                ObjInsert.PDFFileName = p_PDFFileName;
                ObjInsert.DOCUMENT_DATE = p_DOCUMENT_DATE;
                ObjInsert.USER_ID = p_USER_ID;
                return ObjInsert.ExecuteTable().Rows[0]["INVOICE_MASTER_ID2"].ToString();                
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return exp.ToString();
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