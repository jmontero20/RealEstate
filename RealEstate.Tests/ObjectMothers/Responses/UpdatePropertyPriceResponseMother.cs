using RealEstate.Application.UsecCases.Property.Commands.UpdatePropertyPrice;

namespace RealEstate.Tests.ObjectMothers.Responses
{
    public static class UpdatePropertyPriceResponseMother
    {
        /// <summary>
        /// Crea una respuesta exitosa básica con cambio de precio
        /// </summary>
        public static UpdatePropertyPriceResponse Valid() => new()
        {
            PropertyId = 1,
            OldPrice = 750000.00m,
            NewPrice = 850000.00m,
            UpdatedAt = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea una respuesta con ID de propiedad específico
        /// </summary>
        public static UpdatePropertyPriceResponse WithPropertyId(int propertyId)
        {
            var response = Valid();
            response.PropertyId = propertyId;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con precio anterior específico
        /// </summary>
        public static UpdatePropertyPriceResponse WithOldPrice(decimal oldPrice)
        {
            var response = Valid();
            response.OldPrice = oldPrice;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con precio nuevo específico
        /// </summary>
        public static UpdatePropertyPriceResponse WithNewPrice(decimal newPrice)
        {
            var response = Valid();
            response.NewPrice = newPrice;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con precios específicos
        /// </summary>
        public static UpdatePropertyPriceResponse WithPrices(decimal oldPrice, decimal newPrice)
        {
            var response = Valid();
            response.OldPrice = oldPrice;
            response.NewPrice = newPrice;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con fecha de actualización específica
        /// </summary>
        public static UpdatePropertyPriceResponse WithUpdatedAt(DateTime updatedAt)
        {
            var response = Valid();
            response.UpdatedAt = updatedAt;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con todos los datos personalizados
        /// </summary>
        public static UpdatePropertyPriceResponse WithCustomData(
            int propertyId,
            decimal oldPrice,
            decimal newPrice,
            DateTime? updatedAt = null) => new()
            {
                PropertyId = propertyId,
                OldPrice = oldPrice,
                NewPrice = newPrice,
                UpdatedAt = updatedAt ?? new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc)
            };

        /// <summary>
        /// Crea una respuesta para aumento de precio (más caro)
        /// </summary>
        public static UpdatePropertyPriceResponse PriceIncrease() => new()
        {
            PropertyId = 1,
            OldPrice = 500000.00m,
            NewPrice = 650000.00m, // Aumento de $150,000
            UpdatedAt = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea una respuesta para reducción de precio (más barato)
        /// </summary>
        public static UpdatePropertyPriceResponse PriceDecrease() => new()
        {
            PropertyId = 2,
            OldPrice = 800000.00m,
            NewPrice = 720000.00m, // Reducción de $80,000
            UpdatedAt = new DateTime(2024, 1, 15, 11, 0, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea una respuesta para aumento significativo de precio (lujo)
        /// </summary>
        public static UpdatePropertyPriceResponse LuxuryPriceIncrease() => new()
        {
            PropertyId = 999,
            OldPrice = 2000000.00m,
            NewPrice = 2500000.00m, // Aumento de $500,000
            UpdatedAt = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea una respuesta para ajuste menor de precio
        /// </summary>
        public static UpdatePropertyPriceResponse MinorPriceAdjustment() => new()
        {
            PropertyId = 100,
            OldPrice = 350000.00m,
            NewPrice = 365000.00m, // Aumento de $15,000
            UpdatedAt = new DateTime(2024, 1, 15, 9, 30, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea una respuesta para corrección de precio (vuelta al precio original)
        /// </summary>
        public static UpdatePropertyPriceResponse PriceCorrection() => new()
        {
            PropertyId = 50,
            OldPrice = 1200000.00m,
            NewPrice = 1000000.00m, // Corrección hacia abajo
            UpdatedAt = new DateTime(2024, 1, 15, 14, 0, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea múltiples respuestas de cambios de precio para testing
        /// </summary>
        public static List<UpdatePropertyPriceResponse> Multiple(int count = 3)
        {
            var responses = new List<UpdatePropertyPriceResponse>();
            var baseDateTime = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc);
            var baseOldPrice = 500000.00m;

            for (int i = 1; i <= count; i++)
            {
                var oldPrice = baseOldPrice + (i * 100000);
                var priceChange = (i % 2 == 0) ? 50000 : -25000; // Alternar entre aumento y reducción

                responses.Add(new UpdatePropertyPriceResponse
                {
                    PropertyId = i,
                    OldPrice = oldPrice,
                    NewPrice = oldPrice + priceChange,
                    UpdatedAt = baseDateTime.AddMinutes(i * 15)
                });
            }

            return responses;
        }

        /// <summary>
        /// Crea una respuesta con cambio de precio reciente (útil para testing de timestamps)
        /// </summary>
        public static UpdatePropertyPriceResponse RecentPriceChange()
        {
            var response = Valid();
            response.UpdatedAt = DateTime.UtcNow.AddMinutes(-1);
            return response;
        }

        /// <summary>
        /// Crea una respuesta con cambio de precio mínimo
        /// </summary>
        public static UpdatePropertyPriceResponse MinimalPriceChange() => new()
        {
            PropertyId = 1,
            OldPrice = 100000m,
            NewPrice = 101000m, // Cambio mínimo de $1,000
            UpdatedAt = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea una respuesta con cambio de precio máximo permitido
        /// </summary>
        public static UpdatePropertyPriceResponse MaximalPriceChange() => new()
        {
            PropertyId = 1,
            OldPrice = 500000m,
            NewPrice = 999999999.99m, // Precio máximo del sistema
            UpdatedAt = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea respuestas que simulan historial de cambios de precio de una propiedad
        /// </summary>
        public static List<UpdatePropertyPriceResponse> PriceHistory(int propertyId, int changes = 3)
        {
            var responses = new List<UpdatePropertyPriceResponse>();
            var baseDateTime = new DateTime(2024, 1, 1, 10, 0, 0, DateTimeKind.Utc);
            var currentPrice = 500000.00m;

            for (int i = 0; i < changes; i++)
            {
                var oldPrice = currentPrice;
                currentPrice += (i % 2 == 0 ? 25000 : -15000); // Alternar cambios

                responses.Add(new UpdatePropertyPriceResponse
                {
                    PropertyId = propertyId,
                    OldPrice = oldPrice,
                    NewPrice = currentPrice,
                    UpdatedAt = baseDateTime.AddDays(i * 30) // Un cambio por mes
                });
            }

            return responses;
        }
    }
}