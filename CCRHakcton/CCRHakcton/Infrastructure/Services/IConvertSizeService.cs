using System;
namespace Core
{
    public interface IConvertSizeService
    {
        string GetConvertSize(long quota);
    }

    public class ConvertSizeService : IConvertSizeService
    {
        public string GetConvertSize(long quota)
        {
            string[] units = new string[] { " B", " KB", " MB", " GB", " TB", " PB" };
            double size = Convert.ToDouble(quota);
            double mod = 1024.0;
            int i = 0;
            while (size >= mod)
            {
                size /= mod;
                i++;
            }
            return Math.Round(size) + units[i];
        }
    }
}
