using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Models
{
    public class VehicleInspectionDetailsModel
    {
        public string Id { get; set; }
        public string depot { get; set; }
        public string vehicleRegistration { get; set; }
        public string vehicleMake { get; set; }
        public decimal mileage { get; set; }
        public string driverFullname { get; set; }
        public bool lightsIndicators { get; set; }
        public bool reflectorMarkers { get; set; }
        public bool mirrors { get; set; }
        public bool OilCoolantLevel { get; set; }
        public bool adBlueLevel { get; set; }
        public bool tyres { get; set; }
        public bool wheels { get; set; }
        public bool bodyPanels { get; set; }
        public bool horns { get; set; }
        public bool fuelOilLeaks { get; set; }
        public bool speedometer { get; set; }
        public bool exhaustAndSmoke { get; set; }
        public bool battery { get; set; }
        public bool seatBelts { get; set; }
        public bool doorCondition { get; set; }
        public bool instrumentPanel { get; set; }
        public bool windscreenCondition { get; set; }
        public bool wipersAndWashers { get; set; }
        public bool fireExtinguisher { get; set; }
        public bool dashCam { get; set; }
        public bool wheelChangingKit { get; set; }
        public bool spareWheel { get; set; }
        public bool warningTriangle { get; set; }
        public bool firstAidKit { get; set; }
        public bool drivingLicense { get; set; }
        public bool alcoholOrDrugs { get; set; }
        public string contactDetails { get; set; }
        public string commentsLightsIndicators { get; set; }
        public string commentsReflectorsMarkers { get; set; }
        public string commentsMirrors { get; set; }
        public string commentsOilCoolantLevel { get; set; }
        public string commentsAdBlueLevel { get; set; }
        public string commentsTyres { get; set; }
        public string commentsWheels { get; set; }
        public string commentsBodyPanels { get; set; }
        public string commentsHorn { get; set; }
        public string commentsFuelOilLeaks { get; set; }
        public string commentsSpeedometer { get; set; }
        public string commentsExhaustAndSmoke { get; set; }
        public string commentsBattery { get; set; }
        public string commentsSeatBelts { get; set; }
        public string commentsDoorsCondition { get; set; }
        public string commentsWipersAndWashers { get; set; }
        public string commentsInstrumentPanel { get; set; }
        public string commentsWindscreenCondition { get; set; }
        public string commentsFireExtinguisher { get; set; }
        public string commentsDashcam { get; set; }
        public string commentsWheelChangingKit { get; set; }
        public string commentsSpareWheel { get; set; }
        public string commentsWarningTriangle { get; set; }
        public string commentsFirstAidKit { get; set; }
        public string commentsDrivingLicense { get; set; }
        public string commentsAlcoholOrDrugs { get; set; }
        public string checkDate { get; set; }
        public byte[] Signature { get; set; }
    }

    public class VehicleInspectionModel
    {
        public int Id { get; set; }
        public string depot { get; set; }
        public string vehicleRegistration { get; set; }
        public string vehicleMake { get; set; }
        public int mileage { get; set; }
        public string driverFullname { get; set; }
        public string checkDate { get; set; }
    }
}
