using System;

namespace HotelManagment.Data
{
    public static class MapperHelper
    {
        public static string MapToRoomType(this byte value)
        {
            switch (value)
            {
                case 0:
                    return "Single";
                case 1:
                    return "Twin";
                case 2:
                    return "Lux";
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
