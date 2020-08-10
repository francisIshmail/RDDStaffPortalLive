using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RDDStaffPortal.DAL
{
    public class Global
    {
        public static string ConnectionString
        {
            get
            {
                try
                {
                    return System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Unable to get Database Connection string from app/web Config File. Contact the site Administrator" + ex);
                }
            }
        }

        public static string getConnectionStringByName(string connStringName)
        {
            
                try
                {
                    return System.Configuration.ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;

                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Unable to get Database Connection string from app/web Config File. Contact the site Administrator" + ex);
                }
        }


        public static string getAppSettingsDataForKey(string apkey)
        {

            return System.Configuration.ConfigurationManager.AppSettings[apkey];
        }

        public static int getAppSettingsDataForKeyInt(string apkey)
        {

            int techPageSize;
            if (!int.TryParse(System.Configuration.ConfigurationManager.AppSettings[apkey], out techPageSize))
            {
                throw new InvalidOperationException("Invalid TechPageSize in web.config");
            }
            return techPageSize;
        }

        public static class CacheConfig
        {
            public static int Duration = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Duration"].ToString());
        }
        public class MyOutputCacheAttribute : OutputCacheAttribute
        {
            public MyOutputCacheAttribute()
            {
                this.Duration = CacheConfig.Duration;
            }
        }

    }
}
