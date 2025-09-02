using RealEstate.Application.UsecCases.Property.Commands.UpdatePropertyPrice;

namespace RealEstate.Tests.ObjectMothers.Commands
{
    public static class UpdatePropertyPriceCommandMother
    {
        /// <summary>
        /// Crea un comando válido básico para actualizar precio de propiedad
        /// </summary>
        public static UpdatePropertyPriceCommand Valid() => new(
            PropertyId: 1,
            NewPrice: 850000.00m
        );

        /// <summary>
        /// Crea un comando con ID de propiedad específico
        /// </summary>
        public static UpdatePropertyPriceCommand WithPropertyId(int propertyId) => new(
            PropertyId: propertyId,
            NewPrice: 850000.00m
        );

        /// <summary>
        /// Crea un comando con precio específico
        /// </summary>
        public static UpdatePropertyPriceCommand WithNewPrice(decimal newPrice) => new(
            PropertyId: 1,
            NewPrice: newPrice
        );

        /// <summary>
        /// Crea un comando con todos los datos personalizados
        /// </summary>
        public static UpdatePropertyPriceCommand WithCustomData(
            int propertyId,
            decimal newPrice) => new(
            PropertyId: propertyId,
            NewPrice: newPrice
        );

        /// <summary>
        /// Crea un comando para aumento significativo de precio
        /// </summary>
        public static UpdatePropertyPriceCommand SignificantIncrease() => new(
            PropertyId: 1,
            NewPrice: 1200000.00m // Aumento significativo
        );

        /// <summary>
        /// Crea un comando para aumento menor de precio
        /// </summary>
        public static UpdatePropertyPriceCommand MinorIncrease() => new(
            PropertyId: 1,
            NewPrice: 775000.00m // Aumento de $25K
        );

        /// <summary>
        /// Crea un comando para reducción significativa de precio
        /// </summary>
        public static UpdatePropertyPriceCommand SignificantDecrease() => new(
            PropertyId: 1,
            NewPrice: 500000.00m // Reducción significativa
        );

        /// <summary>
        /// Crea un comando para reducción menor de precio
        /// </summary>
        public static UpdatePropertyPriceCommand MinorDecrease() => new(
            PropertyId: 1,
            NewPrice: 725000.00m // Reducción de $25K
        );

        /// <summary>
        /// Crea un comando para precio de propiedad de lujo
        /// </summary>
        public static UpdatePropertyPriceCommand ToLuxuryPrice() => new(
            PropertyId: 1,
            NewPrice: 2500000.00m // Precio de lujo
        );

        /// <summary>
        /// Crea un comando para precio de propiedad económica
        /// </summary>
        public static UpdatePropertyPriceCommand ToBudgetPrice() => new(
            PropertyId: 1,
            NewPrice: 200000.00m // Precio económico
        );

        /// <summary>
        /// Crea un comando para ajuste de mercado (aumento típico)
        /// </summary>
        public static UpdatePropertyPriceCommand MarketAdjustmentUp() => new(
            PropertyId: 1,
            NewPrice: 800000.00m // Ajuste de mercado hacia arriba
        );

        /// <summary>
        /// Crea un comando para ajuste de mercado (reducción típica)
        /// </summary>
        public static UpdatePropertyPriceCommand MarketAdjustmentDown() => new(
            PropertyId: 1,
            NewPrice: 700000.00m // Ajuste de mercado hacia abajo
        );

        /// <summary>
        /// Crea un comando para corrección de precio (vuelta a precio original)
        /// </summary>
        public static UpdatePropertyPriceCommand PriceCorrection() => new(
            PropertyId: 1,
            NewPrice: 750000.00m // Precio original/correcto
        );

        /// <summary>
        /// Crea un comando para precio máximo del sistema
        /// </summary>
        public static UpdatePropertyPriceCommand MaximumPrice() => new(
            PropertyId: 1,
            NewPrice: 999999999.99m // Precio máximo permitido
        );

        /// <summary>
        /// Crea un comando para precio mínimo válido
        /// </summary>
        public static UpdatePropertyPriceCommand MinimumPrice() => new(
            PropertyId: 1,
            NewPrice: 1.00m // Precio mínimo válido
        );

        /// <summary>
        /// Crea múltiples comandos para testing en lote
        /// </summary>
        public static List<UpdatePropertyPriceCommand> Multiple(int count = 3)
        {
            var commands = new List<UpdatePropertyPriceCommand>();
            var basePrice = 600000.00m;

            for (int i = 1; i <= count; i++)
            {
                var priceChange = (i % 2 == 0) ? 50000 : -25000; // Alternar entre aumento y reducción
                commands.Add(new UpdatePropertyPriceCommand(
                    PropertyId: i,
                    NewPrice: basePrice + (i * 100000) + priceChange
                ));
            }

            return commands;
        }

        /// <summary>
        /// Crea una secuencia de cambios de precio para una propiedad (historial)
        /// </summary>
        public static List<UpdatePropertyPriceCommand> PriceHistory(int propertyId, int changes = 5)
        {
            var commands = new List<UpdatePropertyPriceCommand>();
            var currentPrice = 500000.00m;

            for (int i = 0; i < changes; i++)
            {
                // Simular fluctuaciones de mercado
                var priceChange = (i % 3) switch
                {
                    0 => 25000m,  // Aumento
                    1 => -15000m, // Reducción
                    _ => 10000m   // Aumento menor
                };

                currentPrice += priceChange;
                commands.Add(new UpdatePropertyPriceCommand(
                    PropertyId: propertyId,
                    NewPrice: currentPrice
                ));
            }

            return commands;
        }

