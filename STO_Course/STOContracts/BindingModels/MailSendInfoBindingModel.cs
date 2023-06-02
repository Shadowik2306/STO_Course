using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOContracts.BindingModels
{
	public class MailSendInfoBindingModel
	{

		public string MailAddress { get; set; } = string.Empty;

		public string Subject { get; set; } = string.Empty;

		public string Text { get; set; } = string.Empty;

		public byte[] File { get; set; } = Array.Empty<byte>();


	}
}
