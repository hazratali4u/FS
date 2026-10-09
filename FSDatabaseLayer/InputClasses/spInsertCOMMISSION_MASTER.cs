using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;
namespace FSDatabaseLayer.Classes
{
	public class spInsertCOMMISSION_MASTER
	{
		#region Private Members
		private string sp_Name = "spInsertCOMMISSION_MASTER" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_DISTRIBUTOR_ID;
		private int m_ACCOUNT_HEAD_ID;
		private int m_USER_ID;
		private DateTime m_DOCUMENT_DATE;
		private DateTime m_DOCUMENT_MONTH;
		private decimal m_TAX;
		private long m_PEOPLE_ID;
		private long m_COMMISSION_MASTER_ID;
		#endregion
		#region Public Properties
		public int DISTRIBUTOR_ID
		{
			set
			{
				m_DISTRIBUTOR_ID = value ;
			}
			get
			{
				return m_DISTRIBUTOR_ID;
			}
		}
		public int ACCOUNT_HEAD_ID
		{
			set
			{
				m_ACCOUNT_HEAD_ID = value ;
			}
			get
			{
				return m_ACCOUNT_HEAD_ID;
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
		public DateTime DOCUMENT_DATE
		{
			set
			{
				m_DOCUMENT_DATE = value ;
			}
			get
			{
				return m_DOCUMENT_DATE;
			}
		}
		public DateTime DOCUMENT_MONTH
		{
			set
			{
				m_DOCUMENT_MONTH = value ;
			}
			get
			{
				return m_DOCUMENT_MONTH;
			}
		}
		public decimal TAX
		{
			set
			{
				m_TAX = value ;
			}
			get
			{
				return m_TAX;
			}
		}
		public long PEOPLE_ID
		{
			set
			{
				m_PEOPLE_ID = value ;
			}
			get
			{
				return m_PEOPLE_ID;
			}
		}
		public long COMMISSION_MASTER_ID
		{
			get
			{
				return m_COMMISSION_MASTER_ID;
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
		public spInsertCOMMISSION_MASTER()
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
				m_COMMISSION_MASTER_ID = (long)((IDataParameter)(cmd.Parameters["@COMMISSION_MASTER_ID"])).Value;
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
				m_DISTRIBUTOR_ID= Convert.ToInt32(dr["DISTRIBUTOR_ID"]);
				m_ACCOUNT_HEAD_ID= Convert.ToInt32(dr["ACCOUNT_HEAD_ID"]);
				m_USER_ID= Convert.ToInt32(dr["USER_ID"]);
				m_DOCUMENT_DATE= Convert.ToDateTime(dr["DOCUMENT_DATE"]);
				m_DOCUMENT_MONTH= Convert.ToDateTime(dr["DOCUMENT_MONTH"]);
				m_TAX= Convert.ToDecimal(dr["TAX"]);
				m_PEOPLE_ID=Convert.ToInt64(dr["PEOPLE_ID"]);
				m_COMMISSION_MASTER_ID=Convert.ToInt64(dr["COMMISSION_MASTER_ID"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@DISTRIBUTOR_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_DISTRIBUTOR_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_DISTRIBUTOR_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@ACCOUNT_HEAD_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_ACCOUNT_HEAD_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_ACCOUNT_HEAD_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@USER_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_USER_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_USER_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@DOCUMENT_DATE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_DOCUMENT_DATE==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_DOCUMENT_DATE;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@DOCUMENT_MONTH" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_DOCUMENT_MONTH==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_DOCUMENT_MONTH;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@TAX" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_TAX==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_TAX;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@PEOPLE_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			if(m_PEOPLE_ID==Constants.LongNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PEOPLE_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@COMMISSION_MASTER_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			parameter.Direction = ParameterDirection.Output;
			pparams.Add(parameter);


		}
		#endregion
	}
}
