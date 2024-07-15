using BookingHotel.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace BookingHotel.Data

{
    public class DbInitializer
    {
        public static void Initialize(HotelContext hotelContext)
        {
            // Room Context
            hotelContext.Database.EnsureCreated();

            if (hotelContext.Rooms.Any())
            {
                return;   // DB has been seeded
            }

            var roomTypes = new RoomType[]
            {
                new RoomType{roomTypeName="OceanFront Room", roomLeft=8, price=445},
                new RoomType{roomTypeName="City View Room", roomLeft=3, price=500},
                new RoomType{roomTypeName="Surf Club Room", roomLeft=8, price=354},
                new RoomType{roomTypeName="Bay-View Room", roomLeft=5, price=731},
                new RoomType{roomTypeName="Ocean Bungalow", roomLeft=3, price=612},
                new RoomType{roomTypeName="Ocean-View Corner Studio", roomLeft=8, price=231},
                new RoomType{roomTypeName="Bay-View Corner Studio", roomLeft=8, price=547},
                new RoomType{roomTypeName="OceanFront Four Bedroom Suite", roomLeft=6, price=888},
                new RoomType{roomTypeName="Marybelle Penthouse Suite", roomLeft=1, price=999},
                new RoomType{roomTypeName="Bay-View One Bedroom Suite", roomLeft=7, price=666},
                new RoomType{roomTypeName="OceanFront One Bedroom Suite", roomLeft=6, price=453},
                new RoomType{roomTypeName="OceanFront Two Bedroom Suite", roomLeft=4, price=746},
                new RoomType{roomTypeName="Bay-View Two Bedroom Suite", roomLeft=6, price=414},
                new RoomType{roomTypeName="Surf Club One Bedroom Suite", roomLeft=3, price=352},
                new RoomType{roomTypeName="City-View Two Bedroom Suite", roomLeft=2, price=532},
                new RoomType{roomTypeName="OceanFront Accessible Room", roomLeft=2, price=333},
            };
            hotelContext.RoomTypes.AddRange(roomTypes);
            hotelContext.SaveChanges();

            var rooms = new Room[]
            {
                new Room{roomName="501", roomTypeID=roomTypes.Single(s => s.roomTypeID == 1).roomTypeID, status = "Empty"},
                new Room{roomName="601", roomTypeID=roomTypes.Single(s => s.roomTypeID == 1).roomTypeID, status = "Empty"},
                new Room{roomName="701", roomTypeID=roomTypes.Single(s => s.roomTypeID == 1).roomTypeID, status = "Empty"},
                new Room{roomName="801", roomTypeID=roomTypes.Single(s => s.roomTypeID == 1).roomTypeID, status = "Empty"},
                new Room{roomName="901", roomTypeID=roomTypes.Single(s => s.roomTypeID == 1).roomTypeID, status = "Empty"},
                new Room{roomName="1001", roomTypeID=roomTypes.Single(s => s.roomTypeID == 1).roomTypeID, status = "Empty"},
                new Room{roomName="1101", roomTypeID=roomTypes.Single(s => s.roomTypeID == 1).roomTypeID, status = "Empty"},
                new Room{roomName="1201", roomTypeID=roomTypes.Single(s => s.roomTypeID == 1).roomTypeID, status = "Empty"},

                new Room{roomName="503", roomTypeID=roomTypes.Single(s => s.roomTypeID == 2).roomTypeID, status = "Empty"},
                new Room{roomName="603", roomTypeID=roomTypes.Single(s => s.roomTypeID == 2).roomTypeID, status = "Empty"},
                new Room{roomName="703", roomTypeID=roomTypes.Single(s => s.roomTypeID == 2).roomTypeID, status = "Empty"},

                new Room{roomName="502", roomTypeID=roomTypes.Single(s => s.roomTypeID == 3).roomTypeID, status = "Empty"},
                new Room{roomName="602", roomTypeID=roomTypes.Single(s => s.roomTypeID == 3).roomTypeID, status = "Empty"},
                new Room{roomName="702", roomTypeID=roomTypes.Single(s => s.roomTypeID == 3).roomTypeID, status = "Empty"},
                new Room{roomName="802", roomTypeID=roomTypes.Single(s => s.roomTypeID == 3).roomTypeID, status = "Empty"},
                new Room{roomName="902", roomTypeID=roomTypes.Single(s => s.roomTypeID == 3).roomTypeID, status = "Empty"},
                new Room{roomName="1002", roomTypeID=roomTypes.Single(s => s.roomTypeID == 3).roomTypeID, status = "Empty"},
                new Room{roomName="1102", roomTypeID=roomTypes.Single(s => s.roomTypeID == 3).roomTypeID, status = "Empty"},
                new Room{roomName="1202", roomTypeID=roomTypes.Single(s => s.roomTypeID == 3).roomTypeID, status = "Empty"},

                new Room{roomName="803", roomTypeID=roomTypes.Single(s => s.roomTypeID == 4).roomTypeID, status = "Empty"},
                new Room{roomName="903", roomTypeID=roomTypes.Single(s => s.roomTypeID == 4).roomTypeID, status = "Empty"},
                new Room{roomName="1003", roomTypeID=roomTypes.Single(s => s.roomTypeID == 4).roomTypeID, status = "Empty"},
                new Room{roomName="1103", roomTypeID=roomTypes.Single(s => s.roomTypeID == 4).roomTypeID, status = "Empty"},
                new Room{roomName="1203", roomTypeID=roomTypes.Single(s => s.roomTypeID == 4).roomTypeID, status = "Empty"},

                new Room{roomName="201", roomTypeID=roomTypes.Single(s => s.roomTypeID == 5).roomTypeID, status = "Empty"},
                new Room{roomName="202", roomTypeID=roomTypes.Single(s => s.roomTypeID == 5).roomTypeID, status = "Empty"},
                new Room{roomName="203", roomTypeID=roomTypes.Single(s => s.roomTypeID == 5).roomTypeID, status = "Empty"},

                new Room{roomName="504", roomTypeID=roomTypes.Single(s => s.roomTypeID == 6).roomTypeID, status = "Empty"},
                new Room{roomName="604", roomTypeID=roomTypes.Single(s => s.roomTypeID == 6).roomTypeID, status = "Empty"},
                new Room{roomName="704", roomTypeID=roomTypes.Single(s => s.roomTypeID == 6).roomTypeID, status = "Empty"},
                new Room{roomName="804", roomTypeID=roomTypes.Single(s => s.roomTypeID == 6).roomTypeID, status = "Empty"},
                new Room{roomName="904", roomTypeID=roomTypes.Single(s => s.roomTypeID == 6).roomTypeID, status = "Empty"},
                new Room{roomName="1004", roomTypeID=roomTypes.Single(s => s.roomTypeID == 6).roomTypeID, status = "Empty"},
                new Room{roomName="1104", roomTypeID=roomTypes.Single(s => s.roomTypeID == 6).roomTypeID, status = "Empty"},
                new Room{roomName="1204", roomTypeID=roomTypes.Single(s => s.roomTypeID == 6).roomTypeID, status = "Empty"},

                new Room{roomName="505", roomTypeID=roomTypes.Single(s => s.roomTypeID == 7).roomTypeID, status = "Empty"},
                new Room{roomName="605", roomTypeID=roomTypes.Single(s => s.roomTypeID == 7).roomTypeID, status = "Empty"},
                new Room{roomName="705", roomTypeID=roomTypes.Single(s => s.roomTypeID == 7).roomTypeID, status = "Empty"},
                new Room{roomName="805", roomTypeID=roomTypes.Single(s => s.roomTypeID == 7).roomTypeID, status = "Empty"},
                new Room{roomName="905", roomTypeID=roomTypes.Single(s => s.roomTypeID == 7).roomTypeID, status = "Empty"},
                new Room{roomName="1005", roomTypeID=roomTypes.Single(s => s.roomTypeID == 7).roomTypeID, status = "Empty"},
                new Room{roomName="1105", roomTypeID=roomTypes.Single(s => s.roomTypeID == 7).roomTypeID, status = "Empty"},
                new Room{roomName="1205", roomTypeID=roomTypes.Single(s => s.roomTypeID == 7).roomTypeID, status = "Empty"},

                new Room{roomName="301", roomTypeID=roomTypes.Single(s => s.roomTypeID == 8).roomTypeID, status = "Empty"},
                new Room{roomName="302", roomTypeID=roomTypes.Single(s => s.roomTypeID == 8).roomTypeID, status = "Empty"},
                new Room{roomName="303", roomTypeID=roomTypes.Single(s => s.roomTypeID == 8).roomTypeID, status = "Empty"},
                new Room{roomName="401", roomTypeID=roomTypes.Single(s => s.roomTypeID == 8).roomTypeID, status = "Empty"},
                new Room{roomName="402", roomTypeID=roomTypes.Single(s => s.roomTypeID == 8).roomTypeID, status = "Empty"},
                new Room{roomName="403", roomTypeID=roomTypes.Single(s => s.roomTypeID == 8).roomTypeID, status = "Empty"},

                new Room{roomName="Penthouse", roomTypeID=roomTypes.Single(s => s.roomTypeID == 9).roomTypeID, status = "Empty"},

                new Room{roomName="406", roomTypeID=roomTypes.Single(s => s.roomTypeID == 10).roomTypeID, status = "Empty"},
                new Room{roomName="506", roomTypeID=roomTypes.Single(s => s.roomTypeID == 10).roomTypeID, status = "Empty"},
                new Room{roomName="606", roomTypeID=roomTypes.Single(s => s.roomTypeID == 10).roomTypeID, status = "Empty"},
                new Room{roomName="706", roomTypeID=roomTypes.Single(s => s.roomTypeID == 10).roomTypeID, status = "Empty"},
                new Room{roomName="806", roomTypeID=roomTypes.Single(s => s.roomTypeID == 10).roomTypeID, status = "Empty"},
                new Room{roomName="906", roomTypeID=roomTypes.Single(s => s.roomTypeID == 10).roomTypeID, status = "Empty"},
                new Room{roomName="1006", roomTypeID=roomTypes.Single(s => s.roomTypeID == 10).roomTypeID, status = "Empty"},

                new Room{roomName="507", roomTypeID=roomTypes.Single(s => s.roomTypeID == 11).roomTypeID, status = "Empty"},
                new Room{roomName="607", roomTypeID=roomTypes.Single(s => s.roomTypeID == 11).roomTypeID, status = "Empty"},
                new Room{roomName="707", roomTypeID=roomTypes.Single(s => s.roomTypeID == 11).roomTypeID, status = "Empty"},
                new Room{roomName="807", roomTypeID=roomTypes.Single(s => s.roomTypeID == 11).roomTypeID, status = "Empty"},
                new Room{roomName="907", roomTypeID=roomTypes.Single(s => s.roomTypeID == 11).roomTypeID, status = "Empty"},
                new Room{roomName="1007", roomTypeID=roomTypes.Single(s => s.roomTypeID == 11).roomTypeID, status = "Empty"},

                new Room{roomName="708", roomTypeID=roomTypes.Single(s => s.roomTypeID == 12).roomTypeID, status = "Empty"},
                new Room{roomName="808", roomTypeID=roomTypes.Single(s => s.roomTypeID == 12).roomTypeID, status = "Empty"},
                new Room{roomName="908", roomTypeID=roomTypes.Single(s => s.roomTypeID == 12).roomTypeID, status = "Empty"},
                new Room{roomName="1008", roomTypeID=roomTypes.Single(s => s.roomTypeID == 12).roomTypeID, status = "Empty"},

                new Room{roomName="409", roomTypeID=roomTypes.Single(s => s.roomTypeID == 13).roomTypeID, status = "Empty"},
                new Room{roomName="509", roomTypeID=roomTypes.Single(s => s.roomTypeID == 13).roomTypeID, status = "Empty"},
                new Room{roomName="609", roomTypeID=roomTypes.Single(s => s.roomTypeID == 13).roomTypeID, status = "Empty"},
                new Room{roomName="709", roomTypeID=roomTypes.Single(s => s.roomTypeID == 13).roomTypeID, status = "Empty"},
                new Room{roomName="809", roomTypeID=roomTypes.Single(s => s.roomTypeID == 13).roomTypeID, status = "Empty"},
                new Room{roomName="909", roomTypeID=roomTypes.Single(s => s.roomTypeID == 13).roomTypeID, status = "Empty"},
                new Room{roomName="1009", roomTypeID=roomTypes.Single(s => s.roomTypeID == 13).roomTypeID, status = "Empty"},

                new Room{roomName="404", roomTypeID=roomTypes.Single(s => s.roomTypeID == 14).roomTypeID, status = "Empty"},
                new Room{roomName="405", roomTypeID=roomTypes.Single(s => s.roomTypeID == 14).roomTypeID, status = "Empty"},
                new Room{roomName="407", roomTypeID=roomTypes.Single(s => s.roomTypeID == 14).roomTypeID, status = "Empty"},

                new Room{roomName="408", roomTypeID=roomTypes.Single(s => s.roomTypeID == 15).roomTypeID, status = "Empty"},
                new Room{roomName="508", roomTypeID=roomTypes.Single(s => s.roomTypeID == 15).roomTypeID, status = "Empty"},

                new Room{roomName="608", roomTypeID=roomTypes.Single(s => s.roomTypeID == 16).roomTypeID, status = "Empty"},
                new Room{roomName="710", roomTypeID=roomTypes.Single(s => s.roomTypeID == 16).roomTypeID, status = "Empty"},
            };
            hotelContext.Rooms.AddRange(rooms);
            hotelContext.SaveChanges();

            var roomTypeDetails = new RoomTypeDetail[]
            {
                new RoomTypeDetail{roomTypeID = roomTypes.Single(s => s.roomTypeID == 1).roomTypeID,
                    description="With a focus on the shimmering Surfside beach and Atlantic Ocean through a full wall of windows, our oceanfront hotel and " +
                    "rooms let you enjoy the panoramic coastline not only from your furnished balcony, but also from the luxurious, spacious interior for " +
                    "true indoor-outdoor living.",
                    maxPeople=3, view="Ocean view", size="645 to 735 sq. ft. (60 to 68 m2). 5th to 12th floors", bed= "One king or two double beds, One crib"},

                new RoomTypeDetail{roomTypeID = roomTypes.Single(s => s.roomTypeID == 2).roomTypeID,
                    description="Perched above iconic Collins Avenue, our City-View Rooms allow you to enjoy streetscape views from the quiet comfort of a " +
                    "luxurious guest room filled with natural light through a full wall of windows.",
                    maxPeople=3, view="City view", size="600 sq. ft. (56 m2). 5th to 7th floors", bed="One king bed, One Rollaway or One Crib"},

                new RoomTypeDetail{roomTypeID = roomTypes.Single(s => s.roomTypeID == 3).roomTypeID,
                    description="If you expect to be spending most of your time exploring Surfside’s beautiful beach and Miami’s vibrant culture, our Surf " +
                    "Club Room is the ideal option for experiencing the luxury of Four Seasons Hotel at The Surf Club, Surfside, at maximum value.",
                    maxPeople=2, view="Surrounding grounds and courtyard", size="435 sq. ft. (40 m2). 5th to 12th floors", bed="One king bed, One crib"},

                new RoomTypeDetail{roomTypeID = roomTypes.Single(s => s.roomTypeID == 4).roomTypeID,
                    description="Located on the upper floors of the Hotel, offering a panoramic view of the city skyline through a full wall of windows, " +
                    "spacious Bay-View Rooms are a calming oasis during your stay at Surfside.",
                    maxPeople=3, view="Biscayne Bay and Downtown Miami", size="600 sq. ft. (56 m2). Hotel Tower, Floors 8–12", bed="One king bed, One crib or rollaway"},

                new RoomTypeDetail{roomTypeID = roomTypes.Single(s => s.roomTypeID == 5).roomTypeID,
                    description="With privileged beach and pool views, our Ocean Bungalows are luxurious hideaways located on the second floor of The Surf " +
                    "Club’s legendary Cabana Row. From the earliest years, original architect Russell Pancost instinctively understood that these sun sanctuaries " +
                    "would always remain the true soul of The Surf Club.",
                    maxPeople=2, view="Ocean view", size="415 sq. ft. (39 m2). 2nd floor", bed="One king bed, One crib"},

                new RoomTypeDetail{roomTypeID = roomTypes.Single(s => s.roomTypeID == 6).roomTypeID,
                    description="Take in the most stunning sights from our Ocean-View Corner Studios. Wake up to a breathtaking panoramic vista of the shimmering " +
                    "Atlantic Ocean through a full wall of windows, and in the evenings, enjoy a glass of wine on the wraparound corner balcony that looks " +
                    "out over the city skyline.",
                    maxPeople=3, view="Ocean view", size="700 sq. ft. (65 m2). 5th to 12th floors", bed="One king bed, One rollaway or one crib"},

                new RoomTypeDetail{roomTypeID = roomTypes.Single(s => s.roomTypeID == 7).roomTypeID,
                    description="Offering a panoramic view of the city skyline through a full wall of windows, spacious Bay-View Corner Studios are a calming " +
                    "oasis during your stay in Surfside.",
                    maxPeople=3, view="City view", size="735 sq. ft. (68 m2). 5th to 12th floors", bed="One king bed, One rollaway or one crib"},

                new RoomTypeDetail{roomTypeID = roomTypes.Single(s => s.roomTypeID == 8).roomTypeID,
                    description="Step out of a dedicated elevator and walk into your Oceanfront Four Bedroom Suite to enjoy indoor-outdoor living as it’s " +
                    "meant to be lived.",
                    maxPeople=9, view="Ocean and city", size="4328 sq. ft. (402 m2). 3rd and 4th floors, Hotel Residence Tower", bed="Two king beds and two queen beds, " +
                    "One rollaway or one crib upon request"},

                new RoomTypeDetail{roomTypeID = roomTypes.Single(s => s.roomTypeID == 9).roomTypeID,
                    description="Named after the yacht where Harvey Firestone and his peers discovered the land on which to build The Surf Club, the Marybelle " +
                    "is our largest, most exclusive suite. Soak in unimpeded sunset views from four bedrooms, your private rooftop pool and terrace or the " +
                    "expansive living spaces designed by Joseph Dirand.",
                    maxPeople=8, view="Ocean and city", size="7,200 sq. ft. (668 m2). Hotel Residence Tower", bed="Two king beds, one queen bed and one twin bed, " +
                    "One rollaway"},

                new RoomTypeDetail{roomTypeID = roomTypes.Single(s => s.roomTypeID == 10).roomTypeID,
                    description="Unwind in South Florida style in our Bay-View One-Bedroom Suites, where spacious accommodations and Joseph Dirand signature " +
                    "kitchens and bathrooms make it easy to feel at home during your stay in Miami.",
                    maxPeople=3, view="Bay view", size="1400 sq. ft. (130 m2). 4th to 10th floors", bed="One king bed, One rollaway or crib"},

                new RoomTypeDetail{roomTypeID = roomTypes.Single(s => s.roomTypeID == 11).roomTypeID,
                    description="For home-away-from-home comforts and stunning views of the Atlantic Ocean, our Oceanfront One Bedroom Suites are your ideal " +
                    "accommodations.",
                    maxPeople=4, view="Ocean view", size="1800 sq. ft. (167 m2). 5th to 10th floors", bed="King bed, Sofabed and crib upon request"},

                new RoomTypeDetail{roomTypeID = roomTypes.Single(s => s.roomTypeID == 12).roomTypeID,
                    description="Step out from your dedicated elevator and walk into our oceanfront two bedroom suites to enjoy indoor outdoor living as its " +
                    "meant to be lived.",
                    maxPeople=5, view="Ocean view", size="2000 sq. ft. (186 m2). 7th to 10th floors", bed="Two king beds, One rollaway or crib"},

                new RoomTypeDetail{roomTypeID = roomTypes.Single(s => s.roomTypeID == 13).roomTypeID,
                    description="Feel at home in the Miami Beaches in our spacious Bay-View Two-Bedroom suites, from the full designer kitchen to the furnished " +
                    "balcony, perfect for taking in sunset views over the bay.",
                    maxPeople=5, view="Bay view", size="1800 sq. ft. (167 m2). 4th to 10th floors", bed="One King Bed and One Queen Bed, One rollaway or crib"},

                new RoomTypeDetail{roomTypeID = roomTypes.Single(s => s.roomTypeID == 14).roomTypeID,
                    description="Enjoy the comforts of home in South Florida style with custom accommodations by Joseph Dirand, including a white marble bathroom " +
                    "and gourmet kitchenette.",
                    maxPeople=3, view="Local Town of Surfside", size="965 sq. ft. (89.65 m2). Hotel Residence Tower 4th floor", bed="One king bed, One rollaway or crib"},


                new RoomTypeDetail{roomTypeID = roomTypes.Single(s => s.roomTypeID == 15).roomTypeID,
                    description="Feel at home in these spacious two-bedroom suites overlooking the city.",
                    maxPeople=5, view="City view", size="1800 sq. ft. (167 m2). Floors 4 and 5", bed="One King Bed and One Queen Bed, One rollaway or crib"},

                new RoomTypeDetail{roomTypeID = roomTypes.Single(s => s.roomTypeID == 16).roomTypeID,
                    description="With a focus on the shimmering Atlantic Ocean through a full wall of windows, our Oceanfront Accessible Rooms let you enjoy the " +
                    "panoramic coastline not only from your furnished balcony, but also from the luxurious, spacious interior for true indoor-outdoor living.",
                    maxPeople=3, view="Ocean view", size="645 to 735 sq. ft. (60 to 68 m2) 6th to 7th floors", bed="Two double beds, One crib"},
            };
            hotelContext.RoomTypeDetails.AddRange(roomTypeDetails);
            hotelContext.SaveChanges();

            var services = new Service[]
            {
                new Service{roomHighlights="Refrigerated private bar, In-room safe, Tea/coffee maker", bedAndBath="Customizable Four Seasons Bed – with your choice of plush, " +
                "signature or firm mattress topper, Down duvets and pillows, Orthopedic and hypo-allergenic pillows, on request, Iron and ironing board, Cotton bathrobes, " +
                "Lighted make-up/shaving mirror, Hair dryer, Deluxe toiletries",
                    servicesAndAmenities="Complimentary local and international newspapers, Complimentary overnight shoeshine service, Valet parking, Twice-daily housekeeping, " +
                    "24-hour Concierge", roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 1).roomTypeDetailID},
                new Service{roomHighlights="Refrigerated private bar, In-room safe, Tea/coffee maker", bedAndBath="Customizable Four Seasons Bed – with your choice of plush, " +
                "signature or firm mattress topper, Down duvets and pillows, Orthopedic and hypo-allergenic pillows, on request, Iron and ironing board, Cotton bathrobes, " +
                "Lighted make-up/shaving mirror, Hair dryer, Deluxe toiletries",
                    servicesAndAmenities="Complimentary local and international newspapers, Complimentary overnight shoeshine service, Valet parking, Twice-daily housekeeping, " +
                    "24-hour Concierge", roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 2).roomTypeDetailID},
                new Service{roomHighlights="Refrigerated private bar, In-room safe, Tea/coffee maker", bedAndBath="Customizable Four Seasons Bed – with your choice of plush, " +
                "signature or firm mattress topper, Down duvets and pillows, Orthopedic and hypo-allergenic pillows, on request, Iron and ironing board, Cotton bathrobes, " +
                "Lighted make-up/shaving mirror, Hair dryer, Deluxe toiletries",
                    servicesAndAmenities="Complimentary local and international newspapers, Complimentary overnight shoeshine service, Valet parking, Twice-daily housekeeping, " +
                    "24-hour Concierge", roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 3).roomTypeDetailID},
                new Service{roomHighlights="Refrigerated private bar, In-room safe, Tea/coffee maker", bedAndBath="Customizable Four Seasons Bed – with your choice of plush, " +
                "signature or firm mattress topper, Down duvets and pillows, Orthopedic and hypo-allergenic pillows, on request, Iron and ironing board, Cotton bathrobes, " +
                "Lighted make-up/shaving mirror, Hair dryer, Deluxe toiletries",
                    servicesAndAmenities="Complimentary local and international newspapers, Complimentary overnight shoeshine service, Valet parking, Twice-daily housekeeping, " +
                    "24-hour Concierge", roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 4).roomTypeDetailID},
                new Service{roomHighlights="Refrigerated private bar, In-room safe, Tea/coffee maker", bedAndBath="Customizable Four Seasons Bed – with your choice of plush, " +
                "signature or firm mattress topper, Down duvets and pillows, Orthopedic and hypo-allergenic pillows, on request, Iron and ironing board, Cotton bathrobes, " +
                "Lighted make-up/shaving mirror, Hair dryer, Deluxe toiletries",
                    servicesAndAmenities="Complimentary local and international newspapers, Complimentary overnight shoeshine service, Valet parking, Twice-daily housekeeping, " +
                    "24-hour Concierge", roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 5).roomTypeDetailID},
                new Service{roomHighlights="Refrigerated private bar, In-room safe, Tea/coffee maker", bedAndBath="Customizable Four Seasons Bed – with your choice of plush, " +
                "signature or firm mattress topper, Down duvets and pillows, Orthopedic and hypo-allergenic pillows, on request, Iron and ironing board, Cotton bathrobes, " +
                "Lighted make-up/shaving mirror, Hair dryer, Deluxe toiletries",
                    servicesAndAmenities="Complimentary local and international newspapers, Complimentary overnight shoeshine service, Valet parking, Twice-daily housekeeping, " +
                    "24-hour Concierge", roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 6).roomTypeDetailID},
                new Service{roomHighlights="Refrigerated private bar, In-room safe, Tea/coffee maker", bedAndBath="Customizable Four Seasons Bed – with your choice of plush, " +
                "signature or firm mattress topper, Down duvets and pillows, Orthopedic and hypo-allergenic pillows, on request, Iron and ironing board, Cotton bathrobes, " +
                "Lighted make-up/shaving mirror, Hair dryer, Deluxe toiletries",
                    servicesAndAmenities="Complimentary local and international newspapers, Complimentary overnight shoeshine service, Valet parking, Twice-daily housekeeping, " +
                    "24-hour Concierge", roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 7).roomTypeDetailID},
                new Service{roomHighlights="Refrigerated private bar, In-room safe, Tea/coffee maker", bedAndBath="Customizable Four Seasons Bed – with your choice of plush, " +
                "signature or firm mattress topper, Down duvets and pillows, Orthopedic and hypo-allergenic pillows, on request, Iron and ironing board, Cotton bathrobes, " +
                "Lighted make-up/shaving mirror, Hair dryer, Deluxe toiletries",
                    servicesAndAmenities="Complimentary local and international newspapers, Complimentary overnight shoeshine service, Valet parking, Twice-daily housekeeping, " +
                    "24-hour Concierge", roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 8).roomTypeDetailID},
                new Service{roomHighlights="Refrigerated private bar, In-room safe, Tea/coffee maker", bedAndBath="Customizable Four Seasons Bed – with your choice of plush, " +
                "signature or firm mattress topper, Down duvets and pillows, Orthopedic and hypo-allergenic pillows, on request, Iron and ironing board, Cotton bathrobes, " +
                "Lighted make-up/shaving mirror, Hair dryer, Deluxe toiletries",
                    servicesAndAmenities="Complimentary local and international newspapers, Complimentary overnight shoeshine service, Valet parking, Twice-daily housekeeping, " +
                    "24-hour Concierge", roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 9).roomTypeDetailID},
                new Service{roomHighlights="Refrigerated private bar, In-room safe, Tea/coffee maker", bedAndBath="Customizable Four Seasons Bed – with your choice of plush, " +
                "signature or firm mattress topper, Down duvets and pillows, Orthopedic and hypo-allergenic pillows, on request, Iron and ironing board, Cotton bathrobes, " +
                "Lighted make-up/shaving mirror, Hair dryer, Deluxe toiletries",
                    servicesAndAmenities="Complimentary local and international newspapers, Complimentary overnight shoeshine service, Valet parking, Twice-daily housekeeping, " +
                    "24-hour Concierge", roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 10).roomTypeDetailID},
                new Service{roomHighlights="Refrigerated private bar, In-room safe, Tea/coffee maker", bedAndBath="Customizable Four Seasons Bed – with your choice of plush, " +
                "signature or firm mattress topper, Down duvets and pillows, Orthopedic and hypo-allergenic pillows, on request, Iron and ironing board, Cotton bathrobes, " +
                "Lighted make-up/shaving mirror, Hair dryer, Deluxe toiletries",
                    servicesAndAmenities="Complimentary local and international newspapers, Complimentary overnight shoeshine service, Valet parking, Twice-daily housekeeping, " +
                    "24-hour Concierge", roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 11).roomTypeDetailID},
                new Service{roomHighlights="Refrigerated private bar, In-room safe, Tea/coffee maker", bedAndBath="Customizable Four Seasons Bed – with your choice of plush, " +
                "signature or firm mattress topper, Down duvets and pillows, Orthopedic and hypo-allergenic pillows, on request, Iron and ironing board, Cotton bathrobes, " +
                "Lighted make-up/shaving mirror, Hair dryer, Deluxe toiletries",
                    servicesAndAmenities="Complimentary local and international newspapers, Complimentary overnight shoeshine service, Valet parking, Twice-daily housekeeping, " +
                    "24-hour Concierge", roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 12).roomTypeDetailID},
                new Service{roomHighlights="Refrigerated private bar, In-room safe, Tea/coffee maker", bedAndBath="Customizable Four Seasons Bed – with your choice of plush, " +
                "signature or firm mattress topper, Down duvets and pillows, Orthopedic and hypo-allergenic pillows, on request, Iron and ironing board, Cotton bathrobes, " +
                "Lighted make-up/shaving mirror, Hair dryer, Deluxe toiletries",
                    servicesAndAmenities="Complimentary local and international newspapers, Complimentary overnight shoeshine service, Valet parking, Twice-daily housekeeping, " +
                    "24-hour Concierge", roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 13).roomTypeDetailID},
                new Service{roomHighlights="Refrigerated private bar, In-room safe, Tea/coffee maker", bedAndBath="Customizable Four Seasons Bed – with your choice of plush, " +
                "signature or firm mattress topper, Down duvets and pillows, Orthopedic and hypo-allergenic pillows, on request, Iron and ironing board, Cotton bathrobes, " +
                "Lighted make-up/shaving mirror, Hair dryer, Deluxe toiletries",
                    servicesAndAmenities="Complimentary local and international newspapers, Complimentary overnight shoeshine service, Valet parking, Twice-daily housekeeping, " +
                    "24-hour Concierge", roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 14).roomTypeDetailID},
                new Service{roomHighlights="Refrigerated private bar, In-room safe, Tea/coffee maker", bedAndBath="Customizable Four Seasons Bed – with your choice of plush, " +
                "signature or firm mattress topper, Down duvets and pillows, Orthopedic and hypo-allergenic pillows, on request, Iron and ironing board, Cotton bathrobes, " +
                "Lighted make-up/shaving mirror, Hair dryer, Deluxe toiletries",
                    servicesAndAmenities="Complimentary local and international newspapers, Complimentary overnight shoeshine service, Valet parking, Twice-daily housekeeping, " +
                    "24-hour Concierge", roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 15).roomTypeDetailID},
                new Service{roomHighlights="Refrigerated private bar, In-room safe, Tea/coffee maker", bedAndBath="Customizable Four Seasons Bed – with your choice of plush, " +
                "signature or firm mattress topper, Down duvets and pillows, Orthopedic and hypo-allergenic pillows, on request, Iron and ironing board, Cotton bathrobes, " +
                "Lighted make-up/shaving mirror, Hair dryer, Deluxe toiletries",
                    servicesAndAmenities="Complimentary local and international newspapers, Complimentary overnight shoeshine service, Valet parking, Twice-daily housekeeping, " +
                    "24-hour Concierge", roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 16).roomTypeDetailID},
            };
            hotelContext.Services.AddRange(services);
            hotelContext.SaveChanges();

            var roomTypeImages = new RoomTypeImage[]
            {
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 1).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront room/1.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 1).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront room/2.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 1).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront room/3.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 1).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront room/4.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 1).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront room/5.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 1).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront room/6.jpg"},

                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 2).roomTypeDetailID, imagePath="/img/roomtypeimages/city view room/1.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 2).roomTypeDetailID, imagePath="/img/roomtypeimages/city view room/2.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 2).roomTypeDetailID, imagePath="/img/roomtypeimages/city view room/3.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 2).roomTypeDetailID, imagePath="/img/roomtypeimages/city view room/4.jpg"},

                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 3).roomTypeDetailID, imagePath="/img/roomtypeimages/surf club room/1.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 3).roomTypeDetailID, imagePath="/img/roomtypeimages/surf club room/2.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 3).roomTypeDetailID, imagePath="/img/roomtypeimages/surf club room/3.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 3).roomTypeDetailID, imagePath="/img/roomtypeimages/surf club room/4.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 3).roomTypeDetailID, imagePath="/img/roomtypeimages/surf club room/5.jpg"},

                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 4).roomTypeDetailID, imagePath="/img/roomtypeimages/bay-view room/1.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 4).roomTypeDetailID, imagePath="/img/roomtypeimages/bay-view room/2.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 4).roomTypeDetailID, imagePath="/img/roomtypeimages/bay-view room/3.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 4).roomTypeDetailID, imagePath="/img/roomtypeimages/bay-view room/4.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 4).roomTypeDetailID, imagePath="/img/roomtypeimages/bay-view room/5.jpg"},

                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 5).roomTypeDetailID, imagePath="/img/roomtypeimages/ocean bungalow/1.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 5).roomTypeDetailID, imagePath="/img/roomtypeimages/ocean bungalow/2.jpg"},

                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 6).roomTypeDetailID, imagePath="/img/roomtypeimages/ocean-view corner studio/1.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 6).roomTypeDetailID, imagePath="/img/roomtypeimages/ocean-view corner studio/2.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 6).roomTypeDetailID, imagePath="/img/roomtypeimages/ocean-view corner studio/3.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 6).roomTypeDetailID, imagePath="/img/roomtypeimages/ocean-view corner studio/4.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 6).roomTypeDetailID, imagePath="/img/roomtypeimages/ocean-view corner studio/5.jpg"},

                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 7).roomTypeDetailID, imagePath="/img/roomtypeimages/bay-view corner studio/1.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 7).roomTypeDetailID, imagePath="/img/roomtypeimages/bay-view corner studio/2.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 7).roomTypeDetailID, imagePath="/img/roomtypeimages/bay-view corner studio/3.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 7).roomTypeDetailID, imagePath="/img/roomtypeimages/bay-view corner studio/4.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 7).roomTypeDetailID, imagePath="/img/roomtypeimages/bay-view corner studio/5.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 7).roomTypeDetailID, imagePath="/img/roomtypeimages/bay-view corner studio/6.jpg"},

                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 8).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront four bedroom suite/1.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 8).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront four bedroom suite/2.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 8).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront four bedroom suite/3.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 8).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront four bedroom suite/4.jpg"},

                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 9).roomTypeDetailID, imagePath="/img/roomtypeimages/marybelle penthouse suite/1.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 9).roomTypeDetailID, imagePath="/img/roomtypeimages/marybelle penthouse suite/2.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 9).roomTypeDetailID, imagePath="/img/roomtypeimages/marybelle penthouse suite/3.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 9).roomTypeDetailID, imagePath="/img/roomtypeimages/marybelle penthouse suite/4.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 9).roomTypeDetailID, imagePath="/img/roomtypeimages/marybelle penthouse suite/5.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 9).roomTypeDetailID, imagePath="/img/roomtypeimages/marybelle penthouse suite/6.jpg"},

                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 10).roomTypeDetailID, imagePath="/img/roomtypeimages/bay-view one bedroom suite/1.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 10).roomTypeDetailID, imagePath="/img/roomtypeimages/bay-view one bedroom suite/2.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 10).roomTypeDetailID, imagePath="/img/roomtypeimages/bay-view one bedroom suite/3.jpg"},

                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 11).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront one bedroom suite/1.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 11).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront one bedroom suite/2.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 11).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront one bedroom suite/3.jpg"},

                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 12).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront two bedroom suite/1.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 12).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront two bedroom suite/2.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 12).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront two bedroom suite/3.jpg"},

                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 13).roomTypeDetailID, imagePath="/img/roomtypeimages/bay-view two bedroom suite/1.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 13).roomTypeDetailID, imagePath="/img/roomtypeimages/bay-view two bedroom suite/2.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 13).roomTypeDetailID, imagePath="/img/roomtypeimages/bay-view two bedroom suite/3.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 13).roomTypeDetailID, imagePath="/img/roomtypeimages/bay-view two bedroom suite/4.jpg"},

                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 14).roomTypeDetailID, imagePath="/img/roomtypeimages/surf club one bedroom suite/1.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 14).roomTypeDetailID, imagePath="/img/roomtypeimages/surf club one bedroom suite/2.jpg"},

                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 15).roomTypeDetailID, imagePath="/img/roomtypeimages/city-view two bedroom suite/1.jpg"},

                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 16).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront accessible room/1.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 16).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront accessible room/2.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 16).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront accessible room/3.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 16).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront accessible room/4.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 16).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront accessible room/5.jpg"},
                new RoomTypeImage{roomTypeDetailID= roomTypeDetails.Single(s => s.roomTypeDetailID == 16).roomTypeDetailID, imagePath="/img/roomtypeimages/oceanfront accessible room/6.jpg"},

            };
            hotelContext.roomTypeImages.AddRange(roomTypeImages);
            hotelContext.SaveChanges();

            var accounts = new Account[]
            {
                new Account{username = "pmq0511", password ="123123", name = "Phạm Minh Quang", phoneNumber= "0346991600", role=1 },
                new Account { username = "user123", password = "123123", name = "Lê Văn Ngọ", phoneNumber = "0909379473", role=0 },
                new Account{username = "user234", password ="123456", name= "Võ Văn Minh", phoneNumber= "0948393843", role=0 }
            };
            hotelContext.Accounts.AddRange(accounts);
            hotelContext.SaveChanges();
            var requests = new Request[]
            {

                new Request {accountID= accounts.Single(s =>s.accountID ==1).accountID, dateCheckIn=DateTime.Parse("2024-05-15"),dateCheckOut=DateTime.Parse("2024-05-17"),roomTypeID=1,guestCount=3, message="OK", status="Waiting" },
                new Request {accountID= accounts.Single(s =>s.accountID ==2).accountID, dateCheckIn=DateTime.Parse("2024-03-12"),dateCheckOut=DateTime.Parse("2024-03-17"),roomTypeID=2,guestCount=2, message="Verry Good", status="Waiting" },
                new Request {accountID= accounts.Single(s =>s.accountID ==3).accountID, dateCheckIn=DateTime.Parse("2024-04-14"),dateCheckOut=DateTime.Parse("2024-04-17"),roomTypeID=3,guestCount=4, message="Good Good Good", status="Waiting" },

            };
            hotelContext.Requests.AddRange(requests);
            hotelContext.SaveChanges();

            var enrolllments = new Enrollment[]
            {
                new Enrollment {roomID = rooms.Single(s => s.roomID== 1).roomID, accountID = accounts.Single(a => a.accountID ==1).accountID, dateOfReceipt=DateTime.Parse("2024-05-15")},
                new Enrollment {roomID = rooms.Single(s => s.roomID== 2).roomID, accountID = accounts.Single(a => a.accountID ==2).accountID, dateOfReceipt=DateTime.Parse("2024-03-12")},
                new Enrollment {roomID = rooms.Single(s => s.roomID== 3).roomID, accountID = accounts.Single(a => a.accountID ==3).accountID, dateOfReceipt=DateTime.Parse("2024-04-04")}
            };
            hotelContext.Enrollments.AddRange(enrolllments);
            hotelContext.SaveChanges();

            var comboSales = new ComboSale[]
            {
                new ComboSale{title="IM OFFER UP TO 45%",genre="Friendly Service",ShortContent="EARLY BIRD OFFER - BOOK 2 WEEKS EARLY, ALMOST HALF PRICE OFF\r\nDiscount up to 45% when booking 14 day",
                    content="No matter what, why not book early to secure a summer trip with up to 45% better prices?\r\nDon't miss the opportunity to enjoy a vacation at one of the most attractive beaches in Vietnam at an ideal summer price, only when booking 14 days in advance, and comes with many exclusive perks:\r\n✨ 10% discount on F&B services at the hotel restaurant (not applicable to mini bar)\r\n✨ 10% off spa and karaoke services\r\n✨ Free some fun activities at Kid's club\r\n✨ Free early check-in/late check-out, room upgrade (only for guests booking via website/livechat)\r\n\r\n⏰ Booking: From now - August 5, 2024\r\n⏰ Stay: June 21 - August 17, 2024\r\n\r\nBook earlier, travel cheaper! Contact Hotline FLC Hotels & Resorts immediately or leave a comment for detailed promotion advice.",
                    image="7e5eb548-db09-4c57-a989-11cbac29cb3a_about-3.jpg", price=45},
                new ComboSale{title="WHEN THE SUMMER IS FAR FAR, OUR HOME IS MORE CLOSE" ,genre="Friendly Service",ShortContent="At FLC Quy Nhon and FLC City Hotel Beach Quy Nhon" ,
                    content="Choose Quy Nhon destination this summer, let FLC Hotels & Resorts accompany you to make your vacation in a far away place come true. Multi-experience comes from a rich system of amenities suitable for the whole family, combined with preferential prices, making traveling has never been easier.\r\n\r\nFrom only 1,150,000 VND/guest, you and your family members will receive package benefits including:\r\n- 01 standard night stay, including breakfast buffet\r\n- 01 lunch at the hotel restaurant\r\n- 50% discount on karaoke services\r\n- Free use of swimming pool and other facilities at the complex\r\nIn particular, customers staying 02 nights or more will be able to choose 01 of the following 02 incentives:\r\n(1) 01 complimentary airport pick-up/drop-off\r\n(2) Free of charge for 01 child from 6-11 years old (no extra bed) (*)\r\nBooking and stay period: From now - August 17, 2024\r\nSummer vacation has begun, hurry up to capture the most beautiful travel time of the year! Contact FLC Hotels & Resorts for advice on a family trip.",
                    image="84d2bf1e-b6cd-412d-8d30-5a07eac4ab5d_about-1.jpg", price=1150000},
                new ComboSale{title="DISCOVER THE ESSENCE OF INDIAN CUISINE AT Hotel IM" ,genre="Get Breakfast",ShortContent="Good news! IM is the first hotel in the country to offer a new Indian culinary experience.",
                    content="‍Directly managed by a talented and experienced Indian Chef, FLC Ha Long promises to make visitors \"oh wow\" when enjoying unique dishes with irresistible flavors. \r\n\r\nFrom scrumptious curries served with naan or fragrant basmati rice, to crispy, fatty samosas; Surely you will be amazed and delighted with the dishes that explode your taste buds when you come here.\r\n\r\nDon't hesitate any longer, contact us immediately to explore and add color to your culinary experience journey!\r\n\r\nLocation: Bamboo Restaurant - M Floor, FLC Ha Long Hotel\r\n⏰ Time: 6:30 p.m. - 9:30 p.m., every Tuesday - Sunday",
                    image="b5f5e1ec-01b2-4d9c-b6f5-63ad4f0e268b_menu-6.jpg", price=399},
                new ComboSale{title="Premium Skincare Staycation Package" ,genre="Suits & SPA",ShortContent="Rejuvenate your skin and elevate your vacation with the Premium Skincare Staycation Package",
                    content="‍- Complimentary premium Sulwhasoo Premium trial set for each stay\r\n- Enjoy free breakfast at The Canvas restaurant, for 02 guests\r\n- Early check-in at 11:00 am and late check-out at 4:00 pm (subject to availability)\r\n- Preferential price to Executive Lounge 800,000++ VND for 02 guests and upgrade to River View room (subject to availability)\r\n- 20% discount for treatments at Legend Healing Spa",
                    image="456d1221-a004-4d6a-967a-002cba8fea74_ce8c095ae04fbb8dba30da9c61437a07.jpg", price=3560000},
                new ComboSale{title="Premium Suite" ,genre="Cozy Rooms",ShortContent="Enjoy private room space and Club Lounge Access privileges.",
                    content="‍Experience the ultimate in luxury and comfort in our Premium Suite, designed to deliver unparalleled levels of luxury and sophistication. This spacious suite features luxurious interiors, combining modern design with classic touches to create an inviting and stylish atmosphere.\r\n- Free one night use of Deluxe Suite room.\r\n- Free buffet breakfast at the restaurant.\r\n- Complimentary use of Club Lounge Access all day including all day portion.\r\n- Free 01 one-way pick-up at the airport or hotel (destinations within the city) during your stay.\r\n- Free non-alcoholic drinks at the bar during your stay.\r\n- Complimentary bottle of sparkling wine with strawberries covered in chocolate sauce served in the room.\r\n- Free late check-out until 18:00.\r\n- 10% discount on food and beverage services (except Crystal Jade Palace Restaurant).\r\n- 20% off spa services.",
                    image="a23caef1-0b98-4444-bfd6-588764c7af11_4265-14-1920-room-LTHO.jpg.thumb.1920.1920.jpg", price=3500000},
                new ComboSale{title="FREE SHUTTLE SERVICE" ,genre="Transfer Services",ShortContent="Hotel shuttle schedule: - From hotel to city center",
                    content="‍🌟 Hotel shuttle schedule:\r\n\r\n– From IM hotel to the city center (Toa Kham wharf – 49 Le Loi – Hue City): 08:30 am and 03:00 pm –\r\n\r\nFrom Hue City Center (Toa Kham Pier - 49 Le Loi - Hue City) to La Vela Hue Hotel: 11:30 am and 08:30 pm\r\n\r\nPlease note that seats are subject to availability and should be booked in advance with our Customer services team.\r\n\r\n(This free shuttle is available to guests after checking in at the hotel)",
                    image="36a710a6-ded3-4e67-a9d6-eb2cd21db9f5_lorenzo-hamers-uRFPwY2iogw-unsplash.jpg", price=1000000},

            };
            hotelContext.ComboSales.AddRange(comboSales);
            hotelContext.SaveChanges();

            var menus = new Menu[]
            {
                new Menu{dishName="Cake Socola", dishDescription="A small river named Duden flows by their place and supplies",dishPrice= 50 , dishImage="/images/Menu/menu-1.jpg"},
                new Menu{dishName="Pearl fresh cream", dishDescription="A delightful dessert combining smooth fresh cream with chewy tapioca pearls, offering a creamy and satisfying treat.",dishPrice= 50 , dishImage="/images/Menu/menu-2.jpg"},
                new Menu{dishName="BBQ grilled beef", dishDescription="Tender beef marinated in a savory BBQ sauce, grilled to perfection, delivering a smoky and flavorful experience.",dishPrice= 50 , dishImage="/images/Menu/menu-4.jpg"},
                new Menu{dishName="The grilled fresh butter", dishDescription="A succulent dish featuring tender grilled butter, seasoned to perfection for a rich, indulgent flavor.",dishPrice= 50 , dishImage="/images/Menu/menu-5.jpg"},
                new Menu{dishName="Seafood fried rice", dishDescription="A savory medley of tender seafood, fragrant rice, and crisp vegetables, expertly stir-fried to perfection.",dishPrice= 50 , dishImage="/images/Menu/menu-6.jpg"},
                new Menu{dishName="Tropical tea", dishDescription="A refreshing blend of exotic fruits and aromatic tea leaves, delivering a taste of the tropics with every sip.",dishPrice= 50 , dishImage="/images/Menu/menu-7.jpg"},
                new Menu{dishName="Four seasons ice cream", dishDescription="Indulge in a delightful blend of four distinct flavors, offering a taste of all seasons in every spoonful.",dishPrice= 50 , dishImage="/images/Menu/menu-8.jpg"},
                new Menu{dishName="Fried chicken with fish saucen", dishDescription="Fried chicken with fish sauce, a savory delight that tantalizes the taste buds.",dishPrice= 50 , dishImage="/images/Menu/images.jpg"}

            };
            hotelContext.Menus.AddRange(menus);
            hotelContext.SaveChanges();
        }
    }
}
