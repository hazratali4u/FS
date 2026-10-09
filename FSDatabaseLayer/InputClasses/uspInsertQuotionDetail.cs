using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;

namespace FSDatabaseLayer.Classes
{
	public class uspInsertQuotionDetail
	{
		#region Private Members
		private string sp_Name = "uspInsertQuotionDetail" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_SKU_ID;
		private int m_QUANTITY;
		private int m_CURRENCY_ID;
		private bool m_IS_QUOTATION;
		private decimal m_RATE;
		private long m_QUOTATION_MASTER_ID;
		private long m_ENQUIREY_MASTER_ID;
		private long m_ENQUIREY_DETAIL_ID;
		private string m_DURATION;
		#endregion
		#region Public Properties
		public int SKU_ID
		{
			set
			{
				m_SKU_ID = value ;
			}
			get
			{
				return m_SKU_ID;
			}
		}
		public int QUANTITY
		{
			set
			{
				m_QUANTITY = value ;
			}
			get
			{
				return m_QUANTITY;
			}
		}
		public int CURRENCY_ID
		{
			set
			{
				m_CURRENCY_ID = value ;
			}
			get
			{
				return m_CURRENCY_ID;
			}
		}
		public bool IS_QUOTATION
		{
			set
			{
				m_IS_QUOTATION = value ;
			}
			get
			{
				return m_IS_QUOTATION;
			}
		}
		public decimal RATE
		{
			set
			{
				m_RATE = value ;
			}
			get
			{
				return m_RATE;
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
		public long ENQUIREY_MASTER_ID
		{
			set
			{
				m_ENQUIREY_MASTER_ID = value ;
			}
			get
			{
				return m_ENQUIREY_MASTER_ID;
			}
		}
		public long ENQUIREY_DETAIL_ID
		{
			set
			{
				m_ENQUIREY_DETAIL_ID = value ;
			}
			get
			{
				return m_ENQUIREY_DETAIL_ID;
			}
		}
		public  string DURATION
		{
			set
			{
				m_DURATION = value ;
			}
			get
			{
				return m_DURATION;
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
		public uspInsertQuotionDetail()
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
				m_SKU_ID= Convert.ToInt32(dr["SKU_ID"]);
				m_QUANTITY= Convert.ToInt32(dr["QUANTITY"]);
				m_CURRENCY_ID= Convert.ToInt32(dr["CURRENCY_ID"]);
				m_IS_QUOTATION=Convert.ToBoolean(dr["IS_QUOTATION"]);
				m_RATE= Convert.ToDecimal(dr["RATE"]);
				m_QUOTATION_MASTER_ID=Convert.ToInt64(dr["QUOTATION_MASTER_ID"]);
				m_ENQUIREY_MASTER_ID=Convert.ToInt64(dr["ENQUIREY_MASTER_ID"]);
				m_ENQUIREY_DETAIL_ID=Convert.ToInt64(dr["ENQUIREY_DETAIL_ID"]);
				m_DURATION= Convert.ToString(dr["DURATION"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@SKU_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_SKU_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_SKU_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@QUANTITY" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_QUANTITY==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_QUANTITY;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CURRENCY_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_CURRENCY_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CURRENCY_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@IS_QUOTATION" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
				parameter.Value = m_IS_QUOTATION;
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@RATE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_RATE==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_RATE;
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
			parameter.ParameterName = "@ENQUIREY_MASTER_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			if(m_ENQUIREY_MASTER_ID==Constants.LongNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_ENQUIREY_MASTER_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@ENQUIREY_DETAIL_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			if(m_ENQUIREY_DETAIL_ID==Constants.LongNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_ENQUIREY_DETAIL_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@DURATION" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_DURATION== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_DURATION;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}
