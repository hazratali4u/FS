using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;

namespace FSDatabaseLayer.Classes
{
	public class uspInsertEnquiryPayment
	{
		#region Private Members
		private string sp_Name = "uspInsertEnquiryPayment" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_PAYMENT_MODE;
		private int m_USER_ID;
		private DateTime m_CHEQUE_DATE;
		private DateTime m_PAYMENT_DATE;
		private decimal m_AMOUNT;
		private long m_ENQUIRY_ID;
		private long m_INVOICE_ID;
		private long m_ENQUIRY_PAYMENT_ID;
        private long m_ENQUIREY_DETAIL_ID;
		private string m_PAYMENT_REFERENCE;
		private string m_CHEQUE_NO;
		private string m_BANK_NAME;
		private string m_SLIP_NO;
		private string m_REMARKS;
		#endregion
		#region Public Properties
        
		public int PAYMENT_MODE
		{
			set
			{
				m_PAYMENT_MODE = value ;
			}
			get
			{
				return m_PAYMENT_MODE;
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
		public DateTime CHEQUE_DATE
		{
			set
			{
				m_CHEQUE_DATE = value ;
			}
			get
			{
				return m_CHEQUE_DATE;
			}
		}
		public DateTime PAYMENT_DATE
		{
			set
			{
				m_PAYMENT_DATE = value ;
			}
			get
			{
				return m_PAYMENT_DATE;
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
		public long ENQUIRY_PAYMENT_ID
		{
			get
			{
				return m_ENQUIRY_PAYMENT_ID;
			}
		}
        public long ENQUIREY_DETAIL_ID
        {
            set { m_ENQUIREY_DETAIL_ID = value; }
            get { return m_ENQUIREY_DETAIL_ID; }
        }
		public  string PAYMENT_REFERENCE
		{
			set
			{
				m_PAYMENT_REFERENCE = value ;
			}
			get
			{
				return m_PAYMENT_REFERENCE;
			}
		}
		public  string CHEQUE_NO
		{
			set
			{
				m_CHEQUE_NO = value ;
			}
			get
			{
				return m_CHEQUE_NO;
			}
		}
		public  string BANK_NAME
		{
			set
			{
				m_BANK_NAME = value ;
			}
			get
			{
				return m_BANK_NAME;
			}
		}
		public  string SLIP_NO
		{
			set
			{
				m_SLIP_NO = value ;
			}
			get
			{
				return m_SLIP_NO;
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
		public uspInsertEnquiryPayment()
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
				m_ENQUIRY_PAYMENT_ID = (long)((IDataParameter)(cmd.Parameters["@ENQUIRY_PAYMENT_ID"])).Value;
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
            if (dr.Read())
            {
                m_PAYMENT_MODE = Convert.ToInt32(dr["PAYMENT_MODE"]);
                m_USER_ID = Convert.ToInt32(dr["USER_ID"]);
                m_CHEQUE_DATE = Convert.ToDateTime(dr["CHEQUE_DATE"]);
                m_PAYMENT_DATE = Convert.ToDateTime(dr["PAYMENT_DATE"]);
                m_AMOUNT = Convert.ToDecimal(dr["AMOUNT"]);
                m_ENQUIRY_ID = Convert.ToInt64(dr["ENQUIRY_ID"]);
                m_INVOICE_ID = Convert.ToInt64(dr["INVOICE_ID"]);
                m_ENQUIRY_PAYMENT_ID = Convert.ToInt64(dr["ENQUIRY_PAYMENT_ID"]);
                m_PAYMENT_REFERENCE = Convert.ToString(dr["PAYMENT_REFERENCE"]);
                m_CHEQUE_NO = Convert.ToString(dr["CHEQUE_NO"]);
                m_BANK_NAME = Convert.ToString(dr["BANK_NAME"]);
                m_SLIP_NO = Convert.ToString(dr["SLIP_NO"]);
                m_REMARKS = Convert.ToString(dr["REMARKS"]);
            }
        }
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
            
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@PAYMENT_MODE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_PAYMENT_MODE==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PAYMENT_MODE;
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
			parameter.ParameterName = "@CHEQUE_DATE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_CHEQUE_DATE==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CHEQUE_DATE;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@PAYMENT_DATE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_PAYMENT_DATE==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PAYMENT_DATE;
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


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@ENQUIRY_PAYMENT_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			parameter.Direction = ParameterDirection.Output;
			pparams.Add(parameter);


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
			parameter.ParameterName = "@PAYMENT_REFERENCE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_PAYMENT_REFERENCE== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PAYMENT_REFERENCE;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CHEQUE_NO" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_CHEQUE_NO== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CHEQUE_NO;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@BANK_NAME" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_BANK_NAME== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_BANK_NAME;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@SLIP_NO" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_SLIP_NO== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_SLIP_NO;
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


		}
		#endregion
	}
}
