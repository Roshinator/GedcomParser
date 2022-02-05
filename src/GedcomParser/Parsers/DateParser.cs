using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers
{
    public static class DateParser
    {
        internal static DatePlace ParseDatePlace(this ResultContainer resultContainer, GedcomChunk chunk)
        {
            var datePlace = new DatePlace
            {
                Date = chunk.SubChunks.SingleOrDefault(c => c.Type == "DATE")?.Data,
                Place = chunk.SubChunks.SingleOrDefault(c => c.Type == "PLAC")?.Data
            };
            var map = chunk.SubChunks.SingleOrDefault(c => c.Type == "MAP");
            if (map != null)
            {
                datePlace.Latitude = map.SubChunks.SingleOrDefault(c => c.Type == "LATI")?.Data;
                datePlace.Longitude = map.SubChunks.SingleOrDefault(c => c.Type == "LONG")?.Data;
            }

            var note = chunk.SubChunks.SingleOrDefault(c => c.Type == "NOTE");
            if (note != null)
            {
                datePlace.Note = resultContainer.ParseNote(datePlace.Note, note);
            }

            return datePlace;
        }
        internal static DatePlace ParseDatePlaceUnknown(this ResultContainer resultContainer, GedcomChunk chunk)
        {
            var datePlace = new DatePlace
            {
                Date = chunk.SubChunks.SingleOrDefault(c => c.Type == "DATE")?.Data,
                Place = chunk.SubChunks.SingleOrDefault(c => c.Type == "PLAC")?.Data
            };
            var map = chunk.SubChunks.SingleOrDefault(c => c.Type == "MAP");
            if (map != null)
            {
                datePlace.Latitude = map.SubChunks.SingleOrDefault(c => c.Type == "LATI")?.Data;
                datePlace.Longitude = map.SubChunks.SingleOrDefault(c => c.Type == "LONG")?.Data;
            }

            var note = chunk.SubChunks.SingleOrDefault(c => c.Type != "DATE" && c.Type != "PLAC" && c.Type != "MAP" && c.Type != "LATI" && c.Type != "LONG");
            if (note != null)
            {
                datePlace.Note = resultContainer.ParseNote(datePlace.Note, note);
            }

            return datePlace;
        }


        internal static string ParseDateTime(this GedcomChunk chunk)
        {
            return (chunk.SubChunks.SingleOrDefault(c => c.Type == "DATE")?.Data + " " + chunk.SubChunks.SingleOrDefault(c => c.Type == "TIME")?.Data).Trim();
        }
    }
}