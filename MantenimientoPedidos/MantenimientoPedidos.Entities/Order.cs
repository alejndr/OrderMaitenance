using System;

namespace MantenimientoPedidos.Entities
{
    /// <summary>
    /// Order data.
    /// </summary>
    public class Order
    {

        public int ID { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public string ShipMethod { get; set; }

        public bool Enable { get; set; }
    }
}
