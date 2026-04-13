using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsemkaEsportYesi
{
    internal class Alert
    {
        public static DialogResult error(string msg)
        {
            return MessageBox.Show(msg,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
        public static DialogResult info(string msg)
        {
            return MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static DialogResult confirm(string msg)
        {
            return MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
