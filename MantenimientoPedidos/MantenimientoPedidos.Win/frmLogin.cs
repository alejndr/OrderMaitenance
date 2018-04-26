using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MantenimientoPedidos.BussinesLogic;

namespace MantenimientoPedidos.Win
{
    /// <summary>
    /// Formulario de login.
    /// </summary>
    public partial class frmLogin : frmBase
    {

        #region Global variables

        
       

        #endregion Variables globales

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public frmLogin()
        {
            InitializeComponent();
        }


        #endregion Constructor

        #region Eventos

        /// <summary>
        /// Initializa the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Connect with the main form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Conectar_Click(object sender, EventArgs e)
        {
            checkLogin();
            
            

        }

        /// <summary>
        /// Id textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtIDUser_TextChanged(object sender, EventArgs e)
        {
            EnableConect();
        }

        /// <summary>
        /// Password textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            EnableConect();
        }

        #endregion Events

        #region Private Methods


        /// <summary>
        /// Check the id to coincide with a existing id and let the user to the main form.
        /// </summary>
        private void checkLogin()
        {
            CustomerBussinesLogic LoginBL = new CustomerBussinesLogic();

            //Get search criteria
            string IDintroduced = txtIDUser.Text;
            int id;
            

            if (int.TryParse(IDintroduced, out id))
            {
                if (LoginBL.ValidateExistCustomer(id))
                {
                    this.Visible = false;

                    frmMain frmMain = new frmMain(id);
                    frmMain.ShowDialog();

                    
                    
                }
                else
                {
                    base.ShowMessage("El usuario no existe");
                    txtIDUser.Clear();
                    txtPassword.Clear();
                    txtIDUser.Focus();

                }
            }
            else
            {
                base.ShowMessage("Tiene que introducir un numero");
                txtIDUser.Clear();
                txtIDUser.Focus();
            }
             


        }

       /// <summary>
       /// Enable the buttons if the textboxes have any text
       /// </summary>
        private void EnableConect()
        {
            if ((txtIDUser.TextLength > 0) && (txtPassword.TextLength > 0))
            {
                btn_Conect.Enabled = true;
            }
            else
            {
                btn_Conect.Enabled = false;
            }

        }

       
        #endregion Metodos privados

        
    }
}
