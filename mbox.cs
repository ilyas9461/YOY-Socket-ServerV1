using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace socketio_client
{
    class mbox
    {

        public const string MSJ_TIP_BILGI = "bilgi";
        public const string MSJ_TIP_HATA = "hata";
        public const string MSJ_TIP_SORU = "soru";
        
        DialogResult secim = new DialogResult();

        public DialogResult Show(string mesaj,string tip)
        {
            if (tip.Equals(MSJ_TIP_BILGI))
            {
                
                MessageBox.Show(mesaj, "YOY SERVER", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (tip.Equals(MSJ_TIP_HATA)) MessageBox.Show(mesaj, "YOY SERVER", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (tip.Equals(MSJ_TIP_SORU))
            {
                secim=MessageBox.Show(mesaj, "YOY SERVER", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes) return DialogResult.Yes;
                if (secim == DialogResult.No) return DialogResult.No;
            }

            return DialogResult.Cancel;
        }

        public DialogResult ShowTimeOut(string mesaj, string tip)
        {
            if (tip.Equals(MSJ_TIP_BILGI))
            {
                (new System.Threading.Thread(CloseIt)).Start();
                MessageBox.Show(mesaj, "YOY SERVER", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (tip.Equals(MSJ_TIP_HATA))
            {
                (new System.Threading.Thread(CloseIt)).Start();
                MessageBox.Show(mesaj, "YOY SERVER", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (tip.Equals(MSJ_TIP_SORU))
            {
                (new System.Threading.Thread(CloseIt)).Start();
                secim = MessageBox.Show(mesaj, "YOY SERVER", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes) return DialogResult.Yes;
                if (secim == DialogResult.No) return DialogResult.No;
            }

            return DialogResult.Cancel;
        }

        //(new System.Threading.Thread(CloseIt)).Start();
        public void CloseIt()
        {
            //System.Threading.Thread.Sleep(5000);
            //Microsoft.VisualBasic.Interaction.AppActivate(
            //     System.Diagnostics.Process.GetCurrentProcess().Id);
            //System.Windows.Forms.SendKeys.SendWait(" ");
        }
    }
}
