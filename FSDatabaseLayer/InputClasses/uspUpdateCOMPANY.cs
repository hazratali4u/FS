using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;

namespace FSDatabaseLayer.Classes
{
	public class uspUpdateCOMPANY
	{
		#region Private Members
		private string sp_Name = "uspUpdateCOMPANY" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_COMPANY_ID;
		private bool m_ISDELETED;
		private string m_COMPANY_NAME;
		private string m_ADDRESS1;
		private string m_ADDRESS2;
		private string m_PHONE;
		private string m_WEBSITE;
		private string m_EMAIL_ADDRESS;
        private string m_NTN_NO;
		#endregion
		#region Public Properties
		public int COMPANY_ID
		{
			set
			{
				m_COMPANY_ID = value ;
			}
			get
			{
				return m_COMPANY_ID;
			}
		}
		public bool ISDELETED
		{
			set
			{
				m_ISDELETED = value ;
			}
			get
			{
				return m_ISDELETED;
			}
		}
		public  string COMPANY_NAME
		{
			set
			{
				m_COMPANY_NAME = value ;
			}
			get
			{
				return m_COMPANY_NAME;
			}
		}
		public  string ADDRESS1
		{
			set
			{
				m_ADDRESS1 = value ;
			}
			get
			{
				return m_ADDRESS1;
			}
		}
		public  string ADDRESS2
		{
			set
			{
				m_ADDRESS2 = value ;
			}
			get
			{
				return m_ADDRESS2;
			}
		}
		public  string PHONE
		{
			set
			{
				m_PHONE = value ;
			}
			get
			{
				return m_PHONE;
			}
		}
		public  string WEBSITE
		{
			set
			{
				m_WEBSITE = value ;
			}
			get
			{
				return m_WEBSITE;
			}
		}
		public  string EMAIL_ADDRESS
		{
			set
			{
				m_EMAIL_ADDRESS = value ;
			}
			get
			{
				return m_EMAIL_ADDRESS;
			}
		}
        public string NTN_NO
        {
            set { m_NTN_NO = value; }
            get { return m_NTN_NO; }

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
		public uspUpdateCOMPANY()
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
				m_COMPANY_ID= Convert.ToInt32(dr["COMPANY_ID"]);
				m_ISDELETED=Convert.ToBoolean(dr["ISDELETED"]);
				m_COMPANY_NAME= Convert.ToString(dr["COMPANY_NAME"]);
				m_ADDRESS1= Convert.ToString(dr["ADDRESS1"]);
				m_ADDRESS2= Convert.ToString(dr["ADDRESS2"]);
				m_PHONE= Convert.ToString(dr["PHONE"]);
				m_WEBSITE= Convert.ToString(dr["WEBSITE"]);
				m_EMAIL_ADDRESS= Convert.ToString(dr["EMAIL_ADDRESS"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@COMPANY_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_COMPANY_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_COMPANY_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@ISDELETED" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
				parameter.Value = m_ISDELETED;
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@COMPANY_NAME" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_COMPANY_NAME== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_COMPANY_NAME;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@ADDRESS1" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_ADDRESS1== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_ADDRESS1;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@ADDRESS2" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_ADDRESS2== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_ADDRESS2;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@PHONE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_PHONE== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PHONE;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@WEBSITE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_WEBSITE== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_WEBSITE;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@EMAIL_ADDRESS" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_EMAIL_ADDRESS== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_EMAIL_ADDRESS;
			}
			pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@NTN_NO";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_NTN_NO == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_NTN_NO;
            }
            pparams.Add(parameter);


		}
		#endregion
	}
}
