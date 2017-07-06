using System;
using System.Windows.Forms;
using Gdot.Care.Common.Crypto;

namespace CareGateway.CryptoTool
{
    public partial class Crypto : Form
    {
        public Crypto()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtClearText1.Text))
            {
                txtEncryptedValue1.Clear();
                txtEncryptedValue1.Text = Cryptography.Encrypt(txtClearText1.Text, txtEncryptionKey.Text);
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEncryptedValue2.Text))
            {
                txtClearText2.Clear();
                txtClearText2.Text = Cryptography.Decrypt(txtEncryptedValue2.Text, txtEncryptionKey.Text);
            }
        }
    }
}
