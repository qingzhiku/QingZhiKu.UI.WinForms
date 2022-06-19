using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    public partial class DropShadowFormTest : Form
    {
        public DropShadowFormTest()
        {
            InitializeComponent();
        }


        protected override void CreateHandle()
        {
            base.CreateHandle();
            
            DropShadowForm.ExtendForm(this, configration =>
            {
                configration.MasterMoveable = true;
                configration.ShadowBlur = 35;
                configration.ShadowSpread = 8;
                configration.CornerRound = 8;
                configration.StandardMaximizedBounds = true;
            });

        }
    }
}
