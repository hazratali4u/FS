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
    /// Class For Enquiry Price Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert Price Enquiry
    /// </item>
    /// <term>
    /// Update Price Enquiry
    /// </term>
    /// <item>
    /// Get Enquiry Price
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
    public class EnquiryPriceController
    {
        #region Constructor

        /// <summary>
        /// Constructor for EnquiryPriceController
        /// </summary>
        public EnquiryPriceController()
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
        /// Get Enquiry 
        /// </summary>
        /// <remarks>
        /// Returns Enquiry as Datatable
        /// </remarks>
        /// <param name="p_ENQUIREY_MASTER_ID">Enquiry</param>
        /// <returns>Enquiry as Datatable</returns>
        public DataTable GetEnquiry(long p_ENQUIREY_MASTER_ID, int p_DISTRIBUTOR_ID, int p_USER_ID, int p_TYPE_ID)
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
                mEnquiry.TYPE_ID = p_TYPE_ID;
                DataTable dt = mEnquiry.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
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
        public DataTable GetEnquiryView(long p_ENQUIREY_MASTER_ID, int p_DISTRIBUTOR_ID, int p_USER_ID, int p_TYPE_ID, int p_STATUS_ID, int p_CUSTOMER_TYPE)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetEnquiryView mEnquiry = new uspGetEnquiryView();
                mEnquiry.Connection = mConnection;
                mEnquiry.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
                mEnquiry.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mEnquiry.USER_ID = p_USER_ID;
                mEnquiry.TYPE_ID = p_TYPE_ID;
                mEnquiry.STATUS_ID = p_STATUS_ID;
                mEnquiry.CUSTOMER_TYPE = p_CUSTOMER_TYPE;
                DataTable dt = mEnquiry.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
        }

        public DataTable GetCustomerCategoryEnquiry(int p_DISTRIBUTOR_ID, int p_TYPE_ID, long p_CUSTOMER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetCustomerCategoryEnquiry mEnquiry = new uspGetCustomerCategoryEnquiry();
                mEnquiry.Connection = mConnection;
                mEnquiry.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mEnquiry.TYPE_ID = p_TYPE_ID;
                mEnquiry.CUSTOMER_ID = p_CUSTOMER_ID;
                DataTable dt = mEnquiry.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
        }

        /// <summary>
        /// Gets Product Supplier
        /// </summary>
        /// <remarks>
        /// Returns Product Suppler Data as Datatable
        /// </remarks>
        /// <param name="p_ProductID">Product</param>
        /// <returns>Assigned Supplier Data as Datatable</returns>
        public DataTable GetProductSupplier(int p_ProductID, long p_ENQUIREY_MASTER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetProductSupplier mSkuHierarchy = new uspGetProductSupplier();
                mSkuHierarchy.Connection = mConnection;

                mSkuHierarchy.PRODUCT_ID = p_ProductID;
                mSkuHierarchy.ENQUIREY_MASTER_ID = p_ENQUIREY_MASTER_ID;
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
        #endregion

        #region Insert, Update, Deleted

        public bool InsertEnquiryPrice(long p_ENQUIRY_ID, int p_PRODUCT_ID, int p_SUPPLIER_ID, decimal p_Rate, decimal p_RateCustomer, string p_INFO, string p_REMARKS, bool p_IS_QUOTATION, int p_USER_ID, int p_CURRENCY_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                uspDeleteEnquiryPrice mDelEnquiryPrice = new uspDeleteEnquiryPrice();
                mDelEnquiryPrice.Connection = mConnection;
                mDelEnquiryPrice.Transaction = mTransaction;
                mDelEnquiryPrice.ENQUIRY_ID = p_ENQUIRY_ID;
                mDelEnquiryPrice.PRODUCT_ID = p_PRODUCT_ID;
                mDelEnquiryPrice.SUPPLIER_ID = p_SUPPLIER_ID;
                mDelEnquiryPrice.ExecuteQuery();

                uspInsertEnquiryPrice ObjInsert = new uspInsertEnquiryPrice();
                ObjInsert.Connection = mConnection;
                ObjInsert.ENQUIRY_ID = p_ENQUIRY_ID;
                ObjInsert.PRODUCT_ID = p_PRODUCT_ID;
                ObjInsert.SUPPLIER_ID = p_SUPPLIER_ID;
                ObjInsert.Rate = p_Rate;
                ObjInsert.RateCustomer = p_RateCustomer;
                ObjInsert.INFO = p_INFO;
                ObjInsert.REMARKS = p_REMARKS;
                ObjInsert.IS_QUOTATION = p_IS_QUOTATION;
                ObjInsert.USER_ID = p_USER_ID;
                ObjInsert.CURRENCY_ID = p_CURRENCY_ID;
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

        #endregion
    }
}

