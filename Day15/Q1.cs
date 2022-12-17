
namespace Day15
{
    public class Position
    {
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x{get;set;}
        public int y{get;set;}
    }

    public class Beacon
    {
        public Beacon(string[] beaconParts)
        {
            Coordinate = new Position(Convert.ToInt32(beaconParts[0].Replace("x=", string.Empty)), Convert.ToInt32(beaconParts[1].Replace(" y=", string.Empty)));
        }
        public Position Coordinate { get; private set; }

    }

    public class Sensor
    {
        public Sensor(string input)
        {
            string[] parts = input.Split(':');
            string[] sensorParts = parts[0].Replace("Sensor at ", string.Empty).Split(',');
            string[] beaconParts = parts[1].Replace(" closest beacon is at ", string.Empty).Split(',');
            Coordinate = new Position(Convert.ToInt32(sensorParts[0].Replace("x=", string.Empty)), Convert.ToInt32(sensorParts[1].Replace(" y=", string.Empty)));
            NearestBeacon = new Beacon(beaconParts);
            DistanceToNearestBeacon = Math.Abs(Coordinate.x - NearestBeacon.Coordinate.x) + Math.Abs(Coordinate.y - NearestBeacon.Coordinate.y);
        }

        public Position Coordinate { get; private set; }

        public Beacon NearestBeacon { get; set; }

        public int DistanceToNearestBeacon { get; private set; }

        public List<int> CalculatePositionsForBeaconCannotExist(int atRowIndex)
        {
            List<int> returnList = new();
            int yDistance = Math.Abs(Coordinate.y - atRowIndex);
            if (yDistance > DistanceToNearestBeacon)
            {
                return returnList;
            }
            int x1 = (yDistance - DistanceToNearestBeacon) + Coordinate.x;
            int x2 = -(yDistance - DistanceToNearestBeacon) + Coordinate.x;

            int minX = Math.Min(x1, x2);
            int maxX = Math.Max(x1, x2);
            bool nearestBeaconOnRow = NearestBeacon.Coordinate.y == atRowIndex;
            returnList = Enumerable.Range(minX, (maxX - minX) + 1).ToList();
            if (nearestBeaconOnRow)
            {
                returnList.Remove(NearestBeacon.Coordinate.x);
            }

            return returnList;
        }

        public (bool found, Range range) CalculatePositionsForBeaconCannotExist(int atRowIndex, int min, int max)
        {
            int yDistance = Math.Abs(Coordinate.y - atRowIndex);
            Position start = new Position(-1, -1);
            Position end = new Position(-1, -1);
            if (yDistance > DistanceToNearestBeacon)
            {
                return (false, new Range(start,end));
            }
            int x1 = (yDistance - DistanceToNearestBeacon) + Coordinate.x;
            int x2 = -(yDistance - DistanceToNearestBeacon) + Coordinate.x;
            int minX = Math.Min(x1, x2);
            int maxX = Math.Max(x1, x2);

            var intersectResult = SearchSpacesIntersection((minX, atRowIndex), (maxX, atRowIndex), (min, atRowIndex), (max, atRowIndex));
            if (intersectResult.intersects)
            {
                return (true, intersectResult.range);
            }
            else
            {
                return (false, new Range(start,end));
            }
        }

        public (bool intersects, Range range) SearchSpacesIntersection((int x, int y) bottomLeft1, (int x, int y) topRight1, (int x, int y) bottomLeft2, (int x, int y) topRight2)
        {
            int x5 = Math.Max(bottomLeft1.x, bottomLeft2.x);
            int y5 = Math.Max(bottomLeft1.y, bottomLeft2.y);

            int x6 = Math.Min(topRight1.x, topRight2.x);
            int y6 = Math.Min(topRight1.y, topRight2.y);

            if (x5 > x6 || y5 > y6)
            {
                return (false, new Range(new Position(-1, -1), new Position(-1, -1)));
            }
            return (true, new Range(new Position(x5, y5), new Position(x6, y6)));
        }
    }


    public class Range
    {
        public Range(Position start, Position end)
        {
            Start = start;
            End = end;
        }
        public Position Start { get; set; }
        public Position End { get; set; }
    }
    public class Q1 : BaseQuestion
    {
        public override object Run()
        {
            string[] commands = ReadAllLines();
            List<Sensor> sensorList = new();
            HashSet<int> cannotExistXCoordinates = new();
            foreach (var line in commands)
            {
                sensorList.Add(new Sensor(line));
            }
            foreach (var sensor in sensorList)
            {
                var cannotExistXs = sensor.CalculatePositionsForBeaconCannotExist(2000000);
                cannotExistXCoordinates.UnionWith(cannotExistXs);
            }
            return cannotExistXCoordinates.Count;
        }
    }
}