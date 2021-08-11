using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.MobileControls;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DOTWconnect.Service.Entities
{
    namespace InternalCodeRequest
    {
		// using System.Xml.Serialization;
		// XmlSerializer serializer = new XmlSerializer(typeof(Customer));
		// using (StringReader reader = new StringReader(xml))
		// {
		//    var test = (Customer)serializer.Deserialize(reader);
		// }

		[XmlRoot(ElementName = "filters", Namespace = "")]
		public class Filters
		{

			[XmlElement(ElementName = "countryCode", Namespace = "")]
			public string CountryCode;

			[XmlElement(ElementName = "countryName", Namespace = "")]
			public string CountryName;
		}

		[XmlRoot(ElementName = "fields", Namespace = "")]
		public class Fields
		{
            public Fields()
            {
				
            }

			[XmlElement(ElementName = "field", Namespace = "")]
			public List<string> Field;
		}

		[XmlRoot(ElementName = "return", Namespace = "")]
		public class Return
		{
            public Return()
            {
				Filters = new Filters();
				Fields = new Fields();
            }
			[XmlElement(ElementName = "filters", Namespace = "")]
			public Filters Filters;

			[XmlElement(ElementName = "fields", Namespace = "")]
			public Fields Fields;
		}

		[XmlRoot(ElementName = "request", Namespace = "")]
		public class Request
		{
            public Request()
            {
				Return = new Return();
            }
			[XmlElement(ElementName = "return")]
			public Return Return;

			[XmlAttribute(AttributeName = "command")]
			public string Command;

			[XmlText]
			public string Text;
		}
		
		[XmlRoot(ElementName = "customer")]
		public class Customer
		{

			public Customer()
			{
				Username = DOTWconnectService.LoginID;
				Password = DOTWconnectService.Password;
				Id = DOTWconnectService.CompanyCode;
				Source = 1;
				Request = new Request();
			}

			[XmlElement(ElementName = "username", Namespace = "")]
			public string Username;

			[XmlElement(ElementName = "password", Namespace = "")]
			public string Password;

			[XmlElement(ElementName = "id", Namespace = "")]
			public string Id;

			[XmlElement(ElementName = "source", Namespace = "")]
			public int Source;

			[XmlElement(ElementName = "request", Namespace = "")]
			public Request Request;
		}


	}
}
