using RealEstate.Application.UsecCases.Property.Commands.CreateProperty;

namespace RealEstate.Tests.ObjectMothers.Commands
{
    public static class CreatePropertyCommandMother
    {
        /// <summary>
        /// Crea un comando válido básico para crear propiedad
        /// </summary>
        public static CreatePropertyCommand Valid() => new(
            Name: "Modern Downtown Apartment",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 750000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con nombre específico
        /// </summary>
        public static CreatePropertyCommand WithName(string name) => new(
            Name: name,
            Address: "123 Main Street, Miami, FL 33101",
            Price: 750000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con dirección específica
        /// </summary>
        public static CreatePropertyCommand WithAddress(string address) => new(
            Name: "Modern Downtown Apartment",
            Address: address,
            Price: 750000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con precio específico
        /// </summary>
        public static CreatePropertyCommand WithPrice(decimal price) => new(
            Name: "Modern Downtown Apartment",
            Address: "123 Main Street, Miami, FL 33101",
            Price: price,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con código interno específico
        /// </summary>
        public static CreatePropertyCommand WithCodeInternal(string codeInternal) => new(
            Name: "Modern Downtown Apartment",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 750000.00m,
            CodeInternal: codeInternal,
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con año específico
        /// </summary>
        public static CreatePropertyCommand WithYear(int year) => new(
            Name: "Modern Downtown Apartment",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 750000.00m,
            CodeInternal: "MIA001",
            Year: year,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con propietario específico
        /// </summary>
        public static CreatePropertyCommand WithOwnerId(int ownerId) => new(
            Name: "Modern Downtown Apartment",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 750000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: ownerId
        );

        /// <summary>
        /// Crea un comando con todos los datos personalizados
        /// </summary>
        public static CreatePropertyCommand WithCustomData(
            string name,
            string address,
            decimal price,
            string codeInternal,
            int year,
            int ownerId) => new(
            Name: name,
            Address: address,
            Price: price,
            CodeInternal: codeInternal,
            Year: year,
            OwnerId: ownerId
        );

        /// <summary>
        /// Crea un comando para propiedad de lujo
        /// </summary>
        public static CreatePropertyCommand LuxuryProperty() => new(
            Name: "Luxury Penthouse Suite",
            Address: "999 Premium Avenue, Miami Beach, FL 33139",
            Price: 2500000.00m,
            CodeInternal: "LUX999",
            Year: 2022,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando para propiedad económica
        /// </summary>
        public static CreatePropertyCommand BudgetProperty() => new(
            Name: "Cozy Starter Home",
            Address: "100 Budget Street, Orlando, FL 32801",
            Price: 150000.00m,
            CodeInternal: "BUD100",
            Year: 2010,
            OwnerId: 2
        );

        /// <summary>
        /// Crea un comando para propiedad vintage/histórica
        /// </summary>
        public static CreatePropertyCommand VintageProperty() => new(
            Name: "Historic Colonial Mansion",
            Address: "500 Heritage Lane, Key West, FL 33040",
            Price: 1800000.00m,
            CodeInternal: "VIN500",
            Year: 1920,
            OwnerId: 3
        );

        /// <summary>
        /// Crea un comando para propiedad moderna
        /// </summary>
        public static CreatePropertyCommand ModernProperty() => new(
            Name: "Ultra-Modern Smart Home",
            Address: "300 Tech Boulevard, Tampa, FL 33602",
            Price: 1200000.00m,
            CodeInternal: "MOD300",
            Year: 2023,
            OwnerId: 1
        );

        /// <summary>
        /// Crea múltiples comandos para testing en lote
        /// </summary>
        public static List<CreatePropertyCommand> Multiple(int count = 3)
        {
            var commands = new List<CreatePropertyCommand>();

            for (int i = 1; i <= count; i++)
            {
                commands.Add(new CreatePropertyCommand(
                    Name: $"Test Property {i}",
                    Address: $"{i * 100} Test Street, Miami, FL 3310{i}",
                    Price: 500000.00m + (i * 100000),
                    CodeInternal: $"TEST{i:D3}",
                    Year: 2020 + i,
                    OwnerId: (i % 3) + 1 // Rotar entre owners 1, 2, 3
                ));
            }

            return commands;
        }

        /// <summary>
        /// Crea un comando con datos mínimos requeridos
        /// </summary>
        public static CreatePropertyCommand Minimal() => new(
            Name: "Test Property",
            Address: "Test Address",
            Price: 100000m,
            CodeInternal: "TEST001",
            Year: 2020,
            OwnerId: 1
        );

        // ============================================
        // COMANDOS PARA TESTING DE VALIDACIÓN
        // ============================================

        /// <summary>
        /// Crea un comando con nombre vacío (para testing de validación)
        /// </summary>
        public static CreatePropertyCommand WithEmptyName() => new(
            Name: string.Empty,
            Address: "123 Main Street, Miami, FL 33101",
            Price: 750000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con nombre nulo (para testing de validación)
        /// </summary>
        public static CreatePropertyCommand WithNullName() => new(
            Name: null!,
            Address: "123 Main Street, Miami, FL 33101",
            Price: 750000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con nombre muy largo (para testing de validación)
        /// </summary>
        public static CreatePropertyCommand WithTooLongName() => new(
            Name: new string('A', 201), // 201 caracteres, excede el límite de 200
            Address: "123 Main Street, Miami, FL 33101",
            Price: 750000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con dirección vacía (para testing de validación)
        /// </summary>
        public static CreatePropertyCommand WithEmptyAddress() => new(
            Name: "Modern Downtown Apartment",
            Address: string.Empty,
            Price: 750000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con dirección muy larga (para testing de validación)
        /// </summary>
        public static CreatePropertyCommand WithTooLongAddress() => new(
            Name: "Modern Downtown Apartment",
            Address: new string('A', 501), // 501 caracteres, excede el límite de 500
            Price: 750000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con precio cero (para testing de validación)
        /// </summary>
        public static CreatePropertyCommand WithZeroPrice() => new(
            Name: "Modern Downtown Apartment",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 0m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con precio negativo (para testing de validación)
        /// </summary>
        public static CreatePropertyCommand WithNegativePrice() => new(
            Name: "Modern Downtown Apartment",
            Address: "123 Main Street, Miami, FL 33101",
            Price: -100000m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con precio excesivo (para testing de validación)
        /// </summary>
        public static CreatePropertyCommand WithExcessivePrice() => new(
            Name: "Modern Downtown Apartment",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 1000000000m, // Excede el límite de 999,999,999.99
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con código interno vacío (para testing de validación)
        /// </summary>
        public static CreatePropertyCommand WithEmptyCodeInternal() => new(
            Name: "Modern Downtown Apartment",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 750000.00m,
            CodeInternal: string.Empty,
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con código interno muy largo (para testing de validación)
        /// </summary>
        public static CreatePropertyCommand WithTooLongCodeInternal() => new(
            Name: "Modern Downtown Apartment",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 750000.00m,
            CodeInternal: new string('A', 51), // 51 caracteres, excede el límite de 50
            Year: 2020,
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con año muy antiguo (para testing de validación)
        /// </summary>
        public static CreatePropertyCommand WithTooOldYear() => new(
            Name: "Modern Downtown Apartment",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 750000.00m,
            CodeInternal: "MIA001",
            Year: 1800, // Igual al límite, debería fallar si es GreaterThan
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con año futuro (para testing de validación)
        /// </summary>
        public static CreatePropertyCommand WithFutureYear() => new(
            Name: "Modern Downtown Apartment",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 750000.00m,
            CodeInternal: "MIA001",
            Year: DateTime.Now.Year + 1, // Año futuro
            OwnerId: 1
        );

        /// <summary>
        /// Crea un comando con propietario ID cero (para testing de validación)
        /// </summary>
        public static CreatePropertyCommand WithZeroOwnerId() => new(
            Name: "Modern Downtown Apartment",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 750000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 0
        );

        /// <summary>
        /// Crea un comando con propietario ID negativo (para testing de validación)
        /// </summary>
        public static CreatePropertyCommand WithNegativeOwnerId() => new(
            Name: "Modern Downtown Apartment",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 750000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: -1
        );

        /// <summary>
        /// Crea un comando con propietario inexistente (para testing de validación)
        /// </summary>
        public static CreatePropertyCommand WithNonExistentOwnerId() => new(
            Name: "Modern Downtown Apartment",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 750000.00m,
            CodeInternal: "MIA001",
            Year: 2020,
            OwnerId: 99999 // ID que no existe
        );

        /// <summary>
        /// Crea un comando con código interno duplicado (para testing de validación)
        /// </summary>
        public static CreatePropertyCommand WithDuplicateCodeInternal() => new(
            Name: "Modern Downtown Apartment",
            Address: "123 Main Street, Miami, FL 33101",
            Price: 750000.00m,
            CodeInternal: "MIA001", // Mismo código que el válido por defecto
            Year: 2020,
            OwnerId: 1
        );
    }
}