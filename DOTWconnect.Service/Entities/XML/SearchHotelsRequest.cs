using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DOTWconnect.Service.Entities
{
	namespace SearchHotelsRequest
	{
		// using System.Xml.Serialization;
		// XmlSerializer serializer = new XmlSerializer(typeof(Customer));
		// using (StringReader reader = new StringReader(xml))
		// {
		//    var test = (Customer)serializer.Deserialize(reader);
		// }

		public class Child
        {
			[XmlAttribute(AttributeName = "runno")]
			public int Runno { get; set; }

			[XmlText]
			public int ChildAge { get; set; }
		}

		[XmlRoot(ElementName = "children")]
		public class Children
		{

			[XmlAttribute(AttributeName = "no")]
			public int No { get; set; }

			[XmlElement(ElementName = "child")]
			public List<Child> Child { get; set; }
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
			[XmlElement(ElementName = "passengerNationality")]
			public int PassengerNationality { get; set; }

			[XmlElement(ElementName = "passengerCountryOfResidence")]
			public int PassengerCountryOfResidence { get; set; }

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

		[XmlRoot(ElementName = "fieldValues")]
		public class FieldValues
		{

			[XmlElement(ElementName = "fieldValue", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
			public List<string> FieldValue { get; set; }
		}

		[XmlRoot(ElementName = "condition", Namespace = "http://us.dotwconnect.com/xsd/atomicCondition")]
		public class Condition
		{
			
			public Condition condition { get; set; }
			
			
			[XmlElement(ElementName = "fieldName", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
			public string FieldName { get; set; }

			[XmlElement(ElementName = "fieldTest", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
			public string FieldTest { get; set; }

			[XmlElement(ElementName = "fieldValues", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
			public FieldValues FieldValues { get; set; }

			
			[XmlElement(ElementName = "operator", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
			public string Operator { get; set; }

		}

		[XmlRoot(ElementName = "filters")]
		public class Filters
		{

			[XmlElement(ElementName = "city")]
			public int City { get; set; }

			[XmlElement(ElementName = "nearbyCities")]
			public bool NearbyCities { get; set; }

            [XmlElement(ElementName = "noPrice", IsNullable = true)]
			public bool? NoPrice { get; set; }

			[XmlElement(ElementName = "condition", Namespace = "http://us.dotwconnect.com/xsd/complexCondition")]
			public Condition Condition { get; set; }

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

		[XmlRoot(ElementName = "fields")]
		public class Fields
		{

			[XmlElement(ElementName = "field")]
			public List<string> Field { get; set; }

			[XmlElement(ElementName = "roomField")]
			public List<string> RoomField { get; set; }
		}

		[XmlRoot(ElementName = "return")]
		public class Return
		{
			[XmlElement(ElementName = "getRooms")]
			public string GetRooms { get; set; }

			[XmlElement(ElementName = "filters")]
			public Filters Filters { get; set; }

			[XmlElement(ElementName = "fields")]
			public Fields Fields { get; set; }
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
			public string Id { get; set; }

			[XmlElement(ElementName = "source")]
			public int Source { get; set; }

			[XmlElement(ElementName = "product")]
			public string Product { get; set; }

			[XmlElement(ElementName = "language")]
			public string Language { get; set; }

			[XmlElement(ElementName = "request")]
			public Request Request { get; set; }
		}



	}
}
