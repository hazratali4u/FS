using System;
using System.Data;
using FSDataAccessLayer.Classes;
using FSCommonLayer.Classes;

namespace FSDatabaseLayer.Classes
{
	public class uspUpdateIndentMaster
	{
		#region Private Members
		private string sp_Name = "uspUpdateIndentMaster" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_DISTRIBUTOR_ID;
		private int m_CUSTOMER_ID;
		private int m_SUPPLIER_ID;
		private int m_STATUS_ID;
		private int m_USER_ID;
		private int m_DAY1;
		private int m_DAY2;
		private DateTime m_DOCUMENT_DATE;
		private bool m_IS_ACTIVE;
		private long m_INDENT_MASTER_ID;
		private long m_ENQUIRY_ID;
		private string m_REMARKS;
		private string m_AccountNo;
		private string m_BRANCH;
		private string m_BRANCH_ADDRESS;
		private string m_BRANCH_CODE;
		private string m_RTGS_CODE;
		private string m_WIFT_CODE;
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
		public int CUSTOMER_ID
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
		public int STATUS_ID
		{
			set
			{
				m_STATUS_ID = value ;
			}
			get
			{
				return m_STATUS_ID;
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
		public int DAY1
		{
			set
			{
				m_DAY1 = value ;
			}
			get
			{
				return m_DAY1;
			}
		}
		public int DAY2
		{
			set
			{
				m_DAY2 = value ;
			}
			get
			{
				return m_DAY2;
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
		public bool IS_ACTIVE
		{
			set
			{
				m_IS_ACTIVE = value ;
			}
			get
			{
				return m_IS_ACTIVE;
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
		public  string AccountNo
		{
			set
			{
				m_AccountNo = value ;
			}
			get
			{
				return m_AccountNo;
			}
		}
		public  string BRANCH
		{
			set
			{
				m_BRANCH = value ;
			}
			get
			{
				return m_BRANCH;
			}
		}
		public  string BRANCH_ADDRESS
		{
			set
			{
				m_BRANCH_ADDRESS = value ;
			}
			get
			{
				return m_BRANCH_ADDRESS;
			}
		}
		public  string BRANCH_CODE
		{
			set
			{
				m_BRANCH_CODE = value ;
			}
			get
			{
				return m_BRANCH_CODE;
			}
		}
		public  string RTGS_CODE
		{
			set
			{
				m_RTGS_CODE = value ;
			}
			get
			{
				return m_RTGS_CODE;
			}
		}
		public  string WIFT_CODE
		{
			set
			{
				m_WIFT_CODE = value ;
			}
			get
			{
				return m_WIFT_CODE;
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
		public uspUpdateIndentMaster()
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
				m_DISTRIBUTOR_ID= Convert.ToInt32(dr["DISTRIBUTOR_ID"]);
				m_CUSTOMER_ID= Convert.ToInt32(dr["CUSTOMER_ID"]);
				m_SUPPLIER_ID= Convert.ToInt32(dr["SUPPLIER_ID"]);
				m_STATUS_ID= Convert.ToInt32(dr["STATUS_ID"]);
				m_USER_ID= Convert.ToInt32(dr["USER_ID"]);
				m_DAY1= Convert.ToInt32(dr["DAY1"]);
				m_DAY2= Convert.ToInt32(dr["DAY2"]);
				m_DOCUMENT_DATE= Convert.ToDateTime(dr["DOCUMENT_DATE"]);
				m_IS_ACTIVE=Convert.ToBoolean(dr["IS_ACTIVE"]);
				m_INDENT_MASTER_ID=Convert.ToInt64(dr["INDENT_MASTER_ID"]);
				m_ENQUIRY_ID=Convert.ToInt64(dr["ENQUIRY_ID"]);
				m_REMARKS= Convert.ToString(dr["REMARKS"]);
				m_AccountNo= Convert.ToString(dr["AccountNo"]);
				m_BRANCH= Convert.ToString(dr["BRANCH"]);
				m_BRANCH_ADDRESS= Convert.ToString(dr["BRANCH_ADDRESS"]);
				m_BRANCH_CODE= Convert.ToString(dr["BRANCH_CODE"]);
				m_RTGS_CODE= Convert.ToString(dr["RTGS_CODE"]);
				m_WIFT_CODE= Convert.ToString(dr["WIFT_CODE"]);
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
			parameter.ParameterName = "@CUSTOMER_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_CUSTOMER_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CUSTOMER_ID;
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
			parameter.ParameterName = "@STATUS_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_STATUS_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_STATUS_ID;
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
			parameter.ParameterName = "@DAY1" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_DAY1==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_DAY1;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@DAY2" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_DAY2==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_DAY2;
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
			parameter.ParameterName = "@IS_ACTIVE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
				parameter.Value = m_IS_ACTIVE;
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
			parameter.ParameterName = "@AccountNo" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_AccountNo== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_AccountNo;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@BRANCH" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_BRANCH== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_BRANCH;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@BRANCH_ADDRESS" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_BRANCH_ADDRESS== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_BRANCH_ADDRESS;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@BRANCH_CODE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_BRANCH_CODE== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_BRANCH_CODE;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@RTGS_CODE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_RTGS_CODE== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_RTGS_CODE;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@WIFT_CODE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_WIFT_CODE== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_WIFT_CODE;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}
