using System;
using System.Data;
using FSCommonLayer.Classes;
using FSDataAccessLayer.Classes;

namespace FSDatabaseLayer.Classes
{
	public class spInsertPeople
	{
		#region Private Members

		private string sp_Name = "spInsertPeople" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_TITLE;
		private int m_USER_ID;
		private DateTime m_DOB;
		private long m_ORGANIZATION_ID;
		private long m_PEOPLE_ID;
        private int m_LOCATION_ID;
        private string m_DESIGNATION_ID;
		private string m_FNAME;
		private string m_LNAME;
		private int m_DEPARTMENT;
		private string m_EMAIL;
		private string m_PHONE;
        private string m_ACCOUNT_NO;
		
        #endregion

        #region Public Properties

		public int TITLE
		{
			set
			{
				m_TITLE = value ;
			}
			get
			{
				return m_TITLE;
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
		public DateTime DOB
		{
			set
			{
				m_DOB = value ;
			}
			get
			{
				return m_DOB;
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
		public long PEOPLE_ID
		{
			get
			{
				return m_PEOPLE_ID;
			}
		}
        public int LOCATION_ID
        {
            set
            {
                m_LOCATION_ID = value;
            }
            get
            {
                return m_LOCATION_ID;
            }
        }
        public string DESIGNATION_ID
        {
            set
            {
                m_DESIGNATION_ID = value;
            }
            get
            {
                return m_DESIGNATION_ID;
            }

        }
		public  string FNAME
		{
			set
			{
				m_FNAME = value ;
			}
			get
			{
				return m_FNAME;
			}
		}
		public  string LNAME
		{
			set
			{
				m_LNAME = value ;
			}
			get
			{
				return m_LNAME;
			}
		}
		public  int DEPARTMENT
		{
			set
			{
				m_DEPARTMENT = value ;
			}
			get
			{
				return m_DEPARTMENT;
			}
		}
		public  string EMAIL
		{
			set
			{
				m_EMAIL = value ;
			}
			get
			{
				return m_EMAIL;
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
        public string ACCOUNT_NO
        {
            set { m_ACCOUNT_NO = value; }
            get { return m_ACCOUNT_NO; }
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
		
        public spInsertPeople()
		{
            m_ACCOUNT_NO = null;
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
				m_PEOPLE_ID = (long)((IDataParameter)(cmd.Parameters["@PEOPLE_ID"])).Value;
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
				m_TITLE= Convert.ToInt32(dr["TITLE"]);
				m_USER_ID= Convert.ToInt32(dr["USER_ID"]);
				m_DOB= Convert.ToDateTime(dr["DOB"]);
				m_ORGANIZATION_ID=Convert.ToInt64(dr["ORGANIZATION_ID"]);
				m_PEOPLE_ID=Convert.ToInt64(dr["PEOPLE_ID"]);
                m_LOCATION_ID = Convert.ToInt32(dr["LOCATION_ID"]);
				m_FNAME= Convert.ToString(dr["FNAME"]);
				m_LNAME= Convert.ToString(dr["LNAME"]);
				m_DEPARTMENT= Convert.ToInt32(dr["DEPARTMENT"]);
				m_EMAIL= Convert.ToString(dr["EMAIL"]);
				m_PHONE= Convert.ToString(dr["PHONE"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@TITLE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_TITLE==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_TITLE;
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
			parameter.ParameterName = "@DOB" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_DOB==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_DOB;
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
			parameter.ParameterName = "@PEOPLE_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			parameter.Direction = ParameterDirection.Output;
			pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@LOCATION_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_LOCATION_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_LOCATION_ID;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DESIGNATION_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_DESIGNATION_ID == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DESIGNATION_ID;
            }
            pparams.Add(parameter);

			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@FNAME" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_FNAME== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_FNAME;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@LNAME" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_LNAME== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_LNAME;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@DEPARTMENT" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_DEPARTMENT== Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_DEPARTMENT;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@EMAIL" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_EMAIL== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_EMAIL;
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
            parameter.ParameterName = "@ACCOUNT_NO";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_ACCOUNT_NO == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_ACCOUNT_NO;
            }
            pparams.Add(parameter);

		}
		
        #endregion
	}
}
