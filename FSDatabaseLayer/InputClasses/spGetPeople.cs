using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;

namespace FSDatabaseLayer.Classes
{
	public class spGetPeople
	{
		#region Private Members
		private string sp_Name = "spGetPeople" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private long m_PEOPLE_ID;
        private int m_USER_ID;
        private int m_TYPE;
		private long m_ORGANIZATION_ID;
        private string m_FNAME;
        private string m_LNAME;
		#endregion

		#region Public Properties
		public long PEOPLE_ID
		{
			set
			{
				m_PEOPLE_ID = value ;
			}
			get
			{
				return m_PEOPLE_ID;
			}
		}
		public long ORGANIZATION_ID
		{
			set
			{
				m_ORGANIZATION_ID = value ;
			}
			get
			{
				return m_ORGANIZATION_ID;
			}
		}

        public int USER_ID
        {
            set { m_USER_ID = value; }
            get { return m_USER_ID; }
        }

        public int TYPE
        {
            set { m_TYPE = value; }
            get { return m_TYPE; }
        }

        public string FNAME
        {
            set
            {
                m_FNAME = value;
            }
            get
            {
                return m_FNAME;
            }
        }

        public string LNAME
        {
            set
            {
                m_LNAME = value;
            }
            get
            {
                return m_LNAME;
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

        public spGetPeople()
        {
            m_PEOPLE_ID = Constants.LongNullValue;
            m_USER_ID = Constants.IntNullValue;
            m_TYPE = Constants.IntNullValue;
            m_ORGANIZATION_ID = Constants.LongNullValue;
            m_FNAME = null;
            m_LNAME = null;
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
                m_PEOPLE_ID = Convert.ToInt64(dr["PEOPLE_ID"]);
                m_ORGANIZATION_ID = Convert.ToInt64(dr["ORGANIZATION_ID"]);
                m_FNAME = dr["FNAME"].ToString();
                m_LNAME = dr["LNAME"].ToString();
            }
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@PEOPLE_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			if(m_PEOPLE_ID==Constants.LongNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PEOPLE_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@ORGANIZATION_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			if(m_ORGANIZATION_ID==Constants.LongNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_ORGANIZATION_ID;
			}
			pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@USER_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
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
            parameter.ParameterName = "@TYPE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_TYPE == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_TYPE;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@FNAME";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_FNAME == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_FNAME;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@LNAME";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_LNAME == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_LNAME;
            }
            pparams.Add(parameter);


		}
		#endregion
	}
}
