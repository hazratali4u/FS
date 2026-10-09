using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;


namespace FSDatabaseLayer.Classes
{
	public class spInsertPRODUCT_ASSIGNMENT
	{
		#region Private Members

		private string sp_Name = "spInsertPRODUCT_ASSIGNMENT" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;

		private int m_PRODUCT_ID;
		private int m_SUPPLIER_ID;

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
        public spInsertPRODUCT_ASSIGNMENT()
        {
            m_PRODUCT_ID = Constants.IntNullValue;
            m_SUPPLIER_ID = Constants.IntNullValue;
        }
		#endregion

		#region public Methods
		public bool  ExecuteQuery()
		{
			try
			{
			    IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				cmd.CommandType =  CommandType.StoredProcedure;
				cmd.CommandText = "spInsertPRODUCT_ASSIGNMENT";
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
				command.CommandText = "spInsertPRODUCT_ASSIGNMENT";
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
				command.CommandText = "spInsertPRODUCT_ASSIGNMENT";
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
				command.CommandText = "spInsertPRODUCT_ASSIGNMENT";
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


		}
		#endregion
	}
}
