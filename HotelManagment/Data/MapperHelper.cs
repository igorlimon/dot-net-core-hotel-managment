using System;

namespace HotelManagment.Data
{
    public static class MapperHelper
    {
        public static RoomType MapToRoomType(this byte value)
        {
            switch (value)
            {
                case 0:
                    return RoomType.Single;
                case 1:
                    return RoomType.Twin;
                case 2:
                    return RoomType.Lux;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
