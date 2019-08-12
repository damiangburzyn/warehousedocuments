using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace System
{
   public static class FormExtentions
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static T WrapException<T>(this Form form, Func<T> func) 

        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd");
                _logger.Error("Podczas operacji wystąpił błąd", ex);
                return default(T);
            }
        }


        public static void WrapException(this Form form, Action func)

        {
            try
            {
                 func();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd");
                _logger.Error("Podczas operacji wystąpił błąd", ex);
                
            }
        }

    }
}
