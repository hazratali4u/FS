using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;

namespace FSDatabaseLayer.Classes
{
	public class uspUpdateMenu
	{
		#region Private Members
		private string sp_Name = "uspUpdateMenu" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_MENU_ID;
		private int m_MENU_PARENT_ID;
		private int m_MENU_TYPE_ID;
		private int m_MENU_SORT_ID;
		private string m_MENU_DESCRIPTION;
        private string m_MENU_KEY;
		#endregion
		#region Public Properties
		public int MENU_ID
		{
			set
			{
				m_MENU_ID = value ;
			}
			get
			{
				return m_MENU_ID;
			}
		}
		public int MENU_PARENT_ID
		{
			set
			{
				m_MENU_PARENT_ID = value ;
			}
			get
			{
				return m_MENU_PARENT_ID;
			}
		}
		public int MENU_TYPE_ID
		{
			set
			{
				m_MENU_TYPE_ID = value ;
			}
			get
			{
				return m_MENU_TYPE_ID;
			}
		}
		public int MENU_SORT_ID
		{
			set
			{
				m_MENU_SORT_ID = value ;
			}
			get
			{
				return m_MENU_SORT_ID;
			}
		}
		public  string MENU_DESCRIPTION
		{
			set
			{
				m_MENU_DESCRIPTION = value ;
			}
			get
			{
				return m_MENU_DESCRIPTION;
			}
		}
        public string MENU_KEY
        {
            set { m_MENU_KEY = value; }
            get { return m_MENU_KEY; }
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
		public uspUpdateMenu()
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
				m_MENU_ID= Convert.ToInt32(dr["MENU_ID"]);
				m_MENU_PARENT_ID= Convert.ToInt32(dr["MENU_PARENT_ID"]);
				m_MENU_TYPE_ID= Convert.ToInt32(dr["MENU_TYPE_ID"]);
				m_MENU_SORT_ID= Convert.ToInt32(dr["MENU_SORT_ID"]);
				m_MENU_DESCRIPTION= Convert.ToString(dr["MENU_DESCRIPTION"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@MENU_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_MENU_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_MENU_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@MENU_PARENT_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_MENU_PARENT_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_MENU_PARENT_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@MENU_TYPE_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_MENU_TYPE_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_MENU_TYPE_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@MENU_SORT_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_MENU_SORT_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_MENU_SORT_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@MENU_DESCRIPTION" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_MENU_DESCRIPTION== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_MENU_DESCRIPTION;
			}
			pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@MENU_KEY";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_MENU_KEY == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_MENU_KEY;
            }
            pparams.Add(parameter);


		}
		#endregion
	}
}
