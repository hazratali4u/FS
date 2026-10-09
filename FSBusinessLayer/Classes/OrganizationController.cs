using System;
using FSDatabaseLayer.Classes;
using FSDataAccessLayer.Classes;
using FSCommonLayer.Classes;
using System.Data;

namespace FSBusinessLayer.Classes
{
    /// <summary>
    /// Class For Organization Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert Organization
    /// </item>
    /// <item>
    /// Get Organization
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
    public class OrganizationController
    {
        #region Public Methods

        #region Select

        /// <summary>
        /// Gets Organization
        /// </summary>
        /// <param name="p_ORGANIZATION_ID">OrganizationID</param>
        /// <param name="p_TYPE">Type</param>
        /// <returns>Datatable</returns>
        public DataTable GetOrganization(long p_ORGANIZATION_ID, int p_TYPE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetOrganization ObjSelect = new spGetOrganization();
                ObjSelect.Connection = mConnection;
                ObjSelect.ORGANIZATION_ID = p_ORGANIZATION_ID;
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
        /// Gets Organization
        /// </summary>
        /// <param name="p_ORGANIZATION_ID">OrganizationID</param>
        /// <param name="p_TYPE">Type</param>
        /// <returns>Datatable</returns>
        public DataTable GetOrganization(int p_USER_ID, int p_TYPE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetOrganization ObjSelect = new spGetOrganization();
                ObjSelect.Connection = mConnection;
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
        /// Gets Organization
        /// </summary>
        /// <param name="p_ORGANIZATION_ID">OrganizationID</param>
        /// <param name="p_TYPE">Type</param>
        /// <returns>Datatable</returns>
        public DataTable GetOrganization(long p_ORGANIZATION_ID, int p_TYPE, string p_SearchName)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetOrganization ObjSelect = new spGetOrganization();
                ObjSelect.Connection = mConnection;
                ObjSelect.ORGANIZATION_ID = p_ORGANIZATION_ID;
                ObjSelect.TYPE = p_TYPE;
                ObjSelect.SearchName = p_SearchName;
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
        /// Gets Organization Detail
        /// </summary>
        /// <param name="p_PEOPLE_ID">People</param>
        /// <returns>Datatable</returns>
        public DataTable GetOrganizationDetail(long p_ORGANIZATION_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetOrganizationDetail ObjSelect = new spGetOrganizationDetail();
                ObjSelect.Connection = mConnection;
                ObjSelect.ORGANIZATION_ID = p_ORGANIZATION_ID;
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
        /// Gets Assigned Customer
        /// </summary>
        /// <remarks>
        /// Returns Assigned Customer Data as Datatable
        /// </remarks>
        /// <param name="p_type">Type</param>
        /// <param name="p_UserId">User</param>
        /// <returns>Assigned Customer Data as Datatable</returns>
        public DataTable GetAssignedCustomer(int p_type, int p_UserId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspGetAssignCustomer mSkuHierarchy = new UspGetAssignCustomer();
                mSkuHierarchy.Connection = mConnection;

                mSkuHierarchy.TYPE_ID = p_type;
                mSkuHierarchy.USER_ID = p_UserId;
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
        /// Gets Assigned Supplier
        /// </summary>
        /// <remarks>
        /// Returns Assigned Suppler Data as Datatable
        /// </remarks>
        /// <param name="p_type">Type</param>
        /// <param name="p_UserId">User</param>
        /// <returns>Assigned Supplier Data as Datatable</returns>
        public DataTable GetAssignedSupplier(int p_type, int p_ProductID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspGetAssignSupplier mSkuHierarchy = new UspGetAssignSupplier();
                mSkuHierarchy.Connection = mConnection;

                mSkuHierarchy.TYPE_ID = p_type;
                mSkuHierarchy.PRODUCT_ID = p_ProductID;
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
        /// Gets City
        /// </summary>
        /// <param name="p_COUNTRY_ID">Country</param>
        /// <returns>Datatable</returns>
        public DataTable GetCity(int p_COUNTRY_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetCity ObjSelect = new uspGetCity();
                ObjSelect.Connection = mConnection;
                ObjSelect.COUNTRY_ID = p_COUNTRY_ID;
                
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
        /// Gets Assigned Product
        /// </summary>
        /// <remarks>
        /// Returns Assigned Supplier Data as Datatable
        /// </remarks>
        /// <param name="p_type">Type</param>
        /// <param name="p_UserId">User</param>
        /// <returns>Assigned Products Data as Datatable</returns>
        public DataTable GetAssignedProduct(int p_type, int p_SupplierID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspGetAssignProduct mSkuHierarchy = new UspGetAssignProduct();
                mSkuHierarchy.Connection = mConnection;

                mSkuHierarchy.TYPE_ID = p_type;
                mSkuHierarchy.SUPPLIER_ID = p_SupplierID;
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

        #region Insert, Update, Delete
        
        /// <summary>
        /// Inserts Organization
        /// </summary>
        /// <param name="p_NAME">Name</param>
        /// <param name="p_TYPE">Type</param>
        /// <param name="p_EMAIL">Email</param>
        /// <param name="p_WEBSITE">Website</param>
        /// <param name="p_FACSIMILE">Facsimile</param>
        /// <param name="p_PHONE1">Phone1</param>
        /// <param name="p_PHONE2">Phone2</param>
        /// <param name="p_UAN">UAN</param>
        /// <param name="p_ADDRESS1">Address1</param>
        /// <param name="p_City">City</param>
        /// <param name="p_NTN">NTN</param>
        /// <param name="p_GST">GST</param>
        /// <param name="p_SWIFT">Swift</param>
        /// <param name="p_IBAN">IBAN</param>
        /// <param name="p_RTGS">RTGS</param>
        /// <param name="p_OTHER">Other</param>
        /// <param name="p_BANK_ACCOUNT">AccountNo</param>
        /// <param name="p_BANK_CODE">BankCode</param>
        /// <param name="p_BANK_ADDRESS">BankAddress</param>
        /// <returns>True on Success And False on Failure</returns>
        public long InsertOrganization(string p_NAME, int p_TYPE, int p_City, int p_USER_ID, DataTable dtDetail)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spInsertOrganization ObjInsert = new spInsertOrganization();
                ObjInsert.Connection = mConnection;
                ObjInsert.Transaction = mTransaction;

                ObjInsert.NAME = p_NAME;
                ObjInsert.TYPE = p_TYPE;                
                ObjInsert.City = p_City;                
                ObjInsert.USER_ID = p_USER_ID;
                ObjInsert.ExecuteQuery();
                if (dtDetail.Rows.Count > 0)
                {
                    spInsertOrganizationDetail ObjInsertDetail = new spInsertOrganizationDetail();
                    ObjInsertDetail.Connection = mConnection;
                    ObjInsertDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtDetail.Rows)
                    {
                        ObjInsertDetail.ORGANIZATION_ID = ObjInsert.ORGANIZATION_ID;
                        ObjInsertDetail.FIELD_TYPE = dr["FIELD_TYPE"].ToString();
                        ObjInsertDetail.FIELD_VALUE = dr["FIELD_VALUE"].ToString();

                        ObjInsertDetail.ExecuteQuery();
                    }
                }
                mTransaction.Commit();
                return ObjInsert.ORGANIZATION_ID;

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
        /// Updates Organization
        /// </summary>
        /// <param name="p_ORGANIZATION_ID">Organization</param>
        /// <param name="p_NAME">Name</param>
        /// <param name="p_TYPE">Type</param>
        /// <param name="p_EMAIL">Email</param>
        /// <param name="p_WEBSITE">Website</param>
        /// <param name="p_FACSIMILE">Facsimile</param>
        /// <param name="p_PHONE1">Phone1</param>
        /// <param name="p_PHONE2">Phone2</param>
        /// <param name="p_UAN">UAN</param>
        /// <param name="p_ADDRESS1">Address1</param>
        /// <param name="p_City">City</param>
        /// <param name="p_NTN">NTN</param>
        /// <param name="p_GST">GST</param>
        /// <param name="p_SWIFT">Swift</param>
        /// <param name="p_IBAN">IBAN</param>
        /// <param name="p_RTGS">RTGS</param>
        /// <param name="p_OTHER">Other</param>
        /// <param name="p_BANK_ACCOUNT">BankAccount</param>
        /// <param name="p_BANK_CODE">BankCode</param>
        /// <param name="p_BANK_ADDRESS">BankAddress</param>
        /// <returns>True on Success And False on Failure</returns>
        public bool UpdateOrganization(long p_ORGANIZATION_ID, string p_NAME, int p_TYPE, int p_City, int p_USER_ID, DataTable dtDetail)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdateOrganization ObjUpdate = new spUpdateOrganization();
                ObjUpdate.Connection = mConnection;
                ObjUpdate.Transaction = mTransaction;

                ObjUpdate.ORGANIZATION_ID = p_ORGANIZATION_ID;
                ObjUpdate.NAME = p_NAME;
                ObjUpdate.TYPE = p_TYPE;
                ObjUpdate.City = p_City;
                ObjUpdate.USER_ID = p_USER_ID;

                ObjUpdate.ExecuteQuery();

                //Delete Old Detail
                spDeleteOrganizationDetail objDelelete = new spDeleteOrganizationDetail();
                objDelelete.Connection = mConnection;
                objDelelete.Transaction = mTransaction;

                objDelelete.ORGANIZATION_ID = p_ORGANIZATION_ID;

                objDelelete.ExecuteQuery();

                if (dtDetail.Rows.Count > 0)
                {
                    spInsertOrganizationDetail ObjInsertDetail = new spInsertOrganizationDetail();
                    ObjInsertDetail.Connection = mConnection;
                    ObjInsertDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtDetail.Rows)
                    {
                        ObjInsertDetail.ORGANIZATION_ID = p_ORGANIZATION_ID;
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
        /// Deletes Organization
        /// </summary>
        /// <param name="p_RGANIZATION_ID">Organization</param>
        /// <returns>True on Success And False on Failure</returns>
        public bool DeleteOrganization(long p_RGANIZATION_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spDeleteOrganization ObjUpdate = new spDeleteOrganization();
                ObjUpdate.Connection = mConnection;

                ObjUpdate.ORGANIZATION_ID = p_RGANIZATION_ID;

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
        /// Inserts Customer Assignment For A User
        /// </summary>
        /// <param name="p_companyId">Company</param>
        /// <param name="p_UserId">User</param>
        public void InsertAssignCustomer(int p_companyId, int p_UserId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spInsertCUSTOMER_ASSIGNMENT mInsertBrand = new spInsertCUSTOMER_ASSIGNMENT();
                mInsertBrand.Connection = mConnection;
                mInsertBrand.PRINCIPAL_ID = p_companyId;
                mInsertBrand.USER_ID = p_UserId;
                mInsertBrand.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);

            }
            finally
            {
            }
        }

        /// <summary>
        /// Deletes Customer Assignment For a User
        /// </summary>
        /// <param name="p_companyId">Company</param>
        /// <param name="p_UserId">User</param>
        public void DeleteAssignCustomer(int p_companyId, int p_UserId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spDeleteCUSTOMER_ASSIGNMENT mDeleteBrand = new spDeleteCUSTOMER_ASSIGNMENT();
                mDeleteBrand.Connection = mConnection;
                mDeleteBrand.PRINCIPAL_ID = p_companyId;
                mDeleteBrand.USER_ID = p_UserId;
                mDeleteBrand.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);

            }
            finally
            {
            }
        }

        /// <summary>
        /// Inserts Product Assignment For A Supplier
        /// </summary>
        /// <param name="p_companyId">Company</param>
        /// <param name="p_UserId">User</param>
        public void InsertProductAssign(int p_PRODUCT_ID, int p_SUPPLIER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spInsertPRODUCT_ASSIGNMENT mInsertBrand = new spInsertPRODUCT_ASSIGNMENT();
                mInsertBrand.Connection = mConnection;
                mInsertBrand.PRODUCT_ID = p_PRODUCT_ID;
                mInsertBrand.SUPPLIER_ID = p_SUPPLIER_ID;
                mInsertBrand.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);

            }
            finally
            {
            }
        }

        /// <summary>
        /// Deletes Product Assignment For a Supplier
        /// </summary>
        /// <param name="p_companyId">Company</param>
        /// <param name="p_UserId">User</param>
        public void DeleteProductAssignment(int p_PRODUCT_ID, int p_SUPPLIER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spDeletePRODUCT_ASSIGNMENT mDeleteBrand = new spDeletePRODUCT_ASSIGNMENT();
                mDeleteBrand.Connection = mConnection;
                mDeleteBrand.PRODUCT_ID = p_PRODUCT_ID;
                mDeleteBrand.SUPPLIER_ID = p_SUPPLIER_ID;
                mDeleteBrand.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);

            }
            finally
            {
            }
        }

        #endregion

        #endregion
    }
}
