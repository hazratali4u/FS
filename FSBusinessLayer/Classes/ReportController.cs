using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;
using FSDatabaseLayer.Classes;

namespace FSBusinessLayer.Classes
{
    /// <summary>
    /// Class For Reports Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Get Reports
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
    public class ReportController
    {
        #region Public Methods

        #region Select

        /// <summary>
        /// Gets Notification Report Data
        /// </summary>
        /// <param name="p_CUSTOMER_ID">Customer</param>
        /// <returns>DataTable</returns>
        public DataTable GetCurrentNotificationReport(int p_CUSTOMER_ID, int p_NoOfMonth, int p_SKU_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetCurrentNotificationReport ObjSelect = new uspGetCurrentNotificationReport();
                ObjSelect.Connection = mConnection;
                ObjSelect.CUSTOMER_ID = p_CUSTOMER_ID;
                ObjSelect.NoOfMonth = p_NoOfMonth;
                ObjSelect.SKU_ID = p_SKU_ID;
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
        /// Gets Pending Payment Report Data
        /// </summary>
        /// <param name="p_CUSTOMER_ID">Customer</param>
        /// <returns>DataTable</returns>
        public DataTable GetPendingPaymentReport(int p_CUSTOMER_ID, int p_SKU_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetPendingPayment ObjSelect = new uspGetPendingPayment();
                ObjSelect.Connection = mConnection;
                ObjSelect.CUSTOMER_ID = p_CUSTOMER_ID;
                ObjSelect.SKU_ID = p_SKU_ID;
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
        /// Gets Monthly Report Data
        /// </summary>
        /// <param name="p_CUSTOMER_ID">Customer</param>
        /// <returns>DataTable</returns>
        public DataTable GetMonthlyReport(DateTime p_FROM_DATE, DateTime p_TO_DATE, int p_CUSTOMER_ID, int p_CATEGORY_ID, int p_REPORT_TYPE, string p_PAYMENT_MODE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetMonthlyReport ObjSelect = new uspGetMonthlyReport();
                ObjSelect.Connection = mConnection;
                ObjSelect.CUSTOMER_ID = p_CUSTOMER_ID;
                ObjSelect.CATEGORY_ID = p_CATEGORY_ID;
                ObjSelect.REPORT_TYPE = p_REPORT_TYPE;
                ObjSelect.PAYMENT_MODE = p_PAYMENT_MODE;
                ObjSelect.FROM_DATE = p_FROM_DATE;
                ObjSelect.TO_DATE = p_TO_DATE;
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

        public DataTable GetRecurringInvoices(int p_CUSTOMER_ID, int p_CATEGORY_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetRecurrsiveInvoices ObjSelect = new uspGetRecurrsiveInvoices();
                ObjSelect.Connection = mConnection;
                ObjSelect.CUSTOMER_ID = p_CUSTOMER_ID;
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

        #endregion

        #endregion
    }
}
