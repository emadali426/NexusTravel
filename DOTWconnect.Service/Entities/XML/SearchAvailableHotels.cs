using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace DOTWconnect.Service.Entities
{
	namespace SearchAvailableHotels
	{


		// using System.Xml.Serialization;
		// XmlSerializer serializer = new XmlSerializer(typeof(Customer));
		// using (StringReader reader = new StringReader(xml))
		// {
		//    var test = (Customer)serializer.Deserialize(reader);
		// }

		[XmlRoot(ElementName = "children")]
		public class Children
		{

			[XmlAttribute(AttributeName = "no")]
			public int No { get; set; }

			[XmlElement(ElementName = "child")]
			public Child Child { get; set; }

			[XmlText]
			public string Text { get; set; }
		}

		[XmlRoot(ElementName = "room")]
		public class Room
		{

			[XmlElement(ElementName = "adultsCode")]
			public int AdultsCode { get; set; }

			[XmlElement(ElementName = "children")]
			public Children Children { get; set; }

			[XmlElement(ElementName = "rateBasis")]
			public int RateBasis { get; set; }

			//[XmlElement(ElementName = "passengerNationality")]
			//public int PassengerNationality { get; set; }

			//[XmlElement(ElementName = "passengerCountryOfResidence")]
			//public int PassengerCountryOfResidence { get; set; }

			[XmlAttribute(AttributeName = "runno")]
			public int Runno { get; set; }

			[XmlText]
			public string Text { get; set; }
		}

		[XmlRoot(ElementName = "child")]
		public class Child
		{

			[XmlAttribute(AttributeName = "runno")]
			public int Runno { get; set; }

			[XmlText]
			public string Text { get; set; }
		}

		[XmlRoot(ElementName = "rooms")]
		public class Rooms
		{

			[XmlElement(ElementName = "room")]
			public List<Room> Room { get; set; }

			[XmlAttribute(AttributeName = "no")]
			public int No { get; set; }

			[XmlText]
			public string Text { get; set; }
		}

		[XmlRoot(ElementName = "bookingDetails")]
		public class BookingDetails
		{

			[XmlElement(ElementName = "fromDate")]
			public string FromDate { get; set; }

			[XmlElement(ElementName = "toDate")]
			public string ToDate { get; set; }

			[XmlElement(ElementName = "currency")]
			public int Currency { get; set; }

			[XmlElement(ElementName = "rooms")]
			public Rooms Rooms { get; set; }
		}

		[XmlRoot(ElementName = "filters")]
		public class Filters
		{

			[XmlElement(ElementName = "city")]
			public int City { get; set; }
			

			[XmlAttribute(AttributeName = "a")]
			public string A { get; set; }

			[XmlAttribute(AttributeName = "c")]
			public string C { get; set; }

			[XmlText]
			public string Text { get; set; }

			[XmlNamespaceDeclarations]
			public XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();

			public Filters()
			{
				xmlns.Add("a", "http://us.dotwconnect.com/xsd/atomicCondition");
				xmlns.Add("c", "http://us.dotwconnect.com/xsd/complexCondition");
			}
		}

		[XmlRoot(ElementName = "return")]
		public class Return
		{
			
			[XmlElement(ElementName = "filters")]
			public Filters Filters { get; set; }
		}

		[XmlRoot(ElementName = "request")]
		public class Request
		{

			[XmlElement(ElementName = "bookingDetails")]
			public BookingDetails BookingDetails { get; set; }

			[XmlElement(ElementName = "return")]
			public Return Return { get; set; }

			[XmlAttribute(AttributeName = "command")]
			public string Command { get; set; }

			[XmlText]
			public string Text { get; set; }
		}

		[XmlRoot(ElementName = "customer")]
		public class Customer
		{

			[XmlElement(ElementName = "username")]
			public string Username { get; set; }

			[XmlElement(ElementName = "password")]
			public string Password { get; set; }

			[XmlElement(ElementName = "id")]
			public int Id { get; set; }

			[XmlElement(ElementName = "source")]
			public int Source { get; set; }

			[XmlElement(ElementName = "product")]
			public string Product { get; set; }

			[XmlElement(ElementName = "request")]
			public Request Request { get; set; }
		}



	}
}
