using System.ComponentModel;

namespace PFM_API.Models
{
    public enum MCCEnum
    {
        //Enumeration mcc_code are standardized 4 digit numbers used by the payments industry where this code classifies a particular business by market segment.
        //index starts with 1
        [Description("Telecommunication service including local and long distance calls, credit card calls, calls through use of magneticstrip reading telephones and fax services")]
        Code1 = 4814,
        [Description("VisaPhone")]
        Code2 = 4815,
        [Description("Telegraph services")]
        Code3 = 4821,
        [Description("Money Orders - Wire Transfer")]
        Code4 = 4829,
        [Description("Cable and other pay television (previously Cable Services)")]
        Code5 = 4899,
        [Description("Electric, Gas, Sanitary and Water Utilities")]
        Code6 = 4900,
        [Description("Motor vehicle supplies and new parts")]
        Code7 = 5013,
        [Description("Office and Commercial Furniture")]
        Code8 = 5021,
        [Description("Construction Materials, Not Elsewhere Classified")]
        Code9 = 5039,
        [Description("Office, Photographic, Photocopy, and Microfilm Equipment")]
        Code10 = 5044,
        [Description("Computers, Computer Peripheral Equipment, Software")]
        Code11 = 5045,
        [Description("Commercial Equipment, Not Elsewhere Classified")]
        Code12 = 5046,
        [Description("Medical, Dental Ophthalmic, Hospital Equipment and Supplies")]
        Code13 = 5047,
        [Description("Metal Service Centers and Offices")]
        Code14 = 5051,
        [Description("Electrical Parts and Equipment")]
        Code15 = 5065,
        [Description("Hardware Equipment and Supplies")]
        Code16 = 5072,
        [Description("Plumbing and Heating Equipment and Supplies")]
        Code17 = 5074,
        [Description("Industrial Supplies, Not Elsewhere Classified")]
        Code18 = 5085,
        [Description("Precious Stones and Metals, Watches and Jewelry")]
        Code19 = 5094,
        [Description("Durable Goods, Not Elsewhere Classified")]
        Code20 = 5099,
        [Description("Stationery, Office Supplies, Printing, and Writing Paper")]
        Code21 = 5111,
        [Description("Drugs, Drug Proprietors, and Druggist’s Sundries")]
        Code22 = 5122,
        [Description("Piece Goods, Notions, and Other Dry Goods")]
        Code23 = 5131,
        [Description("Men’s Women’s and Children’s Uniforms and Commercial Clothing")]
        Code24 = 5137,
        [Description("Commercial Footwear")]
        Code25 = 5139,
        [Description("Chemicals and Allied Products, Not Elsewhere Classified")]
        Code26 = 5169,
        [Description("Petroleum and Petroleum Products")]
        Code27 = 5172,
        [Description("Books, Periodicals, and Newspapers")]
        Code28 = 5192,
        [Description("Florists’ Supplies, Nursery Stock and Flowers")]
        Code29 = 5193,
        [Description("Paints, Varnishes, and Supplies")]
        Code30 = 5198,
        [Description("Non-durable Goods, Not Elsewhere Classified")]
        Code31 = 5199,
        [Description("Home Supply Warehouse Stores")]
        Code32 = 5200,
        [Description("Lumber and Building Materials Stores")]
        Code33 = 5211,
        [Description("Glass Stores")]
        Code34 = 5231,
        [Description("Paint and Wallpaper Stores")]
        Code35 = 5231,
        [Description("Wallpaper Stores")]
        Code36 = 5231,
        [Description("Hardware Stores")]
        Code37 = 5251,
        [Description("Nurseries – Lawn and Garden Supply Store")]
        Code38 = 5261,
        [Description("Mobile Home Dealers")]
        Code39 = 5271,
        [Description("Wholesale Clubs")]
        Code40 = 5300,
        [Description("Duty Free Store")]
        Code41 = 5309,
        [Description("Discount Stores")]
        Code42 = 5310,
        [Description("Department Stores")]
        Code43 = 5311,
        [Description("Variety Stores")]
        Code44 = 5331,
        [Description("Misc. General Merchandise")]
        Code45 = 5399,
        [Description("Grocery Stores")]
        Code46 = 5411,
        [Description("Supermarkets")]
        Code47 = 5411,
        [Description("Freezer and Locker Meat Provisioners")]
        Code48 = 5422,
        [Description("Meat Provisioners – Freezer and Locker")]
        Code49 = 5422,
        [Description("Candy Stores")]
        Code50 = 5441,
        [Description("Confectionery Stores")]
        Code51 = 5441,
        [Description("Nut Stores")]
        Code52 = 5441,
        [Description("Dairy Products Stores")]
        Code53 = 5451,
        [Description("Bakeries")]
        Code54 = 5462,
        [Description("Misc. Food Stores – Convenience Stores and Specialty Markets")]
        Code55 = 5499,
        [Description("Car and Truck Dealers (New and Used) Sales, Service, Repairs, Parts, and Leasing")]
        Code56 = 5511,
        [Description("Automobile and Truck Dealers (Used Only)")]
        Code57 = 5521,
        [Description("Automobile Supply Stores")]
        Code58 = 5531,
        [Description("Automotive Tire Stores")]
        Code59 = 5532,
        [Description("Automotive Parts, Accessories Stores")]
        Code60 = 5533,
        [Description("Service Stations (with or without ancillary services)")]
        Code61 = 5541,
        [Description("Automated Fuel Dispensers")]
        Code62 = 5542,
        [Description("Boat Dealers")]
        Code63 = 5551,
        [Description("Recreational and Utility Trailers, Camp Dealers")]
        Code64 = 5561,
        [Description("Motorcycle Dealers")]
        Code65 = 5571,
        [Description("Motor Home Dealers")]
        Code66 = 5592,
        [Description("Snowmobile Dealers")]
        Code67 = 5598,
        [Description("Men’s and Boy’s Clothing and Accessories Stores")]
        Code68 = 5611,
        [Description("Women’s Ready - to - Wear Stores")]
        Code69 = 5621,
        [Description("Women’s Accessory and Specialty Shops")]
        Code70 = 5631,
        [Description("Children’s and Infant’s Wear Stores")]
        Code71 = 5641,
        [Description("Family Clothing Stores")]
        Code72 = 5651,
        [Description("Sports Apparel, Riding Apparel Stores")]
        Code73 = 5655,
        [Description("Shoe Stores")]
        Code74 = 5661,
        [Description("Furriers and Fur Shops")]
        Code75 = 5681,
        [Description("Men’s and Women’s Clothing Stores")]
        Code76 = 5691,
        [Description("Tailors, Seamstress, Mending, and Alterations")]
        Code77 = 5697,
        [Description("Wig and Toupee Stores")]
        Code78 = 5698,
        [Description("Miscellaneous Apparel and Accessory Shops")]
        Code79 = 5699,
        [Description("Furniture, Home Furnishings, and Equipment Stores, Except Appliances")]
        Code80 = 5712,
        [Description("Floor Covering Stores")]
        Code81 = 5713,
        [Description("Drapery, Window Covering and Upholstery Stores")]
        Code82 = 5714,
        [Description("Fireplace, Fireplace Screens, and Accessories Stores")]
        Code83 = 5718,
        [Description("Miscellaneous Home Furnishing Specialty Stores")]
        Code84 = 5719,
        [Description("Household Appliance Stores")]
        Code85 = 5722,
        [Description("Electronic Sales")]
        Code86 = 5732,
        [Description("Music Stores, Musical Instruments, Piano Sheet Music")]
        Code87 = 5733,
        [Description("Computer Software Stores")]
        Code88 = 5734,
        [Description("Record Shops")]
        Code89 = 5735,
        [Description("Caterers")]
        Code90 = 5811,
        [Description("Eating places and Restaurants")]
        Code91 = 5812,
        [Description("Drinking Places (Alcoholic Beverages), Bars, Taverns, Cocktail lounges, Nightclubs and Discotheques")]
        Code92 = 5813,
        [Description("Fast Food Restaurants")]
        Code93 = 5814,
        [Description("Drug Stores and Pharmacies")]
        Code94 = 5912,
        [Description("Package Stores - Beer, Wine, and Liquor")]
        Code95 = 5921,
        [Description("Used Merchandise and Secondhand Stores")]
        Code96 = 5931,
        [Description("Antique Shops - Sales, Repairs, and Restoration Services")]
        Code97 = 5832,
        [Description("Pawn Shops and Salvage Yards")]
        Code98 = 5933,
        [Description("Wrecking and Salvage Yards")]
        Code99 = 5935,
        [Description("Antique Reproductions")]
        Code100 = 5937,
        [Description("Bicycle Shops - Sales and Service")]
        Code101 = 5940,
        [Description("Sporting Goods Stores")]
        Code102 = 5941,
        [Description("Book Stores")]
        Code103 = 5942,
        [Description("Stationery Stores, Office and School Supply Stores")]
        Code104 = 5943,
        [Description("Watch, Clock, Jewelry, and Silverware Stores")]
        Code105 = 5944,
        [Description("Hobby, Toy, and Game Shops")]
        Code106 = 5945,
        [Description("Camera and Photographic Supply Stores")]
        Code107 = 5946,
        [Description("Card Shops, Gift, Novelty, and Souvenir Shops")]
        Code108 = 5947,
        [Description("Leather Foods Stores")]
        Code109 = 5948,
        [Description("Sewing, Needle, Fabric, and Price Goods Stores")]
        Code110 = 5949,
        [Description("Glassware/Crystal Stores")]
        Code111 = 5950,
        [Description("Direct Marketing - Insurance Service")]
        Code112 = 5960,
        [Description("Mail Order Houses Including Catalog Order Stores, Book/Record Clubs (No longer permitted for .S. original presentments)")]
        Code113 = 5961,
        [Description("Direct Marketing - Travel Related Arrangements Services")]
        Code114 = 5962,
        [Description("Door - to - Door Sales")]
        Code115 = 5963,
        [Description("Direct Marketing - Catalog Merchant")]
        Code116 = 5964,
        [Description("Direct Marketing - Catalog and Catalog and Retail Merchant Direct Marketing - Outbound Telemarketing Merchant")]
        Code117 = 5965,
        [Description("Direct Marketing -Inbound Teleservices Merchant")]
        Code118 = 5967,
        [Description("Direct Marketing - Continuity/Subscription Merchant")]
        Code119 = 5968,
        [Description("Direct Marketing - Not Elsewhere Classified")]
        Code120 = 5969,
        [Description("Artist’s Supply and Craft Shops")]
        Code121 = 5970,
        [Description("Art Dealers and Galleries")]
        Code122 = 5971,
        [Description("Stamp and Coin Stores - Philatelic and Numismatic Supplies")]
        Code123 = 5972,
        [Description("Religious Goods Stores")]
        Code124 = 5973,
        [Description("Hearing Aids - Sales, Service, and Supply Stores")]
        Code125 = 5975,
        [Description("Orthopedic Goods Prosthetic Devices")]
        Code126 = 5976,
        [Description("Cosmetic Stores")]
        Code127 = 5977,
        [Description("Typewriter Stores - Sales, Rental, Service")]
        Code128 = 5978,
        [Description("Fuel - Fuel Oil, Wood, Coal, Liquefied Petroleum")]
        Code129 = 5983,
        [Description("Florists")]
        Code130 = 5992,
        [Description("Cigar Stores and Stands")]
        Code131 = 5993,
        [Description("News Dealers and Newsstands")]
        Code132 = 5994,
        [Description("Pet Shops, Pet Foods, and Supplies Stores")]
        Code133 = 5995,
        [Description("Swimming Pools - Sales, Service, and Supplies")]
        Code134 = 5996,
        [Description("Electric Razor Stores - Sales and Service")]
        Code135 = 5997,
        [Description("Tent and Awning Shops")]
        Code136 = 5998,
        [Description("Miscellaneous and Specialty Retail Stores")]
        Code137 = 5999,
        [Description("Financial Institutions - Manual Cash Disbursements")]
        Code138 = 6010,
        [Description("Financial Institutions - Manual Cash Disbursements")]
        Code139 = 6011,
        [Description("Financial Institutions - Merchandise and Services")]
        Code140 = 6012,
        [Description("Non - Financial Institutions - Foreign Currency, Money Orders (not wire transfer) and Travelers Cheques")]
        Code141 = 6051,
        [Description("Security Brokers/Dealers")]
        Code142 = 6211,
        [Description("Insurance Sales, Underwriting, and Premiums")]
        Code143 = 6300,
        [Description("Insurance Premiums, (no longer valid for first presentment work)")]
        Code144 = 6381,
        [Description("Insurance, Not Elsewhere Classified (no longer valid for first presentment work)")]
        Code145 = 6399,
        [Description("Lodging - Hotels, Motels, Resorts, Central Reservation Services (not elsewhere classified)")]
        Code146 = 7011,
        [Description("Timeshares")]
        Code147 = 7012,
        [Description("Sporting and Recreational Camps")]
        Code148 = 7032,
        [Description("Trailer Parks and Camp Grounds")]
        Code149 = 7033,
        [Description("Laundry, Cleaning, and Garment Services")]
        Code150 = 7210,
        [Description("Laundry - Family and Commercial")]
        Code151 = 7211,
        [Description("Dry Cleaners")]
        Code152 = 7216,
        [Description("Carpet and Upholstery Cleaning")]
        Code153 = 7217,
        [Description("Photographic Studios")]
        Code154 = 7221,
        [Description("Barber and Beauty Shops")]
        Code155 = 7230,
        [Description("Shop Repair Shops and Shoe Shine Parlors, and Hat Cleaning Shops")]
        Code156 = 7251,
        [Description("Funeral Service and Crematories")]
        Code157 = 7261,
        [Description("Dating and Escort Services")]
        Code158 = 7273,
        [Description("Tax Preparation Service")]
        Code159 = 7276,
        [Description("Counseling Service - Debt, Marriage, Personal")]
        Code160 = 7277,
        [Description("Buying/Shopping Services, Clubs")]
        Code161 = 7278,
        [Description("Clothing Rental - Costumes, Formal Wear, Uniforms")]
        Code162 = 7296,
        [Description("Massage Parlors")]
        Code163 = 7297,
        [Description("Health and Beauty Shops")]
        Code164 = 7298,
        [Description("Miscellaneous Personal Services (not elsewhere classifies)")]
        Code165 = 7299,
        [Description("Advertising Services")]
        Code166 = 7311,
        [Description("Consumer Credit Reporting Agencies")]
        Code167 = 7321,
        [Description("Blueprinting and Photocopying Services")]
        Code168 = 7332,
        [Description("Commercial Photography, Art and Graphics")]
        Code169 = 7333,
        [Description("Quick Copy, Reproduction and Blueprinting Services")]
        Code170 = 7338,
        [Description("Stenographic and Secretarial Support Services")]
        Code171 = 7339,
        [Description("Disinfecting Services")]
        Code172 = 7342,
        [Description("Exterminating and Disinfecting Services")]
        Code173 = 7342,
        [Description("Cleaning and Maintenance, Janitorial Services")]
        Code174 = 7349,
        [Description("Employment Agencies, Temporary Help Services")]
        Code175 = 7361,
        [Description("Computer Programming, Integrated Systems Design and Data Processing Services")]
        Code176 = 7372,
        [Description("Information Retrieval Services")]
        Code177 = 7375,
        [Description("Computer Maintenance and Repair Services, Not Elsewhere Classified")]
        Code178 = 7379,
        [Description("Management, Consulting, and Public Relations Services")]
        Code179 = 7392,
        [Description("Protective and Security Services - Including Armored Cars and Guard Dogs")]
        Code180 = 7393,
        [Description("Equipment Rental and Leasing Services, Tool Rental, Furniture Rental, and Appliance Rental")]
        Code181 = 7394,
        [Description("Photofinishing Laboratories, Photo Developing")]
        Code182 = 7395,
        [Description("Business Services, Not Elsewhere Classified")]
        Code183 = 7399,
        [Description("Car Rental Companies (Not Listed Below)")]
        Code184 = 7512,
        [Description("Truck and Utility Trailer Rentals")]
        Code185 = 7513,
        [Description("Motor Home and Recreational Vehicle Rentals")]
        Code186 = 7519,
        [Description("Automobile Parking Lots and Garages")]
        Code187 = 7523,
        [Description("Automotive Body Repair Shops")]
        Code188 = 7531,
        [Description("Tire Re - treading and Repair Shops")]
        Code189 = 7534,
        [Description("Paint Shops - Automotive")]
        Code190 = 7535,
        [Description("Automotive Service Shops")]
        Code191 = 7538,
        [Description("Car Washes")]
        Code192 = 7542,
        [Description("Towing Services")]
        Code193 = 7549,
        [Description("Radio Repair Shops")]
        Code194 = 7622,
        [Description("Air Conditioning and Refrigeration Repair Shops")]
        Code195 = 7623,
        [Description("Electrical And Small Appliance Repair Shops")]
        Code196 = 7629,
        [Description("Watch, Clock, and Jewelry Repair")]
        Code197 = 7631,
        [Description("Furniture, Furniture Repair, and Furniture Refinishing")]
        Code198 = 7641,
        [Description("Welding Repair")]
        Code199 = 7692,
        [Description("Repair Shops and Related Services - Miscellaneous")]
        Code200 = 7699,
        [Description("Motion Pictures and Video Tape Production and Distribution")]
        Code201 = 7829,
        [Description("Motion Picture Theaters")]
        Code202 = 7832,
        [Description("Video Tape Rental Stores")]
        Code203 = 7841,
        [Description("Dance Halls, Studios and Schools")]
        Code204 = 7911,
        [Description("Theatrical Producers (Except Motion Pictures), Ticket Agencies")]
        Code205 = 7922,
        [Description("Bands. Orchestras, and Miscellaneous Entertainers (Not Elsewhere Classified)")]
        Code206 = 7929,
        [Description("Billiard and Pool Establishments")]
        Code207 = 7932,
        [Description("Bowling Alleys")]
        Code208 = 7933,
        [Description("Commercial Sports, Athletic Fields, Professional Sport Clubs, and Sport Promoters")]
        Code209 = 7941,
        [Description("Tourist Attractions and Exhibits")]
        Code210 = 7991,
        [Description("Golf Courses - Public")]
        Code211 = 7992,
        [Description("Video Amusement Game Supplies")]
        Code212 = 7993,
        [Description("Video Game Arcades/Establishments")]
        Code213 = 7994,
        [Description("Betting (including Lottery Tickets, Casino Gaming Chips, Off - track Betting and Wagers)")]
        Code214 = 7995,
        [Description("Amusement Parks, Carnivals, Circuses, Fortune Tellers")]
        Code215 = 7996,
        [Description("Membership Clubs (Sports, Recreation, Athletic), Country Clubs, and Private Golf Courses")]
        Code216 = 7997,
        [Description("Aquariums, Sea - aquariums, Dolphinariums")]
        Code217 = 7998,
        [Description("Recreation Services (Not Elsewhere Classified)")]
        Code218 = 7999,
        [Description("Doctors and Physicians (Not Elsewhere Classified)")]
        Code219 = 8011,
        [Description("Dentists and Orthodontists")]
        Code220 = 8021,
        [Description("Osteopaths")]
        Code221 = 8031,
        [Description("Chiropractors")]
        Code222 = 8041,
        [Description("Optometrists and Ophthalmologists")]
        Code223 = 8042,
        [Description("Opticians, Opticians Goods and Eyeglasses")]
        Code224 = 8043,
        [Description("Opticians, Optical Goods, and Eyeglasses (no longer valid for first presentments)")]
        Code225 = 8044,
        [Description("Podiatrists and Chiropodists")]
        Code226 = 8049,
        [Description("Nursing and Personal Care Facilities")]
        Code227 = 8050,
        [Description("Hospitals")]
        Code228 = 8062,
        [Description("Medical and Dental Laboratories")]
        Code229 = 8071,
        [Description("Medical Services and Health Practitioners (Not Elsewhere Classified)")]
        Code230 = 8099,
        [Description("Legal Services and Attorneys")]
        Code231 = 8111,
        [Description("Elementary and Secondary Schools")]
        Code232 = 8211,
        [Description("Colleges, Junior Colleges, Universities, and Professional Schools")]
        Code233 = 8220,
        [Description("Correspondence Schools")]
        Code234 = 8241,
        [Description("Business and Secretarial Schools")]
        Code235 = 8244,
        [Description("Vocational Schools and Trade Schools")]
        Code236 = 8249,
        [Description("Schools and Educational Services (Not Elsewhere Classified)")]
        Code237 = 8299,
        [Description("Child Care Services")]
        Code238 = 8351,
        [Description("Charitable and Social Service Organizations")]
        Code239 = 8398,
        [Description("Civic, Fraternal, and Social Associations")]
        Code240 = 8641,
        [Description("Political Organizations")]
        Code241 = 8651,
        [Description("Religious Organizations")]
        Code242 = 8661,
        [Description("Automobile Associations")]
        Code243 = 8675,
        [Description("Membership Organizations (Not Elsewhere Classified)")]
        Code244 = 8699,
        [Description("Testing Laboratories (non - medical)")]
        Code245 = 8734,
        [Description("Architectural - Engineering and Surveying Services")]
        Code246 = 8911,
        [Description("Accounting, Auditing, and Bookkeeping Services")]
        Code247 = 8931,
        [Description("Professional Services (Not Elsewhere Defined)")]
        Code248 = 8999,
        [Description("Court Costs, including Alimony and Child Support")]
        Code249 = 9211,
        [Description("Fines")]
        Code250 = 9222,
        [Description("Bail and Bond Payments")]
        Code251 = 9223,
        [Description("Tax Payments")]
        Code252 = 9311,
        [Description("Government Services (Not Elsewhere Classified)")]
        Code253 = 9399,
        [Description("Postal Services - Government Only")]
        Code254 = 9402,
        [Description("Intra - Government Transactions")]
        Code255 = 9405,
        [Description("Automated Referral Service (For Visa Only)")]
        Code256 = 9700,
        [Description("Visa Credential Service (For Visa Only)")]
        Code257 = 9701,
        [Description("GCAS Emergency Services (For Visa Only)")]
        Code258 = 9702,
        [Description("Intra - Company Purchases (For Visa Only)")]
        Code259 = 9950,
    }
}