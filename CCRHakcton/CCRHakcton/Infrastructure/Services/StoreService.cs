using System.IO;
using LiteDB;

namespace Core
{
    public static class StoreService
    {
        static ILiteDatabase _instance;
        static object __lock = new object();

        public static void Start(string pwd)
        {
            lock (__lock)
            {
                Dispose();
                _instance = new LiteDatabase(new ConnectionString(Constants.Database) { Password = pwd });
            }
        }

        public static ILiteDatabase Instance
        {
            get
            {
                lock (__lock)
                    return _instance;
            }
        }

        public static void Dispose()
        {
            _instance?.Dispose();
            _instance = null;
        }

        public static void DeleteDataBase()
        {
            Dispose();
            File.Delete(Constants.Database);
        }
    }
}
