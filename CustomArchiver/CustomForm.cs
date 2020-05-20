using System.Collections.Generic;
using System.Windows.Forms;

namespace CustomArchiver
{
    // класс UI формы
    internal partial class CustomForm : Form
    {
        internal CustomForm()
        {
            InitializeComponent();
            UiLogic.InitUi(new List<Control> {bt_root, bt_create, bt_open, bt_unpack, tb_log});
        }
    }
}