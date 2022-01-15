using System;

namespace Web.Constants.File
{
    public struct StorageUnits
    {
        public static double Byte { get; } = Math.Pow(2, 0); // 1 bytes
        public static double KiloByte { get; } = Math.Pow(2, 10); // 1,024 bytes
        public static double Megabyte { get; } = Math.Pow(2, 20); // 1,048,576 bytes
        public static double Gigabyte { get; } = Math.Pow(2, 30); // 1,073,741,824 bytes
    }
}
