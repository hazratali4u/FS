using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;
namespace FSDatabaseLayer.Classes
{
	public class uspInsertMarketingEmail
	{
		#region Private Members
		private string sp_Name = "uspInsertMarketingEmail" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_MARKETING_TEMPLATE_ID;
		private int m_USER_ID;
		private DateTime m_DOCUMENT_DATE;
		private long m_MARKETING_EMAIL_ID;
		private string m_TEMPLATE_BODY;
		private string m_FROM;
		private string m_TO;
		#endregion
		#region Public Properties
		public int MARKETING_TEMPLATE_ID
		{
			set
			{
				m_MARKETING_TEMPLATE_ID = value ;
			}
			get
			{
				return m_MARKETING_TEMPLATE_ID;
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
		public long MARKETING_EMAIL_ID
		{
			get
			{
				return m_MARKETING_EMAIL_ID;
			}
		}
		public  string TEMPLATE_BODY
		{
			set
			{
				m_TEMPLATE_BODY = value ;
			}
			get
			{
				return m_TEMPLATE_BODY;
			}
		}
		public  string FROM
		{
			set
			{
				m_FROM = value ;
			}
			get
			{
				return m_FROM;
			}
		}
		public  string TO
		{
			set
			{
				m_TO = value ;
			}
			get
			{
				return m_TO;
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
		public uspInsertMarketingEmail()
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
				m_MARKETING_EMAIL_ID = (long)((IDataParameter)(cmd.Parameters["@MARKETING_EMAIL_ID"])).Value;
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
				m_MARKETING_TEMPLATE_ID= Convert.ToInt32(dr["MARKETING_TEMPLATE_ID"]);
				m_USER_ID= Convert.ToInt32(dr["USER_ID"]);
				m_DOCUMENT_DATE= Convert.ToDateTime(dr["DOCUMENT_DATE"]);
				m_MARKETING_EMAIL_ID=Convert.ToInt64(dr["MARKETING_EMAIL_ID"]);
				m_TEMPLATE_BODY= Convert.ToString(dr["TEMPLATE_BODY"]);
				m_FROM= Convert.ToString(dr["FROM"]);
				m_TO= Convert.ToString(dr["TO"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@MARKETING_TEMPLATE_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_MARKETING_TEMPLATE_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_MARKETING_TEMPLATE_ID;
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
			parameter.ParameterName = "@MARKETING_EMAIL_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			parameter.Direction = ParameterDirection.Output;
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@TEMPLATE_BODY" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_TEMPLATE_BODY== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_TEMPLATE_BODY;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@FROM" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_FROM== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_FROM;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@TO" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_TO== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_TO;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}
