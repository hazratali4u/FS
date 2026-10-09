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
    /// Class For Marketing Contact Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert Marketing Contact
    /// </item>
    /// <term>
    /// Update Marketing Contact
    /// </term>
    /// <item>
    /// Get Marketing Contact
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
    public class MarketingController
    {
        #region Constructor

        /// <summary>
        /// Constructor for MarketingController
        /// </summary>
        public MarketingController()
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

        public DataTable GetMarketingContact(long p_MARKETING_CONTACT_ID, int p_PAGENO, int p_PAGESIZE,string p_SEARCHTEXT)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetMarketingContact mContact = new uspGetMarketingContact();
                mContact.Connection = mConnection;
                mContact.MARKETING_CONTACT_ID = p_MARKETING_CONTACT_ID;
                mContact.PAGENO = p_PAGENO;
                mContact.PAGESIZE = p_PAGESIZE;
                mContact.SEARCHTEXT = p_SEARCHTEXT;
                DataTable dt = mContact.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
        }

        public DataTable GetMarketingTemplate(int p_MARKETING_TEMPLATE_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetMarketingTemplate mContact = new uspGetMarketingTemplate();
                mContact.Connection = mConnection;
                mContact.MARKETING_TEMPLATE_ID = p_MARKETING_TEMPLATE_ID;
                DataTable dt = mContact.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
        }

        public DataTable GetMarketingEmail(long p_MARKETING_EMAIL_ID, int p_PAGENO, int p_PAGESIZE)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetMarketingEmail mContact = new uspGetMarketingEmail();
                mContact.Connection = mConnection;
                mContact.MARKETING_EMAIL_ID = p_MARKETING_EMAIL_ID;
                mContact.PAGENO = p_PAGENO;
                mContact.PAGESIZE = p_PAGESIZE;
                DataTable dt = mContact.ExecuteTable();
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

        public bool UpdateMarketingContact(long p_MARKETING_CONTACT_ID, string p_CONTACT_NAME,string p_CONTACT_CELL,string p_CONTACT_DESIGNATION,string p_CONTACT_EMAIL,string p_CONTACT_COMPANY,int p_USER_ID, bool p_IS_DELETED)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspUpdateMarketingContact mContact = new uspUpdateMarketingContact();
                mContact.Connection = mConnection;
                mContact.MARKETING_CONTACT_ID = p_MARKETING_CONTACT_ID;
                mContact.CONTACT_NAME = p_CONTACT_NAME;
                mContact.CONTACT_CELL = p_CONTACT_CELL;
                mContact.CONTACT_DESIGNATION = p_CONTACT_DESIGNATION;
                mContact.CONTACT_EMAIL = p_CONTACT_EMAIL;
                mContact.CONTACT_COMPANY = p_CONTACT_COMPANY;
                mContact.USER_ID = p_USER_ID;
                mContact.IS_DELETED = p_IS_DELETED;
                mContact.ExecuteQuery();
                return true;
            }

            catch (Exception excp)
            {
                ExceptionPublisher.PublishException(excp);
                return false;
            }

        }

        public bool InsertMarketingTemplate(string p_TEMPLATE_NAME,string p_TEMPLATE_BODY, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspInsertMarketingTemplate mContact = new uspInsertMarketingTemplate();
                mContact.Connection = mConnection;
                mContact.TEMPLATE_NAME = p_TEMPLATE_NAME;
                mContact.TEMPLATE_BODY = p_TEMPLATE_BODY;
                mContact.USER_ID = p_USER_ID;
                mContact.ExecuteQuery();
                return true;
            }

            catch (Exception excp)
            {
                ExceptionPublisher.PublishException(excp);
                return false;
            }

        }

        public bool UpdateMarketingTemplate(int p_MARKETING_TEMPLATE_ID,string p_TEMPLATE_NAME, string p_TEMPLATE_BODY, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspUpdateMarketingTemplate mContact = new uspUpdateMarketingTemplate();
                mContact.Connection = mConnection;
                mContact.MARKETING_TEMPLATE_ID = p_MARKETING_TEMPLATE_ID;
                mContact.TEMPLATE_NAME = p_TEMPLATE_NAME;
                mContact.TEMPLATE_BODY = p_TEMPLATE_BODY;
                mContact.USER_ID = p_USER_ID;
                mContact.ExecuteQuery();
                return true;
            }

            catch (Exception excp)
            {
                ExceptionPublisher.PublishException(excp);
                return false;
            }

        }

        public bool InsertMarketingEmail(DateTime p_DOCUMENT_DATE, int p_MARKETING_TEMPLATE_ID,string p_TEMPLATE_BODY,string p_FROM,string p_TO, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspInsertMarketingEmail mContact = new uspInsertMarketingEmail();
                mContact.Connection = mConnection;
                mContact.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mContact.MARKETING_TEMPLATE_ID = p_MARKETING_TEMPLATE_ID;
                mContact.TEMPLATE_BODY = p_TEMPLATE_BODY;
                mContact.FROM = p_FROM;
                mContact.TO = p_TO;
                mContact.USER_ID = p_USER_ID;
                mContact.ExecuteQuery();
                return true;
            }

            catch (Exception excp)
            {
                ExceptionPublisher.PublishException(excp);
                return false;
            }

        }

        #endregion
    }
}