using RealEstate.Application.UsecCases.Property.Commands.UpdateProperty;

namespace RealEstate.Tests.ObjectMothers.Responses
{
    public static class UpdatePropertyResponseMother
    {
        /// <summary>
        /// Crea una respuesta exitosa básica con datos válidos
        /// </summary>
        public static UpdatePropertyResponse Valid() => new()
        {
            PropertyId = 1,
            Name = "Modern Downtown Apartment",
            Address = "123 Main Street, Miami, FL 33101",
            Price = 750000.00m,
            CodeInternal = "MIA001",
            Year = 2020,
            OwnerName = "John Smith",
            UpdatedAt = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc),
            PriceChanged = false
        };

        /// <summary>
        /// Crea una respuesta con cambio de precio
        /// </summary>
        public static UpdatePropertyResponse WithPriceChange()
        {
            var response = Valid();
            response.PriceChanged = true;
            response.Price = 850000.00m;
            response.UpdatedAt = new DateTime(2024, 1, 15, 10, 1, 0, DateTimeKind.Utc);
            return response;
        }

        /// <summary>
        /// Crea una respuesta con precio específico
        /// </summary>
        public static UpdatePropertyResponse WithPrice(decimal price)
        {
            var response = Valid();
            response.Price = price;
            response.PriceChanged = price != 750000.00m;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con ID específico
        /// </summary>
        public static UpdatePropertyResponse WithId(int propertyId)
        {
            var response = Valid();
            response.PropertyId = propertyId;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con nombre específico
        /// </summary>
        public static UpdatePropertyResponse WithName(string name)
        {
            var response = Valid();
            response.Name = name;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con dirección específica
        /// </summary>
        public static UpdatePropertyResponse WithAddress(string address)
        {
            var response = Valid();
            response.Address = address;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con código interno específico
        /// </summary>
        public static UpdatePropertyResponse WithCodeInternal(string codeInternal)
        {
            var response = Valid();
            response.CodeInternal = codeInternal;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con año específico
        /// </summary>
        public static UpdatePropertyResponse WithYear(int year)
        {
            var response = Valid();
            response.Year = year;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con nombre de propietario específico
        /// </summary>
        public static UpdatePropertyResponse WithOwnerName(string ownerName)
        {
            var response = Valid();
            response.OwnerName = ownerName;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con fecha de actualización específica
        /// </summary>
        public static UpdatePropertyResponse WithUpdatedAt(DateTime updatedAt)
        {
            var response = Valid();
            response.UpdatedAt = updatedAt;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con todos los datos personalizados
        /// </summary>
        public static UpdatePropertyResponse WithCustomData(
            int propertyId,
            string name,
            string address,
            decimal price,
            string codeInternal,
            int year,
            string ownerName,
            bool priceChanged = false) => new()
            {
                PropertyId = propertyId,
                Name = name,
                Address = address,
                Price = price,
                CodeInternal = codeInternal,
                Year = year,
                OwnerName = ownerName,
                UpdatedAt = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc),
                PriceChanged = priceChanged
            };

        /// <summary>
        /// Crea una respuesta para testing de propiedades de lujo
        /// </summary>
        public static UpdatePropertyResponse LuxuryProperty() => new()
        {
            PropertyId = 999,
            Name = "Luxury Penthouse Updated",
            Address = "999 Premium Avenue, Miami Beach, FL 33139",
            Price = 2500000.00m,
            CodeInternal = "LUX999",
            Year = 2021,
            OwnerName = "Premium Owner",
            PriceChanged = true,
            UpdatedAt = new DateTime(2024, 1, 15, 11, 0, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea una respuesta para testing de propiedades económicas
        /// </summary>
        public static UpdatePropertyResponse BudgetProperty() => new()
        {
            PropertyId = 100,
            Name = "Cozy Apartment Updated",
            Address = "100 Budget Street, Orlando, FL 32801",
            Price = 150000.00m,
            CodeInternal = "BUD100",
            Year = 2010,
            OwnerName = "Budget Owner",
            PriceChanged = false,
            UpdatedAt = new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea múltiples respuestas para testing de listas
        /// </summary>
        public static List<UpdatePropertyResponse> Multiple(int count = 3)
        {
            var responses = new List<UpdatePropertyResponse>();
            var baseDateTime = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc);

            for (int i = 1; i <= count; i++)
            {
                responses.Add(new UpdatePropertyResponse
                {
                    PropertyId = i,
                    Name = $"Property {i}",
                    Address = "123 Main Street, Miami, FL 33101",
                    Price = 750000.00m + (i * 50000),
                    CodeInternal = $"TEST{i:D3}",
                    Year = 2020,
                    OwnerName = "John Smith",
                    PriceChanged = i % 2 == 0,
                    UpdatedAt = baseDateTime.AddMinutes(i * 10)
                });
            }

            return responses;
        }

        /// <summary>
        /// Crea una respuesta con datos mínimos requeridos
        /// </summary>
        public static UpdatePropertyResponse Minimal() => new()
        {
            PropertyId = 1,
            Name = "Test Property",
            Address = "Test Address",
            Price = 100000m,
            CodeInternal = "TEST001",
            Year = 2020,
            OwnerName = "Test Owner",
            UpdatedAt = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc),
            PriceChanged = false
        };
    }
}