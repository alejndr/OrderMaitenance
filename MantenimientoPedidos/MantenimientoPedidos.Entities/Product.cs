using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantenimientoPedidos.Entities
{
    public class Product
    {
        public int ID { get; set; }

        public string TrackingNumber { get; set; }

        public int OrderQty { get; set; }

        public string ProductName { get; set; }
        
        public int SalesOrderDetailID { get; set; }

    }
}
