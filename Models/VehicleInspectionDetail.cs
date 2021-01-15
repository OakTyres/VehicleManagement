using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Models
{
    public class VehicleDefectsDetailModel
    {
        public string Depot { get; set; }
        public string VanRegistration { get; set; }
        public string UsualDriver { get; set; }
        public string CurrentDriver { get; set; }
        public string CommentsLightsIndicators { get; set; }
        public string CommentsReflectorsMarkers { get; set; }
        public string CommentsMirrors { get; set; }
        public string CommentsOilCoolantLevel { get; set; }
        public string CommentsAdBlueLevel { get; set; }
        public string CommentsTyres { get; set; }
        public string CommentsWheels { get; set; }
        public string CommentsBodyPanels { get; set; }
        public string CommentsHorn { get; set; }
        public string CommentsFuelOilLeaks { get; set; }
        public string CommentsSpeedometer { get; set; }
        public string CommentsExhaustAndSmoke { get; set; }
        public string CommentsBattery { get; set; }
        public string CommentsSeatBelts { get; set; }
        public string CommentsDoorsCondition { get; set; }
        public string CommentsWipersAndWashers { get; set; }
        public string CommentsInstrumentPanel { get; set; }
        public string CommentsWindscreenCondition { get; set; }
        public string CommentsFireExtinguisher { get; set; }
        public string CommentsDashcam { get; set; }
        public string CommentsWheelChangingKit { get; set; }
        public string CommentsSpareWheel { get; set; }
        public string CommentsWarningTriangle { get; set; }
        public string CommentsFirstAidKit { get; set; }
        public string CommentsDrivingLicense { get; set; }
        public string CommentsAlcoholOrDrugs { get; set; }
        public int TotalDefects { get; set; }
        public decimal NSFDepth { get; set; }
        public decimal OSFDepth { get; set; }
        public decimal NSRDepth { get; set; }
        public decimal OSRDepth { get; set; }
        public decimal SpareDepth { get; set; }
    }
}
