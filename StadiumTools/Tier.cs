﻿using System.Collections.Generic;

namespace StadiumTools
{
    /// <summary>
    /// Represents the data of a single seating tier.
    /// </summary>
    public class Tier
    {
        //Enums
        /// <summary>
        /// Available methods to generate a reference point for a tier
        /// </summary>
        public enum ReferencePtType
        {
            ByPOF,
            ByEndOfPrevTier
        }

        //Properties
        /// <summary>
        /// The method for determining the reference point of the tier
        /// </summary>
        public ReferencePtType RefPtType { get; set; }
        /// <summary>
        /// Reference Point of tier relative to StartX and StartY
        /// </summary>
        public Pt2d RefPt { get; set; }
        /// <summary>
        /// Index of the tier within a section
        /// </summary>
        public int SectionIndex { get; set; }
        /// <summary>
        /// Index of the tier within the plan
        /// </summary>
        public int PlanIndex { get; set; }
        /// <summary>
        /// Coeffecient for model unit space of the tier (mm, m, in, ft) 
        /// </summary>
        public double Unit;
        /// <summary>
        /// Point of focus for all spectators in this tier 
        /// </summary>
        public Pt2d POF { get; set; }
        /// <summary>
        /// Horizontal offset of tier start from reference point
        /// </summary>
        public double StartX { get; set; }
        /// <summary>
        /// Vertical offset of tier start from reference point
        /// </summary>
        public double StartY { get; set; }
        /// <summary>
        /// Minimum allowable c-value for spectators in this tier.
        /// </summary>
        public double MinimumC { get; set; }
        /// <summary>
        /// Horizontal offset of seated spectator eyes from row start 
        /// </summary>
        public double EyeX { get; set; }
        /// <summary>
        /// Vertical offset of seated spectator eyes from row floor
        /// </summary>
        public double EyeY { get; set; }
        /// <summary>
        /// Horizontal offset of standing spectator eyes from row start
        /// </summary>
        public double SEyeX { get; set; }
        /// <summary>
        /// Vertical offset of seated spectator eyes from row floor
        /// </summary>
        public double SEyeY { get; set; }
        /// <summary>
        /// Number of rows in this tier (super riser == row)
        /// </summary>
        public int RowCount { get; set; }
        /// <summary>
        /// Width(s) of row (distance from riser to riser)
        /// </summary>
        public double[] RowWidths { get; set; }
        /// <summary>
        /// True if tier contains a vomitory
        /// </summary>
        public bool VomHas { get; set; }
        /// <summary>
        /// Row number of vomitory start
        /// </summary>
        public int VomStart { get; set; }
        /// <summary>
        /// Height of vomitory (in rows)
        /// </summary>
        public int VomHeight { get; set; }
        /// <summary>
        /// Vertical height of fascia below the first row.
        /// </summary>
        public double FasciaH { get; set; }
        /// <summary>
        /// True if tier has a super riser
        /// </summary>
        public bool SuperHas { get; set; }
        /// <summary>
        /// Row number for inserting super riser
        /// </summary>
        public int SuperRow { get; set; }
        /// <summary>
        /// Width of super riser row
        /// </summary>
        public double SuperWidth { get; set; }
        /// <summary>
        /// Optional curb distance before super riser 
        /// </summary>
        public double SuperCurb { get; set; }
        /// <summary>
        /// Horizontal offset of spectator eyes from nose of super riser 
        /// </summary>
        public double SuperEyeX { get; set; }
        /// <summary>
        /// Vertical offset of spectator eyes from floor of super riser
        /// </summary>
        public double SuperEyeY { get; set; }
        /// <summary>
        /// Horizontal offset of STANDING spectator eyes from nose of super riser 
        /// </summary>
        public double SuperSEyeX { get; set; }
        /// <summary>
        /// Vertical offset of STANDING spectator eyes from floor of super riser
        /// </summary>
        public double SuperSEyeY { get; set; }
        /// <summary>
        /// Increment size to round riser heights to
        /// </summary>
        public double RoundTo { get; set; }
        /// <summary>
        /// The maximum allowable rake angle in radians. 
        /// </summary>
        public double MaxRakeAngle { get; set; }
        /// <summary>
        /// An ordered list of Spectators in the tier
        /// </summary>
        public Spectator[] Spectators { get; set; }
        /// <summary>
        /// Number of Pt2d objects this tier describes
        /// </summary>
        public int Points2dCount { get; set; }
        /// <summary>
        /// Pt2d objects that represents the top outline geometry of the tier
        /// </summary>
        public Pt2d[] Points2d { get; set; }
       
        //Constructors
        /// <summary>
        /// Initializes a new default Tier
        /// </summary>
        public Tier()
        {
            Initialize();
        }

        //Methods
        /// <summary>
        /// Initializes a tier 2d instance with default values
        /// </summary>
        private void Initialize()
        {
            this.Unit = UnitHandler.m;
            this.RefPtType = ReferencePtType.ByPOF;
            this.StartX = 5.0 * Unit;
            this.StartY = 1.0 * Unit;
            this.MinimumC = 0.09 * Unit;
            this.EyeX = 0.15 * Unit;
            this.EyeY = 1.2 * Unit;
            this.SEyeX = 0.6 * Unit;
            this.SEyeY = 1.4 * Unit;
            this.RowCount = 25;

            // Initialize all row widths to default value
            double[] rowWidths = new double[this.RowCount];
            double defaultRowWidth = 0.8 * Unit;
            for (int i = 0; i < rowWidths.Length; i++)
            {
                rowWidths[i] = defaultRowWidth;
            }

            this.RowWidths = rowWidths;
            this.VomHas = false;
            this.VomStart = 5;
            this.VomHeight = 5;
            this.FasciaH = 1.0 * Unit;
            this.SuperHas = true;
            this.SuperRow = 11;
            this.SuperWidth = 2.4;

            if (this.SuperHas)
            {
                rowWidths[this.SuperRow] = this.SuperWidth;
            }
            
            this.SuperCurb = 0.0 * Unit;
            this.SuperEyeX = 1.6 * Unit;
            this.SuperEyeY = 1.2 * Unit;
            this.SuperSEyeX = 1.8 * Unit;
            this.SuperSEyeY = 1.4;
            this.RoundTo = 0.001 * Unit;
            this.Points2dCount = GetTierPtCount(this);
            this.Points2d = new Pt2d[this.Points2dCount];
            this.MaxRakeAngle = .593412; //radians
            this.Spectators = new Spectator[this.RowCount];
        }

        /// <summary>
        /// Calculates the geometric point count for a Tier
        /// </summary>
        /// <param name="tier"></param>
        private static int GetTierPtCount(Tier tier)
        {
            int tierPtCount = (tier.RowCount * 2);
            if (tier.FasciaH != 0.0)
            {
                tierPtCount += 1;
            }
            if (tier.SuperCurb != 0.0)
            {
                tierPtCount += 1;
            }

            return tierPtCount;   
        }

    }

}
