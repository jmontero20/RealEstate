using RealEstate.Application.UsecCases.Property.Commands.UpdateProperty;

namespace RealEstate.Tests.ObjectMothers.Commands
{
    public static class UpdatePropertyCommandMother
    {
        /// <summary>
        /// Crea un comando válido básico para actualizar propiedad
        /// </summary>
        public static UpdatePropertyCommand Valid() => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con ID específico
        /// </summary>
        public static UpdatePropertyCommand WithId(int id) => new(
            Id: id,
            Name: "Modern Downtown Apartment - Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con nombre específico
        /// </summary>
        public static UpdatePropertyCommand WithName(string name) => new(
            Id: 1,
            Name: name,
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con dirección específica
        /// </summary>
        public static UpdatePropertyCommand WithAddress(string address) => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Updated",
            Address: address,
            Price: 800000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con precio específico
        /// </summary>
        public static UpdatePropertyCommand WithPrice(decimal price) => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: price,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con código interno específico
        /// </summary>
        public static UpdatePropertyCommand WithCodeInternal(string codeInternal) => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: codeInternal,
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con año específico
        /// </summary>
        public static UpdatePropertyCommand WithYear(int year) => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: "MIA001",
            Year: year,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con propietario específico
        /// </summary>
        public static UpdatePropertyCommand WithOwnerId(int ownerId) => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: ownerId
        );

        /// <summary>
        /// Crea un comando con todos los datos personalizados
        /// </summary>
        public static UpdatePropertyCommand WithCustomData(
            int id,
            string name,
            string address,
            decimal price,
            string codeInternal,
            int year,
            int ownerId) => new(
            Id: id,
            Name: name,
            Address: address,
            Price: price,
            CodeInternal: codeInternal,
            Year: year,
            OwnerId: ownerId
        );

        /// <summary>
        /// Crea un comando para actualizar propiedad a lujo
        /// </summary>
        public static UpdatePropertyCommand ToLuxuryProperty() => new(
            Id: 1,
            Name: "Luxury Penthouse Suite - Premium Updated",
            Address: "999 Premium Avenue, Miami Beach, FL 33139",
            Price: 2500000.00m,
            CodeInternal: "LUX999",
            Year: 2022,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando para actualizar propiedad a económica
        /// </summary>
        public static UpdatePropertyCommand ToBudgetProperty() => new(
            Id: 1,
            Name: "Cozy Starter Home - Budget Updated",
            Address: "100 Budget Street, Orlando, FL 32801",
            Price: 150000.00m,
            CodeInternal: "BUD100",
            Year: 2010,
            OwnerId: 2
        );

        /// <summary>
        /// Crea un comando con cambio de precio (aumento)
        /// </summary>
        public static UpdatePropertyCommand WithPriceIncrease() => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Price Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 850000.00m, // Aumento de $100K desde el precio por defecto
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con cambio de precio (reducción)
        /// </summary>
        public static UpdatePropertyCommand WithPriceDecrease() => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Price Reduced",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 650000.00m, 
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con cambio de propietario
        /// </summary>
        public static UpdatePropertyCommand WithOwnershipChange() => new(
            Id: 1,
            Name: "Modern Downtown Apartment - New Owner",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 2 // Cambio de propietario
        );

        /// <summary>
        /// Crea un comando con cambio de código interno
        /// </summary>
        public static UpdatePropertyCommand WithCodeInternalChange() => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Recoded",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: "MIA002", // Nuevo código interno
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando solo con cambios menores (nombre y dirección)
        /// </summary>
        public static UpdatePropertyCommand WithMinorChanges() => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Renovated",
            Address: "123 Main Street, Unit 5A, Miami, FL 33101", // Dirección más específica
            Price: 750000.00m, // Precio original sin cambio
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea múltiples comandos para testing en lote
        /// </summary>
        public static List<UpdatePropertyCommand> Multiple(int count = 3)
        {
            var commands = new List<UpdatePropertyCommand>();

            for (int i = 1; i <= count; i++)
            {
                commands.Add(new UpdatePropertyCommand(
                    Id: i,
                    Name: $"Updated Property {i}",
                    Address: $"{i * 100} Updated Street, Miami, FL 3310{i}",
                    Price: 600000.00m + (i * 100000),
                    CodeInternal: $"UPD{i:D3}",
                    Year: 2021 + i,
                    OwnerId: (i % 3) + 1 // Rotar entre owners 1, 2, 3
                ));
            }

            return commands;
        }

        /// <summary>
        /// Crea un comando con datos mínimos requeridos
        /// </summary>
        public static UpdatePropertyCommand Minimal() => new(
            Id: 1,
            Name: "Updated Test Property",
            Address: "Updated Test Address",
            Price: 100000m,
            CodeInternal: "TEST001",
            Year: 2020,
            OwnerId: 1
        );

        // ============================================
        // COMANDOS PARA TESTING DE VALIDACIÓN
        // ============================================

        /// <summary>
        /// Crea un comando con ID cero (para testing de validación)
        /// </summary>
        public static UpdatePropertyCommand WithZeroId() => new(
            Id: 0,
            Name: "Modern Downtown Apartment - Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con ID negativo (para testing de validación)
        /// </summary>
        public static UpdatePropertyCommand WithNegativeId() => new(
            Id: -1,
            Name: "Modern Downtown Apartment - Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con ID inexistente (para testing de validación)
        /// </summary>
        public static UpdatePropertyCommand WithNonExistentId() => new(
            Id: 99999,
            Name: "Modern Downtown Apartment - Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con nombre vacío (para testing de validación)
        /// </summary>
        public static UpdatePropertyCommand WithEmptyName() => new(
            Id: 1,
            Name: string.Empty,
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con nombre nulo (para testing de validación)
        /// </summary>
        public static UpdatePropertyCommand WithNullName() => new(
            Id: 1,
            Name: null!,
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con nombre muy largo (para testing de validación)
        /// </summary>
        public static UpdatePropertyCommand WithTooLongName() => new(
            Id: 1,
            Name: new string('A', 201), // 201 caracteres, excede el límite de 200
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con dirección vacía (para testing de validación)
        /// </summary>
        public static UpdatePropertyCommand WithEmptyAddress() => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Updated",
            Address: string.Empty,
            Price: 800000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con dirección muy larga (para testing de validación)
        /// </summary>
        public static UpdatePropertyCommand WithTooLongAddress() => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Updated",
            Address: new string('A', 501), // 501 caracteres, excede el límite de 500
            Price: 800000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con precio cero (para testing de validación)
        /// </summary>
        public static UpdatePropertyCommand WithZeroPrice() => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 0m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con precio negativo (para testing de validación)
        /// </summary>
        public static UpdatePropertyCommand WithNegativePrice() => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: -100000m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con precio excesivo (para testing de validación)
        /// </summary>
        public static UpdatePropertyCommand WithExcessivePrice() => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 1000000000m, // Excede el límite de 999,999,999.99
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con código interno vacío (para testing de validación)
        /// </summary>
        public static UpdatePropertyCommand WithEmptyCodeInternal() => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: string.Empty,
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con código interno muy largo (para testing de validación)
        /// </summary>
        public static UpdatePropertyCommand WithTooLongCodeInternal() => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: new string('A', 51), // 51 caracteres, excede el límite de 50
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con código interno duplicado (para testing de validación)
        /// </summary>
        public static UpdatePropertyCommand WithDuplicateCodeInternal(int propertyId = 1) => new(
            Id: propertyId,
            Name: "Modern Downtown Apartment - Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: "MIA001", // Código que ya existe en otra propiedad
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con año muy antiguo (para testing de validación)
        /// </summary>
        public static UpdatePropertyCommand WithTooOldYear() => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: "MIA001",
            Year: 1800, // Igual al límite, debería fallar si es GreaterThan
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con año futuro (para testing de validación)
        /// </summary>
        public static UpdatePropertyCommand WithFutureYear() => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: "MIA001",
            Year: DateTime.Now.Year + 1, // Año futuro
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con propietario ID cero (para testing de validación)
        /// </summary>
        public static UpdatePropertyCommand WithZeroOwnerId() => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 0
        );

        /// <summary>
        /// Crea un comando con propietario ID negativo (para testing de validación)
        /// </summary>
        public static UpdatePropertyCommand WithNegativeOwnerId() => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: -1
        );

        /// <summary>
        /// Crea un comando con propietario inexistente (para testing de validación)
        /// </summary>
        public static UpdatePropertyCommand WithNonExistentOwnerId() => new(
            Id: 1,
            Name: "Modern Downtown Apartment - Updated",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 800000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 99999 // ID que no existe
        );
    }
}