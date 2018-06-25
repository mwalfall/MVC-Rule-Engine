using System;

namespace RuleEngine
{
    public class VehicleModelProxy
    {
        public long VehicleID { get; set; }
        public long SeriesID { get; set; }
        public int Config { get; set; }
        public int VID { get; set; }
        public bool AcceptedByFleet { get; set; }
        public bool ActiveFleet { get; set; }
        public int TransitionStatus { get; set; }
        public int VehicleStatus { get; set; }
        public DateTime ProjectedReceiveDate { get; set; }
        public DateTime ActualReceiveDate { get; set; }
        public DateTime AcceptedDate { get; set; }
        public DateTime InServiceDate { get; set; }
        public DateTime PendCdmDate { get; set; }
        public long OrderItemID { get; set; }
        public long ModItemID { get; set; }
        public string Owner { get; set; }
        public int OMBYear { get; set; }
        public string VIN { get; set; }
        public string Plate { get; set; }
        public int DID { get; set; }
        public int HseLocID { get; set; }
        public int PMLocID { get; set; }
        public short PMLine { get; set; }
        public short OSC { get; set; }
        public string EngineSN { get; set; }
        public string TransSN { get; set; }
        public string BodySN { get; set; }
        public int Fuel { get; set; }
        public bool FleetVeh { get; set; }
        public string RadioNo { get; set; }
        public string RFIDTag { get; set; }
        public int RecLocationID { get; set; }
        public string EngineMN { get; set; }
        public string TransMN { get; set; }
        public DateTime CondemDate { get; set; }
        public DateTime C511Date { get; set; }
        public DateTime RelinqDate { get; set; }
        public string RelinqNo { get; set; }
        public string RelinqLocation { get; set; }
        public DateTime ReactivationDate { get; set; }
        public bool Formalized { get; set; }
        public string Comments { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }
}
