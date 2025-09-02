using RealEstate.Application.UsecCases.Property.Queries.GetPropertiesWithFilters;

namespace RealEstate.Tests.ObjectMothers.Responses
{
    public static class GetPropertiesWithFiltersResponseMother
    {
        /// <summary>
        /// Crea una respuesta exitosa básica
        /// </summary>
        public static GetPropertiesWithFiltersResponse Valid() => new()
        {
            PropertyId = 1,
            Name = "Modern Downtown Apartment",
            Address = "123 Main Street, Miami, FL 33101",
            Price = 750000.00m,
            CodeInternal = "MIA001",
            Year = 2020,
            OwnerName = "John Smith",
            ImageUrls = new List<string>
            {
                "https://teststorage.blob.core.windows.net/property-images/12345678-9abc-def0-1234-56789abcdef0_exterior.jpg",
                "https://teststorage.blob.core.windows.net/property-images/87654321-cdef-0123-4567-89abcdef0123_living-room.jpg"
            },
            CreatedAt = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea una respuesta con ID específico
        /// </summary>
        public static GetPropertiesWithFiltersResponse WithId(int propertyId)
        {
            var response = Valid();
            response.PropertyId = propertyId;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con nombre específico
        /// </summary>
        public static GetPropertiesWithFiltersResponse WithName(string name)
        {
            var response = Valid();
            response.Name = name;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con dirección específica
        /// </summary>
        public static GetPropertiesWithFiltersResponse WithAddress(string address)
        {
            var response = Valid();
            response.Address = address;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con precio específico
        /// </summary>
        public static GetPropertiesWithFiltersResponse WithPrice(decimal price)
        {
            var response = Valid();
            response.Price = price;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con código interno específico
        /// </summary>
        public static GetPropertiesWithFiltersResponse WithCodeInternal(string codeInternal)
        {
            var response = Valid();
            response.CodeInternal = codeInternal;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con año específico
        /// </summary>
        public static GetPropertiesWithFiltersResponse WithYear(int year)
        {
            var response = Valid();
            response.Year = year;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con nombre de propietario específico
        /// </summary>
        public static GetPropertiesWithFiltersResponse WithOwnerName(string ownerName)
        {
            var response = Valid();
            response.OwnerName = ownerName;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con URLs de imagen específicas
        /// </summary>
        public static GetPropertiesWithFiltersResponse WithImageUrls(IList<string> imageUrls)
        {
            var response = Valid();
            response.ImageUrls = imageUrls;
            return response;
        }

        /// <summary>
        /// Crea una respuesta sin imágenes
        /// </summary>
        public static GetPropertiesWithFiltersResponse WithoutImages()
        {
            var response = Valid();
            response.ImageUrls = new List<string>();
            return response;
        }

        /// <summary>
        /// Crea una respuesta con una sola imagen
        /// </summary>
        public static GetPropertiesWithFiltersResponse WithSingleImage()
        {
            var response = Valid();
            response.ImageUrls = new List<string>
            {
                "https://teststorage.blob.core.windows.net/property-images/single-12345678-9abc-def0-1234-56789abcdef0_property.jpg"
            };
            return response;
        }

        /// <summary>
        /// Crea una respuesta con fecha de creación específica
        /// </summary>
        public static GetPropertiesWithFiltersResponse WithCreatedAt(DateTime createdAt)
        {
            var response = Valid();
            response.CreatedAt = createdAt;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con datos personalizados completos
        /// </summary>
        public static GetPropertiesWithFiltersResponse WithCustomData(
            int propertyId,
            string name,
            string address,
            decimal price,
            string codeInternal,
            int year,
            string ownerName,
            IList<string>? imageUrls = null,
            DateTime? createdAt = null) => new()
            {
                PropertyId = propertyId,
                Name = name,
                Address = address,
                Price = price,
                CodeInternal = codeInternal,
                Year = year,
                OwnerName = ownerName,
                ImageUrls = imageUrls ?? new List<string>(),
                CreatedAt = createdAt ?? new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc)
            };

        /// <summary>
        /// Crea una respuesta para propiedad de lujo
        /// </summary>
        public static GetPropertiesWithFiltersResponse LuxuryProperty() => new()
        {
            PropertyId = 999,
            Name = "Luxury Penthouse Suite",
            Address = "999 Premium Avenue, Miami Beach, FL 33139",
            Price = 2500000.00m,
            CodeInternal = "LUX999",
            Year = 2022,
            OwnerName = "Premium Owner LLC",
            ImageUrls = new List<string>
            {
                "https://teststorage.blob.core.windows.net/property-images/luxury-exterior-12345678.jpg",
                "https://teststorage.blob.core.windows.net/property-images/luxury-living-87654321.jpg",
                "https://teststorage.blob.core.windows.net/property-images/luxury-kitchen-11111111.jpg",
                "https://teststorage.blob.core.windows.net/property-images/luxury-bedroom-22222222.jpg",
                "https://teststorage.blob.core.windows.net/property-images/luxury-view-33333333.jpg"
            },
            CreatedAt = new DateTime(2024, 1, 10, 15, 0, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea una respuesta para propiedad económica
        /// </summary>
        public static GetPropertiesWithFiltersResponse BudgetProperty() => new()
        {
            PropertyId = 100,
            Name = "Cozy Starter Home",
            Address = "100 Budget Street, Orlando, FL 32801",
            Price = 150000.00m,
            CodeInternal = "BUD100",
            Year = 2010,
            OwnerName = "Budget Owner",
            ImageUrls = new List<string>
            {
                "https://teststorage.blob.core.windows.net/property-images/budget-exterior-aaaaaaaa.jpg",
                "https://teststorage.blob.core.windows.net/property-images/budget-interior-bbbbbbbb.jpg"
            },
            CreatedAt = new DateTime(2024, 1, 5, 9, 0, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea una respuesta para propiedad vintage/histórica
        /// </summary>
        public static GetPropertiesWithFiltersResponse VintageProperty() => new()
        {
            PropertyId = 500,
            Name = "Historic Colonial Mansion",
            Address = "500 Heritage Lane, Key West, FL 33040",
            Price = 1800000.00m,
            CodeInternal = "VIN500",
            Year = 1920,
            OwnerName = "Heritage Preservation Trust",
            ImageUrls = new List<string>
            {
                "https://teststorage.blob.core.windows.net/property-images/vintage-facade-cccccccc.jpg",
                "https://teststorage.blob.core.windows.net/property-images/vintage-interior-dddddddd.jpg",
                "https://teststorage.blob.core.windows.net/property-images/vintage-details-eeeeeeee.jpg"
            },
            CreatedAt = new DateTime(2023, 12, 20, 14, 30, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea una respuesta para propiedad moderna
        /// </summary>
        public static GetPropertiesWithFiltersResponse ModernProperty() => new()
        {
            PropertyId = 300,
            Name = "Ultra-Modern Smart Home",
            Address = "300 Tech Boulevard, Tampa, FL 33602",
            Price = 1200000.00m,
            CodeInternal = "MOD300",
            Year = 2023,
            OwnerName = "Tech Innovations Inc",
            ImageUrls = new List<string>
            {
                "https://teststorage.blob.core.windows.net/property-images/modern-exterior-ffffffff.jpg",
                "https://teststorage.blob.core.windows.net/property-images/modern-kitchen-gggggggg.jpg",
                "https://teststorage.blob.core.windows.net/property-images/modern-living-hhhhhhhh.jpg",
                "https://teststorage.blob.core.windows.net/property-images/modern-tech-iiiiiiii.jpg"
            },
            CreatedAt = new DateTime(2024, 1, 20, 11, 45, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea múltiples respuestas para testing de listas
        /// </summary>
        public static List<GetPropertiesWithFiltersResponse> Multiple(int count = 3)
        {
            var responses = new List<GetPropertiesWithFiltersResponse>();
            var baseDateTime = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc);
            var basePrice = 500000.00m;

            for (int i = 1; i <= count; i++)
            {
                responses.Add(new GetPropertiesWithFiltersResponse
                {
                    PropertyId = i,
                    Name = $"Property {i}",
                    Address = $"{i * 100} Test Street, Miami, FL 3310{i}",
                    Price = basePrice + (i * 100000),
                    CodeInternal = $"TEST{i:D3}",
                    Year = 2020 + i,
                    OwnerName = $"Owner {i}",
                    ImageUrls = new List<string>
                    {
                        $"https://teststorage.blob.core.windows.net/property-images/prop{i}-exterior.jpg",
                        $"https://teststorage.blob.core.windows.net/property-images/prop{i}-interior.jpg"
                    },
                    CreatedAt = baseDateTime.AddDays(i)
                });
            }

            return responses;
        }

        /// <summary>
        /// Crea una lista diversa con diferentes tipos de propiedades
        /// </summary>
        public static List<GetPropertiesWithFiltersResponse> DiverseProperties()
        {
            return new List<GetPropertiesWithFiltersResponse>
            {
                BudgetProperty(),
                Valid(), // Propiedad promedio
                ModernProperty(),
                VintageProperty(),
                LuxuryProperty()
            };
        }

        /// <summary>
        /// Crea propiedades filtradas por rango de precio
        /// </summary>
        public static List<GetPropertiesWithFiltersResponse> ByPriceRange(decimal minPrice, decimal maxPrice, int count = 3)
        {
            var responses = new List<GetPropertiesWithFiltersResponse>();
            var priceIncrement = (maxPrice - minPrice) / count;
            var baseDateTime = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc);

            for (int i = 1; i <= count; i++)
            {
                var price = minPrice + (priceIncrement * i);
                responses.Add(new GetPropertiesWithFiltersResponse
                {
                    PropertyId = i,
                    Name = $"Property in Range {i}",
                    Address = $"{i * 100} Price Range Street, Miami, FL 3310{i}",
                    Price = Math.Round(price, 2),
                    CodeInternal = $"RANGE{i:D3}",
                    Year = 2020,
                    OwnerName = $"Range Owner {i}",
                    ImageUrls = new List<string>
                    {
                        $"https://teststorage.blob.core.windows.net/property-images/range{i}-exterior.jpg"
                    },
                    CreatedAt = baseDateTime.AddHours(i)
                });
            }

            return responses;
        }

        /// <summary>
        /// Crea propiedades filtradas por año de construcción
        /// </summary>
        public static List<GetPropertiesWithFiltersResponse> ByYear(int year, int count = 3)
        {
            var responses = new List<GetPropertiesWithFiltersResponse>();
            var baseDateTime = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc);

            for (int i = 1; i <= count; i++)
            {
                responses.Add(new GetPropertiesWithFiltersResponse
                {
                    PropertyId = i,
                    Name = $"Property Built in {year} #{i}",
                    Address = $"{i * 100} Year {year} Street, Miami, FL 3310{i}",
                    Price = 500000.00m + (i * 50000),
                    CodeInternal = $"Y{year}{i:D2}",
                    Year = year,
                    OwnerName = $"Year {year} Owner {i}",
                    ImageUrls = new List<string>
                    {
                        $"https://teststorage.blob.core.windows.net/property-images/year{year}-prop{i}.jpg"
                    },
                    CreatedAt = baseDateTime.AddMinutes(i * 30)
                });
            }

            return responses;
        }

        /// <summary>
        /// Crea propiedades de un propietario específico
        /// </summary>
        public static List<GetPropertiesWithFiltersResponse> ByOwner(string ownerName, int count = 3)
        {
            var responses = new List<GetPropertiesWithFiltersResponse>();
            var baseDateTime = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc);

            for (int i = 1; i <= count; i++)
            {
                responses.Add(new GetPropertiesWithFiltersResponse
                {
                    PropertyId = i,
                    Name = $"{ownerName}'s Property {i}",
                    Address = $"{i * 100} Owner Street, Miami, FL 3310{i}",
                    Price = 600000.00m + (i * 75000),
                    CodeInternal = $"OWN{i:D3}",
                    Year = 2018 + i,
                    OwnerName = ownerName,
                    ImageUrls = new List<string>
                    {
                        $"https://teststorage.blob.core.windows.net/property-images/owner-prop{i}.jpg"
                    },
                    CreatedAt = baseDateTime.AddDays(i * 7)
                });
            }

            return responses;
        }

        /// <summary>
        /// Crea una lista vacía (para testing de resultados sin datos)
        /// </summary>
        public static List<GetPropertiesWithFiltersResponse> Empty()
        {
            return new List<GetPropertiesWithFiltersResponse>();
        }

        /// <summary>
        /// Crea respuesta con datos mínimos requeridos
        /// </summary>
        public static GetPropertiesWithFiltersResponse Minimal() => new()
        {
            PropertyId = 1,
            Name = "Test Property",
            Address = "Test Address",
            Price = 100000m,
            CodeInternal = "TEST001",
            Year = 2020,
            OwnerName = "Test Owner",
            ImageUrls = new List<string>(),
            CreatedAt = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea propiedades para testing de paginación
        /// </summary>
        public static List<GetPropertiesWithFiltersResponse> ForPagination(int page, int pageSize)
        {
            var responses = new List<GetPropertiesWithFiltersResponse>();
            var startId = (page - 1) * pageSize + 1;
            var baseDateTime = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc);

            for (int i = 0; i < pageSize; i++)
            {
                var id = startId + i;
                responses.Add(new GetPropertiesWithFiltersResponse
                {
                    PropertyId = id,
                    Name = $"Paginated Property {id}",
                    Address = $"{id * 100} Page Street, Miami, FL 3310{page}",
                    Price = 400000.00m + (id * 25000),
                    CodeInternal = $"PAGE{id:D3}",
                    Year = 2020,
                    OwnerName = $"Page {page} Owner",
                    ImageUrls = new List<string>
                    {
                        $"https://teststorage.blob.core.windows.net/property-images/page{page}-prop{id}.jpg"
                    },
                    CreatedAt = baseDateTime.AddHours(id)
                });
            }

            return responses;
        }
    }
}