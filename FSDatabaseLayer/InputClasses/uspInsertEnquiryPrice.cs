using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;

namespace FSDatabaseLayer.Classes
{
	public class uspInsertEnquiryPrice
	{
		#region Private Members
		private string sp_Name = "uspInsertEnquiryPrice" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_PRODUCT_ID;
		private int m_SUPPLIER_ID;
		private int m_USER_ID;
        private int m_CURRENCY_ID;
        private decimal m_Rate;
        private decimal m_RateCustomer;
		private long m_ENQUIRY_ID;
		private string m_INFO;
		private string m_REMARKS;
        private bool m_IS_QUOTATION;
		#endregion
		#region Public Properties
		public int PRODUCT_ID
		{
			set
			{
				m_PRODUCT_ID = value ;
			}
			get
			{
				return m_PRODUCT_ID;
			}
		}
		public int SUPPLIER_ID
		{
			set
			{
				m_SUPPLIER_ID = value ;
			}
			get
			{
				return m_SUPPLIER_ID;
			}
		}
        public int CURRENCY_ID
        {
            set
            {
                m_CURRENCY_ID = value;
            }
            get
            {
                return m_CURRENCY_ID;
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
        public decimal Rate
        {
            set
            {
                m_Rate = value;
            }
            get
            {
                return m_Rate;
            }
        }
        public decimal RateCustomer
        {
            set
            {
                m_RateCustomer = value;
            }
            get
            {
                return m_RateCustomer;
            }
        }
		public long ENQUIRY_ID
		{
			set
			{
				m_ENQUIRY_ID = value ;
			}
			get
			{
				return m_ENQUIRY_ID;
			}
		}
		public  string INFO
		{
			set
			{
				m_INFO = value ;
			}
			get
			{
				return m_INFO;
			}
		}
		public  string REMARKS
		{
			set
			{
				m_REMARKS = value ;
			}
			get
			{
				return m_REMARKS;
			}
		}

        public bool IS_QUOTATION
        {
            set
            {
                m_IS_QUOTATION = value;
            }
            get
            {
                return m_IS_QUOTATION;
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
		public uspInsertEnquiryPrice()
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
				m_PRODUCT_ID= Convert.ToInt32(dr["PRODUCT_ID"]);
				m_SUPPLIER_ID= Convert.ToInt32(dr["SUPPLIER_ID"]);
				m_USER_ID= Convert.ToInt32(dr["USER_ID"]);
                m_Rate = Convert.ToDecimal(dr["Rate"]);
				m_ENQUIRY_ID=Convert.ToInt64(dr["ENQUIRY_ID"]);
				m_INFO= Convert.ToString(dr["INFO"]);
				m_REMARKS= Convert.ToString(dr["REMARKS"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@PRODUCT_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_PRODUCT_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PRODUCT_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@SUPPLIER_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_SUPPLIER_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_SUPPLIER_ID;
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
            parameter.ParameterName = "@Rate";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_Rate == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Rate;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@RateCustomer";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_RateCustomer == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_RateCustomer;
            }
            pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@ENQUIRY_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			if(m_ENQUIRY_ID==Constants.LongNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_ENQUIRY_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@INFO" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_INFO== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_INFO;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@REMARKS" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_REMARKS== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_REMARKS;
			}
			pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IS_QUOTATION";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            if (m_IS_QUOTATION == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_IS_QUOTATION;
            }
            pparams.Add(parameter);


		}
		#endregion
	}
}
