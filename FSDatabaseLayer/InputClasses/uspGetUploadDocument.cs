using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;


namespace FSDatabaseLayer.Classes
{
    public class uspGetUploadDocument
    {
        #region Private Members
        private string sp_Name = " uspGetUploadDocument";
        private IDbConnection m_connection;
        private IDbTransaction m_transaction;


        private long m_LINK_ID;
        private int m_TYPE_ID;
        private long m_DOCUMENT_ID;
        #endregion


        #region Public Properties
        public long LINK_ID
        {
            set
            {
                m_LINK_ID = value;
            }
            get
            {
                return m_LINK_ID;
            }
        }


        public int TYPE_ID
        {
            set
            {
                m_TYPE_ID = value;
            }
            get
            {
                return m_TYPE_ID;
            }
        }

        public long DOCUMENT_ID
        {
            set
            {
                m_DOCUMENT_ID = value;
            }
            get
            {
                return m_DOCUMENT_ID;
            }
        }


        public IDbConnection Connection
        {
            set
            {
                m_connection = value;
            }
            get
            {
                return m_connection;
            }
        }
        public IDbTransaction Transaction
        {
            set
            {
                m_transaction = value;
            }
            get
            {
                return m_transaction;
            }
        }
        #endregion


        #region Constructor
        public uspGetUploadDocument()
        {
            m_LINK_ID = Constants.LongNullValue;
            m_TYPE_ID = Constants.IntNullValue;
            m_DOCUMENT_ID = Constants.LongNullValue;
        }
        #endregion

        #region public Methods
        public bool ExecuteQuery()
        {
            try
            {
                IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "uspGetUploadDocument";
                cmd.Connection = m_connection;
                if (m_transaction != null)
                {
                    cmd.Transaction = m_transaction;
                }
                GetParameterCollection(ref cmd);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {


            }
        }


        public IDataReader ExecuteReader()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "uspGetUploadDocument";
                command.Connection = m_connection;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                IDataReader dr = command.ExecuteReader();
                return dr;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
            }
        }


        public DataTable ExecuteTable()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "uspGetUploadDocument";
                command.Connection = m_connection;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                IDbDataAdapter da = ProviderFactory.GetAdapter(EnumProviders.SQLClient);
                da.SelectCommand = command;
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {


            }
        }


        public string ExecuteScalar()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "uspGetUploadDocument";
                command.Connection = m_connection;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                object o;
                o = command.ExecuteScalar();


                return o.ToString();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
            }
        }


        public void FirstReader(IDataReader dr)
        {
            if (dr.Read())
            {
                m_LINK_ID = Convert.ToInt64(dr["LINK_ID"]);
                m_TYPE_ID = Convert.ToInt32(dr["TYPE_ID"]);
                m_DOCUMENT_ID = Convert.ToInt64(dr["NAME"]);
            }
        }


        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@LINK_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_LINK_ID == Constants.LongNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_LINK_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TYPE_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_TYPE_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_TYPE_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DOCUMENT_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_DOCUMENT_ID == Constants.LongNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DOCUMENT_ID;
            }
            pparams.Add(parameter);
            
        }
        #endregion
    }
}
