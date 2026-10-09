using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;

namespace FSDatabaseLayer.Classes
{
	public class uspInsertIndentDetail
	{
		#region Private Members
		private string sp_Name = "uspInsertIndentDetail" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_SKU_ID;
		private int m_QUANTITY;
		private int m_PAYMENT_ID;
		private int m_SHIPMENT_ID;
		private int m_SHIPMENTMODE_ID;
		private int m_DELIVERYLOCATION_ID;
		private int m_UOM_ID;
        private int m_CURRENCY_ID;
		private DateTime m_LEAD_TIME;
		private decimal m_PRICE;
		private long m_INDENT_MASTER_ID;
        private string m_SKU_CODE;
		#endregion
		#region Public Properties
        public string SKU_CODE
        {
            set
            {
                m_SKU_CODE = value;
            }
            get
            {
                return m_SKU_CODE;
            }
        }
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
		public int PAYMENT_ID
		{
			set
			{
				m_PAYMENT_ID = value ;
			}
			get
			{
				return m_PAYMENT_ID;
			}
		}
		public int SHIPMENT_ID
		{
			set
			{
				m_SHIPMENT_ID = value ;
			}
			get
			{
				return m_SHIPMENT_ID;
			}
		}
		public int SHIPMENTMODE_ID
		{
			set
			{
				m_SHIPMENTMODE_ID = value ;
			}
			get
			{
				return m_SHIPMENTMODE_ID;
			}
		}
		public int DELIVERYLOCATION_ID
		{
			set
			{
				m_DELIVERYLOCATION_ID = value ;
			}
			get
			{
				return m_DELIVERYLOCATION_ID;
			}
		}
		public int UOM_ID
		{
			set
			{
				m_UOM_ID = value ;
			}
			get
			{
				return m_UOM_ID;
			}
		}
        public int CURRENCY_ID
        {
            set
            {
                m_CURRENCY_ID = value; ;
            }
            get
            {
                return m_CURRENCY_ID;
            }
        }
		public DateTime LEAD_TIME
		{
			set
			{
				m_LEAD_TIME = value ;
			}
			get
			{
				return m_LEAD_TIME;
			}
		}
		public decimal PRICE
		{
			set
			{
				m_PRICE = value ;
			}
			get
			{
				return m_PRICE;
			}
		}
		public long INDENT_MASTER_ID
		{
			set
			{
				m_INDENT_MASTER_ID = value ;
			}
			get
			{
				return m_INDENT_MASTER_ID;
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
		public uspInsertIndentDetail()
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
				m_PAYMENT_ID= Convert.ToInt32(dr["PAYMENT_ID"]);
				m_SHIPMENT_ID= Convert.ToInt32(dr["SHIPMENT_ID"]);
				m_SHIPMENTMODE_ID= Convert.ToInt32(dr["SHIPMENTMODE_ID"]);
				m_DELIVERYLOCATION_ID= Convert.ToInt32(dr["DELIVERYLOCATION_ID"]);
				m_UOM_ID= Convert.ToInt32(dr["UOM_ID"]);
				m_LEAD_TIME= Convert.ToDateTime(dr["LEAD_TIME"]);
				m_PRICE= Convert.ToDecimal(dr["PRICE"]);
				m_INDENT_MASTER_ID=Convert.ToInt64(dr["INDENT_MASTER_ID"]);
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
			parameter.ParameterName = "@PAYMENT_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_PAYMENT_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PAYMENT_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@SHIPMENT_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_SHIPMENT_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_SHIPMENT_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@SHIPMENTMODE_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_SHIPMENTMODE_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_SHIPMENTMODE_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@DELIVERYLOCATION_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_DELIVERYLOCATION_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_DELIVERYLOCATION_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@UOM_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_UOM_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_UOM_ID;
			}
			pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CURRENCY_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_CURRENCY_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_CURRENCY_ID;
            }
            pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@LEAD_TIME" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_LEAD_TIME==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_LEAD_TIME;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@PRICE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_PRICE==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PRICE;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@INDENT_MASTER_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			if(m_INDENT_MASTER_ID==Constants.LongNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_INDENT_MASTER_ID;
			}
			pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SKU_CODE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_SKU_CODE == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SKU_CODE;
            }
            pparams.Add(parameter);


		}
		#endregion
	}
}
