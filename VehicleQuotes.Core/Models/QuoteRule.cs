using Microsoft.EntityFrameworkCore;
using VehicleQuotes.Core.Validation;

namespace VehicleQuotes.Core.Models;

[Index(nameof(FeatureType), nameof(FeatureValue), IsUnique = true)]
public class QuoteRule
{
    public static class FeatureTypes
    {
        public const string BodyType = "BodyType";
        public const string Size = "Size";
        public const string ItMoves = "ItMoves";
        public const string HasAllWheels = "HasAllWheels";
        public const string HasAlloyWheels = "HasAlloyWheels";
        public const string HasAllTires = "HasAllTires";
        public const string HasKey = "HasKey";
        public const string HasTitle = "HasTitle";
        public const string RequiresPickup = "RequiresPickup";
        public const string HasEngine = "HasEngine";
        public const string HasTransmission = "HasTransmission";
        public const string HasCompleteInterior = "HasCompleteInterior";

        public static string[] All => [
            BodyType, Size, ItMoves, HasAllWheels, HasAlloyWheels, HasAllTires,
            HasKey, HasTitle, RequiresPickup, HasEngine, HasTransmission, HasCompleteInterior
        ];
    }

    public int ID { get; set; }

    [FeatureType]
    public required string FeatureType { get; set; }
    public required string FeatureValue { get; set; }
    public int PriceModifier { get; set; }
}
