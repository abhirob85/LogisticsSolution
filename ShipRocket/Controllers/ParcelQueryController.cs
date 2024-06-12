using Logistics.CQRS.EventSourcing;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShipRocket.Controllers
{
    
    [Route("Parcel")]
    [ApiController]
    public class ParcelQueryController : ControllerBase
    {
        private ParcelQueryHandler _parcelQueryHandler { get; set; }
        private ParcelSateQueryHandler _parcelSateQueryHandler { get; set; }

        public ParcelQueryController(
            ParcelQueryHandler parcelQueryHandler,
            ParcelSateQueryHandler parcelSateQueryHandler
            )
        {
            _parcelQueryHandler = parcelQueryHandler;
            _parcelSateQueryHandler = parcelSateQueryHandler;
        }

        // POST api/<ParcelController>
        [HttpGet("{parcelId}/query")]
        public IActionResult Parcel(Guid parcelId)
        {
            var query = new GetParcelByIdQuery { ParcelId = parcelId };
            return Ok(_parcelQueryHandler.Handle(query));
        }

        [HttpGet("{parcelId}/snapshot")]
        public IActionResult Parcel(Guid parcelId, DateTime snapShotTime, bool withAllScans = true)
        {
            var query = new GetParcelByIdQuery { ParcelId = parcelId };
            if (snapShotTime == DateTime.MinValue) snapShotTime = DateTime.Now;

            return Ok(_parcelSateQueryHandler.Handle(parcelId,snapShotTime, withAllScans));
        }

    }
}
