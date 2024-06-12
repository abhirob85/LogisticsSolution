using Logistics.CQRS.EventSourcing;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShipRocket.Controllers
{
    [Route("Parcel")]
    [ApiController]
    public class ParcelCommandController : ControllerBase
    {
        private string[] facility =
        {
            "California",
            "Texas",
            "New York",
            "Florida",
            "Illinois",
            "Pennsylvania",
            "Ohio",
            "Michigan",
            "Georgia",
            "North Carolina"
        };
        private ParcelCommandHandler _parcelCommandHandler { get; set; }

        public ParcelCommandController(ParcelCommandHandler parcelCommandHandler)
        {
            _parcelCommandHandler = parcelCommandHandler;

        }

        // POST api/<ParcelController>
        [HttpPost("Command")]
        public IActionResult Post(Guid parcelId, bool Induct = true, bool Containerized = true, bool ship = true)
        {
            if (parcelId == Guid.Empty) parcelId = Guid.NewGuid();

            if (Induct) ParcelInduct(parcelId);
            if (Containerized) ParcelContainerized(parcelId);
            if (ship) ParcelShip(parcelId);

            return Ok(parcelId);
        }
        private void ParcelInduct(Guid parcelId)
        {
            var inductCommand = new InductParcelCommand
            {
                ParcelId = parcelId,
                Facility = facility[new Random().Next(facility.Length)],
                Weight = Math.Round((decimal)(new Random().NextDouble() * 9.99), 2),
                Dimensions = Math.Round((decimal)(new Random().NextDouble() * 9.99), 2)
            };
            _parcelCommandHandler.Handle(inductCommand);

        }
        private void ParcelContainerized(Guid parcelId)
        {
            var containerizeCommand = new ContainerizeParcelCommand
            {
                ParcelId = parcelId,
                Facility = facility[new Random().Next(facility.Length)],
                ContainerId = Guid.NewGuid()
            };
            _parcelCommandHandler.Handle(containerizeCommand);

        }
        private void ParcelShip(Guid parcelId)
        {
            var shipCommand = new ShipParcelCommand
            {
                ParcelId = parcelId,
                Facility = facility[new Random().Next(facility.Length)],
                DestinationFacility = facility[new Random().Next(facility.Length)],
                BOL = Guid.NewGuid()
            };
            _parcelCommandHandler.Handle(shipCommand);

        }

    }
}
