using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;

namespace FSDatabaseLayer.Classes
{
	public class uspInsertQuotionDetailSub
	{
		#region Private Members
		private string sp_Name = "uspInsertQuotionDetailSub" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
        private long m_ENQUIREY_DETAIL_ID;
		private long m_QUOTATION_MASTER_ID;
		private string m_PRODUCT_DETAIL;
		#endregion
		#region Public Properties
        public long ENQUIREY_DETAIL_ID
		{
			set
			{
                m_ENQUIREY_DETAIL_ID = value;
			}
			get
			{
                return m_ENQUIREY_DETAIL_ID;
			}
		}
		public long QUOTATION_MASTER_ID
		{
			set
			{
				m_QUOTATION_MASTER_ID = value ;
			}
			get
			{
				return m_QUOTATION_MASTER_ID;
			}
		}
		public  string PRODUCT_DETAIL
		{
			set
			{
				m_PRODUCT_DETAIL = value ;
			}
			get
			{
				return m_PRODUCT_DETAIL;
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
		public uspInsertQuotionDetailSub()
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
                m_ENQUIREY_DETAIL_ID = Convert.ToInt64(dr["ENQUIREY_DETAIL_ID"]);
				m_QUOTATION_MASTER_ID=Convert.ToInt64(dr["QUOTATION_MASTER_ID"]);
				m_PRODUCT_DETAIL= Convert.ToString(dr["PRODUCT_DETAIL"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@ENQUIREY_DETAIL_ID"; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_ENQUIREY_DETAIL_ID == Constants.LongNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
                parameter.Value = m_ENQUIREY_DETAIL_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@QUOTATION_MASTER_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			if(m_QUOTATION_MASTER_ID==Constants.LongNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_QUOTATION_MASTER_ID;
			}
			pparams.Add(parameter);

            
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@PRODUCT_DETAIL" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_PRODUCT_DETAIL== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PRODUCT_DETAIL;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}
