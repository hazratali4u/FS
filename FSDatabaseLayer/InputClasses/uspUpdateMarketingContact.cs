using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;
namespace FSDatabaseLayer.Classes
{
	public class uspUpdateMarketingContact
	{
		#region Private Members
		private string sp_Name = "uspUpdateMarketingContact" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_USER_ID;
		private bool m_IS_DELETED;
		private long m_MARKETING_CONTACT_ID;
		private string m_CONTACT_NAME;
		private string m_CONTACT_CELL;
		private string m_CONTACT_DESIGNATION;
		private string m_CONTACT_EMAIL;
		private string m_CONTACT_COMPANY;
		#endregion
		#region Public Properties
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
		public bool IS_DELETED
		{
			set
			{
				m_IS_DELETED = value ;
			}
			get
			{
				return m_IS_DELETED;
			}
		}
		public long MARKETING_CONTACT_ID
		{
			set
			{
				m_MARKETING_CONTACT_ID = value ;
			}
			get
			{
				return m_MARKETING_CONTACT_ID;
			}
		}
		public  string CONTACT_NAME
		{
			set
			{
				m_CONTACT_NAME = value ;
			}
			get
			{
				return m_CONTACT_NAME;
			}
		}
		public  string CONTACT_CELL
		{
			set
			{
				m_CONTACT_CELL = value ;
			}
			get
			{
				return m_CONTACT_CELL;
			}
		}
		public  string CONTACT_DESIGNATION
		{
			set
			{
				m_CONTACT_DESIGNATION = value ;
			}
			get
			{
				return m_CONTACT_DESIGNATION;
			}
		}
		public  string CONTACT_EMAIL
		{
			set
			{
				m_CONTACT_EMAIL = value ;
			}
			get
			{
				return m_CONTACT_EMAIL;
			}
		}
		public  string CONTACT_COMPANY
		{
			set
			{
				m_CONTACT_COMPANY = value ;
			}
			get
			{
				return m_CONTACT_COMPANY;
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
		public uspUpdateMarketingContact()
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
				m_USER_ID= Convert.ToInt32(dr["USER_ID"]);
				m_IS_DELETED=Convert.ToBoolean(dr["IS_DELETED"]);
				m_MARKETING_CONTACT_ID=Convert.ToInt64(dr["MARKETING_CONTACT_ID"]);
				m_CONTACT_NAME= Convert.ToString(dr["CONTACT_NAME"]);
				m_CONTACT_CELL= Convert.ToString(dr["CONTACT_CELL"]);
				m_CONTACT_DESIGNATION= Convert.ToString(dr["CONTACT_DESIGNATION"]);
				m_CONTACT_EMAIL= Convert.ToString(dr["CONTACT_EMAIL"]);
				m_CONTACT_COMPANY= Convert.ToString(dr["CONTACT_COMPANY"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
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
			parameter.ParameterName = "@IS_DELETED" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
				parameter.Value = m_IS_DELETED;
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@MARKETING_CONTACT_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			if(m_MARKETING_CONTACT_ID==Constants.LongNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_MARKETING_CONTACT_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CONTACT_NAME" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_CONTACT_NAME== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CONTACT_NAME;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CONTACT_CELL" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_CONTACT_CELL== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CONTACT_CELL;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CONTACT_DESIGNATION" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_CONTACT_DESIGNATION== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CONTACT_DESIGNATION;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CONTACT_EMAIL" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_CONTACT_EMAIL== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CONTACT_EMAIL;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CONTACT_COMPANY" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_CONTACT_COMPANY== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CONTACT_COMPANY;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}
