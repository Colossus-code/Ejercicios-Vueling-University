namespace WebApi.Models
{
    /// <summary>
    /// Model for the deliver. 
    /// </summary>
    public class DeliverModel
    {
        /// <summary>
        /// User ID of deliver
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///  Introduce ID product and Quantity
        /// </summary>
        public Dictionary<int, int> ProductQuantity { get; set; }

    }
}
