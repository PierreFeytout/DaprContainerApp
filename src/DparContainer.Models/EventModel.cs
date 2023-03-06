namespace DparContainer.Models
{
    /// <summary>
    /// BaseEventModelClass.
    /// </summary>
    public class EventModel
    {
        /// <summary>
        /// The Iot object id.
        /// </summary>
        public string ObjectId { get; set; } = default!;

        /// <summary>
        /// The temperature info.
        /// </summary>
        public int Temperature { get; set; }

        /// <summary>
        /// Enumeration indicating the current object status.
        /// </summary>
        public State Status { get; set; }
    }
}