        /// <summary>
        /// Crea comandos para diferentes rangos de precio
        /// </summary>
        public static List<UpdatePropertyPriceCommand> PriceRanges(int propertyId = 1)
        {
            return new List<UpdatePropertyPriceCommand>
            {
                new(PropertyId: propertyId, NewPrice: 150000.00m),   // Budget
                new(PropertyId: propertyId, NewPrice: 350000.00m),   // Entry-level
                new(PropertyId: propertyId, NewPrice: 750000.00m),   // Mid-range
                new(PropertyId: propertyId, NewPrice: 1500000.00m),  // High-end
                new(PropertyId: propertyId, NewPrice: 3000000.00m)   // Luxury
            };
        }

        /// <summary>
        /// Crea un comando con datos mínimos requeridos
        /// </summary>
        public static UpdatePropertyPriceCommand Minimal() => new(
            PropertyId: 1,
            NewPrice: 100000.00m
        );

        // ============================================
        // COMANDOS PARA TESTING DE VALIDACIÓN
        // ============================================

        /// <summary>
        /// Crea un comando con ID de propiedad cero (para testing de validación)
        /// </summary>
        public static UpdatePropertyPriceCommand WithZeroPropertyId() => new(
            PropertyId: 0,
            NewPrice: 850000.00m
        );

        /// <summary>
        /// Crea un comando con ID de propiedad negativo (para testing de validación)
        /// </summary>
        public static UpdatePropertyPriceCommand WithNegativePropertyId() => new(
            PropertyId: -1,
            NewPrice: 850000.00m
        );

        /// <summary>
        /// Crea un comando con ID de propiedad inexistente (para testing de validación)
        /// </summary>
        public static UpdatePropertyPriceCommand WithNonExistentPropertyId() => new(
            PropertyId: 99999,
            NewPrice: 850000.00m
        );

        /// <summary>
        /// Crea un comando con precio cero (para testing de validación)
        /// </summary>
        public static UpdatePropertyPriceCommand WithZeroPrice() => new(
            PropertyId: 1,
            NewPrice: 0.00m
        );

        /// <summary>
        /// Crea un comando con precio negativo (para testing de validación)
        /// </summary>
        public static UpdatePropertyPriceCommand WithNegativePrice() => new(
            PropertyId: 1,
            NewPrice: -100000.00m
        );

        /// <summary>
        /// Crea un comando con precio excesivo (para testing de validación)
        /// </summary>
        public static UpdatePropertyPriceCommand WithExcessivePrice() => new(
            PropertyId: 1,
            NewPrice: 1000000000.00m // Excede el límite de 999,999,999.99
        );

        /// <summary>
        /// Crea un comando con precio con demasiados decimales (para testing de validación)
        /// </summary>
        public static UpdatePropertyPriceCommand WithTooManyDecimals() => new(
            PropertyId: 1,
            NewPrice: 850000.123456m // Más de 2 decimales
        );

        /// <summary>
        /// Crea comandos para testing de casos límite de precio
        /// </summary>
        public static List<UpdatePropertyPriceCommand> PriceBoundaryTests()
        {
            return new List<UpdatePropertyPriceCommand>
            {
                // Casos válidos en los límites
                new(PropertyId: 1, NewPrice: 0.01m),                // Justo arriba de cero
                new(PropertyId: 1, NewPrice: 999999999.99m),        // Precio máximo válido
                
                // Casos inválidos
                WithZeroPrice(),                                     // Exactamente cero
                WithNegativePrice(),                                 // Negativo
                WithExcessivePrice()                                 // Excede límite
            };
        }

        /// <summary>
        /// Crea comandos para testing de diferentes propiedades
        /// </summary>
        public static List<UpdatePropertyPriceCommand> MultipleProperties()
        {
            return new List<UpdatePropertyPriceCommand>
            {
                new(PropertyId: 1, NewPrice: 850000.00m),   // Propiedad 1
                new(PropertyId: 2, NewPrice: 650000.00m),   // Propiedad 2
                new(PropertyId: 3, NewPrice: 1200000.00m),  // Propiedad 3
                new(PropertyId: 4, NewPrice: 300000.00m),   // Propiedad 4
                new(PropertyId: 5, NewPrice: 2500000.00m)   // Propiedad 5
            };
        }

        /// <summary>
        /// Crea comandos para testing de fluctuaciones de mercado
        /// </summary>
        public static List<UpdatePropertyPriceCommand> MarketFluctuations(int propertyId = 1)
        {
            return new List<UpdatePropertyPriceCommand>
            {
                new(PropertyId: propertyId, NewPrice: 750000.00m),  // Precio base
                new(PropertyId: propertyId, NewPrice: 780000.00m),  // Aumento 4%
                new(PropertyId: propertyId, NewPrice: 760000.00m),  // Reducción leve
                new(PropertyId: propertyId, NewPrice: 800000.00m),  // Recuperación
                new(PropertyId: propertyId, NewPrice: 740000.00m),  // Corrección
                new(PropertyId: propertyId, NewPrice: 820000.00m)   // Nuevo máximo
            };
        }

        /// <summary>
        /// Crea un comando para testing de transacciones (usado en handler tests)
        /// </summary>
        public static UpdatePropertyPriceCommand ForTransactionTest() => new(
            PropertyId: 1,
            NewPrice: 900000.00m // Precio que activará creación de PropertyTrace
        );

        /// <summary>
        /// Crea comandos para testing de concurrencia (misma propiedad, diferentes precios)
        /// </summary>
        public static List<UpdatePropertyPriceCommand> ConcurrencyTest(int propertyId = 1)
        {
            return new List<UpdatePropertyPriceCommand>
            {
                new(PropertyId: propertyId, NewPrice: 850000.00m),
                new(PropertyId: propertyId, NewPrice: 860000.00m),
                new(PropertyId: propertyId, NewPrice: 840000.00m)
            };
        }
    }
}