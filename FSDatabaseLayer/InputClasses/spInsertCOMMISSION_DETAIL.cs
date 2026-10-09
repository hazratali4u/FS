using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;
namespace FSDatabaseLayer.Classes
{
	public class spInsertCOMMISSION_DETAIL
	{
		#region Private Members
		private string sp_Name = "spInsertCOMMISSION_DETAIL" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_COMMISSION_TYPE;
		private decimal m_AMOUNT;
		private decimal m_COMMISSION_VALUE;
		private decimal m_TAX_VALUE;
		private long m_COMMISSION_MASTER_ID;
		private long m_CUSTOMER_ID;
		private long m_INVOICE_ID;
		#endregion
		#region Public Properties
		public int COMMISSION_TYPE
		{
			set
			{
				m_COMMISSION_TYPE = value ;
			}
			get
			{
				return m_COMMISSION_TYPE;
			}
		}
		public decimal AMOUNT
		{
			set
			{
				m_AMOUNT = value ;
			}
			get
			{
				return m_AMOUNT;
			}
		}
		public decimal COMMISSION_VALUE
		{
			set
			{
				m_COMMISSION_VALUE = value ;
			}
			get
			{
				return m_COMMISSION_VALUE;
			}
		}
		public decimal TAX_VALUE
		{
			set
			{
				m_TAX_VALUE = value ;
			}
			get
			{
				return m_TAX_VALUE;
			}
		}
		public long COMMISSION_MASTER_ID
		{
			set
			{
				m_COMMISSION_MASTER_ID = value ;
			}
			get
			{
				return m_COMMISSION_MASTER_ID;
			}
		}
		public long CUSTOMER_ID
		{
			set
			{
				m_CUSTOMER_ID = value ;
			}
			get
			{
				return m_CUSTOMER_ID;
			}
		}
		public long INVOICE_ID
		{
			set
			{
				m_INVOICE_ID = value ;
			}
			get
			{
				return m_INVOICE_ID;
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
		public spInsertCOMMISSION_DETAIL()
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
				m_COMMISSION_TYPE= Convert.ToInt32(dr["COMMISSION_TYPE"]);
				m_AMOUNT= Convert.ToDecimal(dr["AMOUNT"]);
				m_COMMISSION_VALUE= Convert.ToDecimal(dr["COMMISSION_VALUE"]);
				m_TAX_VALUE= Convert.ToDecimal(dr["TAX_VALUE"]);
				m_COMMISSION_MASTER_ID=Convert.ToInt64(dr["COMMISSION_MASTER_ID"]);
				m_CUSTOMER_ID=Convert.ToInt64(dr["CUSTOMER_ID"]);
				m_INVOICE_ID=Convert.ToInt64(dr["INVOICE_ID"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@COMMISSION_TYPE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_COMMISSION_TYPE==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_COMMISSION_TYPE;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@AMOUNT" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_AMOUNT==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_AMOUNT;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@COMMISSION_VALUE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_COMMISSION_VALUE==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_COMMISSION_VALUE;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@TAX_VALUE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_TAX_VALUE==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_TAX_VALUE;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@COMMISSION_MASTER_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			if(m_COMMISSION_MASTER_ID==Constants.LongNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_COMMISSION_MASTER_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CUSTOMER_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			if(m_CUSTOMER_ID==Constants.LongNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CUSTOMER_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@INVOICE_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			if(m_INVOICE_ID==Constants.LongNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_INVOICE_ID;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}
