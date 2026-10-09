using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;

namespace FSDatabaseLayer.Classes
{
	public class uspInsertRightDetail
	{
		#region Private Members
		private string sp_Name = "uspInsertRightDetail" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_RIGHT_DETAIL_ID;
		private int m_RIGHT_MASTER_ID;
		private int m_USER_ID;
		private bool m_CAN_VIEW;
		private bool m_CAN_EDIT;
		private bool m_CAN_DELETE;
		#endregion
		#region Public Properties
		public int RIGHT_DETAIL_ID
		{
			set
			{
				m_RIGHT_DETAIL_ID = value ;
			}
			get
			{
				return m_RIGHT_DETAIL_ID;
			}
		}
		public int RIGHT_MASTER_ID
		{
			set
			{
				m_RIGHT_MASTER_ID = value ;
			}
			get
			{
				return m_RIGHT_MASTER_ID;
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
		public bool CAN_VIEW
		{
			set
			{
				m_CAN_VIEW = value ;
			}
			get
			{
				return m_CAN_VIEW;
			}
		}
		public bool CAN_EDIT
		{
			set
			{
				m_CAN_EDIT = value ;
			}
			get
			{
				return m_CAN_EDIT;
			}
		}
		public bool CAN_DELETE
		{
			set
			{
				m_CAN_DELETE = value ;
			}
			get
			{
				return m_CAN_DELETE;
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
		public uspInsertRightDetail()
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
				m_RIGHT_DETAIL_ID= Convert.ToInt32(dr["RIGHT_DETAIL_ID"]);
				m_RIGHT_MASTER_ID= Convert.ToInt32(dr["RIGHT_MASTER_ID"]);
				m_USER_ID= Convert.ToInt32(dr["USER_ID"]);
				m_CAN_VIEW=Convert.ToBoolean(dr["CAN_VIEW"]);
				m_CAN_EDIT=Convert.ToBoolean(dr["CAN_EDIT"]);
				m_CAN_DELETE=Convert.ToBoolean(dr["CAN_DELETE"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@RIGHT_DETAIL_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_RIGHT_DETAIL_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_RIGHT_DETAIL_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@RIGHT_MASTER_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_RIGHT_MASTER_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_RIGHT_MASTER_ID;
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
			parameter.ParameterName = "@CAN_VIEW" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
				parameter.Value = m_CAN_VIEW;
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CAN_EDIT" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
				parameter.Value = m_CAN_EDIT;
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CAN_DELETE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
				parameter.Value = m_CAN_DELETE;
			pparams.Add(parameter);


		}
		#endregion
	}
}
