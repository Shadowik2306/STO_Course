using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOContracts.BindingModels
{
    internal class MessageInfoBindingModel
    {
		public int Id { get; set; }

		public string MessageId { get; set; } = string.Empty;

		public int? ClientId { get; set; }

		public string SenderName { get; set; } = string.Empty;

		public DateTime DateDelivery { get; set; } = DateTime.Now;

		public string Subject { get; set; } = string.Empty;

		public string Body { get; set; } = string.Empty;

		public bool IsRead { get; set; } = false;

		public string? Answer { get; set; } = string.Empty;
	}
}
