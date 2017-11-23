using T034.Api.Dto.Common;

namespace T034.Api.Dto.Vgiik
{
    public class PublicationDto : AbstractDto<int>
    {
        /// <summary>
        /// Путь к папке с файлами публикайии
        /// </summary>
        public string Path { get; set; }
    }
}
