using BlazingPizza.UI.ComponentsLibrary.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingPizza.UI.Shared
{
    public class OrderWithStatus
    {
        public readonly static TimeSpan PreparationDuration = TimeSpan.FromSeconds(20);
        public readonly static TimeSpan DeliveryDuration = TimeSpan.FromMinutes(1);
        public Order Order { get; set; }
        public string StatusText { get; set; }
        public List<Marker> MapMarkers { get; set; }

        public static OrderWithStatus FromOrder(Order Order)
        {
            string message = "";
            List<Marker> markers = new List<Marker>();
            var DispatchTime = Order.CreatedTime.Value.Add(PreparationDuration);

            if (DateTime.UtcNow < DispatchTime)
            {
                message = "Preparando";
                markers = new List<Marker>()
                {
                    ToMapMarker("Usted", Order.DeliveryLocation, showPopup: true)
                };
            }
            else if (DateTime.UtcNow < DispatchTime + DeliveryDuration)
            {
                message = "En camino";
                var StartPosition = ComputeStartPosition(Order);
                var ProportionOfDeliveryCompleted =
                    Math.Min(1, (DateTime.UtcNow - DispatchTime).TotalMilliseconds / DeliveryDuration.TotalMilliseconds);
                var DriverPosition = LatLong.Interpolate(StartPosition, Order.DeliveryLocation, ProportionOfDeliveryCompleted);
                markers = new List<Marker>
                {
                    ToMapMarker("Usted", Order.DeliveryLocation),
                    ToMapMarker("Repartidor", DriverPosition, showPopup: true)
                };
            }
            else
            {
                message = "Entregado";
                markers = new List<Marker>()
                {
                    ToMapMarker("Ubicación de entrega", Order.DeliveryLocation, showPopup: true)
                };
            }

            return new OrderWithStatus
            {
                Order = Order,
                StatusText = message,
                MapMarkers = markers
            };
        }

        static Marker ToMapMarker(string description, LatLong coords, bool showPopup = false)
            => new Marker
            {
                Description = description,
                X = coords.Longitude,
                Y = coords.Latitude,
                ShowPopup = showPopup
            };

        static LatLong ComputeStartPosition(Order order)
        {
            var Random = new Random(order.OrderId);
            var Distance = 0.01 + Random.NextDouble() * 0.02;
            var Angle = Random.NextDouble() * Math.PI * 2;
            var Offset = (Distance * Math.Cos(Angle), Distance * Math.Sin(Angle));
            return new LatLong(order.DeliveryLocation.Latitude + Offset.Item1,
                order.DeliveryLocation.Longitude + Offset.Item2);
        }
    }
}
