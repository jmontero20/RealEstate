using RealEstate.Application.UsecCases.PropertyImages.Commands.AddPropertyImage;

namespace RealEstate.Tests.ObjectMothers.Commands
{
    public static class AddPropertyImageCommandMother
    {
        /// <summary>
        /// Crea un comando válido básico para agregar imagen a propiedad
        /// </summary>
        public static AddPropertyImageCommand Valid()
        {
            var imageData = CreateSampleImageStream();
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: "property-image.jpg",
                ContentType: "image/jpeg"
            );
        }

        /// <summary>
        /// Crea un comando con ID de propiedad específico
        /// </summary>
        public static AddPropertyImageCommand WithPropertyId(int propertyId)
        {
            var imageData = CreateSampleImageStream();
            return new AddPropertyImageCommand(
                PropertyId: propertyId,
                ImageStream: imageData,
                FileName: "property-image.jpg",
                ContentType: "image/jpeg"
            );
        }

        /// <summary>
        /// Crea un comando con nombre de archivo específico
        /// </summary>
        public static AddPropertyImageCommand WithFileName(string fileName)
        {
            var imageData = CreateSampleImageStream();
            var contentType = GetContentTypeFromFileName(fileName);
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: fileName,
                ContentType: contentType
            );
        }

        /// <summary>
        /// Crea un comando con tipo de contenido específico
        /// </summary>
        public static AddPropertyImageCommand WithContentType(string contentType)
        {
            var imageData = CreateSampleImageStream();
            var fileName = GetFileNameFromContentType(contentType);
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: fileName,
                ContentType: contentType
            );
        }

        /// <summary>
        /// Crea un comando con stream específico
        /// </summary>
        public static AddPropertyImageCommand WithImageStream(Stream imageStream)
        {
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageStream,
                FileName: "custom-image.jpg",
                ContentType: "image/jpeg"
            );
        }

        /// <summary>
        /// Crea un comando con todos los datos personalizados
        /// </summary>
        public static AddPropertyImageCommand WithCustomData(
            int propertyId,
            Stream imageStream,
            string fileName,
            string contentType) => new(
            PropertyId: propertyId,
            ImageStream: imageStream,
            FileName: fileName,
            ContentType: contentType
        );

        /// <summary>
        /// Crea un comando para imagen JPEG
        /// </summary>
        public static AddPropertyImageCommand JpegImage()
        {
            var imageData = CreateSampleImageStream("JPEG");
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: "exterior.jpg",
                ContentType: "image/jpeg"
            );
        }

        /// <summary>
        /// Crea un comando para imagen PNG
        /// </summary>
        public static AddPropertyImageCommand PngImage()
        {
            var imageData = CreateSampleImageStream("PNG");
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: "floorplan.png",
                ContentType: "image/png"
            );
        }

        /// <summary>
        /// Crea un comando para imagen WebP
        /// </summary>
        public static AddPropertyImageCommand WebPImage()
        {
            var imageData = CreateSampleImageStream("WEBP");
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: "modern-kitchen.webp",
                ContentType: "image/webp"
            );
        }

        /// <summary>
        /// Crea un comando para imagen GIF
        /// </summary>
        public static AddPropertyImageCommand GifImage()
        {
            var imageData = CreateSampleImageStream("GIF");
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: "animated-tour.gif",
                ContentType: "image/gif"
            );
        }

        /// <summary>
        /// Crea un comando para imagen de exterior
        /// </summary>
        public static AddPropertyImageCommand ExteriorImage()
        {
            var imageData = CreateSampleImageStream();
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: "exterior-front-view.jpg",
                ContentType: "image/jpeg"
            );
        }

        /// <summary>
        /// Crea un comando para imagen de interior
        /// </summary>
        public static AddPropertyImageCommand InteriorImage()
        {
            var imageData = CreateSampleImageStream();
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: "living-room-interior.jpg",
                ContentType: "image/jpeg"
            );
        }

        /// <summary>
        /// Crea un comando para imagen de cocina
        /// </summary>
        public static AddPropertyImageCommand KitchenImage()
        {
            var imageData = CreateSampleImageStream();
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: "modern-kitchen.jpg",
                ContentType: "image/jpeg"
            );
        }

        /// <summary>
        /// Crea un comando para imagen de baño
        /// </summary>
        public static AddPropertyImageCommand BathroomImage()
        {
            var imageData = CreateSampleImageStream();
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: "master-bathroom.jpg",
                ContentType: "image/jpeg"
            );
        }

        /// <summary>
        /// Crea un comando para imagen de dormitorio
        /// </summary>
        public static AddPropertyImageCommand BedroomImage()
        {
            var imageData = CreateSampleImageStream();
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: "master-bedroom.jpg",
                ContentType: "image/jpeg"
            );
        }

        /// <summary>
        /// Crea múltiples comandos para testing en lote
        /// </summary>
        public static List<AddPropertyImageCommand> Multiple(int count = 3, int propertyId = 1)
        {
            var commands = new List<AddPropertyImageCommand>();
            var imageTypes = new[]
            {
                ("exterior.jpg", "image/jpeg"),
                ("living-room.jpg", "image/jpeg"),
                ("kitchen.png", "image/png"),
                ("bedroom.jpg", "image/jpeg"),
                ("bathroom.webp", "image/webp")
            };

            for (int i = 1; i <= count; i++)
            {
                var (fileName, contentType) = imageTypes[(i - 1) % imageTypes.Length];
                var imageData = CreateSampleImageStream();

                commands.Add(new AddPropertyImageCommand(
                    PropertyId: propertyId,
                    ImageStream: imageData,
                    FileName: $"{i:D2}-{fileName}",
                    ContentType: contentType
                ));
            }

            return commands;
        }

        /// <summary>
        /// Crea una galería completa de imágenes para una propiedad
        /// </summary>
        public static List<AddPropertyImageCommand> PropertyGallery(int propertyId = 1)
        {
            return new List<AddPropertyImageCommand>
            {
                WithCustomData(propertyId, CreateSampleImageStream(), "01-exterior-front.jpg", "image/jpeg"),
                WithCustomData(propertyId, CreateSampleImageStream(), "02-exterior-back.jpg", "image/jpeg"),
                WithCustomData(propertyId, CreateSampleImageStream(), "03-living-room.jpg", "image/jpeg"),
                WithCustomData(propertyId, CreateSampleImageStream(), "04-kitchen.png", "image/png"),
                WithCustomData(propertyId, CreateSampleImageStream(), "05-master-bedroom.jpg", "image/jpeg"),
                WithCustomData(propertyId, CreateSampleImageStream(), "06-master-bathroom.webp", "image/webp"),
                WithCustomData(propertyId, CreateSampleImageStream(), "07-guest-room.jpg", "image/jpeg"),
                WithCustomData(propertyId, CreateSampleImageStream(), "08-dining-room.jpg", "image/jpeg")
            };
        }

        /// <summary>
        /// Crea comandos para todos los formatos soportados
        /// </summary>
        public static List<AddPropertyImageCommand> AllSupportedFormats(int propertyId = 1)
        {
            return new List<AddPropertyImageCommand>
            {
                WithCustomData(propertyId, CreateSampleImageStream("JPEG"), "test.jpg", "image/jpeg"),
                WithCustomData(propertyId, CreateSampleImageStream("JPEG"), "test.jpeg", "image/jpeg"),
                WithCustomData(propertyId, CreateSampleImageStream("PNG"), "test.png", "image/png"),
                WithCustomData(propertyId, CreateSampleImageStream("GIF"), "test.gif", "image/gif"),
                WithCustomData(propertyId, CreateSampleImageStream("WEBP"), "test.webp", "image/webp")
            };
        }

        /// <summary>
        /// Crea un comando con imagen pequeña (para testing de límites)
        /// </summary>
        public static AddPropertyImageCommand SmallImage()
        {
            var imageData = CreateSampleImageStream(size: 1024); // 1KB
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: "small-image.jpg",
                ContentType: "image/jpeg"
            );
        }

        /// <summary>
        /// Crea un comando con imagen grande (para testing de límites)
        /// </summary>
        public static AddPropertyImageCommand LargeImage()
        {
            var imageData = CreateSampleImageStream(size: 4 * 1024 * 1024); // 4MB
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: "large-image.jpg",
                ContentType: "image/jpeg"
            );
        }

        /// <summary>
        /// Crea un comando con datos mínimos requeridos
        /// </summary>
        public static AddPropertyImageCommand Minimal()
        {
            var imageData = CreateSampleImageStream();
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: "test.jpg",
                ContentType: "image/jpeg"
            );
        }

        // ============================================
        // COMANDOS PARA TESTING DE VALIDACIÓN
        // ============================================

        /// <summary>
        /// Crea un comando con ID de propiedad cero (para testing de validación)
        /// </summary>
        public static AddPropertyImageCommand WithZeroPropertyId()
        {
            var imageData = CreateSampleImageStream();
            return new AddPropertyImageCommand(
                PropertyId: 0,
                ImageStream: imageData,
                FileName: "test.jpg",
                ContentType: "image/jpeg"
            );
        }

        /// <summary>
        /// Crea un comando con ID de propiedad negativo (para testing de validación)
        /// </summary>
        public static AddPropertyImageCommand WithNegativePropertyId()
        {
            var imageData = CreateSampleImageStream();
            return new AddPropertyImageCommand(
                PropertyId: -1,
                ImageStream: imageData,
                FileName: "test.jpg",
                ContentType: "image/jpeg"
            );
        }

        /// <summary>
        /// Crea un comando con ID de propiedad inexistente (para testing de validación)
        /// </summary>
        public static AddPropertyImageCommand WithNonExistentPropertyId()
        {
            var imageData = CreateSampleImageStream();
            return new AddPropertyImageCommand(
                PropertyId: 99999,
                ImageStream: imageData,
                FileName: "test.jpg",
                ContentType: "image/jpeg"
            );
        }

        /// <summary>
        /// Crea un comando con stream nulo (para testing de validación)
        /// </summary>
        public static AddPropertyImageCommand WithNullStream() => new(
            PropertyId: 1,
            ImageStream: null!,
            FileName: "test.jpg",
            ContentType: "image/jpeg"
        );

        /// <summary>
        /// Crea un comando con stream vacío (para testing de validación)
        /// </summary>
        public static AddPropertyImageCommand WithEmptyStream()
        {
            var emptyStream = new MemoryStream();
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: emptyStream,
                FileName: "test.jpg",
                ContentType: "image/jpeg"
            );
        }

        /// <summary>
        /// Crea un comando con archivo muy grande (para testing de validación)
        /// </summary>
        public static AddPropertyImageCommand WithOversizedFile()
        {
            var oversizedStream = CreateSampleImageStream(size: 6 * 1024 * 1024); // 6MB (excede límite de 5MB)
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: oversizedStream,
                FileName: "oversized.jpg",
                ContentType: "image/jpeg"
            );
        }

        /// <summary>
        /// Crea un comando con nombre de archivo vacío (para testing de validación)
        /// </summary>
        public static AddPropertyImageCommand WithEmptyFileName()
        {
            var imageData = CreateSampleImageStream();
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: string.Empty,
                ContentType: "image/jpeg"
            );
        }

        /// <summary>
        /// Crea un comando con nombre de archivo nulo (para testing de validación)
        /// </summary>
        public static AddPropertyImageCommand WithNullFileName()
        {
            var imageData = CreateSampleImageStream();
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: null!,
                ContentType: "image/jpeg"
            );
        }

        /// <summary>
        /// Crea un comando con nombre de archivo muy largo (para testing de validación)
        /// </summary>
        public static AddPropertyImageCommand WithTooLongFileName()
        {
            var imageData = CreateSampleImageStream();
            var longFileName = new string('a', 250) + ".jpg"; // 254 caracteres, excede límite de 255
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: longFileName,
                ContentType: "image/jpeg"
            );
        }

        /// <summary>
        /// Crea un comando con extensión de archivo inválida (para testing de validación)
        /// </summary>
        public static AddPropertyImageCommand WithInvalidFileExtension()
        {
            var imageData = CreateSampleImageStream();
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: "document.txt", // Extensión no válida
                ContentType: "image/jpeg"
            );
        }

        /// <summary>
        /// Crea un comando con tipo de contenido vacío (para testing de validación)
        /// </summary>
        public static AddPropertyImageCommand WithEmptyContentType()
        {
            var imageData = CreateSampleImageStream();
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: "test.jpg",
                ContentType: string.Empty
            );
        }

        /// <summary>
        /// Crea un comando con tipo de contenido inválido (para testing de validación)
        /// </summary>
        public static AddPropertyImageCommand WithInvalidContentType()
        {
            var imageData = CreateSampleImageStream();
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: "test.jpg",
                ContentType: "application/pdf" // Tipo no válido para imagen
            );
        }

        /// <summary>
        /// Crea un comando con inconsistencia entre filename y content type (para testing de validación)
        /// </summary>
        public static AddPropertyImageCommand WithMismatchedFileTypeAndContentType()
        {
            var imageData = CreateSampleImageStream();
            return new AddPropertyImageCommand(
                PropertyId: 1,
                ImageStream: imageData,
                FileName: "test.jpg", // Archivo JPG
                ContentType: "image/png" // Pero content type PNG
            );
        }

        // ============================================
        // MÉTODOS HELPER PRIVADOS
        // ============================================

        /// <summary>
        /// Crea un stream de muestra para testing
        /// </summary>
        private static Stream CreateSampleImageStream(string format = "JPEG", int size = 1024)
        {
            var data = new byte[size];

            // Crear header básico según el formato
            switch (format.ToUpper())
            {
                case "JPEG":
                    data[0] = 0xFF; data[1] = 0xD8; // JPEG header
                    break;
                case "PNG":
                    data[0] = 0x89; data[1] = 0x50; data[2] = 0x4E; data[3] = 0x47; // PNG header
                    break;
                case "GIF":
                    data[0] = 0x47; data[1] = 0x49; data[2] = 0x46; // GIF header
                    break;
                case "WEBP":
                    data[0] = 0x52; data[1] = 0x49; data[2] = 0x46; data[3] = 0x46; // WEBP header
                    break;
            }

            // Llenar el resto con datos de muestra
            for (int i = 4; i < size; i++)
            {
                data[i] = (byte)(i % 256);
            }

            return new MemoryStream(data);
        }

        /// <summary>
        /// Obtiene el content type basado en el nombre de archivo
        /// </summary>
        private static string GetContentTypeFromFileName(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                _ => "image/jpeg" // Default
            };
        }

        /// <summary>
        /// Obtiene un nombre de archivo basado en el content type
        /// </summary>
        private static string GetFileNameFromContentType(string contentType)
        {
            return contentType.ToLower() switch
            {
                "image/jpeg" => "image.jpg",
                "image/png" => "image.png",
                "image/gif" => "image.gif",
                "image/webp" => "image.webp",
                _ => "image.jpg" // Default
            };
        }
    }
}