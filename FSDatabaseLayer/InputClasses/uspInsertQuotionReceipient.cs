using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;

namespace FSDatabaseLayer.Classes
{
	public class uspInsertQuotionReceipient
	{
		#region Private Members

		private string sp_Name = "uspInsertQuotionReceipient" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
        private long m_ENQUIRY_MASTER_ID;
        private int m_PRODUCT_ID;
		private int m_RECEIPIENT_TYPE_ID;
        private long m_RECEIPIENT_ID;
        private string m_RECEIPIENT_EMAIL;        
        private int m_EMAIL_ADDRESS_TYPE;
        private int m_USER_ID;		
		
		#endregion

		#region Public Properties

        public long ENQUIRY_MASTER_ID
        {
            set
            {
                m_ENQUIRY_MASTER_ID = value;
            }
            get
            {
                return m_ENQUIRY_MASTER_ID;
            }
        }

        public int PRODUCT_ID
        {
            set
            {
                m_PRODUCT_ID = value;
            }
            get
            {
                return m_PRODUCT_ID;
            }
        }

		public int RECEIPIENT_TYPE_ID
		{
			set
			{
				m_RECEIPIENT_TYPE_ID = value ;
			}
			get
			{
				return m_RECEIPIENT_TYPE_ID;
			}
		}
        public long RECEIPIENT_ID
        {
            set
            {
                m_RECEIPIENT_ID = value;
            }
            get
            {
                return m_RECEIPIENT_ID;
            }
        }
        public string RECEIPIENT_EMAIL
        {
            set
            {
                m_RECEIPIENT_EMAIL = value;
            }
            get
            {
                return m_RECEIPIENT_EMAIL;
            }
        }
		public int EMAIL_ADDRESS_TYPE
		{
			set
			{
				m_EMAIL_ADDRESS_TYPE = value ;
			}
			get
			{
				return m_EMAIL_ADDRESS_TYPE;
			}
		}
		public int USER_ID
		{
			set
			{
				m_USER_ID = value ;
			}
			get
			{
				return m_USER_ID;
			}
		}

		public IDbConnection  Connection
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
		public IDbTransaction  Transaction
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

		public uspInsertQuotionReceipient()
		{
		}

		#endregion
		
        #region public Methods
		public bool  ExecuteQuery()
		{
			try
			{
			    IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				cmd.CommandType =  CommandType.StoredProcedure;
				cmd.CommandText = sp_Name;
				cmd.Connection =   m_connection;
				if(m_transaction!=null)
				{
					cmd.Transaction = m_transaction;
				}
				GetParameterCollection(ref cmd);
				cmd.ExecuteNonQuery();
				return true;
			}
			catch(Exception e)
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
				command.CommandText = sp_Name;
				command.Connection = m_connection;
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				IDataReader dr = command.ExecuteReader();
				return dr;
			}
			catch(Exception exp)
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
				command.CommandText = sp_Name;
				command.Connection = m_connection;
				if(m_transaction!=null)
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
			catch(Exception exp)
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
				command.CommandText = sp_Name;
				command.Connection = m_connection;
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				object o;
				o = command.ExecuteScalar();


				return o.ToString();
			}
			catch(Exception exp)
			{
				throw exp;
			}
			finally
			{
			}
		}
		public void FirstReader(IDataReader dr)
		{
			if(dr.Read())
			{
				m_RECEIPIENT_TYPE_ID= Convert.ToInt32(dr["RECEIPIENT_TYPE_ID"]);
				m_RECEIPIENT_ID= Convert.ToInt32(dr["RECEIPIENT_ID"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@ENQUIRY_MASTER_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_ENQUIRY_MASTER_ID == Constants.LongNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_ENQUIRY_MASTER_ID;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PRODUCT_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_PRODUCT_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PRODUCT_ID;
            }
            pparams.Add(parameter);

			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@RECEIPIENT_TYPE_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_RECEIPIENT_TYPE_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_RECEIPIENT_TYPE_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@RECEIPIENT_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_RECEIPIENT_ID == Constants.LongNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_RECEIPIENT_ID;
            }
			pparams.Add(parameter);
            

			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@EMAIL_ADDRESS_TYPE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_EMAIL_ADDRESS_TYPE==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_EMAIL_ADDRESS_TYPE;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@RECEIPIENT_EMAIL" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_RECEIPIENT_EMAIL==null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_RECEIPIENT_EMAIL;
			}
			pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@USER_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_USER_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_USER_ID;
            }
            pparams.Add(parameter);
		}
		#endregion
	}
}
