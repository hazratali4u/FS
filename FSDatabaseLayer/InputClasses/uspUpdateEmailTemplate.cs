using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;

namespace FSDatabaseLayer.Classes
{
	public class uspUpdateEmailTemplate
	{
		#region Private Members
		private string sp_Name = "uspUpdateEmailTemplate" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_EMAIL_TEMPLATE_ID;
        private int m_CATEGORY_ID;
		private bool m_IS_ACTIVE;
		private string m_SUBJECT;
        private string m_BODY;
        private string m_SIGNATURE;
        private string m_TERMS;
		private int m_USER_ID;
		#endregion
		#region Public Properties
		public int EMAIL_TEMPLATE_ID
		{
			set
			{
				m_EMAIL_TEMPLATE_ID = value ;
			}
			get
			{
				return m_EMAIL_TEMPLATE_ID;
			}
		}
        public int CATEGORY_ID
        {
            set { m_CATEGORY_ID = value; }
            get { return m_CATEGORY_ID; }
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
		public string SUBJECT
		{
			set
			{
				m_SUBJECT = value ;
			}
			get
			{
				return m_SUBJECT;
			}
		}
        public string BODY
        {
            set
            {
                m_BODY = value;
            }
            get
            {
                return m_BODY;
            }
        }

        public string SIGNATURE
        {
            set
            {
                m_SIGNATURE = value;
            }
            get
            {
                return m_SIGNATURE;
            }
        }
        public string TERMS
        {
            set
            {
                m_TERMS = value;
            }
            get
            {
                return m_TERMS;
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
		public uspUpdateEmailTemplate()
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
            if (dr.Read())
            {
                m_EMAIL_TEMPLATE_ID = Convert.ToInt32(dr["EMAIL_TEMPLATE_ID"]);
                m_IS_ACTIVE = Convert.ToBoolean(dr["IS_ACTIVE"]);
                m_SUBJECT = dr["SUBJECT"].ToString();
                m_USER_ID = Convert.ToInt32(dr["USER_ID"]);
            }
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@EMAIL_TEMPLATE_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_EMAIL_TEMPLATE_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_EMAIL_TEMPLATE_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@IS_ACTIVE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
				parameter.Value = m_IS_ACTIVE;
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@SUBJECT" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_SUBJECT == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SUBJECT;
            }		    
			pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@BODY";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_BODY == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_BODY;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SIGNATURE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_SIGNATURE == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SIGNATURE;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TERMS";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_TERMS == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_TERMS;
            }
            pparams.Add(parameter);

			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@USER_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_USER_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_USER_ID;
            }
			pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CATEGORY_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_CATEGORY_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_CATEGORY_ID;
            }
            pparams.Add(parameter);

		}
		#endregion
	}
}
