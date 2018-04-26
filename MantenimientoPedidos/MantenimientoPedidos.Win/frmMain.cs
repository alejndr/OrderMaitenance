using MantenimientoPedidos.BussinesLogic;
using MantenimientoPedidos.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MantenimientoPedidos.Win
{
    public partial class frmMain : frmBase    {

        #region Global variables

        /// <summary>
        /// Person Id
        /// </summary>
        private int _CustomerID;

        private Order _OrderData;

        private Customer _CustomerData;

        private DateTime _DateFrom;

        private DateTime _DateTo;

        #endregion Global variables

        #region Constructor

        
        public frmMain(int customerId)
        {
            InitializeComponent();

            _CustomerID = customerId;
           
        }

        #endregion Constructor

        #region Events

        // TODO: Initialize form -> SetHeader, SetDefaultCriterias, Search

        /// <summary>
        /// Initialize components on load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }

        /// <summary>
        /// Search button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        /// <summary>
        /// Modify grid button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModify_Click(object sender, EventArgs e)
        {
            ShowOrderDetail();
        }

        /// <summary>
        /// Selection changed grid event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdOrders_SelectionChanged(object sender, EventArgs e)
        {
            CheckEnableGridButtons();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveOrder();
        }

        #endregion Events

        #region Private methods

        /// <summary>
        /// Initialize the text boxes with the information of the customer.
        /// </summary>
        private void initializeTxt()
        {
            CustomerBussinesLogic customerBL = new CustomerBussinesLogic();
            Customer getCustomerData = new Customer();
            

            getCustomerData = customerBL.GetCustomer(_CustomerID);

            txtName.Text = getCustomerData.Name;
            txtStore.Text = getCustomerData.Store;
            txtTerritory.Text = getCustomerData.Territory;

            // Store the customer data for the detail form
            _CustomerData = getCustomerData;

        }

        /// <summary>
        /// Initialize grid.
        /// </summary>
        private void InitializeGrid()
        {
            DataGridViewColumn colOrderID = new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "ID",
                Name = "colOrderID",
                HeaderText = "Order ID",
                ValueType = typeof(int),
                Visible = false
            };

            DataGridViewColumn colDateFrom = new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Date",
                Name = "colDateFrom",
                HeaderText = "Start date",
                ValueType = typeof(DateTime),
                Visible = true
            };

            DataGridViewColumn colOrderNumber = new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Number",
                Name = "colOrderNumber",
                HeaderText = "Order Number",
                ValueType = typeof(string),
                Visible = true
            };

            DataGridViewColumn colSendMethod = new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "ShipMethod",
                Name = "colSendMethod",
                HeaderText = "Shipping Method",
                ValueType = typeof(string),
                Visible = true
            };

            DataGridViewColumn colEnable = new DataGridViewCheckBoxColumn()
            {
                DataPropertyName = "Enable",
                Name = "colEnable",
                HeaderText = "Enabled",
                ValueType = typeof(Boolean),
                Visible = true
            };

            grdOrders.Columns.Add(colOrderID);
            grdOrders.Columns.Add(colDateFrom);
            grdOrders.Columns.Add(colOrderNumber);
            grdOrders.Columns.Add(colSendMethod);
            grdOrders.Columns.Add(colEnable);

            // Set properties
            grdOrders.AutoGenerateColumns = false;
            grdOrders.ReadOnly = true;
            grdOrders.MultiSelect = false;
            grdOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdOrders.RowHeadersVisible = false;
            grdOrders.AllowUserToResizeRows = false;
            grdOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Set grid buttons
            btnModify.Enabled = false;
            btnRemove.Enabled = false;


            
        }

        /// <summary>
        /// Initialize Date.
        /// </summary>
        private void InitializeDate()
        {
            dtpFrom.Value = DateTime.Now.AddYears(-1);
            dtpTo.Value = DateTime.Now.AddYears(1);
        }

        /// <summary>
        /// Search function between 2 dates.
        /// </summary>
        private void Search()
        {
            try
            { 
            if (dtpFrom.Value <= dtpTo.Value)
            {
                OrderBussinesLogic OrderBL = new OrderBussinesLogic();
                List<Order> orderList = new List<Order>();

                // Get search criteria
                DateTime dateFrom = dtpFrom.Value;
                DateTime dateTo = dtpTo.Value;
                int id = _CustomerID;
                
                // Set global variables for the dates to reload the grid if needed.
                _DateFrom = dateFrom;
                _DateTo = dateTo;

                // Do search
                orderList = OrderBL.Search(_CustomerID, dateFrom, dateTo);

                // Bind grid
                grdOrders.DataSource = orderList;
                grdOrders.ClearSelection();

                // Check if "No records found" message applies
                if (orderList.Count == 0)
                {
                    base.ShowMessage("No orders found.");
                }

                // Enable/Disable grid buttons
                CheckEnableGridButtons();
            }
            else
            {
                base.ShowMessage("The start date has to be lower than the end date.");
            }
            }
            catch
            {

                throw;
            }


        }

        /// <summary>
        /// Show order details
        /// </summary>
        private void ShowOrderDetail()
        {
            if (grdOrders.SelectedRows.Count > 0)
            {
                
                _OrderData = (Order)grdOrders.CurrentRow.DataBoundItem;

                
                frmDetailOrder frmDetailOrder = new frmDetailOrder(_CustomerData, _OrderData);
                frmDetailOrder.ShowDialog();
                
            }
            
        }

        // TODO: Double click to open the Order detail form

        /// <summary>
        /// Check if grid buttons can be enabled.
        /// </summary>
        private void CheckEnableGridButtons()
        {
            bool existRows = grdOrders.SelectedRows.Count > 0;

            btnModify.Enabled = existRows;
            btnRemove.Enabled = existRows;
            
        }

        /// <summary>
        /// Initialize form.
        /// </summary>
        private void InitializeForm()
        {
            initializeTxt();

            CheckEnableGridButtons();

            InitializeGrid();

            InitializeDate();

        }

        /// <summary>
        /// Logical remove of the selected order
        /// </summary>
        /// <param name="orderToRemove"></param>
        private void RemoveOrder()
        {
            if (ShowConfirmationMessage("Are you sure?", "Remove Order") == DialogResult.Yes)
            {
                if (grdOrders.SelectedRows.Count > 0)
                {
                    Order orderToRemove = (Order)grdOrders.SelectedRows[0].DataBoundItem;

                    try
                    {
                        OrderBussinesLogic orderBL = new OrderBussinesLogic();

                        orderBL.RemoveOrder(orderToRemove);

                        // Refresh
                        
                        List<Order> orderList = orderBL.Search(_CustomerID, _DateFrom, _DateTo);

                        
                        grdOrders.DataSource = orderList;
                        grdOrders.ClearSelection();

                        ShowMessage("Order removed successfully.");

                    }
                    catch 
                    {

                        throw;
                    }
                }
            }
        }


        #endregion Private methods

        
    }
}
