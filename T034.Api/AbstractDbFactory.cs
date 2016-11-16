using T034.Api.DataAccess;

namespace T034.Api
{
    /// <summary>
    /// Абстрактная фабрика
    /// </summary>
    public abstract class AbstractDbFactory
    {
        /// <summary>
        /// Создать базовый менеджер
        /// </summary>
        /// <returns></returns>
        public abstract IBaseDb CreateBaseDb();
    }
}
