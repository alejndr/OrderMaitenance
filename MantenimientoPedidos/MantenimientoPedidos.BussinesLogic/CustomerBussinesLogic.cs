using MantenimientoPedidos.DataAccess;
using MantenimientoPedidos.Entities;

namespace MantenimientoPedidos.BussinesLogic
{
    public class CustomerBussinesLogic
    {
        #region Global variables

        /// <summary>
        /// Customer data access.
        /// </summary>
        private CustomerDataAccess _customerDataAccess;

        #endregion Global variables

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CustomerBussinesLogic()
        {
            _customerDataAccess = new CustomerDataAccess();
        }

        #endregion constructor

        #region Public methods

        /// <summary>
        /// Call for the method to validate the intruced id of a customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public bool ValidateExistCustomer(int customerId)
        {
            return _customerDataAccess.ValidateExistCustomer(customerId);
        }

        public Customer GetCustomer(int customerId)
        {
            return _customerDataAccess.GetCustomer(customerId);
        }

        #endregion Public methods


    }
}
