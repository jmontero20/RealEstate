using RealEstate.Application.UsecCases.PropertyImages.Commands.AddPropertyImage;

namespace RealEstate.Tests.ObjectMothers.Responses
{
    public static class AddPropertyImageResponseMother
    {
        /// <summary>
        /// Crea una respuesta exitosa básica para adición de imagen
        /// </summary>
        public static AddPropertyImageResponse Valid() => new()
        {
            ImageId = 1,
            PropertyId = 1,
            ImageUrl = "https://teststorage.blob.core.windows.net/property-images/12345678-9abc-def0-1234-56789abcdef0_property-image.jpg",
            FileName = "property-image.jpg",
            UploadedAt = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea una respuesta con ID de imagen específico
        /// </summary>
        public static AddPropertyImageResponse WithImageId(int imageId)
        {
            var response = Valid();
            response.ImageId = imageId;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con ID de propiedad específico
        /// </summary>
        public static AddPropertyImageResponse WithPropertyId(int propertyId)
        {
            var response = Valid();
            response.PropertyId = propertyId;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con URL de imagen específica
        /// </summary>
        public static AddPropertyImageResponse WithImageUrl(string imageUrl)
        {
            var response = Valid();
            response.ImageUrl = imageUrl;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con nombre de archivo específico
        /// </summary>
        public static AddPropertyImageResponse WithFileName(string fileName)
        {
            var response = Valid();
            response.FileName = fileName;
            // Actualizar también la URL para que sea consistente
            response.ImageUrl = $"https://teststorage.blob.core.windows.net/property-images/12345678-9abc-def0-1234-56789abcdef0_{fileName}";
            return response;
        }

        /// <summary>
        /// Crea una respuesta con fecha de carga específica
        /// </summary>
        public static AddPropertyImageResponse WithUploadedAt(DateTime uploadedAt)
        {
            var response = Valid();
            response.UploadedAt = uploadedAt;
            return response;
        }

        /// <summary>
        /// Crea una respuesta con todos los datos personalizados
        /// </summary>
        public static AddPropertyImageResponse WithCustomData(
            int imageId,
            int propertyId,
            string imageUrl,
            string fileName,
            DateTime? uploadedAt = null) => new()
            {
                ImageId = imageId,
                PropertyId = propertyId,
                ImageUrl = imageUrl,
                FileName = fileName,
                UploadedAt = uploadedAt ?? new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc)
            };

        /// <summary>
        /// Crea una respuesta para imagen JPEG
        /// </summary>
        public static AddPropertyImageResponse JpegImage() => new()
        {
            ImageId = 1,
            PropertyId = 1,
            ImageUrl = "https://teststorage.blob.core.windows.net/property-images/12345678-9abc-def0-1234-56789abcdef0_exterior.jpg",
            FileName = "exterior.jpg",
            UploadedAt = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea una respuesta para imagen PNG
        /// </summary>
        public static AddPropertyImageResponse PngImage() => new()
        {
            ImageId = 2,
            PropertyId = 1,
            ImageUrl = "https://teststorage.blob.core.windows.net/property-images/87654321-cdef-0123-4567-89abcdef0123_floorplan.png",
            FileName = "floorplan.png",
            UploadedAt = new DateTime(2024, 1, 15, 10, 5, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea una respuesta para imagen WebP
        /// </summary>
        public static AddPropertyImageResponse WebPImage() => new()
        {
            ImageId = 3,
            PropertyId = 1,
            ImageUrl = "https://teststorage.blob.core.windows.net/property-images/11111111-2222-3333-4444-555555555555_kitchen.webp",
            FileName = "kitchen.webp",
            UploadedAt = new DateTime(2024, 1, 15, 10, 10, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea una respuesta para imagen de exterior
        /// </summary>
        public static AddPropertyImageResponse ExteriorImage() => new()
        {
            ImageId = 10,
            PropertyId = 1,
            ImageUrl = "https://teststorage.blob.core.windows.net/property-images/exterior-12345678-9abc-def0-1234-56789abcdef0_front-view.jpg",
            FileName = "front-view.jpg",
            UploadedAt = new DateTime(2024, 1, 15, 9, 0, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea una respuesta para imagen de interior
        /// </summary>
        public static AddPropertyImageResponse InteriorImage() => new()
        {
            ImageId = 20,
            PropertyId = 1,
            ImageUrl = "https://teststorage.blob.core.windows.net/property-images/interior-87654321-cdef-0123-4567-89abcdef0123_living-room.jpg",
            FileName = "living-room.jpg",
            UploadedAt = new DateTime(2024, 1, 15, 9, 30, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea una respuesta para imagen de baño
        /// </summary>
        public static AddPropertyImageResponse BathroomImage() => new()
        {
            ImageId = 30,
            PropertyId = 2,
            ImageUrl = "https://teststorage.blob.core.windows.net/property-images/bathroom-11111111-2222-3333-4444-555555555555_master-bath.jpg",
            FileName = "master-bath.jpg",
            UploadedAt = new DateTime(2024, 1, 15, 11, 0, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea una respuesta para imagen de cocina
        /// </summary>
        public static AddPropertyImageResponse KitchenImage() => new()
        {
            ImageId = 40,
            PropertyId = 3,
            ImageUrl = "https://teststorage.blob.core.windows.net/property-images/kitchen-aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee_modern-kitchen.jpg",
            FileName = "modern-kitchen.jpg",
            UploadedAt = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea múltiples respuestas para testing de galería de imágenes
        /// </summary>
        public static List<AddPropertyImageResponse> Multiple(int count = 3, int propertyId = 1)
        {
            var responses = new List<AddPropertyImageResponse>();
            var baseDateTime = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc);
            var imageTypes = new[] { "exterior.jpg", "living-room.jpg", "kitchen.jpg", "bedroom.jpg", "bathroom.jpg" };

            for (int i = 1; i <= count; i++)
            {
                var fileName = imageTypes[(i - 1) % imageTypes.Length];
                var guid = Guid.NewGuid().ToString();

                responses.Add(new AddPropertyImageResponse
                {
                    ImageId = i,
                    PropertyId = propertyId,
                    ImageUrl = $"https://teststorage.blob.core.windows.net/property-images/{guid}_{fileName}",
                    FileName = fileName,
                    UploadedAt = baseDateTime.AddMinutes(i * 5)
                });
            }

            return responses;
        }

        /// <summary>
        /// Crea una galería completa de imágenes para una propiedad
        /// </summary>
        public static List<AddPropertyImageResponse> PropertyGallery(int propertyId)
        {
            var baseDateTime = new DateTime(2024, 1, 15, 9, 0, 0, DateTimeKind.Utc);

            return new List<AddPropertyImageResponse>
            {
                new()
                {
                    ImageId = 1,
                    PropertyId = propertyId,
                    ImageUrl = $"https://teststorage.blob.core.windows.net/property-images/{Guid.NewGuid()}_exterior-front.jpg",
                    FileName = "exterior-front.jpg",
                    UploadedAt = baseDateTime
                },
                new()
                {
                    ImageId = 2,
                    PropertyId = propertyId,
                    ImageUrl = $"https://teststorage.blob.core.windows.net/property-images/{Guid.NewGuid()}_living-room.jpg",
                    FileName = "living-room.jpg",
                    UploadedAt = baseDateTime.AddMinutes(5)
                },
                new()
                {
                    ImageId = 3,
                    PropertyId = propertyId,
                    ImageUrl = $"https://teststorage.blob.core.windows.net/property-images/{Guid.NewGuid()}_kitchen.jpg",
                    FileName = "kitchen.jpg",
                    UploadedAt = baseDateTime.AddMinutes(10)
                },
                new()
                {
                    ImageId = 4,
                    PropertyId = propertyId,
                    ImageUrl = $"https://teststorage.blob.core.windows.net/property-images/{Guid.NewGuid()}_master-bedroom.jpg",
                    FileName = "master-bedroom.jpg",
                    UploadedAt = baseDateTime.AddMinutes(15)
                },
                new()
                {
                    ImageId = 5,
                    PropertyId = propertyId,
                    ImageUrl = $"https://teststorage.blob.core.windows.net/property-images/{Guid.NewGuid()}_bathroom.jpg",
                    FileName = "bathroom.jpg",
                    UploadedAt = baseDateTime.AddMinutes(20)
                }
            };
        }

        /// <summary>
        /// Crea una respuesta con carga reciente (útil para testing de timestamps)
        /// </summary>
        public static AddPropertyImageResponse RecentlyUploaded()
        {
            var response = Valid();
            response.UploadedAt = DateTime.UtcNow.AddMinutes(-1);
            return response;
        }

        /// <summary>
        /// Crea una respuesta con datos mínimos requeridos
        /// </summary>
        public static AddPropertyImageResponse Minimal() => new()
        {
            ImageId = 1,
            PropertyId = 1,
            ImageUrl = "https://teststorage.blob.core.windows.net/property-images/test.jpg",
            FileName = "test.jpg",
            UploadedAt = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc)
        };

        /// <summary>
        /// Crea respuestas para diferentes formatos de imagen soportados
        /// </summary>
        public static List<AddPropertyImageResponse> AllSupportedFormats(int propertyId = 1)
        {
            var baseDateTime = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc);

            return new List<AddPropertyImageResponse>
            {
                new()
                {
                    ImageId = 1,
                    PropertyId = propertyId,
                    ImageUrl = $"https://teststorage.blob.core.windows.net/property-images/{Guid.NewGuid()}_image.jpg",
                    FileName = "image.jpg",
                    UploadedAt = baseDateTime
                },
                new()
                {
                    ImageId = 2,
                    PropertyId = propertyId,
                    ImageUrl = $"https://teststorage.blob.core.windows.net/property-images/{Guid.NewGuid()}_image.jpeg",
                    FileName = "image.jpeg",
                    UploadedAt = baseDateTime.AddMinutes(1)
                },
                new()
                {
                    ImageId = 3,
                    PropertyId = propertyId,
                    ImageUrl = $"https://teststorage.blob.core.windows.net/property-images/{Guid.NewGuid()}_image.png",
                    FileName = "image.png",
                    UploadedAt = baseDateTime.AddMinutes(2)
                },
                new()
                {
                    ImageId = 4,
                    PropertyId = propertyId,
                    ImageUrl = $"https://teststorage.blob.core.windows.net/property-images/{Guid.NewGuid()}_image.gif",
                    FileName = "image.gif",
                    UploadedAt = baseDateTime.AddMinutes(3)
                },
                new()
                {
                    ImageId = 5,
                    PropertyId = propertyId,
                    ImageUrl = $"https://teststorage.blob.core.windows.net/property-images/{Guid.NewGuid()}_image.webp",
                    FileName = "image.webp",
                    UploadedAt = baseDateTime.AddMinutes(4)
                }
            };
        }
    }
}