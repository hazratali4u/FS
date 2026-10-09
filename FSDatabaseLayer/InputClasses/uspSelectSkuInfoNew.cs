using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;


namespace FSDatabaseLayer.Classes
{
	public class uspSelectSkuInfoNew
	{
		#region Private Members
		private string sp_Name = " uspSelectSkuInfoNew" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;

        private int m_ENQUIRY_MASTER_ID;
		private int m_brand_id;
		private int m_Principal_id;
		private int m_company_id;
		private int m_division_id;
		private int m_category_id;
		#endregion


		#region Public Properties

        public int ENQUIRY_MASTER_ID
        {
            set
            {
                m_ENQUIRY_MASTER_ID = value;
            }
            get
            {
                return m_ENQUIRY_MASTER_ID;
            }
        }

		public int brand_id
		{
			set
			{
				m_brand_id = value ;
			}
			get
			{
				return m_brand_id;
			}
		}


		public int Principal_id
		{
			set
			{
				m_Principal_id = value ;
			}
			get
			{
				return m_Principal_id;
			}
		}


		public int company_id
		{
			set
			{
				m_company_id = value ;
			}
			get
			{
				return m_company_id;
			}
		}


		public int division_id
		{
			set
			{
				m_division_id = value ;
			}
			get
			{
				return m_division_id;
			}
		}


		public int category_id
		{
			set
			{
				m_category_id = value ;
			}
			get
			{
				return m_category_id;
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
		public uspSelectSkuInfoNew()
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
				cmd.CommandText = "uspSelectSkuInfoNew";
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
				command.CommandText = "uspSelectSkuInfoNew";
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
				command.CommandText = "uspSelectSkuInfoNew";
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
				command.CommandText = "uspSelectSkuInfoNew";
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
					m_brand_id= Convert.ToInt32(dr["brand_id"]);
					m_Principal_id= Convert.ToInt32(dr["Principal_id"]);
					m_company_id= Convert.ToInt32(dr["company_id"]);
					m_division_id= Convert.ToInt32(dr["division_id"]);
					m_category_id= Convert.ToInt32(dr["category_id"]);
				}
			}


		    public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			
                parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
                parameter.ParameterName = "@ENQUIRY_MASTER_ID"; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_ENQUIRY_MASTER_ID == Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
                parameter.Value = m_ENQUIRY_MASTER_ID;
			}
			pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@brand_id";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_brand_id == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_brand_id;
            }
            pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@Principal_id" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_Principal_id==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_Principal_id;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@company_id" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_company_id==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_company_id;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@division_id" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_division_id==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_division_id;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@category_id" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_category_id==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_category_id;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}
