using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;
using FSDatabaseLayer.Classes;

namespace FSBusinessLayer.Classes
{
    /// <summary>
    /// Class For Documents Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Inserts Uploaded Documents
    /// </item>
    /// <term>
    /// Deletes Documents
    /// </term>
    /// <item>
    /// Get Documents
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
    public class DocumentController
    {
        #region public Methods

        /// <summary>
        /// Inserts Document
        /// </summary>
        /// <param name="p_LINK_ID">LinkID</param>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_USER_ID">User</param>
        /// <param name="p_NAME">Name</param>
        /// <param name="p_TITLE">Title</param>
        /// <returns>True on Success And False On Failure</returns>
        public bool InsertUploadDocument(long p_LINK_ID, int p_TYPE_ID, int p_USER_ID, string p_NAME, string p_TITLE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspInsertUploadDocument mDocument = new uspInsertUploadDocument();
                mDocument.Connection = mConnection;

                mDocument.LINK_ID = p_LINK_ID;
                mDocument.TYPE_ID = p_TYPE_ID;
                mDocument.USER_ID = p_USER_ID;
                mDocument.NAME = p_NAME;
                mDocument.TITLE = p_TITLE;

                return mDocument.ExecuteQuery();

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
        /// Deletes Document
        /// </summary>
        /// <param name="p_DOCUMENT_ID">Document</param>
        /// <param name="p_USER_ID">User</param>
        /// <returns>True on Success And False on Failure</returns>
        public bool DeleteUploadDocument(long p_DOCUMENT_ID, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspDeleteUploadDocument mDocument = new uspDeleteUploadDocument();
                mDocument.Connection = mConnection;

                mDocument.DOCUMENT_ID = p_DOCUMENT_ID;
                mDocument.USER_ID = p_USER_ID;

                mDocument.ExecuteQuery();
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
        /// Gets Upload Documents
        /// </summary>
        /// <param name="p_LINK_ID">Link</param>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DOCUMENT_ID">Document</param>
        /// <returns>Document Data As Datatable</returns>
        public DataTable GetUploadDocument(long p_LINK_ID,int p_TYPE_ID, long p_DOCUMENT_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetUploadDocument mDocument = new uspGetUploadDocument();
                mDocument.Connection = mConnection;
                mDocument.LINK_ID = p_LINK_ID;
                mDocument.TYPE_ID = p_TYPE_ID;
                mDocument.DOCUMENT_ID = p_DOCUMENT_ID;
                DataTable dt = mDocument.ExecuteTable();
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
    }
}
