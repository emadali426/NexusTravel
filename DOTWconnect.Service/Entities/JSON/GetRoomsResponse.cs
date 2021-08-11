using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTWconnect.Service.Entities
{
    namespace GetRoomsResponse
    {
        public class Xml
        {
            [JsonProperty("@version")]
            public string Version { get; set; }

            [JsonProperty("@encoding")]
            public string Encoding { get; set; }
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class RoomInfo
        {
            [JsonProperty("maxOccupancy")]
            public string MaxOccupancy { get; set; }

            [JsonProperty("maxAdultWithChildren")]
            public string MaxAdultWithChildren { get; set; }

            [JsonProperty("minChildAge")]
            public string MinChildAge { get; set; }

            [JsonProperty("maxChildAge")]
            public string MaxChildAge { get; set; }

            [JsonProperty("maxAdult")]
            public string MaxAdult { get; set; }

            [JsonProperty("maxExtraBed")]
            public string MaxExtraBed { get; set; }

            [JsonProperty("maxChildren")]
            public string MaxChildren { get; set; }
        }

        public class Specials
        {
            [JsonProperty("@count")]
            public string Count { get; set; }
        }

        public class RateType
        {
            [JsonProperty("@currencyid")]
            public string Currencyid { get; set; }

            [JsonProperty("@currencyshort")]
            public string Currencyshort { get; set; }

            [JsonProperty("@description")]
            public string Description { get; set; }

            [JsonProperty("#text")]
            public string Text { get; set; }
        }

        public class AmendCharge
        {
            [JsonProperty("#text")]
            public string Text { get; set; }

            [JsonProperty("formatted")]
            public string Formatted { get; set; }
        }

        public class CancelCharge
        {
            [JsonProperty("#text")]
            public string Text { get; set; }

            [JsonProperty("formatted")]
            public string Formatted { get; set; }
        }

        public class Charge
        {
            [JsonProperty("#text")]
            public string Text { get; set; }

            [JsonProperty("formatted")]
            public string Formatted { get; set; }
        }

        public class Rule
        {
            [JsonProperty("@runno")]
            public string Runno { get; set; }

            [JsonProperty("fromDate")]
            public string FromDate { get; set; }

            [JsonProperty("fromDateDetails")]
            public string FromDateDetails { get; set; }

            [JsonProperty("amendCharge")]
            public AmendCharge AmendCharge { get; set; }

            [JsonProperty("cancelCharge")]
            public CancelCharge CancelCharge { get; set; }

            [JsonProperty("charge")]
            public Charge Charge { get; set; }

            [JsonProperty("noShowPolicy")]
            public string NoShowPolicy { get; set; }
        }

        public class CancellationRules
        {
            [JsonProperty("@count")]
            public string Count { get; set; }

            [JsonProperty("rule")]
            [JsonConverter(typeof(JsonRuleListConverter))]
            public List<Rule> Rule { get; set; }
        }

        public class Total
        {
            [JsonProperty("#text")]
            public string Text { get; set; }

            [JsonProperty("formatted")]
            public string Formatted { get; set; }
        }

        public class Price
        {
            [JsonProperty("#text")]
            public string Text { get; set; }

            [JsonProperty("formatted")]
            public string Formatted { get; set; }
        }

        public class MealType
        {
            [JsonProperty("@code")]
            public string Code { get; set; }

            [JsonProperty("#text")]
            public string Text { get; set; }

            [JsonProperty("@mealtypename")]
            public string Mealtypename { get; set; }

            [JsonProperty("@mealtypecode")]
            public string Mealtypecode { get; set; }

            [JsonProperty("meal")]
            [JsonConverter(typeof(JsonMealListConverter))]
            public List<Meal> Meal { get; set; }
        }

        public class IncludedMeal
        {
            [JsonProperty("@runno")]
            public string Runno { get; set; }

            [JsonProperty("mealName")]
            public string MealName { get; set; }

            [JsonProperty("mealType")]
            public MealType MealType { get; set; }
        }

        public class Including
        {
            [JsonProperty("@count")]
            public string Count { get; set; }

            [JsonProperty("includedMeal")]
            public IncludedMeal IncludedMeal { get; set; }
        }

        public class Date
        {
            [JsonProperty("@runno")]
            public string Runno { get; set; }

            [JsonProperty("@datetime")]
            public string Datetime { get; set; }

            [JsonProperty("@day")]
            public string Day { get; set; }

            [JsonProperty("@wday")]
            public string Wday { get; set; }

            [JsonProperty("price")]
            public Price Price { get; set; }

            [JsonProperty("freeStay")]
            public string FreeStay { get; set; }

            [JsonProperty("dayOnRequest")]
            public string DayOnRequest { get; set; }

            [JsonProperty("including")]
            public Including Including { get; set; }
        }

        public class Dates
        {
            [JsonProperty("@count")]
            public string Count { get; set; }

            [JsonProperty("date")]
            [JsonConverter(typeof(JsonDateListConverter))]
            public List<Date> Date { get; set; }
        }

        public class TariffNotes
        {
            [JsonProperty("#cdata-section")]
            public string CdataSection { get; set; }
        }

        public class RateBasi
        {
            [JsonProperty("@runno")]
            public string Runno { get; set; }

            [JsonProperty("@id")]
            public string Id { get; set; }

            [JsonProperty("@description")]
            public string Description { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("passengerNamesRequiredForBooking")]
            public string PassengerNamesRequiredForBooking { get; set; }

            [JsonProperty("rateType")]
            public RateType RateType { get; set; }

            [JsonProperty("allowsExtraMeals")]
            public string AllowsExtraMeals { get; set; }

            [JsonProperty("allowsSpecialRequests")]
            public string AllowsSpecialRequests { get; set; }

            [JsonProperty("allowsBeddingPreference")]
            public string AllowsBeddingPreference { get; set; }

            [JsonProperty("allocationDetails")]
            public string AllocationDetails { get; set; }

            [JsonProperty("cancellationRules")]
            public CancellationRules CancellationRules { get; set; }

            [JsonProperty("withinCancellationDeadline")]
            public string WithinCancellationDeadline { get; set; }

            [JsonProperty("tariffNotes")]
            public TariffNotes TariffNotes { get; set; }

            [JsonProperty("isBookable")]
            public string IsBookable { get; set; }

            [JsonProperty("onRequest")]
            public string OnRequest { get; set; }

            [JsonProperty("total")]
            public Total Total { get; set; }

            [JsonProperty("dates")]
            public Dates Dates { get; set; }

            [JsonProperty("leftToSell")]
            public string LeftToSell { get; set; }
        }

        public class RateBases
        {
            [JsonProperty("@count")]
            public string Count { get; set; }

            [JsonProperty("rateBasis")]
            [JsonConverter(typeof(JsonRateBasiListConverter))]
            public List<RateBasi> RateBasis { get; set; }
        }

        public class RoomType
        {
            [JsonProperty("@runno")]
            public string Runno { get; set; }

            [JsonProperty("@roomtypecode")]
            public string Roomtypecode { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("twin")]
            public string Twin { get; set; }

            [JsonProperty("roomInfo")]
            public RoomInfo RoomInfo { get; set; }

            [JsonProperty("specials")]
            public Specials Specials { get; set; }

            [JsonProperty("rateBases")]
            public RateBases RateBases { get; set; }
        }

        public class Room
        {
            [JsonProperty("@runno")]
            public string Runno { get; set; }

            [JsonProperty("@count")]
            public string Count { get; set; }

            [JsonProperty("@adults")]
            public string Adults { get; set; }

            [JsonProperty("@children")]
            public string Children { get; set; }

            [JsonProperty("@childrenages")]
            public string Childrenages { get; set; }

            [JsonProperty("@extrabeds")]
            public string Extrabeds { get; set; }

            [JsonProperty("roomType")]
            [JsonConverter(typeof(JsonRoomTypeListConverter))]
            public List<RoomType> RoomType { get; set; }

            [JsonProperty("lookedForText")]
            public string LookedForText { get; set; }
        }

        public class Rooms
        {
            [JsonProperty("@count")]
            public string Count { get; set; }

            [JsonProperty("room")]
            [JsonConverter(typeof(JsonRoomListConverter))]
            public List<Room> Room { get; set; }
        }

        public class MealPrice
        {
            [JsonProperty("#text")]
            public string Text { get; set; }

            [JsonProperty("formatted")]
            public string Formatted { get; set; }
        }

        public class Meal
        {
            [JsonProperty("@runno")]
            public string Runno { get; set; }

            [JsonProperty("@applicablefor")]
            public string Applicablefor { get; set; }

            [JsonProperty("mealCode")]
            public string MealCode { get; set; }

            [JsonProperty("mealName")]
            public string MealName { get; set; }

            [JsonProperty("mealPrice")]
            public MealPrice MealPrice { get; set; }

            [JsonProperty("@startage")]
            public string Startage { get; set; }

            [JsonProperty("@endage")]
            public string Endage { get; set; }
        }

        public class MealDate
        {
            [JsonProperty("@runno")]
            public string Runno { get; set; }

            [JsonProperty("@wday")]
            public string Wday { get; set; }

            [JsonProperty("@day")]
            public string Day { get; set; }

            [JsonProperty("@datetime")]
            public string Datetime { get; set; }

            [JsonProperty("mealType")]
            [JsonConverter(typeof(JsonMealTypeListConverter))]
            public List<MealType> MealType { get; set; }
        }

        public class ExtraMeals
        {
            [JsonProperty("@count")]
            public string Count { get; set; }

            [JsonProperty("mealDate")]
            [JsonConverter(typeof(JsonMealDateListConverter))]
            public List<MealDate> MealDate { get; set; }
        }

        public class Hotel
        {
            [JsonProperty("@id")]
            public string Id { get; set; }

            [JsonProperty("@name")]
            public string Name { get; set; }

            [JsonProperty("allowBook")]
            public string AllowBook { get; set; }

            [JsonProperty("rooms")]
            public Rooms Rooms { get; set; }

            [JsonProperty("extraMeals")]
            public ExtraMeals ExtraMeals { get; set; }
        }

        public class Result
        {
            [JsonProperty("@command")]
            public string Command { get; set; }

            [JsonProperty("@tID")]
            public string TID { get; set; }

            [JsonProperty("@ip")]
            public string Ip { get; set; }

            [JsonProperty("@date")]
            public string Date { get; set; }

            [JsonProperty("@version")]
            public string Version { get; set; }

            [JsonProperty("@elapsedTime")]
            public string ElapsedTime { get; set; }

            [JsonProperty("currencyShort")]
            public string CurrencyShort { get; set; }

            [JsonProperty("hotel")]
            public Hotel Hotel { get; set; }

            [JsonProperty("successful")]
            public string Successful { get; set; }
        }



        public class Root
        {
            [JsonProperty("?xml")]
            public Xml Xml { get; set; }
            [JsonProperty("result")]
            public Result result { get; set; }
        }

        public class JsonRoomListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);

                if (token.Type == JTokenType.Array)
                {

                    return token.ToObject<List<Room>>();
                }

                List<Room> rooms = new List<Room>
                {
                    new Room
                    {
                        Adults = token.ToObject<Room>().Adults,
                        Children = token.ToObject<Room>().Children,
                        Childrenages = token.ToObject<Room>().Childrenages,
                        Count = token.ToObject<Room>().Count,
                        Extrabeds = token.ToObject<Room>().Extrabeds,
                        LookedForText = token.ToObject<Room>().LookedForText,
                        RoomType = token.ToObject<Room>().RoomType,
                        Runno = token.ToObject<Room>().Runno
                    }
                };

                return rooms;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class JsonRoomTypeListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);

                if (token.Type == JTokenType.Array)
                {

                    return token.ToObject<List<RoomType>>();
                }

                List<RoomType> roomTypes = new List<RoomType>
                {
                    new RoomType
                    {
                        Runno = token.ToObject<RoomType>().Runno,
                        Name = token.ToObject<RoomType>().Name,
                        RateBases = token.ToObject<RoomType>().RateBases,
                        RoomInfo = token.ToObject<RoomType>().RoomInfo,
                        Roomtypecode = token.ToObject<RoomType>().Roomtypecode,
                        Specials = token.ToObject<RoomType>().Specials,
                        Twin = token.ToObject<RoomType>().Twin
                       
                    }
                };

                return roomTypes;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class JsonMealDateListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);

                if (token.Type == JTokenType.Array)
                {

                    return token.ToObject<List<MealDate>>();
                }

                List<MealDate> mealDates = new List<MealDate>
                {
                    new MealDate
                    {
                        Runno = token.ToObject<MealDate>().Runno,
                        Datetime = token.ToObject<MealDate>().Datetime,
                        Day = token.ToObject<MealDate>().Day,
                        MealType = token.ToObject<MealDate>().MealType,
                        Wday = token.ToObject<MealDate>().Wday
                        
                    }
                };

                return mealDates;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class JsonMealTypeListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);

                if (token.Type == JTokenType.Array)
                {

                    return token.ToObject<List<MealType>>();
                }

                List<MealType> mealTypes = new List<MealType>
                {
                    new MealType
                    {
                        Meal = token.ToObject<MealType>().Meal,
                        Mealtypecode = token.ToObject<MealType>().Mealtypecode,
                        Mealtypename = token.ToObject<MealType>().Mealtypename,
                        Code = token.ToObject<MealType>().Code,
                        Text = token.ToObject<MealType>().Text
                    }
                };

                return mealTypes;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class JsonMealListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);

                if (token.Type == JTokenType.Array)
                {

                    return token.ToObject<List<Meal>>();
                }

                List<Meal> meals = new List<Meal>
                {
                    new Meal
                    {
                        Applicablefor = token.ToObject<Meal>().Applicablefor,
                        Endage = token.ToObject<Meal>().Endage,
                        MealCode = token.ToObject<Meal>().MealCode,
                        MealName = token.ToObject<Meal>().MealName,
                        MealPrice = token.ToObject<Meal>().MealPrice,
                        Runno = token.ToObject<Meal>().Runno,
                        Startage = token.ToObject<Meal>().Startage
                    }
                };

                return meals;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class JsonRuleListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);

                if (token.Type == JTokenType.Array)
                {

                    return token.ToObject<List<Rule>>();
                }

                List<Rule> rules = new List<Rule>
                {
                    new Rule
                    {
                        AmendCharge = token.ToObject<Rule>().AmendCharge,
                        CancelCharge = token.ToObject<Rule>().CancelCharge,
                        Charge = token.ToObject<Rule>().Charge,
                        FromDate = token.ToObject<Rule>().FromDate,
                        FromDateDetails = token.ToObject<Rule>().FromDateDetails,
                        NoShowPolicy = token.ToObject<Rule>().NoShowPolicy,
                        Runno = token.ToObject<Rule>().Runno                       
                    }
                };

                return rules;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class JsonDateListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);

                if (token.Type == JTokenType.Array)
                {

                    return token.ToObject<List<Date>>();
                }

                List<Date> dates = new List<Date>
                {
                    new Date
                    {
                        Datetime = token.ToObject<Date>().Datetime,
                        Runno = token.ToObject<Date>().Runno,
                        Day = token.ToObject<Date>().Day,
                        DayOnRequest = token.ToObject<Date>().FreeStay,
                        FreeStay = token.ToObject<Date>().Datetime,
                        Including = token.ToObject<Date>().Including,
                        Price = token.ToObject<Date>().Price,
                        Wday = token.ToObject<Date>().Wday
                    }
                };

                return dates;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class JsonRateBasiListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);

                if (token.Type == JTokenType.Array)
                {

                    return token.ToObject<List<RateBasi>>();
                }

                List<RateBasi> rateBasis = new List<RateBasi>
                {
                    new RateBasi
                    {
                        Runno = token.ToObject<RateBasi>().Runno,
                        AllocationDetails = token.ToObject<RateBasi>().AllocationDetails,
                        AllowsBeddingPreference = token.ToObject<RateBasi>().AllowsBeddingPreference,
                        AllowsExtraMeals = token.ToObject<RateBasi>().AllowsExtraMeals,
                        AllowsSpecialRequests = token.ToObject<RateBasi>().AllowsSpecialRequests,
                        CancellationRules = token.ToObject<RateBasi>().CancellationRules,
                        Dates = token.ToObject<RateBasi>().Dates,
                        Description = token.ToObject<RateBasi>().Description,
                        Id = token.ToObject<RateBasi>().Id,
                        IsBookable = token.ToObject<RateBasi>().IsBookable,
                        LeftToSell = token.ToObject<RateBasi>().LeftToSell,
                        OnRequest = token.ToObject<RateBasi>().OnRequest,
                        PassengerNamesRequiredForBooking = token.ToObject<RateBasi>().PassengerNamesRequiredForBooking,
                        RateType = token.ToObject<RateBasi>().RateType,
                        Status = token.ToObject<RateBasi>().Status,
                        TariffNotes = token.ToObject<RateBasi>().TariffNotes,
                        Total = token.ToObject<RateBasi>().Total,
                        WithinCancellationDeadline = token.ToObject<RateBasi>().WithinCancellationDeadline
                    }
                };

                return rateBasis;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }
    }
}
