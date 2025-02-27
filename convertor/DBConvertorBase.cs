namespace Convertor
{
    public abstract class DBConvertorBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #endregion
        private ComponentFactory.Krypton.Toolkit.KryptonButton LoadFile;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox MaincomboBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox MaincomboBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton SaveToFile;
        private ComponentFactory.Krypton.Toolkit.KryptonButton GoNextButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton GoBackButton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton EndButton;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox GroupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox GroupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox textBoxPort3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox textBoxPassword3;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox textBoxUser3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox textBoxServer3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel9;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel10;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox textBoxPassword;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox textBoxUser;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox textBoxServer;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox textBoxPort;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel11;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel12;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox groupBox4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel18;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel13;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox textBoxPort4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel14;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox textBoxPassword4;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox textBoxUser4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel15;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox textBoxServer4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel16;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel17;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox groupBox3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel19;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel20;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox textBoxPort2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel21;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox textBoxPassword2;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox textBoxUser2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel22;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox textBoxServer2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel23;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel24;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox SchemeComboBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox DbComboBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox DbComboBox3;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox SchemeComboBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton TestButton3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton TesButton2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton TestButton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton TestButton4;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox DBTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBox DB2TextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private System.Windows.Forms.BindingSource toolsBindingSource;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.BindingSource configBindingSource;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkBoxColumn;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn Source;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewComboBoxColumn Destination;
        private System.Windows.Forms.DataGridViewLinkColumn TransferColumn;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView DataView;

        public abstract void comboBox1_SelectedIndexChanged(object sender, EventArgs e);
        public abstract void comboBox2_SelectedIndexChanged(object sender, EventArgs e);
        public abstract void MainWindowDefaultLoad(object sender, EventArgs e);
        public abstract void valuesAreExactWhatIsInTextBox();
        protected abstract override void Dispose(bool disposing);
        private abstract void DataView_CellClick(object sender, DataGridViewCellEventArgs e);
        private abstract void EndButton_Click(object sender, EventArgs e);
        private abstract void GoBackButton_Click(object sender, EventArgs e);
        private abstract void GoNextButton_Click(object sender, EventArgs e);
        private abstract void InitializeComponent();
        private abstract void LoadFile_Click(object sender, EventArgs e);
        private abstract void SaveTOFile_Click(object sender, EventArgs e);
        private abstract void TesButton2_Click(object sender, EventArgs e);
        private abstract void TestButton3_Click(object sender, EventArgs e);
        private abstract void TestButton4_Click(object sender, EventArgs e);
        private abstract void TestButton_Click(object sender, EventArgs e);
    }
}